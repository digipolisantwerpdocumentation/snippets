using Digipolis.ApplicationServices;
using Digipolis.Correlation;
using EmployeeExample.ServiceAgent;
using EmployeeExample.ServiceAgent.Handlers;
using EmployeeExample.ServiceAgent.OAuth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace EmployeeExample
{
    public class ServiceHelper
    {
        public ServiceHelper()
        {
        }

        public static IServiceProvider InitializeServices()
        {
            var serviceCollection = new ServiceCollection();

            AddServiceAgents(serviceCollection);

            serviceCollection.AddMemoryCache();

            serviceCollection.AddCorrelation();

            serviceCollection.AddApplicationServices(options =>
            {
                options.ApplicationId = "a9cef471-eaa8-46a1-a9ea-c04d0d23877a";
                options.ApplicationName = "Employee test";
            });

            return serviceCollection.BuildServiceProvider();
        }

        private static IServiceCollection AddServiceAgents(IServiceCollection services)
        {
            services.AddHttpClient(nameof(OAuthAgent), (provider, client) =>
            {
                client.BaseAddress = new Uri(Config.BaseAddress);
            })
            .AddTypedClient<IOAuthAgent, OAuthAgent>();


            services.AddHttpClient(nameof(EmployeeService), (provider, client) =>
            {
                client.BaseAddress = new Uri(Config.BaseAddress);

                client.DefaultRequestHeaders.Add("apikey", Config.ApiKey);
            })
            .AddHttpMessageHandler<CorrelationIdHandler>()
            .AddHttpMessageHandler<MediaTypeJsonHandler>()
            .AddHttpMessageHandler<ContentTypeJsonHandler>()
            .AddHttpMessageHandler<OAuthTokenHandler>()
            .AddTypedClient<IEmployeeService, EmployeeService>();

            // add delegating handlers
            services.TryAddTransient<OAuthTokenHandler>();
            services.TryAddTransient<MediaTypeJsonHandler>();
            services.TryAddTransient<ContentTypeJsonHandler>();
            services.TryAddTransient<CorrelationIdHandler>();

            return services;
        }
    }
}
