using System;
using System.IO;
using System.Net;
namespace usnapus.core
{
    public interface IWebRequestFactory
    {
        IWebRequest Create(Uri requestUri);
    }
    public class WebRequestFactory:IWebRequestFactory
    {
         public IWebRequest Create(Uri requestUri)
         {
             return new WebRequestWrapper(WebRequest.Create(requestUri));
         }
    }
    public interface IWebRequest
    {
        Stream GetRequestStream();
        WebResponse GetResponse();
        IAsyncResult BeginGetResponse(AsyncCallback callback, object state);
        WebResponse EndGetResponse(IAsyncResult asyncResult);
        IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state);
        Stream EndGetRequestStream(IAsyncResult asyncResult);
        void Abort();
        string Method { get; set; }
        Uri RequestUri { get; }
        string ConnectionGroupName { get; set; }
        WebHeaderCollection Headers { get; set; }
        long ContentLength { get; set; }
        string ContentType { get; set; }
        ICredentials Credentials { get; set; }
        bool UseDefaultCredentials { get; set; }
        bool PreAuthenticate { get; set; }
        int Timeout { get; set; }
        object GetLifetimeService();
        object InitializeLifetimeService();
    }
    public class WebRequestWrapper:IWebRequest
    {
        readonly WebRequest _request;

        public WebRequestWrapper(WebRequest request)
        {
            _request = request;
        }

        public Stream GetRequestStream()
        {
            throw new NotImplementedException();
        }

        public WebResponse GetResponse()
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public WebResponse EndGetResponse(IAsyncResult asyncResult)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public Stream EndGetRequestStream(IAsyncResult asyncResult)
        {
            throw new NotImplementedException();
        }

        public void Abort()
        {
            throw new NotImplementedException();
        }

        public string Method
        {
            get { return _request.Method; }
            set { _request.Method = value; }
        }

        public Uri RequestUri { get; private set; }
        public string ConnectionGroupName { get; set; }
        public WebHeaderCollection Headers { get; set; }
        public long ContentLength { get; set; }
        public string ContentType { get; set; }
        public ICredentials Credentials { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool PreAuthenticate { get; set; }
        public int Timeout { get; set; }
        public object GetLifetimeService()
        {
            throw new NotImplementedException();
        }

        public object InitializeLifetimeService()
        {
            throw new NotImplementedException();
        }
    }
}