using adduo.helper.envelopes;
using multiplixe.usuarios.perfil.grpc.Protos;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.grpc.parsers
{
    public class PerfilObterPerfisConectados
    {
        public PerfilConectadoResponse Response(ResponseEnvelope<dto.RedesSociaisPerfisConectados> envelopeResponse)
        {
            var response = new PerfilConectadoResponse();

            response.HttpStatusCode = (int)envelopeResponse.HttpStatusCode;

            response.TemConexao = envelopeResponse.Item.TemConexao;

            foreach (var perfil in envelopeResponse.Item.Perfis)
            {
                response.Perfis.Add(new PerfilMessage
                {
                    PerfilId = perfil.PerfilId,
                    Nome = perfil.Nome,
                    Login = perfil.Login,
                    RedeSocial = (int)perfil.RedeSocial,
                    ImagemUrl = perfil.ImagemUrl
                });
            }

            return response;
        }


    }
}
