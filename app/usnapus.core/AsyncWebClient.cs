using System;
using System.Net;

namespace uSnapUs.Core
{
    public class AsyncWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            var req = base.GetWebRequest(address);
            var hReq = (req as HttpWebRequest);
            if (hReq != null)
            {
                hReq.AllowWriteStreamBuffering = false;
            }
            return hReq ?? req;
        }
    }
}