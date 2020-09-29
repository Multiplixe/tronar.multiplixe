using adduo.helper.envelopes;
using multiplixe.comum.enums;
using multiplixe.comum.exceptions;
using multiplixe.usuarios.perfil.grpc.Protos;
using System;
using System.Net;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.client
{
    public class PerfilClient : BaseClient
    {
        private Perfil.PerfilClient client { get; set; }
        private parsers.PerfilRegistrar perfilRegistrarParser { get; }
        private parsers.PerfilObter perfilObterParser { get; }
        private parsers.PerfilObterPerfisConectados perfilObterPerfisConectadosParser { get; }

        public PerfilClient()
        {
            client = new Perfil.PerfilClient(channel);
            this.perfilRegistrarParser = new parsers.PerfilRegistrar();
            this.perfilObterParser = new parsers.PerfilObter();
            this.perfilObterPerfisConectadosParser = new parsers.PerfilObterPerfisConectados();
        }

        public ResponseEnvelope<dto.Perfil> Registrar(RequestEnvelope<dto.Perfil> request)
        {
            var perfilMessage = perfilRegistrarParser.Request(request);

            var perfilResponse = client.Registrar(perfilMessage);

            var envelope = request.CreateResponse();
            envelope.HttpStatusCode = (HttpStatusCode)perfilResponse.HttpStatusCode;

            if (!envelope.Success)
            {
                throw new GRPCException(envelope.HttpStatusCode);
            }

            return envelope;
        }

        public ResponseEnvelope<dto.Perfil> Obter(Guid empresaId, RedeSocialEnum redeSocial, string perfilId)
        {
            var filtro = new PerfilFiltro()
            {
                EmpresaId = empresaId.ToString(),
                RedeSocial = (int)redeSocial,
                PerfilId = perfilId
            };

            return Obter(filtro);
        }

        public ResponseEnvelope<dto.Perfil> Obter(Guid usuarioId, RedeSocialEnum redeSocial)
        {
            var filtro = new PerfilFiltro()
            {
                UsuarioId = usuarioId.ToString(),
                RedeSocial = (int)redeSocial
            };

            return Obter(filtro);
        }

        private ResponseEnvelope<dto.Perfil> Obter(PerfilFiltro filtro)
        {
            var perfilResponse = client.Obter(filtro);

            var envelope = perfilObterParser.Response(perfilResponse);

            if (envelope.HttpStatusCode == HttpStatusCode.InternalServerError)
            {
                throw new GRPCException(envelope.HttpStatusCode);
            }

            return envelope;
        }

        public ResponseEnvelope<dto.RedesSociaisPerfisConectados> ObterPerfisConectados(Guid usuarioId)
        {
            var parametro = perfilObterPerfisConectadosParser.Request(usuarioId);

            var perfilConectadoResponse = client.ObterPerfisConectados(parametro);

            var envelope = perfilObterPerfisConectadosParser.Response(perfilConectadoResponse);

            if (envelope.HttpStatusCode == HttpStatusCode.InternalServerError)
            {
                throw new GRPCException(envelope.HttpStatusCode);
            }

            return envelope;
        }

    }
}
