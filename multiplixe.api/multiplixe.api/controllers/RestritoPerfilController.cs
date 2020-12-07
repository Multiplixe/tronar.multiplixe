using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;
using multiplixe.comum.enums;
using multiplixe.usuarios.client;
using adduohelper = adduo.helper.envelopes;
using coreenums = multiplixe.comum.enums;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.api.controllers
{
    [ApiController]
    [Route("restrito/perfil")]
    public class RestritoPerfilController : AppRestritoController
    {
        private PerfilClient perfilClient { get; }

        public RestritoPerfilController(
                IConfiguration configuration,
                EmpresaSettings empresaSettings,
                PerfilClient perfilClient
            ) : base(configuration, empresaSettings)
        {
            this.perfilClient = perfilClient;
        }

        [HttpPost]
        [Route("twitter")]
        public IActionResult PostTwitter([FromBody] adduohelper.RequestEnvelope<comum_dto.Perfil> request)
        {
            request.Item.RedeSocial = coreenums.RedeSocialEnum.twitter;

            return RegistrarPerfil(request);
        }

        [HttpPost]
        [Route("facebook")]
        public IActionResult PostFacebook([FromBody] adduohelper.RequestEnvelope<comum_dto.Perfil> request)
        {
            request.Item.RedeSocial = coreenums.RedeSocialEnum.facebook;

            return RegistrarPerfil(request);
        }


        [HttpPost]
        [Route("youtube")]
        public IActionResult PostYoutube([FromBody] adduohelper.RequestEnvelope<comum_dto.Perfil> request)
        {
            request.Item.RedeSocial = coreenums.RedeSocialEnum.youtube;

            return RegistrarPerfil(request);
        }

        private IActionResult RegistrarPerfil(adduohelper.RequestEnvelope<comum_dto.Perfil> request)
        {
            ConfiguraEmpresa(request.Item);
            ConfiguraUsuario(request.Item);

            var registrarPerfil = new integracao_grpc.RegistrarPerfil(request);

            return IntegrarGRPC<comum_dto.Perfil>(registrarPerfil);
        }

        [HttpGet]
        [Route("perfis-conectados")]
        public IActionResult GetPerfisConectados()
        {
            var usuarioId = ObterUsuarioId();

            var obterPerfisConectados = new integracao_grpc.ObterPerfisConectados(usuarioId);

            return IntegrarGRPC<comum_dto.RedesSociaisPerfisConectados>(obterPerfisConectados);

        }

        [HttpGet]
        [Route("facebook")]
        public IActionResult GetPerfilRedeSocialFacebook()
        {
            return ObterPerfilRedeSocial(RedeSocialEnum.facebook);
        }

        [HttpGet]
        [Route("twitter")]
        public IActionResult GetPerfilRedeSocialTwitter()
        {
            return ObterPerfilRedeSocial(RedeSocialEnum.twitter);
        }

        [HttpGet]
        [Route("twitch")]
        public IActionResult GetPerfilRedeSocialTwitch()
        {
            return ObterPerfilRedeSocial(RedeSocialEnum.twitch);
        }

        [HttpGet]
        [Route("youtube")]
        public IActionResult GetPerfilRedeSocialYoutube()
        {
            return ObterPerfilRedeSocial(RedeSocialEnum.youtube);
        }


        [HttpPost]
        [Route("desconectar")]
        public IActionResult Desconectar([FromBody] adduohelper.RequestEnvelope<comum_dto.Perfil> request)
        {
            request.Item.UsuarioId = ObterUsuarioId();

            var desconectarPerfil = new integracao_grpc.DesconectarPerfil(request.Item);

            return IntegrarGRPC(desconectarPerfil);

        }

        private IActionResult ObterPerfilRedeSocial(RedeSocialEnum redesocial)
        {
            var usuarioId = ObterUsuarioId();

            var responsePerfil = perfilClient.Obter(usuarioId, redesocial);

            var dashboard = new comum_dto.PerfilDashboard();

            var response = new adduohelper.ResponseEnvelope<comum_dto.PerfilDashboard>(dashboard);

            if (responsePerfil.Success)
            {
                dashboard.Perfil = responsePerfil.Item;
            }

            return StatusCode((int)responsePerfil.HttpStatusCode, response);
        }
    }
}
