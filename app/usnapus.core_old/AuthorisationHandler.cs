using System;
using System.IO;
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
            request.Credentials =  new NetworkCredential("device",deviceId.ToString("N"));
            var response =  request.GetResponse() as HttpWebResponse;
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                string jsonResponse;
                using (var stream = new StreamReader(response.GetResponseStream()))
                {
                    jsonResponse = stream.ReadToEnd();
                }
                if (!string.IsNullOrEmpty(jsonResponse))
                {
                    var tokenResponse = SimpleJson.DeserializeObject<TokenResponse>(jsonResponse,new PocoJsonSerializerStrategy());
                    return tokenResponse.token;
                }
            }
            return null;
        }

        public class TokenResponse
        {
            public string token { get; set; }
        }
    }
}