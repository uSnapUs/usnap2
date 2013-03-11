using System.Net;
using RestSharp;
using uSnapUs.Core.Contracts;

namespace uSnapUs.Core.Helpers
{

    public class RestClientFactory:IRestClientFactory
    {
        
        public IRestClient CreateClient (string baseUrl)
        {
            var rc =  new RestClient(baseUrl){};
            if (Proxy != null)
                rc.Proxy = Proxy;
            return rc;
        }

        public IWebProxy Proxy { get; set; }

        public IRestRequest CreateRestRequest (string path, Method method)
        {
            return new RestRequest (path, method);
        }
    }
}

