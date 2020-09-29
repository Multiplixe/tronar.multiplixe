using multiplixe.comum.dapper;
using System;

namespace multiplixe.registrador_de_eventos.grpc.servicos.youtube
{
    public class Repositorio : RepositorioBase
    {
        public Repositorio(DapperHelper dapperHelper) : base(dapperHelper)
        {
        }

        public void RegistrarLiveHashtag(Guid id, Guid usuarioId, string postId, string perfilId, DateTime dataEvento, string hashtag)
        {
            var dapper = base.ParametrosDapperEvento(id, usuarioId, postId, perfilId, dataEvento, string.Empty);

            dapper
                .AddParameter("_hashtag", hashtag)
                .Insert("youtube_live_hashtag_registrar");
        }

        public bool VerificarExistenciaLiveHashtag(Guid usuarioId, string postId)
        {
            var result = this.dapperHelper
                            .ResetParameter()
                            .AddParameter("_usuarioId", usuarioId)
                            .AddParameter("_postId", postId)
                            .ExecuteWithBooleanResult("youtube_live_hashtag_verificar");
            return result;
        }
    }
}
