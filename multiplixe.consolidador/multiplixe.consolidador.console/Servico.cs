using multiplixe.comum.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace multiplixe.consolidador.console
{
    public class Servico
    {
        private eventos.Servico eventosService { get; }
        private pontuacao.Servico pontuacaoService { get; }
        private saldo.Servico saldoService { get; }
        private Repositorio repositorio { get; }

        public Servico(
            eventos.Servico eventosService, 
            pontuacao.Servico pontuacaoService, 
            saldo.Servico saldoService,
            Repositorio repositorio)
        {
            this.eventosService = eventosService;
            this.pontuacaoService = pontuacaoService;
            this.saldoService = saldoService;
            this.repositorio = repositorio;
        }

        public void Processar(Ponto ponto)
        {
            eventosService.RegistrarEvento(ponto);
            pontuacaoService.RegistrarPontuacao(ponto);
            saldoService.RegistrarSaldo(ponto);
        }

        public void Rollback(Ponto ponto)
        {
            repositorio.Rollback(ponto);
        }
    }
}
