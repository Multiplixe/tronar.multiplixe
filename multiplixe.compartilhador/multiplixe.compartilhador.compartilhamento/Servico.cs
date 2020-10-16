using multiplixe.enfileirador.client;
using System;
using comum_dto = multiplixe.comum.dto;

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

        public void Compartilhar(comum_dto.Compartilhamento compartilhamento)
        {
            // logica para definir quais redes sociais o usuário  escolheu.
            //- tentar usar Task
            enfileirador.EnfileirarCompartilhadorFacebook(compartilhamento);
            enfileirador.EnfileirarCompartilhadorTwitter(compartilhamento);
        }

        public void Registrar(object request)
        {
            repositorio.Registrar(request);
        }
    }
}
