using System;
using System.Collections.Generic;
using System.Linq;
using dto = multiplixe.comum.dto;

namespace multiplixe.classificador.nivel
{
    public class Servico
    {
        private static Dictionary<Guid, List<dto.Nivel>> cache { get; set; }

        private Repositorio repositorio { get; }
        private Regras regras { get; }

        public Servico(Repositorio repositorio, Regras regras)
        {
            this.repositorio = repositorio;
            this.regras = regras;

            cache = new Dictionary<Guid, List<dto.Nivel>>();
        }

        public List<dto.Nivel> Obter(Guid empresaId)
        {
            PreparaCache(empresaId);

            return cache[empresaId];
        }

        private void PreparaCache(Guid empresaId)
        {
            if (!cache.ContainsKey(empresaId))
            {
                var niveis = repositorio.Obter(empresaId);

                if (!niveis.Any())
                {
                    throw new Exception($"Nenhum nível cadastrado para a empresa {empresaId}.");
                }
                else if (!niveis.Any(a => a.PontuacaoMinima.Equals(0)))
                {
                    throw new Exception($"Nenhum nível cadastrado com pontuação mínima de 0 para a empresa {empresaId}");
                }

                cache[empresaId] = niveis
                    .OrderByDescending(o => o.PontuacaoMinima)
                    .Select(s => new dto.Nivel
                    {
                        Id = s.Id,
                        Nome = s.Nome,
                        PontuacaoMinima = s.PontuacaoMinima
                    }).ToList();
            }
        }

        public dto.Nivel Calcular(int pontos, Guid empresaId)
        {
            var niveis = Obter(empresaId);
            return regras.Calcular(pontos, niveis);
        }

        public dto.Nivel ObterInicial(Guid empresaId)
        {
            var niveis = Obter(empresaId);
            return regras.ObterInicial(niveis);
        }

    }
}
