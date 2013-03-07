using System;
using System.Collections.Generic;
using uSnapUs.Core.Helpers;
using uSnapUs.Core.Model;

namespace uSnapUs.Core.Contracts
{
	public interface IStateManager:IDisposable
	{
		IServer Server{get;}
		ILocationManager LocationManager{get;set;}
		DeviceRegistration CurrentDeviceRegistration { get; }
		string DeviceName { set; }
	    Coordinate? CurrentLocation { get; set; }
	    void UpdateDeviceRegistration(string name, string email,string facebookId);
	}


}

