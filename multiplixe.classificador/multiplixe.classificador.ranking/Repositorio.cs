using adduo.helper.extensionmethods;
using corehelper = multiplixe.comum.helper;
using dto = multiplixe.comum.dto;

using System;
using System.Collections.Generic;
using System.Text;
using coredapper = multiplixe.comum.dapper;

namespace multiplixe.classificador.ranking
{
    public class Repositorio
    {
        private coredapper.DapperHelper dapperHelper { get; }

        public Repositorio(coredapper.DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        public void Processar(Guid empresaId)
        {
            dapperHelper
            .ResetParameter()
            .AddParameter("_empresaId", empresaId)
            .AddParameter("_dataProcessamento", corehelper.DateTimeHelper.Now().ToMySQL())
            .Update("ranking_processar");
        }

        public dto.ranking.Ranking Obter(Guid usuarioId, int menorPosicaoRanking)
        {
            var ranking = new dto.ranking.Ranking();

            var extractor = new extractor.Obter(ranking);

            dapperHelper
            .ResetParameter()
            .AddParameter("_usuarioId", usuarioId)
            .AddParameter("_menorPosicaoRanking", menorPosicaoRanking)
            .ExecuteMultiple("ranking_obter", extractor);

            return ranking;
        }

    }
}
