using multiplixe.comum.enums;
using multiplixe.empresas.client;
using System;

namespace multiplixe.usuarios.perfil.access_token
{
    public class Factory
    {
        private readonly RedeSocialEnum redeSocial;
        private readonly Guid empresaId;
        private readonly EmpresaClient empresaClient;

        public Factory(RedeSocialEnum redeSocial, Guid empresaId, EmpresaClient empresaClient)
        {
            this.redeSocial = redeSocial;
            this.empresaId = empresaId;
            this.empresaClient = empresaClient;
        }

        public  IProcessarAccessToken Obter()
        {
            IProcessarAccessToken servico = null;

            if(redeSocial == RedeSocialEnum.facebook)
            {
                servico = new facebook.ProcessadorToken(empresaId, empresaClient);
            }
            else if (redeSocial == RedeSocialEnum.twitter)
            {
                servico = new twitter.ProcessadorToken(empresaId, empresaClient);
            }
            else if (redeSocial == RedeSocialEnum.twitch)
            {
                servico = new twitch.ProcessadorToken(empresaId, empresaClient);
            }
            else if (redeSocial == RedeSocialEnum.youtube)
            {
                servico = new youtube.ProcessadorToken(empresaId, empresaClient);
            }

            return servico;
        }
    }
}
