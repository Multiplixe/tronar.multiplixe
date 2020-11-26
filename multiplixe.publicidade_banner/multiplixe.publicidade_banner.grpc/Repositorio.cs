using multiplixe.comum.dapper;
using System;
using System.Collections.Generic;

namespace multiplixe.publicidade_banner.grpc
{
    public class Repositorio
    {
        protected DapperHelper dapperHelper { get; }

        public Repositorio(DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        public List<results.Banner> Obter(Guid usuarioId)
        {
            return this.dapperHelper
                .ResetParameter()
                .AddParameter("_usuarioId", usuarioId)
                .ExecuteWithManyResult<results.Banner>("banner_obter");

        }
    }
}
