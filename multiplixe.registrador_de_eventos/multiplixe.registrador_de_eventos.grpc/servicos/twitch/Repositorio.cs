using adduo.helper.extensionmethods;
using multiplixe.comum.dapper;
using System;

namespace multiplixe.registrador_de_eventos.grpc.servicos.twitch
{
    public class Repositorio : RepositorioBase
    {
        public Repositorio(DapperHelper dapperHelper) : base(dapperHelper)
        {
        }

        public void RegistrarPing(Guid id, Guid usuarioId, string postId, string perfilId, DateTime dataEvento, int toleranciaSegundos, int frequenciaMinutos, int pausaMilissegundos, DateTime atual, DateTime ultimo)
        {
            var dapper = base.ParametrosDapperEvento(id, usuarioId, postId, perfilId, dataEvento, string.Empty);

            dapper
                .AddParameter("_toleranciaSegundos", toleranciaSegundos)
                .AddParameter("_frequenciaMinutos", frequenciaMinutos)
                .AddParameter("_pausaMilissegundos", pausaMilissegundos)
                .AddParameter("_ultimo", ultimo.ToMySQL())
                .AddParameter("_atual", atual.ToMySQL())
                .Insert("twitch_ping_registrar");
        }
    }
}
