using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalVaultExample
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

            return serviceCollection.BuildServiceProvider();
        }

        private static IServiceCollection AddServiceAgents(IServiceCollection services)
        {
            // service agents
            services.AddSingleton<DigitalVaultService>();
            return services;
        }
    }
}
