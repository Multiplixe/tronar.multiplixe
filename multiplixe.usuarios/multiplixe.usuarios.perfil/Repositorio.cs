﻿using adduo.helper.extensionmethods;
using multiplixe.comum.enums;
using System;
using System.Collections.Generic;
using dapper = multiplixe.comum.dapper;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.perfil
{
    public class Repositorio
    {
        private dapper.DapperHelper dapperHelper { get; }

        public Repositorio(dapper.DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

        public List<results.Perfil> Obter(Filtro filtro)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_usuarioId", filtro.UsuarioId.ToStringNullIfEmpty())
                .AddParameter("_empresaId", filtro.EmpresaId.ToStringNullIfEmpty())
                .AddParameter("_perfilId", filtro.PerfilId);

            if (filtro.RedeSocial == multiplixe.comum.enums.RedeSocialEnum.none)
            {
                dapperHelper.AddParameterNullValue("_redeSocialId");
            }
            else
            {
                dapperHelper.AddParameter("_redeSocialId", (int)filtro.RedeSocial);
            }

            var result = dapperHelper.ExecuteWithManyResult<results.Perfil>("perfil_obter");

            return result;
        }

        public void Registrar(dto.Perfil perfil)
        {
            var dapper = dapperHelper
                .ResetParameter()
                .AddParameter("_empresaId", perfil.EmpresaId)
                .AddParameter("_usuarioId", perfil.UsuarioId)
                .AddParameter("_perfilId", perfil.PerfilId)
                .AddParameter("_nome", perfil.Nome)
                .AddParameter("_redeSocialId", (int)perfil.RedeSocial)
                .AddParameter("_ativo", perfil.Ativo)
                .AddParameter("_dataCadastro", perfil.DataCadastro)
                .AddParameter("_token", perfil.Token)
                .AddParameter("_imagemUrl", perfil.ImagemUrl)
                .AddParameter("_login", perfil.Login);


            if (perfil.ExpiracaoToken.HasValue)
            {
                dapper.AddParameter("_expiracaoToken", perfil.ExpiracaoToken.Value.ToMySQL());
            }
            else
            {
                dapper.AddParameterNullValue("_expiracaoToken");

            }

            dapper.Insert("perfil_registrar");
        }

        public void Desconectar(Guid usuarioId, int redeSocialId, string perfilId, bool ativo)
        {
            dapperHelper
                .ResetParameter()
                .AddParameter("_usuarioId", usuarioId)
                .AddParameter("_redeSocialId", redeSocialId)
                .AddParameter("_perfilId", perfilId)
                .AddParameter("_ativo", ativo)
                .Insert("perfil_desconectar");
        }

    }
}
