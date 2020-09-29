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

        public List<results.RedeSocial> ExtrairIndividuais(Guid usuarioId)
        {
            var results = dapperHelper
                .ResetParameter()
                .AddParameter("_usuarioId", usuarioId)
                .ExecuteWithManyResult<results.RedeSocial>("pontuacao_redesocial_extrair");

            return results;
        }

        public void RegistrarIndividual(Guid usuarioId, coreenums.RedeSocialEnum redesocial, int pontos)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_usuarioId", usuarioId)
                .AddParameter("_redeSocialId", (int)redesocial)
                .AddParameter("_pontos", pontos)
                .Insert("pontuacao_redesocial_registrar");
        }




    }
}
