using multiplixe.comum.dapper;
using System;
using System.Collections.Generic;

namespace multiplixe.classificador.nivel
{
    public class Repositorio
    {
        private DapperHelper dapperHelper { get; }

        public Repositorio(DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        public List<results.Nivel> Obter(Guid empresaId)
        {
            var lista = dapperHelper
                .ResetParameter()
                .AddParameter("_empresaId", empresaId)
                .ExecuteWithManyResult<results.Nivel>("niveis_obter");

            return lista;
        }

        public void Registrar(Guid usuarioId, int nivelId)
        {
            dapperHelper
               .ResetParameter()
               .AddParameter("_usuarioId", usuarioId)
               .AddParameter("_nivelId", nivelId)
               .Update("nivel_registrar");
        }

    }
}
