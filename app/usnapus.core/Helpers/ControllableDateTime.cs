using System;

namespace uSnapUs.Core.Helpers
{
    internal static class ControllableDateTime
    {
        static DateTime? _frozenDateTime;

        public static void Freeze()
        {
            _frozenDateTime = DateTime.Now;
        }
        public static void Thaw()
        {
            _frozenDateTime = null;
        }

        public static void FreezeAt(DateTime newDateTime)
        {
            _frozenDateTime = newDateTime;
        }
        public static DateTime Now {
            get
            {
                if (_frozenDateTime != null)
                {
                    return _frozenDateTime.Value.ToLocalTime();
                }
                return DateTime.Now;
            }
        }
        public static DateTime UtcNow { get
        {
            if(_frozenDateTime!=null)
            {
                return _frozenDateTime.Value.ToUniversalTime();
            }
            
            return DateTime.UtcNow;
        } }
    }
}