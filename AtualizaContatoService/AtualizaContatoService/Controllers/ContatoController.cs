using AtualizaContatoService.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Shared.Events;

namespace AtualizaContatoService.Controllers
{
    [ApiController]
    [Route("cadastro/api/[controller]")]
    public class ContatosController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ContatosController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarContato(int id, [FromBody] ContatoUpdatedEvent model)
        {
            if (id != model.CTT_ID)
                return BadRequest("ID do corpo não bate com o da URL");

            await _publishEndpoint.Publish(model);
            return Ok("Evento de atualização publicado com sucesso.");
        }
    }
}
