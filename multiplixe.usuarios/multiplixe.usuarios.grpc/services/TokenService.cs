using multiplixe.usuarios.grpc.protos;
using Grpc.Core;
using System.Threading.Tasks;

namespace multiplixe.usuarios.grpc.services
{
    public class tokenervice : Token.TokenBase
    {
        private token.Servico servico { get; }
        private parsers.TokenRegistrar tokenRegistrarParser { get; }
        private parsers.TokenObter tokenObterParser { get; }

        public tokenervice(
            token.Servico registrador,
            parsers.TokenRegistrar tokenRegistrar,
            parsers.TokenObter tokenObter)
        {
            this.servico = registrador;
            this.tokenRegistrarParser = tokenRegistrar;
            this.tokenObterParser = tokenObter;
        }

        public override Task<TokenResponse> Registrar(TokenRequest request, ServerCallContext context)
        {
            var dto = tokenRegistrarParser.Request(request);

            var envelope = servico.Registrar(dto);

            var response = tokenRegistrarParser.Response(envelope);

            return Task.FromResult<TokenResponse>(response);
        }

        public override Task<TokenResponse> Obter(TokenFiltro filtro, ServerCallContext context)
        {
            var dto = tokenObterParser.Request(filtro);

            var envelope = servico.Obter(dto.UsuarioId, dto.Tipo);

            var response = tokenObterParser.Response(envelope);

            return Task.FromResult<TokenResponse>(response);
        }

    }
}
