namespace Questao5.Domain.Entities
{
    /// <summary>
    /// Representa uma conta corrente no sistema bancário.
    /// </summary>
    public class ContaCorrente
    {
        /// <summary>
        /// Identificador único da conta corrente.
        /// </summary>
        public string IdContaCorrente { get; set; }

        /// <summary>
        /// Número da conta corrente.
        /// </summary>
        public int Numero { get; set; }

        /// <summary>
        /// Nome do titular da conta corrente.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Indica se a conta corrente está ativa. 
        /// Verdadeiro se a conta estiver ativa, falso caso contrário.
        /// </summary>
        public bool Ativo { get; set; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="ContaCorrente"/> com um identificador único.
        /// </summary>
        public ContaCorrente()
        {
            IdContaCorrente = Guid.NewGuid().ToString();
        }
    }
}
