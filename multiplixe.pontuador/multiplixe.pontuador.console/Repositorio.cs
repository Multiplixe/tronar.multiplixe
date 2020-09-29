using adduo.helper.extensionmethods;
using coredapper = multiplixe.comum.dapper;
using coredto = multiplixe.comum.dto;

namespace multiplixe.pontuador.console
{
    public class Repositorio
    {
        private coredapper.DapperHelper dapperHelper { get; }

        public Repositorio(coredapper.DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        public void Registrar(coredto.Ponto ponto)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_usuarioId", ponto.UsuarioId)
                .AddParameter("_postId", ponto.PostId)
                .AddParameter("_pontos", ponto.Pontos)
                .AddParameter("_dataEvento", ponto.DataEvento.ToMySQL())
                .AddParameter("_redeSocialId", (int)ponto.RedeSocial)
                .AddParameter("_tipoEventoId", (int)ponto.TipoEvento)
                .Insert("pontuacao_extrato_registrar");
        }
    }
}
