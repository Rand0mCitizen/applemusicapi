using AppleMusic.Common;
using AppleMusic.Common.Helpers;
using AppleMusic.Domain.Model;
using AppleMusic.Store.App.DataContext;
using AppleMusic.Store.App.Helpers;
using AppleMusic.Store.App.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace AppleMusic.Store
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.Configure<AppleMusicConfig>(Configuration.GetSection("AppleMusicConfig"));

            services.AddDbContext<EfContext>(options => options.UseSqlite("Filename=Test.db"));
            services.AddTransient<IArtistRepository, ArtistRepository>();
            services.AddTransient<IArtistService, ArtistService>();
            services.AddHttpClient<IAppleMusicClient, AppleMusicClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
