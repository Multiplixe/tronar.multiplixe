using System;
using System.Collections.Generic;

namespace multiplixe.compartilhador.post
{
    public class Servico
    {
        private readonly Repositorio repositorio;

        public Servico(Repositorio repositorio)
        {
            this.repositorio = repositorio;
        }
        /// <summary>
        /// Obtem os post's pré cadastrados, exceto os que ja foram compartilhados anteriormente.
        /// </summary>
        public List<object> Obter(Guid usuarioId, Guid empresaId)
        {
            var results = repositorio.Obter(usuarioId, empresaId);

            var dtos = results; // mudar para DTO que deve ser criado no projeto multiplixe.comum.dto

            return new List<object>();
        }
    }
}
