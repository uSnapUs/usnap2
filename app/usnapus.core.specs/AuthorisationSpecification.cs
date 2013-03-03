// ReSharper disable InconsistentNaming

using System;
using System.IO;
using System.Net;
using System.Text;
using Machine.Fakes;
using Machine.Specifications;

namespace usnapus.core.specs
{
    [Subject(typeof(AuthorisationHandler))]
    public abstract class AuthorisationSpecification:WithFakes
    {
        Establish context = () =>
            {
                _sut = new AuthorisationHandler(_webRequestFactory = An<IWebRequestFactory>());
                _webRequest = An<IWebRequest>();
                _webRequestFactory.WhenToldTo(webRequestFactory => webRequestFactory.Create(Param.IsAny<Uri>())).Return(_webRequest);
                
            };

        protected static AuthorisationHandler _sut;
        protected static IWebRequestFactory _webRequestFactory;
        protected static IWebRequest _webRequest;
    }
    public class when_registering_a_new_device:AuthorisationSpecification
    {
        Establish context = () =>
            {
                var response = SimpleJson.SerializeObject(new {token = _expectedToken});
                _webResponse = An<HttpWebResponse>();
                _webResponse.WhenToldTo(webResponse=>webResponse.StatusCode).Return(HttpStatusCode.OK);
                _webResponse.WhenToldTo(webResponse => webResponse.GetResponseStream()).Return(new MemoryStream((Encoding.UTF8.GetBytes(response))));
                _webRequest.WhenToldTo(request=>request.GetResponse()).Return(_webResponse);
            };
       public Because of = () => _token = _sut.AuthoriseDevice(_deviceId);
        It should_return_a_token = () => _token.ShouldNotBeNull();
        It should_call_device_register_url = () => _webRequestFactory.WasToldTo(webRequestFactory=>webRequestFactory.Create(Param<Uri>.Matches((p)=>p.Equals(_auth_uri))));
        It should_be_a_post_call = () => _webRequest.Method.ShouldEqual("POST");
        It should_set_credentials = () => _webRequest.Credentials.GetCredential(_auth_uri,"Digest").ShouldNotBeNull();
        It should_set_password_to_device_id = () => _webRequest.Credentials.GetCredential(_auth_uri,"Digest").Password.ShouldEqual(_deviceId.ToString("N"));
        It should_return_correct_token = () => _token.ShouldEqual(_expectedToken);
        static readonly Guid _deviceId = Guid.NewGuid();
        static string _token;
        static Uri _auth_uri = new Uri("https://api.usnap.us/device/register");
        static string _expectedToken = "EXPECTED_TOKEN";
        static HttpWebResponse _webResponse;
    }
}