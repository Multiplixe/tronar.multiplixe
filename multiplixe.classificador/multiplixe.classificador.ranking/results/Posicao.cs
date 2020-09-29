using System;
using System.Collections.Generic;
using System.Text;

namespace multiplixe.classificador.ranking.results
{
    public class Posicao
    {
        public Guid UsuarioId { get; set; }

        public int Pontos { get; set; }

        public int Valor { get; set; }
    }
}
