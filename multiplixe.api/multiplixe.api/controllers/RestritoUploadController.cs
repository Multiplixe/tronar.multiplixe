using multiplixe.enfileirador.client;
using multiplixe.api.dto.settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using comum_dto =  multiplixe.comum.dto;

namespace multiplixe.api.controllers
{
    [Route("restrito/upload")]
    [ApiController]
    public class RestritoUploadController : AppRestritoController
    {
        public EnfileiradorClient EnfileiradorClient { get; }
        private Parametros parametros { get; }

        public RestritoUploadController(
                IConfiguration configuration,
                EmpresaSettings empresaSettings,
                EnfileiradorClient enfileiradorClient,
                Parametros parametros
            ) : base(configuration, empresaSettings)

        {
            this.enfileiradorClient = enfileiradorClient;
            EnfileiradorClient = enfileiradorClient;
            this.parametros = parametros;
        }

        [HttpPost]
        [Route("avatar")]
        public IActionResult Avatar()
        {
            try
            {
                var avatarParaProcessar = new comum_dto.AvatarParaProcessar();

                ConfiguraEmpresa(avatarParaProcessar);
                ConfiguraUsuario(avatarParaProcessar);

                avatarParaProcessar.Avatar.Imagem = string.Concat(avatarParaProcessar.UsuarioId, ".jpg");
                avatarParaProcessar.Caminho = $"{parametros.PastaUpload}/avatar/{avatarParaProcessar.EmpresaId}";

                if (!Directory.Exists(avatarParaProcessar.Caminho))
                {
                    Directory.CreateDirectory(avatarParaProcessar.Caminho);
                }

                using (var fileStream = new FileStream($"{avatarParaProcessar.Caminho}/{avatarParaProcessar.Avatar.Imagem}", FileMode.Create))
                {
                    foreach (var file in Request.Form.Files)
                    {
                        file.CopyTo(fileStream);
                    }
                }

                enfileiradorClient.EnfileirarAvatar(avatarParaProcessar);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}