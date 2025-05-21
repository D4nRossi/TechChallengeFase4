using CadastroService.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Shared.Events;


namespace CadastroService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatosController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public ContatosController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contato contato)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(
                new Uri("queue:contato-created-event-queue")
            );

            await endpoint.Send(new ContatoCreatedEvent
            {
                Id = contato.Id,
                Nome = contato.Nome,
                Email = contato.Email,
                Telefone = contato.Telefone,
                Cep = contato.Cep
            });

            return Ok(new { message = "Contato enviado com sucesso para a fila.", contato.Id });
        }

    }
}
