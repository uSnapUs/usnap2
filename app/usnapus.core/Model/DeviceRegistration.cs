using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using RestSharp.Serializers;

namespace uSnapUs.Core.Model
{
    [DataContract]
    public class DeviceRegistration
    {
        [PrimaryKey,AutoIncrement]
        [JsonIgnore()]
        public int InternalId{get;set;}

        [JsonProperty(PropertyName = "name")]
        public string Name{get;set;}

        [JsonProperty(PropertyName = "email")]
        public string Email{get;set;}

        [JsonProperty(PropertyName = "guid")]
        public string Guid{get;set;}

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "facebook_id")]
        public string FacebookId { get; set; }
    }
}

