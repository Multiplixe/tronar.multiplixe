using adduo.helper.extensionmethods;
using multiplixe.comum.dapper;
using System;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.usuario.registro
{
    public class Repositorio
    {
        private DapperHelper dapperHelper { get; }

        public Repositorio(DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        public void Registrar(dto.Usuario usuario)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_id", usuario.Id)
                .AddParameter("_empresaId", usuario.EmpresaId)
                .AddParameter("_nome", usuario.Nome)
                .AddParameter("_apelido", usuario.Apelido)
                .AddParameter("_email", usuario.Email)
                .AddParameter("_dataCadastro", usuario.DataCadastro.ToMySQL())
                .Insert("usuario_registrar");
        }
    }
}
