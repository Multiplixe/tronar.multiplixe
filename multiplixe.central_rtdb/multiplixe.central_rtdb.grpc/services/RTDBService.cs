using Grpc.Core;
using multiplixe.central_rtdb.grpc.protos;
using System.Threading.Tasks;

namespace multiplixe.central_rtdb.grpc.services
{
    public class RTDBService : RTDB.RTDBBase
    {
        public core.comum.Atividade atividadeComum { get; }
        public core.usuario.Atividade atividade { get; }
        public core.usuario.Iniciador iniciador { get; }

        public RTDBService(core.usuario.Atividade atividade, 
            core.usuario.Iniciador iniciador,
            core.comum.Atividade atividadeComum )
        {
            this.atividade = atividade;
            this.iniciador = iniciador;
            this.atividadeComum = atividadeComum;
        }

        public override Task<Response> RegistrarAtividadeComum(AtividadeRequest request, ServerCallContext context)
        {
            var response = this.atividadeComum.Registrar(request);

            return Task.FromResult(response);
        }

        public override Task<Response> RegistrarAtividade(AtividadeRequest request, ServerCallContext context)
        {
           var response = this.atividade.Registrar(request);

            return Task.FromResult(response);
        }

        public override Task<Response> Iniciar(IniciarRequest request, ServerCallContext context)
        {
            var response = this.iniciador.Iniciar(request.UsuarioId);

            return Task.FromResult(response);
        }

        public override Task<Response> Deletar(DeletarRequest request, ServerCallContext context)
        {
            var response = this.iniciador.Deletar(request.UsuarioId);

            return Task.FromResult(response);
        }
    }
}
