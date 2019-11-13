using AppleMusic.Cli.Helpers;
using AppleMusic.Common;
using AppleMusic.Common.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace AppleMusic.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().InitApp();
        }

        private void InitApp()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");

            var appConfiguration = builder.Build();
            var config = appConfiguration.GetSection("AppleMusicConfig").Get<AppleMusicConfig>();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<ISearchResultDisplay, TableResultDisplay>();
            serviceCollection.AddTransient<IAppleMusicClient, AppleMusicClient>(sp => new AppleMusicClient(HttpClientFactory.Create(), config));
            serviceCollection.AddTransient<AppleMusicApplication>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var app = serviceProvider.GetRequiredService<AppleMusicApplication>();

            app.Run();
        }
    }
}
