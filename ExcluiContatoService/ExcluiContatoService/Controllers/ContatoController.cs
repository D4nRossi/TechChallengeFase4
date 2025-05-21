using ExcluiContatoService.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ExcluiContatoService.Controllers
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarContato(int id)
        {
            var evento = new ContatoDeletedEvent { CTT_ID = id };
            await _publishEndpoint.Publish(evento);
            return NoContent();
        }
    }
}
