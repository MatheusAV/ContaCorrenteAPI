using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Infrastructure.Database.CommandStore.Interfaces;

namespace Questao5.Application.Commands.Handlers
{
    public class MovimentacaoHandler : IRequestHandler<MovimentacaoRequest, MovimentacaoResponse>
    {
        private readonly ICommandStore _commandStore;

        public MovimentacaoHandler(ICommandStore commandStore)
        {
            _commandStore = commandStore;
        }

        public async Task<MovimentacaoResponse> Handle(MovimentacaoRequest request, CancellationToken cancellationToken)
        {
            // Validação da conta, status e valor
            if (!await _commandStore.ContaExiste(request.IdContaCorrente))
                return new MovimentacaoResponse(false, null, "Conta não cadastrada", "INVALID_ACCOUNT");

            if (!await _commandStore.ContaAtiva(request.IdContaCorrente))
                return new MovimentacaoResponse(false, null, "Conta inativa", "INACTIVE_ACCOUNT");

            if (request.Valor <= 0)
                return new MovimentacaoResponse(false, null, "Valor inválido", "INVALID_VALUE");

            if (request.TipoMovimento != "C" && request.TipoMovimento != "D")
                return new MovimentacaoResponse(false, null, "Tipo de movimento inválido", "INVALID_TYPE");

            // Verificar idempotência
            if (await _commandStore.RequisicaoJaProcessada(request.IdRequisicao))
                return new MovimentacaoResponse(false, null, "Requisição duplicada", "DUPLICATE_REQUEST");

            // Registrar movimentação
            var idMovimento = await _commandStore.RegistrarMovimento(request);
            return new MovimentacaoResponse(true, idMovimento);
        }
    }
}
