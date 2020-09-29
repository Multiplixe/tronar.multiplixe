using multiplixe.comum.dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.usuario.consulta
{
    public class Repositorio
    {
        private DapperHelper dapperHelper { get; }

        public Repositorio(DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        public results.Usuario Obter(dto.filtros.UsuarioFiltro filtro)
        {
            var result = dapperHelper
                            .ResetParameter()
                            .AddParameter("_email", filtro.Email)
                             .AddParameter("_empresaId", filtro.EmpresaId)
                            .AddParameter("_usuarioId", filtro.UsuarioId)
                            .AddParameter("_apelido", filtro.Apelido)
                            .ExecuteWithOneResult<results.Usuario>("usuario_obter");

            return result;
        }

        public List<results.Usuario> Listar(dto.filtros.UsuarioFiltro filtro)
        {
            var result = new List<results.Usuario>();

            if (filtro.UsuariosIdLista.Any())
            {
                result = dapperHelper
                                .ResetParameter()
                                .AddParameter("_usuarioListaId", filtro.UsuariosIdString)
                                .ExecuteWithManyResult<results.Usuario>("usuario_listar");
            }

            return result;
        }
    }
}
