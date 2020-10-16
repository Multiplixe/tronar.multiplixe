using multiplixe.compartilhador.client;
using multiplixe.usuarios.client;
using System;

namespace multiplixe.facebook.compartilhador.console
{
    public class Servico
    {
        private readonly PerfilClient perfilClient;
        private readonly PostClient postClient;
        private readonly CompartilhamentoClient compartilhamentoClient;

        public Servico(PerfilClient perfilClient,
            PostClient postClient,
            CompartilhamentoClient compartilhamentoClient)
        {
            this.perfilClient = perfilClient;
            this.postClient = postClient;
            this.compartilhamentoClient = compartilhamentoClient;
        }

        public void Compartilhar(object o)
        {
            var responseToken = perfilClient.ObterAcessToken(Guid.Empty, comum.enums.RedeSocialEnum.facebook);

            if (responseToken.Success)
            {
                var token = responseToken.Item.Valor;

                var envelope = postClient.Obter(new { postId = 1 });

                if (envelope.Success)
                {
                    var post = envelope.Item;

                    // logica de compartilhamento
                    //...

                    // registrar que post foi efetuado com sucesso, armazenando ID do post da rede social, rede social ID, usuario ID e post ID;
                    compartilhamentoClient.Registrar(new { }); 
                }
            }
            else
            {
                // ## TODO logar
            }
        }
    }
}
