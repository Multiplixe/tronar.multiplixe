using multiplixe.comum.helper;
using multiplixe.empresas.client;
using System;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.perfil.access_token.facebook
{
    public class ProcessadorToken : ServicoBase, IProcessarAccessToken
    {

        public ProcessadorToken(Guid empresaId, EmpresaClient empresaClient) : base(empresaId, empresaClient)
        {
        }

        /// <summary>
        /// Troca de token por um novo de longa duração
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public comum_dto.PerfilAccessToken TrocarToken(string json)
        {
            // consultar Facebook GRPC
            return new comum_dto.PerfilAccessToken();
        }
    }
}
