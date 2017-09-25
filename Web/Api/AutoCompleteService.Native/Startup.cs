using AutoCompleteService.Native.Global;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Autofac;
using AutoCompleteService.Services;
using AutoCompleteService.Modules.FileDataProvider.Services;
using AutoCompleteService.Modules.BinarySearch.Services;
using AutoCompleteService.Modules.TreeSearch.Services;
using AutoCompleteService.Modules.LineSearch.Services;


namespace AutoCompleteService.Native
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            //Зарегистрируем различные реализации интерфейсов
            IoC.UpdateRegistrations(x => x.RegisterType<LineSearchAutoCompleteService>().Named<IAutoCompleteService>("LineSearch").SingleInstance());
            IoC.UpdateRegistrations(x => x.RegisterType<BinarySearchAutoCompleteService>().Named<IAutoCompleteService>("BinarySearch").SingleInstance());
            IoC.UpdateRegistrations(x => x.RegisterType<TreeSearchAutoCompleteService>().Named<IAutoCompleteService>("TreeSearch").SingleInstance());

            services.AddSingleton<IDataProviderService, FileDataProviderService>();

            services.AddOptions();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseCors("CorsPolicy");

            app.UseMvc();
        }
    }
}
