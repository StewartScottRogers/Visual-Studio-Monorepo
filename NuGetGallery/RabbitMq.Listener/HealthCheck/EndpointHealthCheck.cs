using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace RabbitMq.Listener.HealthCheck
{
    public class EndpointHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
        {
            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}