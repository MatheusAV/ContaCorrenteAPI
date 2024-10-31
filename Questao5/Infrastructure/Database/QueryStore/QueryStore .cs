using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.QueryStore.Interfaces;
using System.Data;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class QueryStore : IQueryStore
    {
        private readonly IDbConnection _dbConnection;

        public QueryStore(IDbConnection dbConnection)
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

        public async Task<decimal> ObterSaldo(string idConta)
        {
            var creditoQuery = "SELECT COALESCE(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @IdConta AND tipomovimento = 'C'";
            var debitoQuery = "SELECT COALESCE(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @IdConta AND tipomovimento = 'D'";

            var credito = await _dbConnection.ExecuteScalarAsync<decimal>(creditoQuery, new { IdConta = idConta });
            var debito = await _dbConnection.ExecuteScalarAsync<decimal>(debitoQuery, new { IdConta = idConta });

            return credito - debito;
        }

        public async Task<ContaCorrente> ObterContaDetalhes(string idConta)
        {
            var query = "SELECT idcontacorrente AS IdContaCorrente, nome AS NomeTitular, ativo AS Ativo FROM contacorrente WHERE idcontacorrente = @IdConta";
            return await _dbConnection.QueryFirstOrDefaultAsync<ContaCorrente>(query, new { IdConta = idConta });
        }

        public async Task<decimal> ObterTotalCreditos(string idContaCorrente)
        {
            var query = "SELECT IFNULL(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @IdConta AND tipomovimento = 'C'";
            return await _dbConnection.ExecuteScalarAsync<decimal>(query, new { IdConta = idContaCorrente });
        }

        public async Task<decimal> ObterTotalDebitos(string idContaCorrente)
        {
            var query = "SELECT IFNULL(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @IdConta AND tipomovimento = 'D'";
            return await _dbConnection.ExecuteScalarAsync<decimal>(query, new { IdConta = idContaCorrente });
        }
    }
}
