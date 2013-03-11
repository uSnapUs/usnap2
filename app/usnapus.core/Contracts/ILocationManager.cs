using System;
using uSnapUs.Core.Helpers;

namespace uSnapUs.Core.Contracts
{
	public interface ILocationManager
	{
		void SubscribeToLocationUpdates(EventHandler<LocationEventArgs> onLocationUpdated);
	    void UnsubscribeFromLocationUpdates(EventHandler<LocationEventArgs> onLocationUpdated);
	}
	public class LocationEventArgs:EventArgs
	{
		public Coordinate Coordinate{ get; set; }
	}
}

