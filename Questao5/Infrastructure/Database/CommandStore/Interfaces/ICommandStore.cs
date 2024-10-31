using Questao5.Application.Commands.Requests;

namespace Questao5.Infrastructure.Database.CommandStore.Interfaces
{
    public interface ICommandStore
    {
        Task<bool> ContaExiste(string idConta);
        Task<bool> ContaAtiva(string idConta);
        Task<bool> RequisicaoJaProcessada(string idRequisicao);
        Task<string> RegistrarMovimento(MovimentacaoRequest request);
    }
}
