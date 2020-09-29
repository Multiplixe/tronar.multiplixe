using adduo.helper.envelopes;
using dto = multiplixe.comum.dto;
using multiplixe.usuarios.perfil.grpc.Protos;

namespace multiplixe.usuarios.client.parsers
{
    class PerfilRegistrar
    {
        public PerfilMessage Request(RequestEnvelope<dto.Perfil> request)
        {
            var perfilMessage = new PerfilMessage    
            {
                UsuarioId = request.Item.UsuarioId.ToString(),
                EmpresaId = request.Item.EmpresaId.ToString(),
                Nome = request.Item.Nome,
                PerfilId = request.Item.PerfilId,
                RedeSocial = (int)request.Item.RedeSocial,
                Ativo = request.Item.Ativo,
                DataCadastro = request.Item.DataCadastro.Ticks,
                Token = request.Item.Token,
                ImagemUrl = request.Item.ImagemUrl,
                Login = request.Item.Login,
            };

            return perfilMessage;
        }
    }
}
