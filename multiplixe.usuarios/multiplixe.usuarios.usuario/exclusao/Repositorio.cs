using multiplixe.comum.dapper;
using System;

namespace multiplixe.usuarios.usuario.exclusao
{
    public class Repositorio
    {
        private DapperHelper dapperHelper { get; }

        public Repositorio(DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        public void Deletar(Guid usuarioId)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_id", usuarioId)
                .Delete("usuario_deletar");
        }
    }
}
