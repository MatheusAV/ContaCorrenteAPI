using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Controllers
{
    [ApiController]
    [Route("api/contacorrente")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Movimenta uma conta corrente (crédito ou débito).
        /// </summary>
        /// <param name="request">Informações da movimentação (Id da conta, valor, tipo e idempotência).</param>
        /// <returns>Retorna o ID da movimentação em caso de sucesso ou mensagem de erro em caso de falha.</returns>
        /// <response code="200">Movimentação realizada com sucesso, retorna o ID da movimentação.</response>
        /// <response code="400">Erro de validação, retorna a mensagem de erro e tipo.</response>
        [HttpPost("movimentacao")]
        [SwaggerOperation(
            Summary = "Movimenta uma conta corrente",
            Description = "Este endpoint permite realizar movimentações de crédito ou débito em uma conta corrente especificada."
        )]
        [SwaggerResponse(200, "Movimentação realizada com sucesso", typeof(MovimentacaoResponse))]
        [SwaggerResponse(400, "Erro de validação", typeof(object))]
        public async Task<IActionResult> MovimentarConta([FromBody] MovimentacaoRequest request)
        {
            var response = await _mediator.Send(request);

            if (!response.Sucesso)
            {
                return BadRequest(new { response.Mensagem, Tipo = response.TipoErro });
            }

            return Ok(new { response.IdMovimento });
        }

        /// <summary>
        /// Consulta o saldo de uma conta corrente.
        /// </summary>
        /// <param name="idContaCorrente">ID da conta corrente a ser consultada.</param>
        /// <returns>Retorna as informações da conta e o saldo atual em caso de sucesso ou mensagem de erro em caso de falha.</returns>
        /// <response code="200">Consulta realizada com sucesso, retorna o saldo e informações da conta.</response>
        /// <response code="400">Erro de validação, retorna a mensagem de erro e tipo.</response>
        [HttpGet("saldo/{idContaCorrente}")]
        [SwaggerOperation(
            Summary = "Consulta o saldo de uma conta corrente",
            Description = "Este endpoint permite consultar o saldo atual de uma conta corrente específica, retornando o valor do saldo e informações adicionais."
        )]
        [SwaggerResponse(200, "Consulta realizada com sucesso", typeof(SaldoResponse))]
        [SwaggerResponse(400, "Erro de validação", typeof(object))]
        public async Task<IActionResult> ConsultarSaldo(string idContaCorrente)
        {
            var request = new SaldoRequest { IdContaCorrente = idContaCorrente };
            var response = await _mediator.Send(request);

            if (!response.Sucesso)
            {
                return BadRequest(new { response.Mensagem, Tipo = response.TipoErro });
            }

            return Ok(new
            {
                NumeroConta = idContaCorrente,
                response.NomeTitular,
                SaldoAtual = response.ValorSaldo,
                DataConsulta = DateTime.Now // Inclui a data e hora da consulta para atender ao requisito
            });
        }
    }
}
