using ExcluiContatoService.Models;
using MassTransit;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"] ?? "localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona roteamento (necessário pro Prometheus funcionar corretamente)
builder.Services.AddRouting();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

//Middleware do Prometheus para exportar métricas
app.UseHttpMetrics(); // coleta métricas HTTP automaticamente
app.MapMetrics();     // expõe em /metrics

app.MapControllers();
app.Run();
