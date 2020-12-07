using adduohelper = adduo.helper;
using dto = multiplixe.comum.dto;
using enums = multiplixe.comum.enums;
using corehelper = multiplixe.comum.helper;
using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using multiplixe.central_rtdb.client;
using multiplixe.empresas.client;
using multiplixe.comum.helper;

namespace multiplixe.usuarios.perfil
{
    public class PerfilServico
    {
        private readonly EmpresaClient empresaClient;

        private Repositorio repositorio { get; }
        private RTDBAtividadeClient rtdbAtividadeClient { get; }

        public PerfilServico(
            Repositorio repositorio,
            RTDBAtividadeClient rtdbAtividadeClient,
            EmpresaClient empresaClient)
        {
            this.repositorio = repositorio;
            this.rtdbAtividadeClient = rtdbAtividadeClient;
            this.empresaClient = empresaClient;
        }
        public adduohelper.envelopes.ResponseEnvelope<dto.Perfil> Registrar(adduohelper.envelopes.RequestEnvelope<dto.Perfil> request)
        {
            var response = request.CreateResponse();

            var perfil = request.Item;

            ValidarRegistrar(perfil);

            perfil.Ativo = true;
            perfil.DataCadastro = corehelper.DateTimeHelper.Now();

            ProcessarToken(perfil);

            repositorio.Registrar(perfil);

            rtdbAtividadeClient.RegistrarPerfil(perfil.UsuarioId);

            response.HttpStatusCode = HttpStatusCode.Created;

            return response;
        }

        private void ValidarRegistrar(dto.Perfil perfil)
        {
            var redesSociais = enums.EnumHelper.GetValues<enums.RedeSocialEnum>();

            if (perfil.EmpresaId.Equals(Guid.Empty) ||
                perfil.UsuarioId.Equals(Guid.Empty) ||
                string.IsNullOrEmpty(perfil.PerfilId) ||
                string.IsNullOrEmpty(perfil.Nome) ||
                !redesSociais.Contains(perfil.RedeSocial))
            {
                throw new ArgumentException();
            }
        }

        private void ProcessarToken(dto.Perfil perfil)
        {
            if (!string.IsNullOrEmpty(perfil.Token) && perfil.ProcessarToken)
            {
                var factoryProcessarToken = new access_token.Factory(perfil.RedeSocial, perfil.EmpresaId, empresaClient);
                var servicoProcessarToken = factoryProcessarToken.Obter();
                var perfilAccessToken = servicoProcessarToken.TrocarToken(perfil.Token);

                perfil.ExpiracaoToken = perfilAccessToken.Expiracao;

                perfil.Token = perfilAccessToken.Json;
            }
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

        public void Desconectar(dto.Perfil perfil)
        {
            repositorio.Desconectar(perfil.UsuarioId, (int)perfil.RedeSocial, perfil.PerfilId, perfil.Ativo);

            rtdbAtividadeClient.RegistrarPerfil(perfil.UsuarioId);
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
