// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.Net;
using Machine.Fakes;
using Machine.Fakes.Adapters.Moq;
using Machine.Specifications;
using RestSharp;
using System.Linq;
using TinyMessenger;
using uSnapUs.Core;
using uSnapUs.Core.Contracts;
using uSnapUs.Core.Helpers;
using uSnapUs.Core.Model;

namespace usnapus.core.specs.Core
{

    namespace ServerSpecifications
    {
        [Subject(typeof (Server))]
        public abstract class ServerSpecification : WithFakes<MoqFakeEngine>
        {
            protected static Server _sut;

            Establish context = () =>
                {
                    _baseUrl = "http://192.168.0.110:3000/";
                    _restClientFactory = An<IRestClientFactory>();
                    _restClient = An<IRestClient>();
                    _restRequest = An<IRestRequest>();
                    _restResponse = An<IRestResponse<DeviceRegistration>>();
                    _restResponse.StatusCode = HttpStatusCode.OK;
                    _restClientFactory.WhenToldTo(factory => factory.CreateRestRequest("device", Method.POST)).Return(_restRequest);
                    _restClientFactory.WhenToldTo(factory => factory.CreateClient(_baseUrl)).Return(_restClient);
                    _restRequest.WhenToldTo(request=>request.Parameters).Return(_params);
                    _restClient.WhenToldTo(client => client.Execute<DeviceRegistration>(_restRequest)).Return(_restResponse);

                    _sut = new Server(An<ITinyMessengerHub>()) {

                        RestClientFactory = _restClientFactory,
                        BaseUrl = _baseUrl
                    };
                };

            protected static IRestClientFactory _restClientFactory;
            protected static IRestClient _restClient;
            protected static IRestRequest _restRequest;
            protected static IRestResponse<DeviceRegistration> _restResponse;
            static string _baseUrl;
            protected static List<Parameter> _params = new List<Parameter>();
        }
        [Subject(typeof(Server))]
        [Tags("Integration")]
        public abstract class IntegrationServerSpecification : WithFakes<MoqFakeEngine>
        {
            protected static Server _sut;

            Establish context = () =>
            {
                _baseUrl = "http://10.0.1.9:3000/";
              

                _sut = new Server(An<ITinyMessengerHub>())
                {
                    RestClientFactory = new RestClientFactory(),
                    BaseUrl = _baseUrl
                };
            };

            protected static IRestClientFactory _restClientFactory;
            protected static IRestClient _restClient;
            protected static IRestRequest _restRequest;
            protected static IRestResponse<DeviceRegistration> _restResponse;
            static string _baseUrl;
            protected static List<Parameter> _params = new List<Parameter>();
        }
        public class when_registering_initial_device:ServerSpecification
        {
            Establish context = () => _restResponse.WhenToldTo(response=>response.Data).Return(_serverDeviceRegistration);
            Because of = () => _result = _sut.RegisterDevice(_deviceRegistration);

            It should_set_the_device_id_to_the_password = () => ((ApiAuthenticator)_restClient.Authenticator).Password.ShouldEqual(_deviceRegistration.Guid);
            It should_set_the_request_username_to_device = () => ((ApiAuthenticator)_restClient.Authenticator).Username.ShouldEqual("Device");
            It should_return_correct_device = () => _result.ShouldEqual(_serverDeviceRegistration);
            It should_send_a_device_to_the_server = () => _restRequest.WasToldTo(request=>request.AddBody(_deviceRegistration));

            static DeviceRegistration _deviceRegistration = new DeviceRegistration {
                Guid = "0F0F187A-9AD5-461A-BB56-810BFEF41553",

            };

            static DeviceRegistration _serverDeviceRegistration = new DeviceRegistration {
                Guid = "0F0F187A-9AD5-461A-BB56-810BFEF41553",
                Id = "TESTID"
            };

            static DeviceRegistration _result;
        }

        public class integration_when_registering_initial_device : IntegrationServerSpecification
        {
            Because of = () => _result = _sut.RegisterDevice(_deviceRegistration);

            It should_return_correct_device = () => _result.Guid.ShouldEqual(_deviceRegistration.Guid);

            static DeviceRegistration _deviceRegistration = new DeviceRegistration
            {
                
                Guid = "0F0F187A-9AD5-461A-BB56-810BFEF41553",
                Name="test device"

            };

           

            static DeviceRegistration _result;
        }

    }
}

