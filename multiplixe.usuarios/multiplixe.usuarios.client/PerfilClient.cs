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

        public PerfilClient()
        {
            client = new Perfil.PerfilClient(channel);
        }

        public ResponseEnvelope<dto.Perfil> Registrar(RequestEnvelope<dto.Perfil> request)
        {
            var parser = new parsers.PerfilRegistrar();

            var perfilMessage = parser.Request(request);

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
            var parser = new parsers.PerfilObter();

            var perfilResponse = client.Obter(filtro);

            var envelope = parser.Response(perfilResponse);

            if (envelope.HttpStatusCode == HttpStatusCode.InternalServerError)
            {
                throw new GRPCException(envelope.HttpStatusCode);
            }

            return envelope;
        }

        public ResponseEnvelope<dto.RedesSociaisPerfisConectados> ObterPerfisConectados(Guid usuarioId)
        {
            var parser = new parsers.PerfilObterPerfisConectados();

            var parametro = parser.Request(usuarioId);

            var perfilConectadoResponse = client.ObterPerfisConectados(parametro);

            var envelope = parser.Response(perfilConectadoResponse);

            if (envelope.HttpStatusCode == HttpStatusCode.InternalServerError)
            {
                throw new GRPCException(envelope.HttpStatusCode);
            }

            return envelope;
        }

        public ResponseEnvelope<dto.Token> ObterAcessToken(Guid usuarioId, RedeSocialEnum redesocial)
        {
            var parser = new parsers.PerfilObterAccessToken();

            var request = parser.Request(usuarioId, redesocial);

            var accessTokenResponse = client.ObterAccessToken(request);

            var envelope = parser.Response(accessTokenResponse);

            if (envelope.HttpStatusCode == HttpStatusCode.InternalServerError)
            {
                throw new GRPCException(envelope.HttpStatusCode );
            }

            return envelope;
        }
    }
}
