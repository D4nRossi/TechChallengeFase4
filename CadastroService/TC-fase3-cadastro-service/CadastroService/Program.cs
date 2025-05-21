using MassTransit;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

//Configura��o do MassTransit
var configuration = builder.Configuration;
var servidor = configuration.GetSection("MassTransit")["Servidor"] ?? string.Empty;
var usuario = configuration.GetSection("MassTransit")["Usuario"] ?? string.Empty;
var senha = configuration.GetSection("MassTransit")["Senha"] ?? string.Empty;

//Configura��o do MassTransit
builder.Services.AddMassTransit((x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        //Definindo o host
        cfg.Host(servidor, "/", h =>
        {
            //User e password para acessar o RabbitMq
            h.Username(usuario);
            h.Password(senha);
        });

        //Configura��o do endpoint
        cfg.ConfigureEndpoints(context);
    });
}));

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
