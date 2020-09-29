using multiplixe.comum.dapper;
using multiplixe.comum.enums;
using System;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.token
{
    public class Repositorio
    {
        private DapperHelper dapperHelper { get; }

        public Repositorio(DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        public void Registrar(dto.Token push)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_usuarioId", push.UsuarioId)
                 .AddParameter("_tipo", (int)push.Tipo)
                .AddParameter("_valor", push.Valor)
                .Insert("token_registrar");
        }

        public results.Token Obter(Guid usuarioId, TipoTokenEnum tipo)
        {
            var result = dapperHelper
                            .ResetParameter()
                            .AddParameter("_usuarioId", usuarioId)
                             .AddParameter("_tipo", (int)tipo)
                            .ExecuteWithOneResult<results.Token>("token_obter");

            return result;
        }

        public void Delete(Guid usuarioId, TipoTokenEnum tipo)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_usuarioId", usuarioId)
                .AddParameter("_tipo", (int)tipo)
                .Delete("token_deletar");

        }
    }
}
