using System;
using uSnapUs.Core.Presenters;

namespace uSnapUs.Core.Supervisors
{
    public class LandingViewControllerSupervisor
    {
        ILandingViewController _controller;

        public LandingViewControllerSupervisor(ILandingViewController controller)
         {
            _controller = controller;
            _controller.Unload += OnUnload;
            _controller.FindButtonPressed += OnFindButtonPress;

         }

        void OnUnload(object sender, EventArgs e)
        {
            _controller.Unload -= this.OnUnload;
            _controller.FindButtonPressed -= this.OnFindButtonPress;
            _controller = null;
        }

        void OnCreateButtonPress(object sender, EventArgs e)
        {
            
        }

        void OnJoinButtonPress(object sender, EventArgs e)
        {
            
        }

        void OnFindNearbyButtonPress(object sender, EventArgs e)
        {
            
        }

        void OnBackButtonPress(object sender, EventArgs e)
        {
            
        }

        void OnFindButtonPress(object sender, EventArgs e)
        {
            _controller.State = LandingPageViewState.FindNearby;
            /*
            this._landingPageView.ShowFindNearby();
            _locationManager = new CLLocationManager();
            _locationManager.UpdatedLocation += this.OnLocationUpdated_Old;
            _locationManager.LocationsUpdated += this.OnLocationUpdated;
            _locationManager.StartUpdatingLocation();
             */
        }
    }

  
}