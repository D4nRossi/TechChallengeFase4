using AtualizaContatoService.Controllers;
using AtualizaContatoService.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallenge.Shared.Events;

namespace AtualizaContatoService.Tests
{
    public class AtualizaContatoControllerTests
    {
        private readonly Mock<IPublishEndpoint> _publishMock;
        private readonly ContatosController _controller;

        public AtualizaContatoControllerTests()
        {
            _publishMock = new Mock<IPublishEndpoint>();
            _controller = new ContatosController(_publishMock.Object);
        }

        [Fact]
        public async Task AtualizarContato_DeveRetornarOk_QuandoEventoEhPublicado()
        {
            var contato = new ContatoUpdatedEvent
            {
                CTT_ID = 1,
                CTT_NOME = "Daniel",
                CTT_EMAIL = "daniel@teste.com",
                CTT_NUMERO = "912345678",
                CTT_DDD = 11
            };

            var result = await _controller.AtualizarContato(1, contato);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Evento de atualização publicado com sucesso.", okResult.Value);
        }
    }
}
