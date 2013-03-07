using System.Collections.Generic;
using System.IO;
using RestSharp;
using TinyMessenger;
using System;
using uSnapUs.Core.Contracts;
using uSnapUs.Core.Helpers;
using uSnapUs.Core.Messages;
using uSnapUs.Core.Model;

namespace uSnapUs.Core
{
    public class StateManager:IStateManager
    {


        public StateManager()
        {
            Logger.Trace("enter");
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"usnapus.sqlite");
            lock (_dbLock)
            {
                if (Db == null)
                {
                    Db = new SQLiteConnection(dbPath);
                }
                DoMigrations(Db);
            }
            
       
            Logger.Trace("exit");
        }
        
       
        DeviceRegistration _currentDeviceRegistration;
        public DeviceRegistration CurrentDeviceRegistration {
            set {
                lock (_dbLock) {
                Logger.Trace("enter");
                    
                if (_currentDeviceRegistration != null) {
                    Db.Delete (_currentDeviceRegistration);
                }
                if (value != null)
                {
                    Db.Insert(value);
                }
                    _currentDeviceRegistration = value;
                Logger.Trace("exit");
                }
            }
            get {
                Logger.Trace("enter");
                return _currentDeviceRegistration;
            }
        }
        void RegisterDevice (string deviceName)
        {
            Logger.Trace("enter");
            var deviceRegistrationDetails = Db.Find<DeviceRegistration> (reg => true);
            if (deviceRegistrationDetails == null)
            {

                CurrentDeviceRegistration = Server.RegisterDevice(new DeviceRegistration
                                                                      {
                                                                          Guid = Guid.NewGuid().ToString("N"),
                                                                          Name = deviceName
                                                                      });
            }
            else
                _currentDeviceRegistration = deviceRegistrationDetails;
            Logger.Trace("exit");
        }

        void DoMigrations(SQLiteConnection db)
        {
            Logger.Trace("enter");
            db.CreateTable<DeviceRegistration>();
            db.CreateTable<Event>();
            db.CreateTable<CurrentEvent>();
            //db.DeleteAll<CurrentEvent>();
            db.CreateTable<Photo>();
            Logger.Trace("exit");
        }

        static IStateManager _stateManager;
        public static IStateManager Current {
            get { return _stateManager ?? (_stateManager = new StateManager()); }
            internal set {
                _stateManager = value;
            }
        }

        IServer _server;
        readonly object _dbLock = new object();
        internal static SQLiteConnection Db;
        Event _currentEvent;
        static ITinyMessengerHub _messageHub;
        TinyMessageSubscriptionToken _photoUploadProgressSubscription;
        TinyMessageSubscriptionToken _photoUploadCompleteSubscription;

        public ITinyMessengerHub MessageHub
        {
            get { return _messageHub ?? (_messageHub = new TinyMessengerHub()); }
            set { _messageHub = value; }
        }

        public IServer Server {
            get { return _server ?? (_server = new Server(MessageHub)); }
            internal set {
                _server = value;
            }
        }


        public ILocationManager LocationManager {
            get;set;
        }
        
        public string DeviceName
        {
            set { 
                Logger.Trace("enter");
                RegisterDevice(value);
                Logger.Trace("exit");
            }
        }

        public Coordinate? CurrentLocation { get; set; }

       

        public void UpdateDeviceRegistration(string s, string name, string email)
        {

            Logger.Trace("enter");
            if (_currentDeviceRegistration != null)
            {
                lock (_dbLock)
                {
                    _currentDeviceRegistration.Name = name;
                    _currentDeviceRegistration.Email = email;
                    var savedDevice = Server.RegisterDevice(_currentDeviceRegistration);
                    if (savedDevice != null)
                    {
                        _currentDeviceRegistration = savedDevice;
                        Db.DeleteAll<CurrentEvent>();
                        Db.Insert(_currentDeviceRegistration);
                    }
                }
            }
            else
            {
                CurrentDeviceRegistration = Server.RegisterDevice(new DeviceRegistration
                                                                      {
                                                                          Email = email,
                                                                          Name = name,
                                                                          Guid = Guid.NewGuid().ToString("N")
                                                                      });
            }
            Logger.Trace("exit");
        }

       


        public void Dispose ()
        {
            lock (_dbLock) {
                Logger.Trace ("enter");
                if (Db != null) {
                    Db.Dispose ();
                    Db = null;
                }
            }
            Logger.Trace("exit");
        }
    }
}

