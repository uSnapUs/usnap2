using System.Net;
using RestSharp;

namespace uSnapUs.Core.Helpers
{
    public class ApiAuthenticator:IAuthenticator
    {
        readonly string _username;
        readonly string _password;

        public ApiAuthenticator(string username, string password)
        {
            _username = username;
            _password = password;
        }
        internal string Password
        {
            get { return _password; }
        }
        internal string Username
        {
            get { return _username; }
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            var credentialCache = new CredentialCache();
            credentialCache.Add(client.BuildUri(request),"Digest",new NetworkCredential(_username,_password));
            request.Credentials = credentialCache;
        }
    }
}