using RestSharp;

namespace uSnapUs.Core.Contracts
{
	public interface IRestClientFactory
	{
		IRestClient CreateClient (string baseUrl);
		IRestRequest CreateRestRequest (string path, Method method);
	}

}

