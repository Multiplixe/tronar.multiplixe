using System.Collections.Generic;
using System.Linq;
using dto = multiplixe.comum.dto;

namespace multiplixe.classificador.nivel
{
    public class Regras
    {
        public dto.Nivel Calcular(int pontos, List<dto.Nivel> niveis)
        {
            var _pontos = pontos < 0 ? 0 : pontos;

            var nivel = niveis.FirstOrDefault(w => _pontos >= w.PontuacaoMinima);

            return nivel;
        }

        public dto.Nivel ObterInicial(List<dto.Nivel> niveis)
        {
            var nivelInicial = niveis.First(f => f.PontuacaoMinima.Equals(0));

            return nivelInicial;
        }
    }
}
