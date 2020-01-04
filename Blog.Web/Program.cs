using Blog.Web.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using System;

namespace Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            host.Seed().Wait();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
             //.ConfigureAppConfiguration((ctx, builder) =>
             //{
             //    var keyVaultEndpoint = Environment.GetEnvironmentVariable("KEYVAULT_ENDPOINT");
             //    if (!string.IsNullOrEmpty(keyVaultEndpoint))
             //    {
             //        var azureServiceTokenProvider = new AzureServiceTokenProvider();
             //        var keyVaultClient = new KeyVaultClient(
             //            new KeyVaultClient.AuthenticationCallback(
             //                azureServiceTokenProvider.KeyVaultTokenCallback));
             //        builder.AddAzureKeyVault(
             //            keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
             //    }
             //})
            .UseApplicationInsights()
            .UseStartup<Startup>()
            .Build();
    }

    
}
