﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace usnapus.core.specs.Helpers
{
    internal class EventHelpers
    {
        static Dictionary<Type, List<FieldInfo>> dicEventFieldInfos = new Dictionary<Type, List<FieldInfo>>();

        static BindingFlags AllBindings
        {
            get { return BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static; }
        }

        //--------------------------------------------------------------------------------
        static List<FieldInfo> GetTypeEventFields(Type t)
        {
            if (dicEventFieldInfos.ContainsKey(t))
                return dicEventFieldInfos[t];

            List<FieldInfo> lst = new List<FieldInfo>();
            BuildEventFields(t, lst);
            dicEventFieldInfos.Add(t, lst);
            return lst;
        }

        //--------------------------------------------------------------------------------
        static void BuildEventFields(Type t, List<FieldInfo> lst)
        {
            // Type.GetEvent(s) gets all Events for the type AND it's ancestors
            // Type.GetField(s) gets only Fields for the exact type.
            //  (BindingFlags.FlattenHierarchy only works on PROTECTED & PUBLIC
            //   doesn't work because Fieds are PRIVATE)

            // NEW version of this routine uses .GetEvents and then uses .DeclaringType
            // to get the correct ancestor type so that we can get the FieldInfo.
            foreach (EventInfo ei in t.GetEvents(AllBindings))
            {
                Type dt = ei.DeclaringType;
                FieldInfo fi = dt.GetField(ei.Name, AllBindings);
                if (fi != null)
                    lst.Add(fi);
            }

            // OLD version of the code - called itself recursively to get all fields
            // for 't' and ancestors and then tested each one to see if it's an EVENT
            // Much less efficient than the new code
            /*
                  foreach (FieldInfo fi in t.GetFields(AllBindings))
                  {
                    EventInfo ei = t.GetEvent(fi.Name, AllBindings);
                    if (ei != null)
                    {
                      lst.Add(fi);
                      Console.WriteLine(ei.Name);
                    }
                  }
                  if (t.BaseType != null)
                    BuildEventFields(t.BaseType, lst);*/
        }


        //---------------------------------------------------------------
        static EventHandlerList GetStaticEventHandlerList(Type t, object obj)
        {
            MethodInfo mi = t.GetMethod("get_Events", AllBindings);
            return (EventHandlerList)mi.Invoke(obj, new object[] { });
        }

        //--------------------------------------------------------------------------------
        public static IEnumerable<Delegate> GetAllEventHandlers(object obj)
        {
            return GetEventHandlers(obj, "");
        }

        //--------------------------------------------------------------------------------
        public static IEnumerable<Delegate> GetEventHandlers(object obj, string EventName)
        {
            IEnumerable<Delegate> eventHandlers = new Delegate[] { };
            if (obj == null)
                return eventHandlers;

            Type t = obj.GetType();
            List<FieldInfo> eventFields = GetTypeEventFields(t);
            EventHandlerList staticEventHandlers = null;

            foreach (FieldInfo fi in eventFields)
            {
                if (EventName != "" &&
                    System.String.Compare(EventName, fi.Name, System.StringComparison.OrdinalIgnoreCase) != 0)
                    continue;

                // After hours and hours of research and trial and error, it turns out that
                // STATIC Events have to be treated differently from INSTANCE Events...
                if (fi.IsStatic)
                {
                    // STATIC EVENT
                    if (staticEventHandlers == null)
                        staticEventHandlers = GetStaticEventHandlerList(t, obj);

                    object idx = fi.GetValue(obj);
                    Delegate eh = staticEventHandlers[idx];
                    if (eh == null)
                        continue;

                    Delegate[] dels = eh.GetInvocationList();

                    eventHandlers = eventHandlers.Concat(dels);
                }
                else
                {
                    // INSTANCE EVENT
                    EventInfo ei = t.GetEvent(fi.Name, AllBindings);
                    if (ei != null)
                    {
                        object val = fi.GetValue(obj);
                        Delegate mdel = (val as Delegate);
                        if (mdel != null)
                        {
                            eventHandlers = eventHandlers.Concat(mdel.GetInvocationList());
                        }
                    }
                }
            }
            return eventHandlers;
        }

        //--------------------------------------------------------------------------------
    }
}

