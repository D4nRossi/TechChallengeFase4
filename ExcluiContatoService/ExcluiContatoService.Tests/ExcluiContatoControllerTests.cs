using ExcluiContatoService.Controllers;
using ExcluiContatoService.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ExcluiContatoService.Tests
{
    public class ExcluiContatoControllerTests
    {
        private readonly Mock<IPublishEndpoint> _publishMock;
        private readonly ContatosController _controller;

        public ExcluiContatoControllerTests()
        {
            _publishMock = new Mock<IPublishEndpoint>();
            _controller = new ContatosController(_publishMock.Object);
        }

        [Fact]
        public async Task DeletarContato_DeveRetornarNoContent()
        {
            var result = await _controller.DeletarContato(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
