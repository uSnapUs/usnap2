using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using RestSharp;
using System.Collections.Generic;
using RestSharp.Serializers;
using TinyMessenger;
using uSnapUs.Core.Contracts;
using uSnapUs.Core.Helpers;
using uSnapUs.Core.Messages;
using uSnapUs.Core.Model;

namespace uSnapUs.Core
{
    public class Server:IServer
    {
        readonly ITinyMessengerHub _messengerHub;

        public Server(ITinyMessengerHub messengerHub)
        {
            _messengerHub = messengerHub;
        }

        public string BaseUrl = "http://192.168.0.110:3000/";

        public DeviceRegistration RegisterDevice (DeviceRegistration deviceRegistration)
        {
            var client = GetClient();
            client.Authenticator = new ApiAuthenticator("Device",deviceRegistration.Guid);
            var request = RestClientFactory.CreateRestRequest("device", Method.POST);
            
            request.RequestFormat = DataFormat.Json;
            request.AddBody(deviceRegistration);
            var response = client.Post<DeviceRegistration>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Data;
            }
            return null;
        }

        IRestClient GetClient()
        {
            Logger.Trace("enter");
            return RestClientFactory.CreateClient(BaseUrl);
        }

        IRestClientFactory _restClientFactory;

        public IRestClientFactory RestClientFactory {
            get { return _restClientFactory ?? (_restClientFactory = new RestClientFactory()); }
            set{
                _restClientFactory=value;
            }
        }

       
    }

    


}

