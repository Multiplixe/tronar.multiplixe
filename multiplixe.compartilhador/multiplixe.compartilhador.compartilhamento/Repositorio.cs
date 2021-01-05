using System;
using System.Collections.Generic;
using coredapper = multiplixe.comum.dapper;

namespace multiplixe.compartilhador.compartilhamento
{
    public class Repositorio
    {
        private coredapper.DapperHelper dapperHelper { get; }

        /// <summary>
        /// Obtem os Compartilhamentos pré cadastrados.
        /// </summary>
        public List<results.Compartilhamento> Obter(Guid empresaId, int quantidade = 999999999, int? ativo = null)
        {
            var results = dapperHelper
              .ResetParameter()
              .AddParameter("_empresaId", empresaId)
              .AddParameter("_quantidade", quantidade)
              .AddParameter("_ativo", ativo)
              .ExecuteWithManyResult<results.Compartilhamento>("compartilhamento_Obter");

            foreach (var item in results)
            {
                if (item.Origem == 1 )
                {
                    item.Url = "https://multiplyx-compartilhamento.s3.amazonaws.com/" + item.Url;
                }

            }

            return results;
        }
    }
}
