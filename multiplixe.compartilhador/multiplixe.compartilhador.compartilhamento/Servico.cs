
using System;
using System.Collections.Generic;
using multiplixe.enfileirador.client;
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

        /// <summary>
        /// Obtem os Compartilhamentos
        /// </summary>
        public List<comum_dto.Compartilhamento> Obter(Guid empresaId, int quatidade, int tipo)
        {
            var results = repositorio.Obter(empresaId, quatidade, tipo);

            var dtos = results; // mudar para DTO que deve ser criado no projeto 

            return new List<comum_dto.Compartilhamento>();
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
