using Dapper;
using Questao5.Application.Commands.Requests;
using Questao5.Infrastructure.Database.CommandStore.Interfaces;
using System.Data;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class CommandStore : ICommandStore
    {
        private readonly IDbConnection _dbConnection;

        public CommandStore(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> ContaExiste(string idConta)
        {
            var query = "SELECT COUNT(1) FROM contacorrente WHERE idcontacorrente = @IdConta";
            return await _dbConnection.ExecuteScalarAsync<int>(query, new { IdConta = idConta }) > 0;
        }

        public async Task<bool> ContaAtiva(string idConta)
        {
            var query = "SELECT ativo FROM contacorrente WHERE idcontacorrente = @IdConta";
            return await _dbConnection.ExecuteScalarAsync<bool>(query, new { IdConta = idConta });
        }

        public async Task<bool> RequisicaoJaProcessada(string idRequisicao)
        {
            var query = "SELECT COUNT(1) FROM idempotencia WHERE chave_idempotencia = @ChaveIdempotencia";
            return await _dbConnection.ExecuteScalarAsync<int>(query, new { ChaveIdempotencia = idRequisicao }) > 0;
        }

        public async Task<string> RegistrarMovimento(MovimentacaoRequest request)
        {
            var idMovimento = Guid.NewGuid().ToString();

            // Inserir movimento
            var query = @"INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) 
                          VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)";

            await _dbConnection.ExecuteAsync(query, new
            {
                IdMovimento = idMovimento,
                IdContaCorrente = request.IdContaCorrente,
                DataMovimento = DateTime.Now,
                TipoMovimento = request.TipoMovimento,
                Valor = request.Valor
            });

            // Registrar idempotência
            var idempotenciaQuery = "INSERT INTO idempotencia (chave_idempotencia) VALUES (@IdRequisicao)";
            await _dbConnection.ExecuteAsync(idempotenciaQuery, new { IdRequisicao = request.IdRequisicao });

            return idMovimento;
        }
    }
}
