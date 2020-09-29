using multiplixe.comum.dapper;
using multiplixe.comum.dapper;
using System;
using dto = multiplixe.comum.dto;

namespace multiplixe.classificador.classificacao
{
    public class Repositorio
    {
        private DapperHelper dapperHelper { get; }

        public Repositorio(DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        public results.Classificacao Obter(Guid usuarioId)
        {
            var result = dapperHelper
                            .ResetParameter()
                            .AddParameter("_usuarioId", usuarioId)
                            .ExecuteWithOneResult<results.Classificacao>("classificacao_obter");

            return result;
        }

        public void Atualizar(Guid usuarioId, int nivelId, int pontos)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_usuarioId", usuarioId)
                .AddParameter("_nivelId", nivelId)
                .AddParameter("_pontos", pontos)
                .Update("classificacao_atualizar");
        }
    }
}
