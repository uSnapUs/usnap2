// This file has been autogenerated from parsing an Objective-C header file added in Xcode.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.CoreLocation;
using System.Linq;
using System.Diagnostics;

namespace iPhone_FrontEnd
{
	public partial class LandingPageViewController : UIViewController
	{
		LandingPageView _landingPageView;

		CLLocationManager _locationManager;

		CLLocation _currentLocation;

		public LandingPageViewController ():base()
		{
			UIApplication.SharedApplication.SetStatusBarHidden(true,true);
		
			_landingPageView = new LandingPageView();
			_landingPageView.FindButtonPressed+=OnFindButtonPress;
			_landingPageView.BackButtonPressed+=OnBackButtonPress;
			_landingPageView.FindNearbyButtonPressed+=OnFindNearbyButtonPress;
			_landingPageView.JoinButtonPressed+=OnJoinButtonPress;
			_landingPageView.CreateButtonPressed+=OnCreateButtonPress;
			this.View = _landingPageView;
		}
	
		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return UIInterfaceOrientationMask.All;
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}

		void OnLocationUpdated (object sender, CLLocationsUpdatedEventArgs e)
		{
			var newLocation = e.Locations.Last();
			if (_currentLocation == null||newLocation.HorizontalAccuracy>_currentLocation.HorizontalAccuracy) {
				_currentLocation = newLocation;
				_landingPageView.SetLocation(_currentLocation);
			}
			else{_locationManager.StopUpdatingLocation();}
			
		}

		void OnLocationUpdated_Old (object sender, CLLocationUpdatedEventArgs e)
		{
			var locations = new CLLocation[]{e.OldLocation,e.NewLocation};
			OnLocationUpdated(sender,new CLLocationsUpdatedEventArgs(locations));
		}

		void OnFindButtonPress (object sender, EventArgs args){
			this._landingPageView.ShowFindNearby();
			_locationManager = new CLLocationManager();
			_locationManager.UpdatedLocation += this.OnLocationUpdated_Old;
			_locationManager.LocationsUpdated+=this.OnLocationUpdated;
			_locationManager.StartUpdatingLocation();

		}

		void goToMapView ()
		{
			var mapView = this._landingPageView.MapView;
			Console.WriteLine("Breakpoint 1");
			var mapViewController = new FindNearbyEventViewController(mapView);
			this.PresentViewController(mapViewController,false,()=>{this.Dispose();});
			mapView= null;

		}

		void OnFindNearbyButtonPress (object sender, EventArgs e)
		{
			this._landingPageView.AnimateToFullMap(()=>this.goToMapView());
		}

		void OnBackButtonPress (object sender, EventArgs e)
		{
			this._locationManager.StopUpdatingLocation();
			this._landingPageView.HideFindNearby();
		}

		void OnJoinButtonPress (object sender, EventArgs e)
		{
			var eventDashboardViewController = new EventDashboardViewController();
			this.PresentViewController(eventDashboardViewController,true,()=>{this.Dispose();});

		}

		void OnCreateButtonPress (object sender, EventArgs e)
		{
			var createEventViewController = new CreateEventViewController();
			createEventViewController.ModalTransitionStyle = UIModalTransitionStyle.FlipHorizontal;
			this.PresentViewController(createEventViewController,true,()=>{this.Dispose ();});
		}

		void UnwireEvents ()
		{
			_landingPageView.FindButtonPressed-=OnFindButtonPress;
			_landingPageView.BackButtonPressed-=OnBackButtonPress;
			_landingPageView.FindNearbyButtonPressed-=OnFindNearbyButtonPress;
			_landingPageView.JoinButtonPressed-=OnJoinButtonPress;
			_landingPageView.CreateButtonPressed-=OnCreateButtonPress;
		}

		protected override void Dispose (bool disposing)
		{
			Console.WriteLine("Disposing Landing page view controller");
			if (this._locationManager != null) {
				this._locationManager.StopUpdatingLocation ();
			}

			UnwireEvents();
			this._locationManager = null;
			this._landingPageView = null;
			this._currentLocation = null;
			base.Dispose(disposing);
		}
	}
}