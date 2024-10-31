using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{
    /// <summary>
    /// Representa uma requisição para realizar uma movimentação em uma conta corrente.
    /// </summary>
    public class MovimentacaoRequest : IRequest<MovimentacaoResponse>
    {
        /// <summary>
        /// Identificador único da requisição, usado para garantir idempotência.
        /// </summary>
        public string IdRequisicao { get; set; }

        /// <summary>
        /// Identificador único da conta corrente onde a movimentação será realizada.
        /// </summary>
        public string IdContaCorrente { get; set; }

        /// <summary>
        /// Valor da movimentação a ser realizada. Deve ser positivo.
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Tipo de movimentação a ser realizada. Pode ser "C" para crédito ou "D" para débito.
        /// </summary>
        public string TipoMovimento { get; set; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="MovimentacaoRequest"/>.
        /// </summary>
        /// <param name="idRequisicao">O identificador único da requisição.</param>
        /// <param name="idContaCorrente">O identificador da conta corrente.</param>
        /// <param name="valor">O valor da movimentação.</param>
        /// <param name="tipoMovimento">O tipo de movimentação ("C" para crédito ou "D" para débito).</param>
        public MovimentacaoRequest(string idRequisicao, string idContaCorrente, decimal valor, string tipoMovimento)
        {
            IdRequisicao = idRequisicao;
            IdContaCorrente = idContaCorrente;
            Valor = valor;
            TipoMovimento = tipoMovimento;
        }
    }
}
