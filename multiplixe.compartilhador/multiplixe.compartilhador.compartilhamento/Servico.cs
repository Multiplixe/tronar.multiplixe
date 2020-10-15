using multiplixe.enfileirador.client;
using System;

namespace multiplixe.compartilhador.compartilhamento
{
    public class Servico
    {
        private readonly Repositorio repositorio;
        private readonly EnfileiradorClient enfileirador;

        public Servico(Repositorio repositorio, EnfileiradorClient enfileirador)
        {
            this.repositorio = repositorio;
            this.enfileirador = enfileirador;
        }

        public void Compartilhar(object request)
        {
            // logica para definir quais redes sociais o usuário  escolheu.
            //- tentar usar Task
            enfileirador.EnfileirarCompartilhadorFacebook(request);
            enfileirador.EnfileirarCompartilhadorTwitter(request);
        }

        public void Registrar(object request)
        {
            repositorio.Registrar(request);
        }
    }
}
