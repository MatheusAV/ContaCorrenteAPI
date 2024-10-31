using FluentAssertions;
using NSubstitute;
using Questao5.Application.Queries.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.QueryStore.Interfaces;
namespace API.Tests.Handlers
{
    public class SaldoHandlerTests
    {
        private readonly IQueryStore _queryStoreMock;
        private readonly SaldoHandler _handler;

        public SaldoHandlerTests()
        {
            _queryStoreMock = Substitute.For<IQueryStore>();
            _handler = new SaldoHandler(_queryStoreMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnInvalidAccount_WhenAccountDoesNotExist()
        {
            // Arrange
            _queryStoreMock.ContaExiste(Arg.Any<string>()).Returns(false);
            var request = new SaldoRequest { IdContaCorrente = "123" };

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
            _queryStoreMock.ContaExiste(Arg.Any<string>()).Returns(true);
            _queryStoreMock.ContaAtiva(Arg.Any<string>()).Returns(false);
            var request = new SaldoRequest { IdContaCorrente = "123" };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Sucesso.Should().BeFalse();
            result.TipoErro.Should().Be("INACTIVE_ACCOUNT");
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenAccountIsActiveAndExists()
        {
            // Arrange
            _queryStoreMock.ContaExiste(Arg.Any<string>()).Returns(true);
            _queryStoreMock.ContaAtiva(Arg.Any<string>()).Returns(true);
            _queryStoreMock.ObterSaldo(Arg.Any<string>()).Returns(500);
            _queryStoreMock.ObterContaDetalhes(Arg.Any<string>()).Returns(new ContaCorrente
            {
                IdContaCorrente = "123",
                Nome = "Teste Titular",
                Numero = 456,
                Ativo = true
            });

            var request = new SaldoRequest { IdContaCorrente = "123" };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Sucesso.Should().BeTrue();
            result.ValorSaldo.Should().Be(500);
            result.NomeTitular.Should().Be("Teste Titular");
        }
    }
}
