using adduo.helper.extensionmethods;
using multiplixe.comum.dapper;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.usuario.atualizacao
{
    public class Repositorio
    {
        private DapperHelper dapperHelper { get; }

        public Repositorio(DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        public void Atualizar(dto.Usuario usuario)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_usuarioId", usuario.Id)
                .AddParameter("_nome", usuario.Nome)
                .AddParameter("_apelido", usuario.Apelido)
                .AddParameter("_email", usuario.Email)
                .Insert("usuario_atualizar");
        }

        public void UltimoAcesso(dto.UsuarioUltimoAcesso usuarioUltimoAcesso)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_usuarioId", usuarioUltimoAcesso.UsuarioId)
                .AddParameter("_ultimoAcesso", usuarioUltimoAcesso.Acesso.ToMySQL())
                .Update("usuario_ultimo_acesso");
        }

    }
}
