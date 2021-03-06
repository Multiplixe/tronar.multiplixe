﻿using multiplixe.enfileirador.client;
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

            ProcessarPorcentagem(redesSociais);

            return redesSociais;
        }


        private void ProcessarPorcentagem(List<dto.classificacao.RedeSocial> redesSociais)
        {
            var total = redesSociais.Sum(s => s.Percent);

            if (total != 100 && total > 0)
            {
                var decimais = new List<dynamic>();
                var diferenca = 100 - total;

                for (var i = 0; i < redesSociais.Count; i++)
                {
                    var percent = (redesSociais[i].Pontos * 100.0) / total;
                    var d = percent - Math.Truncate(percent);
                    decimais.Add(new { valor = d, index = i });
                }

                var maiorValorDecimal = decimais.OrderByDescending(o => o.valor).First();

                redesSociais[maiorValorDecimal.index].Percent += diferenca;
            }
        }


        public void ProcessarIndividual(Guid usuarioId)
        {
            repositorio.ProcessarIndividuais(usuarioId);
        }


        public void ProcessarTotal(Guid usuarioId)
        {
             repositorio.ProcessarTotal(usuarioId);
        }

    }
}
