using multiplixe.comum.dapper;
using System;
using dto = multiplixe.comum.dto;

namespace multiplixe.classificador.usuario
{
    public class Repositorio
    {
        private DapperHelper dapperHelper { get; }

        public Repositorio(DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        public void Registrar(dto.Usuario usuario, int nivelId)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_id", usuario.Id)
                .AddParameter("_empresaId", usuario.EmpresaId)
                .AddParameter("_nivelId", nivelId)
                .Insert("usuario_registrar");
        }

        public void Sincronizar(dto.Usuario usuario)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_usuarioId", usuario.Id)
                .Update("usuario_sincronizar");
        }

        public void Deletar(Guid usuarioId)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_id", usuarioId)
                .Insert("usuario_deletar");
        }

        public results.Usuario Obter(Guid usuarioId)
        {
            var result = dapperHelper
                            .ResetParameter()
                            .AddParameter("_id", usuarioId)
                            .ExecuteWithOneResult<results.Usuario>("usuario_obter");

            return result;
        }

    }
}
