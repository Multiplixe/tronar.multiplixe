using multiplixe.comum.helper;
using multiplixe.empresas.client;
using System;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.perfil.access_token
{
    public abstract class ServicoBase
    {
        protected EmpresaClient empresaClient { get; }

        protected Guid empresaId { get; }

        public ServicoBase(Guid empresaId, EmpresaClient empresaClient)
        {
            this.empresaId = empresaId;
            this.empresaClient = empresaClient;
        }

        public comum_dto.PerfilAccessToken Parse(string json)
        {
            return DeserializadorHelper.Deserializar<comum_dto.PerfilAccessToken>(json);
        }

    }
}
