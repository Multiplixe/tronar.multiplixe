using multiplixe.comum.dapper;
using System;

namespace multiplixe.classificador.parceiro
{
    public class Repositorio
    {
        private DapperHelper dapperHelper { get; }

        public Repositorio(DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        public results.Parceiro Obter(Guid parceiroId)
        {
            var result = dapperHelper
                            .ResetParameter()
                            .AddParameter("_id", parceiroId)
                            .ExecuteWithOneResult<results.Parceiro>("parceiro_obter");

            return result;
        }

    }
}
