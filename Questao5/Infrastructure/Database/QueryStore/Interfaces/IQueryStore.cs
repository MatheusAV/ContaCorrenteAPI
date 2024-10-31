using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.QueryStore.Interfaces
{
    public interface IQueryStore
    {
        Task<bool> ContaExiste(string idConta);
        Task<bool> ContaAtiva(string idConta);
        Task<decimal> ObterSaldo(string idConta);
        Task<ContaCorrente> ObterContaDetalhes(string idConta);
    }
}
