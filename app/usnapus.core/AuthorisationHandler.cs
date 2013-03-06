using System;
using System.Net;

namespace usnapus.core
{
    public class AuthorisationHandler
    {
        readonly IWebRequestFactory _webRequestFactory;

        public AuthorisationHandler(IWebRequestFactory webRequestFactory)
        {
            _webRequestFactory = webRequestFactory;
        }

        public string AuthoriseDevice(Guid deviceId)
        {
            var uri = new Uri("https://api.usnap.us/device/register");
            var request = _webRequestFactory.Create(uri);
            request.Method = "POST";
            var credentialCache = new System.Net.CredentialCache();
            credentialCache.Add(uri,"Digest", new NetworkCredential("device",deviceId.ToString("N")));
            request.Credentials = credentialCache;
            return "";
        }
    }
}