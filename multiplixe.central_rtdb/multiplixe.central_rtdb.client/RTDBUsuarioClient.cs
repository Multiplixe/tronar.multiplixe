using coreexceptions = multiplixe.comum.exceptions;
using System;
using System.Net;

namespace multiplixe.central_rtdb.client
{
    public class RTDBUsuarioClient : BaseClient   
    {

        public RTDBUsuarioClient()
        {
        }

        public void Iniciar(Guid usuarioId)
        {
            var parser = new parsers.Iniciar();

            var request = parser.Request(usuarioId);

            var response = client.Iniciar(request);

            if (response.HttpStatusCode != (int)HttpStatusCode.OK)
            {
                throw new coreexceptions.GRPCException($"Erro ao iniciar usuário no RTDB -> {response.Erro}");
            }
        }

        public void Deletar(Guid usuarioId)
        {
            var parser = new parsers.Deletar();

            var request = parser.Request(usuarioId);

            var response = client.Deletar(request);

            if (response.HttpStatusCode != (int)HttpStatusCode.OK)
            {
                throw new coreexceptions.GRPCException($"Erro ao deletar usuário no RTDB -> {response.Erro}");
            }
        }


    }
}
