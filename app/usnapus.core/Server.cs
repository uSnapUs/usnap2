using System.Net;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using TinyMessenger;
using uSnapUs.Core.Contracts;
using uSnapUs.Core.Exceptions;
using uSnapUs.Core.Helpers;
using uSnapUs.Core.Model;

namespace uSnapUs.Core
{
    public class Server:IServer
    {
        readonly ITinyMessengerHub _messengerHub;
        readonly ILogger _logger;

        public Server(ITinyMessengerHub messengerHub,ILogger logger)
        {
            _messengerHub = messengerHub;
            _logger = logger;
        }

        public string BaseUrl = "http://192.168.0.110:3000/";

        public DeviceRegistration RegisterDevice (DeviceRegistration deviceRegistration)
        {
            var client = GetClient();
            client.Authenticator = new ApiAuthenticator("Device",deviceRegistration.Guid);
            var request = RestClientFactory.CreateRestRequest("device", Method.POST);
            request.JsonSerializer = new JsonDotNetSerializer();
            request.RequestFormat = DataFormat.Json;
            request.AddBody(deviceRegistration);
            request.AddHeader("Content-Type", "application/json");
            
            var response = client.Post<DeviceRegistration>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Data;
            }
            var exception = new ApiException(response.Content);
            _logger.Exception(exception);
            throw exception;
        }

        IRestClient GetClient()
        {
            
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

    public class JsonDotNetSerializer : ISerializer
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }

        public string ContentType
        {
            get { return "application/json"; }
            set { }
        }
    }
}

