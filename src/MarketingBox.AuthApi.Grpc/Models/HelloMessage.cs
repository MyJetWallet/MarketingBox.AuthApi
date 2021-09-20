using System.Runtime.Serialization;
using MarketingBox.AuthApi.Domain.Models;

namespace MarketingBox.AuthApi.Grpc.Models
{
    [DataContract]
    public class HelloMessage : IHelloMessage
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}
