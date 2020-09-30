using multiplixe.comum.dapper;
using multiplixe.registrador_de_eventos.grpc.results;
using System;

namespace multiplixe.registrador_de_eventos.grpc.servicos.facebook
{
    public class Repositorio : RepositorioBase
    {
        public Repositorio(DapperHelper dapperHelper) : base(dapperHelper)
        {
        }

        public void RegistrarReacao(Guid id, Guid usuarioId, string postId, string perfilId, DateTime dataEvento, string json, string intensidade, int tipo)
        {
            var dapper = base.ParametrosDapperEvento(id, usuarioId, postId, perfilId, dataEvento, json);

            dapper
                .AddParameter("_intensidade", intensidade)
                .AddParameter("_tipo", tipo)
                .Insert("facebook_reacao_registrar");
        }

        public ReacaoResult ObterUltimaReacao(Guid usuarioId, string postId)
        {
            var result = this.dapperHelper
                            .ResetParameter()
                            .AddParameter("_usuarioId", usuarioId)
                            .AddParameter("_postId", postId)
                            .ExecuteWithOneResult<ReacaoResult>("facebook_reacao_obter_ultima");
            return result;
        }
    }
}
