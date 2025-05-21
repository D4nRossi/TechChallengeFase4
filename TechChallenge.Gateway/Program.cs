using Microsoft.AspNetCore.RateLimiting;
using Prometheus;
using System.Threading.RateLimiting;
using Yarp.ReverseProxy;

var builder = WebApplication.CreateBuilder(args);

// Configurar Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: partition => new FixedWindowRateLimiterOptions
            {
                PermitLimit = builder.Configuration.GetValue<int>("RateLimiting:PermitLimit"),
                Window = TimeSpan.Parse(builder.Configuration["RateLimiting:Window"]),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 2
            }));
    options.RejectionStatusCode = 429;
});

// YARP
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

//Middleware do Prometheus para exportar métricas
app.UseHttpMetrics(); // coleta métricas HTTP automaticamente
app.MapMetrics();     // expõe em /metrics

app.UseRateLimiter();
app.MapReverseProxy();

app.Urls.Add("http://192.168.0.3:5000");

app.Run();
