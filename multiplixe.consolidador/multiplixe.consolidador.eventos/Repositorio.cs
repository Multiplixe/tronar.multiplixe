using adduo.helper.extensionmethods;
using coredapper = multiplixe.comum.dapper;
using dto = multiplixe.comum.dto;

namespace multiplixe.consolidador.eventos
{
    public class Repositorio
    {
        private coredapper.DapperHelper dapperHelper { get; }

        public Repositorio(coredapper.DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        public void Registrar(dto.Ponto ponto)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_id", ponto.EventoId)
                .AddParameter("_usuarioId", ponto.UsuarioId)
                .AddParameter("_postId", ponto.PostId)
                .AddParameter("_pontos", ponto.Pontos)
                .AddParameter("_dataEvento", ponto.DataEvento.ToMySQL())
                .AddParameter("_redeSocialId", (int)ponto.RedeSocial)
                .AddParameter("_tipoEventoId", (int)ponto.TipoEvento)
                .Insert("eventos_registrar");
        }
    }
}
