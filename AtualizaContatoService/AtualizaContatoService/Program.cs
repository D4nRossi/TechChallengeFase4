using AtualizaContatoService.Models;
using MassTransit;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var rabbitHost = config["MassTransit:Servidor"];
var rabbitUser = config["MassTransit:Usuario"];
var rabbitPass = config["MassTransit:Senha"];


// MassTransit + RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(rabbitHost, "/", h =>
        {
            h.Username(rabbitUser);
            h.Password(rabbitPass);
        });
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

//Middleware do Prometheus para exportar m�tricas
app.UseHttpMetrics(); // coleta m�tricas HTTP automaticamente
app.MapMetrics();     // exp�e em /metrics

app.MapControllers();
app.Run();
