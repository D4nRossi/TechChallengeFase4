using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Prometheus;

public class MetricsServerHostedService : IHostedService
{
    private IHost? _metricsHost;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _metricsHost = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseUrls("http://*:1234"); // Porta das métricas
                webBuilder.Configure(app =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapMetrics(); // /metrics
                    });
                });
            })
            .Build();

        return _metricsHost.StartAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return _metricsHost?.StopAsync(cancellationToken) ?? Task.CompletedTask;
    }
}
