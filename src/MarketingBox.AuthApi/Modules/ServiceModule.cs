using System;
using System.Collections.Generic;
using Autofac;
using MarketingBox.Auth.Service.Client;
using MarketingBox.Auth.Service.Crypto;
using MarketingBox.Auth.Service.Grpc;
using MarketingBox.Auth.Service.MyNoSql.Users;
using MarketingBox.AuthApi.Domain.Tokens;
using MyJetWallet.Sdk.NoSql;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataReader;

namespace MarketingBox.AuthApi.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var dict = new Dictionary<string, string>();
            foreach (var keyValue in Program.Settings.Tenants.TenantHostMapping)
            {
                foreach (var value in keyValue.Value.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries))
                {
                    dict[value] = keyValue.Key;
                }
            }

            var tenantLocator = new TenantLocator(dict);
            
            builder
                .RegisterInstance(tenantLocator)
                .As<TenantLocator>();

            builder.RegisterAuthServiceClient(Program.Settings.AuthServiceUrl);
            var noSqlClient = builder.CreateNoSqlClient(Program.ReloadedSettings(e => e.MyNoSqlReaderHostPort));

            var subs = new MyNoSqlReadRepository<UserNoSql>(noSqlClient, UserNoSql.TableName);
            builder.RegisterInstance(subs)
                .As<IMyNoSqlServerDataReader<UserNoSql>>();

            TimeSpan.TryParse(Program.Settings.JwtTtl, out var ttl);
            builder.Register(x => new TokensService(
                    x.Resolve<IMyNoSqlServerDataReader<UserNoSql>>(),
                    x.Resolve<IUserService>(),
                    x.Resolve<ICryptoService>(),
                    Program.Settings.JwtSecret,
                    Program.Settings.JwtAudience,
                    ttl))
                .As<ITokensService>()
                .SingleInstance();
        }
    }
}
