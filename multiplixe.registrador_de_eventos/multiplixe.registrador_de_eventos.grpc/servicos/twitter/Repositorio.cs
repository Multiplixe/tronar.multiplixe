using multiplixe.comum.dapper;
using multiplixe.registrador_de_eventos.grpc.results;
using System;

namespace multiplixe.registrador_de_eventos.grpc.servicos.twitter
{
    public class Repositorio : RepositorioBase
    {
        public Repositorio(DapperHelper dapperHelper) : base(dapperHelper)
        {
        }

        public void RegistrarReacao(Guid id, Guid usuarioId, string postId, string perfilId, DateTime dataEvento, string json, int tipo)
        {
            var dapper = base.ParametrosDapperEvento(id, usuarioId, postId, perfilId, dataEvento, json);

            dapper
                .AddParameter("_tipo", tipo)
                .Insert("twitter_reacao_registrar");
        }

        public ReacaoResult ObterUltimaReacao(Guid usuarioId, string postId)
        {
            var result = this.dapperHelper
                            .ResetParameter()
                            .AddParameter("_usuarioId", usuarioId)
                            .AddParameter("_postId", postId)
                            .ExecuteWithOneResult<ReacaoResult>("twitter_reacao_obter_ultima");
            return result;
        }
    }
}
