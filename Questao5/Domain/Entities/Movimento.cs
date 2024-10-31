namespace Questao5.Domain.Entities
{
    /// <summary>
    /// Representa uma movimentação financeira (crédito ou débito) em uma conta corrente.
    /// </summary>
    public class Movimento
    {
        /// <summary>
        /// Identificador único da movimentação.
        /// </summary>
        public string IdMovimento { get; set; }

        /// <summary>
        /// Identificador da conta corrente associada à movimentação.
        /// </summary>
        public string IdContaCorrente { get; set; }

        /// <summary>
        /// Data e hora da movimentação.
        /// </summary>
        public DateTime DataMovimento { get; set; }

        /// <summary>
        /// Tipo de movimentação, podendo ser "C" para crédito ou "D" para débito.
        /// </summary>
        public string TipoMovimento { get; set; }

        /// <summary>
        /// Valor da movimentação.
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="Movimento"/> com um identificador único.
        /// </summary>
        public Movimento()
        {
            IdMovimento = Guid.NewGuid().ToString();
        }
    }
}
