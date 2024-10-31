namespace Questao5.Domain.Entities
{
    /// <summary>
    /// Representa uma entrada de idempotência para garantir que uma requisição seja processada apenas uma vez.
    /// </summary>
    public class Idempotencia
    {
        /// <summary>
        /// Chave única de idempotência usada para identificar a requisição.
        /// </summary>
        public string ChaveIdempotencia { get; set; }

        /// <summary>
        /// Dados originais da requisição, armazenados para referência.
        /// </summary>
        public string Requisicao { get; set; }

        /// <summary>
        /// Resultado da requisição armazenado para garantir a idempotência.
        /// </summary>
        public string Resultado { get; set; }
    }
}
