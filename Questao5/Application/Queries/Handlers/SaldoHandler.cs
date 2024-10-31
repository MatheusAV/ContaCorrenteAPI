using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Database.QueryStore.Interfaces;

namespace Questao5.Application.Queries.Handlers
{
    public class SaldoHandler : IRequestHandler<SaldoRequest, SaldoResponse>
    {
        private readonly IQueryStore _queryStore;

        public SaldoHandler(IQueryStore queryStore)
        {
            _queryStore = queryStore;
        }

        public async Task<SaldoResponse> Handle(SaldoRequest request, CancellationToken cancellationToken)
        {
            // Verificar conta
            if (!await _queryStore.ContaExiste(request.IdContaCorrente))
                return new SaldoResponse(false, 0, null, "Conta não cadastrada", "INVALID_ACCOUNT");

            if (!await _queryStore.ContaAtiva(request.IdContaCorrente))
                return new SaldoResponse(false, 0, null, "Conta inativa", "INACTIVE_ACCOUNT");

            // Obter saldo
            var saldo = await _queryStore.ObterSaldo(request.IdContaCorrente);
            var conta = await _queryStore.ObterContaDetalhes(request.IdContaCorrente);

            return new SaldoResponse(true, saldo, conta.Nome);
        }
    }
}
