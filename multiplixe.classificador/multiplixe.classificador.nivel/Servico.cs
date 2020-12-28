using System;
using System.Collections.Generic;
using System.Linq;
using dto = multiplixe.comum.dto;

namespace multiplixe.classificador.nivel
{
    public class Servico
    {
        private readonly FronteiroServico fronteiroServico;
        private static List<dto.Nivel> cache { get; set; }

        private Repositorio repositorio { get; }
        private Regras regras { get; }

        public Servico(Repositorio repositorio, Regras regras, FronteiroServico fronteiroServico)
        {
            this.repositorio = repositorio;
            this.regras = regras;
            this.fronteiroServico = fronteiroServico;
            cache = new List<dto.Nivel>();
        }

        public List<dto.Nivel> Listar()
        {
            PreparaCache();

            return cache;
        }

        private void PreparaCache()
        {
            var niveis = repositorio.Obter();

            if (!niveis.Any())
            {
                throw new Exception($"Nenhum nível cadastrado .");
            }
            else if (!niveis.Any(a => a.PontuacaoMinima.Equals(0)))
            {
                throw new Exception($"Nenhum nível cadastrado com pontuação mínima de 0");
            }

            cache = niveis
                .OrderByDescending(o => o.PontuacaoMinima)
                .Select(s => new dto.Nivel
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    PontuacaoMinima = s.PontuacaoMinima
                }).ToList();
        }

        public void Processar(Guid usuarioId, int pontos, Guid empresaId)
        {
            var niveis = Listar();
            var nivel = regras.Calcular(pontos, niveis);

            repositorio.Registrar(usuarioId, nivel.Id);
        }

        public dto.Nivel ObterInicial()
        {
            var niveis = Listar();
            return regras.ObterInicial(niveis);
        }

        public dto.classificacao.Nivel ObterFronteiros(int nivelId, int pontos)
        {
            var niveis = Listar();
            var response = fronteiroServico.Obter(nivelId, pontos, niveis);

            return response;
        }

    }
}
