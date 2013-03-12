using System;
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;
using uSnapUs.Core.Contracts;
using uSnapUs.Core.Helpers;

namespace iPhone_FrontEnd
{
    public class LocationDelegate : CLLocationManagerDelegate, ILocationManager
    {
        CLLocationManager _manager;

        NSTimer _updateLocationTimer;


        void Start()
        {
            _manager = new CLLocationManager();

            _manager.StartUpdatingLocation();
            _manager.LocationsUpdated += LocationsUpdated;
            _manager.Failed += Failed;
            _updateLocationTimer = NSTimer.CreateRepeatingTimer(TimeSpan.FromMinutes(1), InitializeLocationUpdate);
        }

        void InitializeLocationUpdate()
        {
            _manager.StartUpdatingLocation();
        }

        void Stop()
        {
            if (_updateLocationTimer != null)
            {
                _updateLocationTimer.Invalidate();
                _updateLocationTimer = null;
            }
            _onLocationUpdate = null;

            if (_manager != null)
            {
                _manager.StopUpdatingLocation();
                _manager = null;
            }

        }

        void Failed(object sender, NSErrorEventArgs failArgs)
        {
            Console.WriteLine(failArgs.Error);
        }

        void LocationsUpdated(object sender, CLLocationsUpdatedEventArgs updatedArgs)
        {

            Console.WriteLine(updatedArgs.Locations[0].HorizontalAccuracy);
            Console.WriteLine(updatedArgs.Locations[0].VerticalAccuracy);
            if (_onLocationUpdate != null)
            {
                _onLocationUpdate.Invoke(this, new LocationEventArgs
                {
                    Coordinate = new Coordinate
                    {
                        Latitude = updatedArgs.Locations[0].Coordinate.Latitude,
                        Longitude = updatedArgs.Locations[0].Coordinate.Longitude,
                        Accuracy = updatedArgs.Locations[0].HorizontalAccuracy
                    }
                });
            }
        }
        EventHandler<LocationEventArgs> _onLocationUpdate;
        public event EventHandler<LocationEventArgs> LocationUpdate
        {
            add
            {
                if (_onLocationUpdate == null)
                {
                    Start();
                }
                _onLocationUpdate += value;

            }
            remove
            {
                if (_onLocationUpdate != null)
                {
                    // ReSharper disable DelegateSubtraction
                    _onLocationUpdate -= value;
                    // ReSharper restore DelegateSubtraction
                }
                if (_onLocationUpdate == null)
                {
                    Stop();
                }
            }
        }
        protected override void Dispose(bool disposing)
        {
            Stop();
            base.Dispose(disposing);

        }

        public void SubscribeToLocationUpdates(EventHandler<LocationEventArgs> onLocationUpdated)
        {
            LocationUpdate += onLocationUpdated;
        }

        public void UnsubscribeFromLocationUpdates(EventHandler<LocationEventArgs> onLocationUpdated)
        {
            LocationUpdate -= onLocationUpdated;
        }
    }
}