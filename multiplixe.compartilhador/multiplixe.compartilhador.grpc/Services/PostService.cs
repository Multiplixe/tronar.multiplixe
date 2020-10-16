using Grpc.Core;
using multiplixe.compartilhador.grpc.parsers;
using multiplixe.compartilhador.grpc.Protos;
using multiplixe.compartilhador.post;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace multiplixe.compartilhador.grpc.Services
{
    public class PostService : Post.PostBase
    {
        private readonly Servico postServico;

        public PostService(Servico servico)
        {
            this.postServico = servico;
        }

        public override Task<ObterResponse> Obter(ConsultaRequest request, ServerCallContext context)
        {
            var response = new ObterResponse();

            try
            {
                var usuarioId = Guid.Parse(request.UsuarioId);
                var empresaId = Guid.Parse(request.EmpresaId);

                var parser = new PostObter();

                var dtos = postServico.Obter(usuarioId, empresaId);

                response = parser.Response(dtos);
            }
            catch(Exception ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return Task.FromResult(response);
        }
    }
}
