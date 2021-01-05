using System;
using System.Collections.Generic;
using coredapper = multiplixe.comum.dapper;

namespace multiplixe.compartilhador.post
{
    public class Repositorio
    {
        private coredapper.DapperHelper dapperHelper { get; }

        /// <summary>
        /// Obtem os post's pré cadastrados, exceto os que ja foram compartilhados anteriormente.
        /// </summary>
        public List<results.Post> Obter(Guid usuarioId, int quantidade = 999999999)
        {
            var results = dapperHelper
              .ResetParameter()
              .AddParameter("_usuarioId", usuarioId)
              .AddParameter("_quantidade", quantidade)
              .ExecuteWithManyResult<results.Post>("post_obter");

            return results;
        }
    }
}
