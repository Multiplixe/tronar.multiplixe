using System.Collections.Generic;
using System.Net;
using adduohelper = adduo.helper;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.usuario.consulta
{
    public class Servico
    {
        private Repositorio repositorio { get; }
        public Servico(Repositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public adduohelper.envelopes.ResponseEnvelope<dto.Usuario> Obter(dto.filtros.UsuarioFiltro filtro)
        {
            var response = new adduohelper.envelopes.ResponseEnvelope<dto.Usuario>();

            var result = repositorio.Obter(filtro);

            if (result == null)
            {
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.Item = Parse(result);
            }

            return response;
        }

        public adduohelper.envelopes.ResponseEnvelope<List<dto.Usuario>> Listar(dto.filtros.UsuarioFiltro filtro)
        {
            var response = new adduohelper.envelopes.ResponseEnvelope<List<dto.Usuario>>();

            var results = repositorio.Listar(filtro);

            foreach (var result in results)
            {
                response.Item.Add(Parse(result));
            }

            return response;
        }

        public dto.Usuario Parse(results.Usuario result)
        {
            if (result == null)
            {
                return null;
            }

            var dto = new dto.Usuario
            {
                Id = result.Id,
                Nome = result.Nome,
                Apelido = result.Apelido,
                Email = result.Email,
                EmpresaId = result.EmpresaId,
                DataCadastro = result.DataCadastro
            };

            return dto;
        }
    }
}
