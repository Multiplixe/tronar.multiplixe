using comum_dto =  multiplixe.comum.dto;
using adduohelper = adduo.helper;
using multiplixe.api.dto.settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using multiplixe.enfileirador.client;
using System.IO;

namespace multiplixe.api.controllers
{
    [ApiController]
    [Route("restrito/usuarios")]
    public class RestritoUsuariosController : AppRestritoController
    {
        public RestritoUsuariosController(
                IConfiguration configuration,
                EmpresaSettings empresaSettings,
                EnfileiradorClient enfileiradorClient
            ) : base(configuration, empresaSettings)

        {
            this.enfileiradorClient = enfileiradorClient;
        }

        [HttpPost]
        [Route("registrar-token-push")]
        public IActionResult RegistrarTokenPush([FromBody]  adduohelper.envelopes.RequestEnvelope<comum_dto.Token> request)
        {

            ConfiguraUsuario(request.Item);

            var registrarTokenPushNotification = new integracao_grpc.RegistrarTokenPushNotification(request);

            return IntegrarGRPC<comum_dto.Token>(registrarTokenPushNotification);
        }

        [HttpPut]
        public IActionResult Put([FromBody] adduohelper.envelopes.RequestEnvelope<comum_dto.entries.UsuarioAtualizacao> request)
        {
            ConfiguraUsuario(request.Item);
            ConfiguraEmpresa(request.Item);

            var atualizarUsuario = new integracao_grpc.AtualizarUsuario(request);

            return IntegrarGRPC<comum_dto.entries.UsuarioAtualizacao>(atualizarUsuario);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var usuarioId = ObterUsuarioId();

            var response = new integracao_grpc.ObterUsuario(usuarioId);

            return IntegrarGRPC<comum_dto.Usuario>(response);
        }

        [HttpPost]
        [Route("avatar")]
        public IActionResult PostAvatar()
        {
            var avatar = new comum_dto.AvatarParaProcessar();

            ConfiguraUsuario(avatar);
            ConfiguraEmpresa(avatar);

            enfileiradorClient.EnfileirarAvatar(avatar);

            return Ok();
        }



    }
}