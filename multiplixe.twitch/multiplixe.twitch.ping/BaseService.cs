using multiplixe.usuarios.client;
using System;
using adduohelper = adduo.helper.envelopes;
using comum_dto = multiplixe.comum.dto;
using enums = multiplixe.comum.enums;

namespace multiplixe.twitch.ping
{
    public class BaseService
    {
        private readonly PerfilClient perfilClient;

        public BaseService(PerfilClient perfilClient)
        {
            this.perfilClient = perfilClient;
        }

        protected adduohelper.ResponseEnvelope<comum_dto.Perfil> ObterPerfil(string user_id, Guid empresaId)
        {
            return perfilClient.Obter(empresaId, enums.RedeSocialEnum.twitch, user_id);
        }

    }
}
