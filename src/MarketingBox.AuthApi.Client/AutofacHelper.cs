using Autofac;
using MarketingBox.AuthApi.Grpc;

// ReSharper disable UnusedMember.Global

namespace MarketingBox.AuthApi.Client
{
    public static class AutofacHelper
    {
        public static void RegisterAssetsDictionaryClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new AuthApiClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetHelloService()).As<IHelloService>().SingleInstance();
        }
    }
}
