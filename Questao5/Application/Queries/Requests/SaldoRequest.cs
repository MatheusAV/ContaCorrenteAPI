using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests
{
    /// <summary>
    /// Representa uma requisição para consultar o saldo de uma conta corrente específica.
    /// </summary>
    public class SaldoRequest : IRequest<SaldoResponse>
    {
        /// <summary>
        /// Identificador único da conta corrente para a qual o saldo será consultado.
        /// </summary>
        public string IdContaCorrente { get; set; }
    }
}
