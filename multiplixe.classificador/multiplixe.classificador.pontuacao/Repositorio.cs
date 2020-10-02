using System;
using System.Collections.Generic;
using coredapper = multiplixe.comum.dapper;
using coreenums = multiplixe.comum.enums;

namespace multiplixe.classificador.pontuacao
{
    public class Repositorio
    {
        private coredapper.DapperHelper dapperHelper { get; }

        public Repositorio(coredapper.DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        public List<results.RedeSocial> ObterIndividuais(Guid usuarioId)
        {
            var results = dapperHelper
                .ResetParameter()
                .AddParameter("_usuarioId", usuarioId)
                .ExecuteWithManyResult<results.RedeSocial>("pontuacao_redesocial_obter");

            return results;
        }

        public void ProcessarIndividuais(Guid usuarioId)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_usuarioId", usuarioId)
                .Update("pontuacao_redesocial_processar");
        }

        public void ProcessarTotal(Guid usuarioId)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_usuarioId", usuarioId)
                .Update("pontuacao_total_processar");
        }

    }
}
