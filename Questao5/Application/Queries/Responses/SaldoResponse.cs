namespace Questao5.Application.Queries.Responses
{
    /// <summary>
    /// Representa a resposta de uma operação de consulta de saldo de uma conta corrente.
    /// </summary>
    public class SaldoResponse
    {
        /// <summary>
        /// Indica se a consulta de saldo foi realizada com sucesso.
        /// </summary>
        public bool Sucesso { get; set; }

        /// <summary>
        /// Mensagem adicional sobre o resultado da operação, útil para fornecer informações ou erros.
        /// </summary>
        public string Mensagem { get; set; }

        /// <summary>
        /// Tipo de erro, caso a operação falhe. Exemplo: "INVALID_ACCOUNT", "INACTIVE_ACCOUNT".
        /// </summary>
        public string TipoErro { get; set; }

        /// <summary>
        /// Valor atual do saldo da conta corrente, retornado em caso de sucesso.
        /// </summary>
        public decimal ValorSaldo { get; set; }

        /// <summary>
        /// Nome do titular da conta corrente, retornado em caso de sucesso.
        /// </summary>
        public string NomeTitular { get; set; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="SaldoResponse"/>.
        /// </summary>
        /// <param name="sucesso">Indica se a consulta de saldo foi realizada com sucesso.</param>
        /// <param name="valorSaldo">Valor do saldo da conta corrente, retornado em caso de sucesso.</param>
        /// <param name="nomeTitular">Nome do titular da conta corrente, retornado em caso de sucesso.</param>
        /// <param name="mensagem">Mensagem adicional sobre o resultado da operação.</param>
        /// <param name="tipoErro">Tipo do erro, caso a operação falhe.</param>
        public SaldoResponse(bool sucesso, decimal valorSaldo = 0, string nomeTitular = null, string mensagem = null, string tipoErro = null)
        {
            Sucesso = sucesso;
            ValorSaldo = valorSaldo;
            NomeTitular = nomeTitular;
            Mensagem = mensagem;
            TipoErro = tipoErro;
        }
    }
}
