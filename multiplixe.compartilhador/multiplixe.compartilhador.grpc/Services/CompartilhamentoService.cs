using Grpc.Core;
using multiplixe.compartilhador.compartilhamento;
using multiplixe.compartilhador.grpc.parsers;
using multiplixe.compartilhador.grpc.Protos;
using System;
using System.Net;
using System.Threading.Tasks;

namespace multiplixe.compartilhador.grpc.Services
{
    public class CompartilhamentoService : Compartilhamento.CompartilhamentoBase
    {
        private readonly Servico compartilhamentoServico;

        public CompartilhamentoService(Servico servico, Servico compartilhamentoServico)
        {
            this.compartilhamentoServico = compartilhamentoServico;
        }
        public override Task<BaseResponse> Compartilhar(CompartilharRequest request, ServerCallContext context)
        {
            var response = new BaseResponse();

            try
            {
                var parser = new CompartilhamentoCompartilhar();

                var r = parser.Request(request);

                 compartilhamentoServico.Compartilhar(r);
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return Task.FromResult(response);
        }

        public override Task<BaseResponse> Registrar(RegistrarRequest request, ServerCallContext context)
        {
            var response = new BaseResponse();

            try
            {
                var parser = new CompartilhamentoRegistrar();

                var r = parser.Request(request);

                compartilhamentoServico.Registrar(r);
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return Task.FromResult(response);
        }
    }
}
