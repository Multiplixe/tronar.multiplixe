using comum_dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.perfil.access_token
{
    public interface IProcessarAccessToken
    {
        /// <summary>
        /// Para algumas redes sociais, é possível trocar o token e login por um token de longa vida, por exemplo Facebook com token de 60 dias.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        comum_dto.PerfilAccessToken TrocarToken(string json);

        /// <summary>
        /// Tranforma o formato do objeto token da rede social para o comum.dto.Token
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        comum_dto.PerfilAccessToken Parse(string json);
    }
}
