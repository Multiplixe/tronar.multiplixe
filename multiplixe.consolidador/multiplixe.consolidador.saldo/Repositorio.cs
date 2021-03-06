﻿using adduo.helper.extensionmethods;
using coredapper = multiplixe.comum.dapper;
using dto = multiplixe.comum.dto;

namespace multiplixe.consolidador.saldo
{
    public class Repositorio
    {
        private coredapper.DapperHelper dapperHelper { get; }

        public Repositorio(coredapper.DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        public void Registrar(dto.Ponto ponto)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_id", ponto.EventoId)
                .Insert("consolidador_saldo_registrar");
        }
    }
}
