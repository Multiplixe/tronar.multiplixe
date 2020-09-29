using multiplixe.comum.dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using multiplixe.registrador_de_eventos.grpc.Protos;
using adduo.helper.extensionmethods;

namespace multiplixe.registrador_de_eventos.grpc
{
    public class RepositorioBase
    {
        protected DapperHelper dapperHelper { get; }

        public RepositorioBase(DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        protected DapperHelper ParametrosDapperEvento(Guid id, Guid usuarioId, string postId, string perfilId, DateTime dataEvento, string json)
        {
            return this.dapperHelper
                .ResetParameter()
                .AddParameter("_id", id)
                .AddParameter("_usuarioId", usuarioId)
                .AddParameter("_postId", postId)
                .AddParameter("_perfilId", perfilId)
                .AddParameter("_dataEvento", dataEvento.ToMySQL())
                .AddParameter("_evento", json);
        }
    }
}
