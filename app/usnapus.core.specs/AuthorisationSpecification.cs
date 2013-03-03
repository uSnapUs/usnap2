// ReSharper disable InconsistentNaming

using System;
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
                _webRequestFactory.WhenToldTo(webRequestFactory => webRequestFactory.Create(Param.IsAny<Uri>())).Return(_webRequest);
                
            };

        protected static AuthorisationHandler _sut;
        protected static IWebRequestFactory _webRequestFactory;
        protected static IWebRequest _webRequest;
    }
    public class when_registering_a_new_device:AuthorisationSpecification
    {
       public Because of = () => _token = _sut.AuthoriseDevice(_deviceId);
        It should_return_a_token = () => _token.ShouldNotBeNull();
        It should_call_device_register_url = () => _webRequestFactory.WasToldTo(webRequestFactory=>webRequestFactory.Create(Param<Uri>.Matches((p)=>p.Equals(_auth_uri))));
        It should_be_a_post_call = () => _webRequest.Method.ShouldEqual("POST");
        It should_set_credentials = () => _webRequest.Credentials.GetCredential(_auth_uri,"Digest").ShouldNotBeNull();
        It should_set_password_to_device_id = () => _webRequest.Credentials.GetCredential(_auth_uri,"Digest").Password.ShouldEqual(_deviceId.ToString("N"));
        static readonly Guid _deviceId = Guid.NewGuid();
        static string _token;
        static Uri _auth_uri = new Uri("https://api.usnap.us/device/register");
    }
}