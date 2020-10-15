using multiplixe.compartilhador.post.results;
using System;
using System.Collections.Generic;

namespace multiplixe.compartilhador.post
{
    public class Repositorio
    {
        /// <summary>
        /// Obtem os post's pré cadastrados, exceto os que ja foram compartilhados anteriormente.
        /// </summary>
        public List<PostResult> Obter(Guid usuarioId, Guid empresaId)
        {
            return new List<PostResult>();
        }
    }
}
