﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace RabbitMq.Listener.HealthCheck
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServiceHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<EndpointHealthCheck>(nameof(EndpointHealthCheck), HealthStatus.Unhealthy);
        }
    }
}