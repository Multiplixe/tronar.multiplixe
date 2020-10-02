using multiplixe.comum.dapper;
using System;

namespace multiplixe.classificador.saldo
{
    public class Repositorio
    {
        private DapperHelper dapperHelper { get; }

        public Repositorio(DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

 
        public void Processar(Guid usuarioId)
        {
            dapperHelper
               .ResetParameter()
               .AddParameter("_usuarioId", usuarioId)
               .Update("saldo_processar");
        }

    }
}
