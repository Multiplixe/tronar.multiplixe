using adduo.helper.envelopes;
using dto = multiplixe.comum.dto;
using multiplixe.comum.enums;
using multiplixe.usuarios.perfil.grpc.Protos;
using System;
using Microsoft.VisualBasic;

namespace multiplixe.usuarios.grpc.parsers
{
    public class PerfilRegistrar
    {
        public RequestEnvelope<dto.Perfil> Request(PerfilMessage perfilMessage)
        {
            var dto = new dto.Perfil
            {
                UsuarioId = Guid.Parse(perfilMessage.UsuarioId),
                EmpresaId = Guid.Parse(perfilMessage.EmpresaId),
                Nome = perfilMessage.Nome,
                PerfilId = perfilMessage.PerfilId,
                RedeSocial = (RedeSocialEnum)perfilMessage.RedeSocial,
                Ativo = perfilMessage.Ativo,
                DataCadastro = new DateTime(perfilMessage.DataCadastro),
                Token = perfilMessage.Token,
                ImagemUrl = perfilMessage.ImagemUrl,
                Login = perfilMessage.Login
            };

            if (!perfilMessage.ExpiracaoToken.Equals(0))
            {
                dto.ExpiracaoToken = new DateTime(perfilMessage.ExpiracaoToken);
            }

            return new RequestEnvelope<dto.Perfil>(dto);
        }
    }
}
