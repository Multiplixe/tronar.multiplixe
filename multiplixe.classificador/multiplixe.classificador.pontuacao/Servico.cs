using multiplixe.enfileirador.client;
using System;
using System.Collections.Generic;
using System.Linq;
using dto = multiplixe.comum.dto;
using coreenums = multiplixe.comum.enums;

namespace multiplixe.classificador.pontuacao
{
    public class Servico
    {
        private Repositorio repositorio { get; }
        private EnfileiradorClient enfileirador { get; }

        public Servico(Repositorio repositorio, EnfileiradorClient enfileirador)
        {
            this.repositorio = repositorio;
            this.enfileirador = enfileirador;
        }

        public List<dto.classificacao.RedeSocial> Obter(Guid usuarioId)
        {
            var results = repositorio.ObterIndividuais(usuarioId);

            var redesSociais = new List<dto.classificacao.RedeSocial>();

            var total = results.Sum(s => s.Pontos);

            foreach (var result in results)
            {
                var redesocial = new dto.classificacao.RedeSocial
                {
                    Percent = 0,
                    Pontos = result.Pontos,
                    Id = result.Id,
                    Nome = result.Nome
                };

                if (total > 0)
                {
                    var percent = (result.Pontos * 100.0) / total;

                    redesocial.Percent = (int)Math.Round((result.Pontos * 100.0) / total);
                }

                redesSociais.Add(redesocial);
            }

            return redesSociais;
        }

        public void ProcessarIndividual(Guid usuarioId)
        {
            var results = repositorio.ExtrairIndividuais(usuarioId);

            foreach (var item in results)
            {
                repositorio.RegistrarIndividual(usuarioId, (coreenums.RedeSocialEnum)item.Id, item.Pontos);
            }
        }


        public int CalcularTotal(Guid usuarioId)
        {
            var results = repositorio.ObterIndividuais(usuarioId);

            var total = results.Sum(s => s.Pontos);

            return total;
        }


    }
}
