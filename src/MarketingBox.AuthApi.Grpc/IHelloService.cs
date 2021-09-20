using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.AuthApi.Grpc.Models;

namespace MarketingBox.AuthApi.Grpc
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        Task<HelloMessage> SayHelloAsync(HelloRequest request);
    }
}
