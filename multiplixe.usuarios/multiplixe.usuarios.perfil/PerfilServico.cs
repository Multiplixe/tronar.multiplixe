using adduohelper = adduo.helper;
using dto = multiplixe.comum.dto;
using enums = multiplixe.comum.enums;
using corehelper = multiplixe.comum.helper;
using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using multiplixe.central_rtdb.client;

namespace multiplixe.usuarios.perfil
{
    public class PerfilServico
    {
        private Repositorio repositorio { get; }
        private RTDBAtividadeClient rtdbAtividadeClient { get; }

        public PerfilServico(Repositorio repositorio, RTDBAtividadeClient rtdbAtividadeClient)
        {
            this.repositorio = repositorio;
            this.rtdbAtividadeClient = rtdbAtividadeClient;
        }
        public adduohelper.envelopes.ResponseEnvelope<dto.Perfil> Registrar(adduohelper.envelopes.RequestEnvelope<dto.Perfil> request)
        {
            var response = request.CreateResponse();

            var redesSociais = enums.EnumHelper.GetValues<enums.RedeSocialEnum>();

            if (request.Item.EmpresaId.Equals(Guid.Empty) ||
                request.Item.UsuarioId.Equals(Guid.Empty) ||
                string.IsNullOrEmpty(request.Item.PerfilId) ||
                string.IsNullOrEmpty(request.Item.Nome) ||
                !redesSociais.Contains(request.Item.RedeSocial))
            {
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            request.Item.Ativo = true;
            request.Item.DataCadastro = corehelper.DateTimeHelper.Now();

            repositorio.Registrar(request.Item);

            rtdbAtividadeClient.RegistrarPerfil(request.Item.UsuarioId);

            response.HttpStatusCode = HttpStatusCode.Created;

            return response;
        }

        public adduohelper.envelopes.ResponseEnvelope<results.Perfil> Obter(Filtro filtro)
        {
            var results = repositorio.Obter(filtro);

            var response = new adduohelper.envelopes.ResponseEnvelope<results.Perfil>();

            if (results == null || !results.Any(a => a.Ativo))
            {
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.Item = results.First();
            }

            return response;
        }

        public adduohelper.envelopes.ResponseEnvelope<dto.RedesSociaisPerfisConectados> ObterPerfisConectados(Guid usuarioId)
        {
            var filtro = new Filtro { UsuarioId = usuarioId };

            var results = repositorio.Obter(filtro);

            var response = new adduohelper.envelopes.ResponseEnvelope<dto.RedesSociaisPerfisConectados>();

            if (results.Any())
            {
                response.Item.Perfis = Parser(results);
                response.Item.TemConexao = true;
            }

            return response;
        }

        private List<dto.Perfil> Parser(List<results.Perfil> results)
        {
            if (results == null)
            {
                return null;
            }

            var response = new List<dto.Perfil>();

            foreach (var result in results)
            {
                response.Add(new dto.Perfil
                {
                    EmpresaId = result.EmpresaId,
                    UsuarioId = result.UsuarioId,
                    PerfilId = result.PerfilId,
                    Nome = result.Nome,
                    RedeSocial = (enums.RedeSocialEnum)result.RedeSocialId,
                    Ativo = result.Ativo,
                    DataCadastro = result.DataCadastro,
                    Token = result.Token,
                    ImagemUrl = result.ImagemUrl,
                    Login = result.Login
                });
            }

            return response;
        }

    }
}
