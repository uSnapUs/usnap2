using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using uSnapUs.Core.Helpers;
using uSnapUs.Core.Model;

namespace uSnapUs.Core.Contracts
{
	public interface IServer
	{
		DeviceRegistration RegisterDevice (DeviceRegistration deviceRegistration);
		IRestClientFactory RestClientFactory{  get; }
	}
}

