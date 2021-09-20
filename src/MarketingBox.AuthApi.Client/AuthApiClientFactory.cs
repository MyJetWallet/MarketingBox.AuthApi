using JetBrains.Annotations;
using MarketingBox.AuthApi.Grpc;
using MyJetWallet.Sdk.Grpc;

namespace MarketingBox.AuthApi.Client
{
    [UsedImplicitly]
    public class AuthApiClientFactory: MyGrpcClientFactory
    {
        public AuthApiClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IHelloService GetHelloService() => CreateGrpcService<IHelloService>();
    }
}
