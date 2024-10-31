namespace Questao5.Application.Commands.Responses
{
    /// <summary>
    /// Representa a resposta de uma operação de movimentação em uma conta corrente.
    /// </summary>
    public class MovimentacaoResponse
    {
        /// <summary>
        /// Indica se a movimentação foi realizada com sucesso.
        /// </summary>
        public bool Sucesso { get; set; }

        /// <summary>
        /// Mensagem detalhada sobre o resultado da operação, útil para informações ou erros.
        /// </summary>
        public string Mensagem { get; set; }

        /// <summary>
        /// Tipo de erro caso a operação falhe. Exemplo: "INVALID_ACCOUNT", "INACTIVE_ACCOUNT".
        /// </summary>
        public string TipoErro { get; set; }

        /// <summary>
        /// Identificador único do movimento realizado, retornado em caso de sucesso.
        /// </summary>
        public string IdMovimento { get; set; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="MovimentacaoResponse"/>.
        /// </summary>
        /// <param name="sucesso">Indica se a movimentação foi realizada com sucesso.</param>
        /// <param name="idMovimento">O identificador do movimento, caso a operação tenha sido bem-sucedida.</param>
        /// <param name="mensagem">Mensagem detalhada sobre o resultado da operação.</param>
        /// <param name="tipoErro">Tipo do erro caso a operação falhe.</param>
        public MovimentacaoResponse(bool sucesso, string idMovimento = null, string mensagem = null, string tipoErro = null)
        {
            Sucesso = sucesso;
            IdMovimento = idMovimento;
            Mensagem = mensagem;
            TipoErro = tipoErro;
        }
    }
}
