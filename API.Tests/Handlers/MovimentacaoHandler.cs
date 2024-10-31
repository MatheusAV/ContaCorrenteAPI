using FluentAssertions;
using NSubstitute;
using Questao5.Application.Commands.Handlers;
using Questao5.Application.Commands.Requests;
using Questao5.Infrastructure.Database.CommandStore.Interfaces;

namespace API.Tests.Handlers
{
    public class MovimentacaoHandlerTests
    {
        private readonly ICommandStore _commandStoreMock;
        private readonly MovimentacaoHandler _handler;

        public MovimentacaoHandlerTests()
        {
            _commandStoreMock = Substitute.For<ICommandStore>();
            _handler = new MovimentacaoHandler(_commandStoreMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnInvalidAccount_WhenAccountDoesNotExist()
        {
            // Arrange
            _commandStoreMock.ContaExiste(Arg.Any<string>()).Returns(false);
            var request = new MovimentacaoRequest("id1", "123", 100, "C");

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Sucesso.Should().BeFalse();
            result.TipoErro.Should().Be("INVALID_ACCOUNT");
        }

        [Fact]
        public async Task Handle_ShouldReturnInactiveAccount_WhenAccountIsInactive()
        {
            // Arrange
            _commandStoreMock.ContaExiste(Arg.Any<string>()).Returns(true);
            _commandStoreMock.ContaAtiva(Arg.Any<string>()).Returns(false);
            var request = new MovimentacaoRequest("id2", "123", 100, "C");

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Sucesso.Should().BeFalse();
            result.TipoErro.Should().Be("INACTIVE_ACCOUNT");
        }

        [Fact]
        public async Task Handle_ShouldReturnInvalidValue_WhenValueIsNonPositive()
        {
            // Arrange
            _commandStoreMock.ContaExiste(Arg.Any<string>()).Returns(true);
            _commandStoreMock.ContaAtiva(Arg.Any<string>()).Returns(true);
            var request = new MovimentacaoRequest("id3", "123", -50, "C");

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Sucesso.Should().BeFalse();
            result.TipoErro.Should().Be("INVALID_VALUE");
        }

        [Fact]
        public async Task Handle_ShouldReturnInvalidType_WhenTypeIsNotCreditOrDebit()
        {
            // Arrange
            _commandStoreMock.ContaExiste(Arg.Any<string>()).Returns(true);
            _commandStoreMock.ContaAtiva(Arg.Any<string>()).Returns(true);
            var request = new MovimentacaoRequest("id4", "123", 100, "X");

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Sucesso.Should().BeFalse();
            result.TipoErro.Should().Be("INVALID_TYPE");
        }

        [Fact]
        public async Task Handle_ShouldReturnDuplicateRequest_WhenRequestIsDuplicated()
        {
            // Arrange
            _commandStoreMock.ContaExiste(Arg.Any<string>()).Returns(true);
            _commandStoreMock.ContaAtiva(Arg.Any<string>()).Returns(true);
            _commandStoreMock.RequisicaoJaProcessada(Arg.Any<string>()).Returns(true);
            var request = new MovimentacaoRequest("id5", "123", 100, "C");

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Sucesso.Should().BeFalse();
            result.TipoErro.Should().Be("DUPLICATE_REQUEST");
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenTransactionIsValid()
        {
            // Arrange
            _commandStoreMock.ContaExiste(Arg.Any<string>()).Returns(true);
            _commandStoreMock.ContaAtiva(Arg.Any<string>()).Returns(true);
            _commandStoreMock.RequisicaoJaProcessada(Arg.Any<string>()).Returns(false);
            _commandStoreMock.RegistrarMovimento(Arg.Any<MovimentacaoRequest>()).Returns("mov1");

            var request = new MovimentacaoRequest("id6", "123", 100, "C");

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Sucesso.Should().BeTrue();
            result.IdMovimento.Should().Be("mov1");
        }
    }
}
