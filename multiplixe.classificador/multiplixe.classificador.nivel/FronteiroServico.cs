using System.Collections.Generic;
using System.Linq;

namespace multiplixe.classificador.nivel
{
    public class FronteiroServico
    {

        /// <summary>
        /// A partir do nivel atual e da pontuação do usuário, obtem o nível anterior e a pontuação necessária para o próximo nível.
        /// </summary>
        /// <param name="nivelId"></param>
        /// <param name="pontos"></param>
        /// <returns></returns>

        public comum.dto.classificacao.Nivel Obter(int nivelId, int pontos, List<comum.dto.Nivel> niveis)
        {
            var response = new comum.dto.classificacao.Nivel();

            ProcessarAnterior(response, nivelId, niveis);
            ProcessarProximo(response, nivelId, pontos, niveis);
            ProcessarAtual(response, nivelId, pontos, niveis);
            
            return response;
        }

        private void ProcessarAnterior(comum.dto.classificacao.Nivel dto, int nivelId, List<comum.dto.Nivel> niveis)
        {
            dto.Anterior = new comum.dto.classificacao.NivelItem { Mostrar = false };

            var anterior = niveis.Where(w => w.Id == nivelId-1).LastOrDefault();

            if (anterior != null)
            {
                dto.Anterior = new comum.dto.classificacao.NivelItem
                {
                    Id = anterior.Id,
                    Nome = anterior.Nome,
                    Mostrar = true
                };
            }
        }

        private void ProcessarAtual(comum.dto.classificacao.Nivel dto, int nivelId, int pontos, List<comum.dto.Nivel> niveis)
        {
            var atual = niveis.Where(w => w.Id == nivelId).FirstOrDefault();

            dto.Atual = new comum.dto.classificacao.NivelItemAtual
            {
                Id = atual.Id,
                Nome = atual.Nome,
                Mostrar = true
            };

            if(dto.Proximo.Mostrar)
            {
                dto.Atual.PontosParaProximoNivel = dto.Proximo.Pontos - pontos;
            }
        }


        private void ProcessarProximo(comum.dto.classificacao.Nivel dto, int nivelId, int pontos, List<comum.dto.Nivel> niveis)
        {
            dto.Proximo = new comum.dto.classificacao.NivelItemProximo { Mostrar = false };

            var proximo = niveis.Where(w => w.Id == nivelId + 1).FirstOrDefault();

            if (proximo != null)
            {
                dto.Proximo = new comum.dto.classificacao.NivelItemProximo
                {
                    Id = proximo.Id,
                    Nome = proximo.Nome,
                    Mostrar = true,
                    Pontos = proximo.PontuacaoMinima
                };
            }
        }
    }
}
