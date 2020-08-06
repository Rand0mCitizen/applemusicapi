using System;
using System.Net.Http;
using AppleMusic.Cli.Helpers;
using AppleMusic.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            var apiConfig = new ApiConfig { ApiUrl = "http://localhost:8082/api" };

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<ISearchResultDisplay, TableResultDisplay>();
            serviceCollection.AddTransient<ApiClient>(sp => new ApiClient(HttpClientFactory.Create(), apiConfig));
            serviceCollection.AddTransient<AppleMusicApplication>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var app = serviceProvider.GetRequiredService<AppleMusicApplication>();

            app.Run();
        }
    }
}
