using System;
using dto = multiplixe.comum.dto;
using adduo.helper.envelopes;
using adduo.helper.extensionmethods;
using System.Net;
using multiplixe.classificador.interfaces;

namespace multiplixe.classificador.usuario
{
    public class Servico : IConsultarUsuario
    {
        private Repositorio repositorio { get; }
        private nivel.Servico nivelService { get; }

        public Servico(Repositorio repositorio,
            nivel.Servico nivelService)
        {
            this.repositorio = repositorio;
            this.nivelService = nivelService;
        }

        public ResponseEnvelope Registrar(dto.Usuario usuario)
        {
            var response = new ResponseEnvelope();

            if (usuario.Id.Equals(Guid.Empty) ||
                usuario.EmpresaId.Equals(Guid.Empty))
            {
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var nivelInicial = nivelService.ObterInicial();

            repositorio.Registrar(usuario, nivelInicial.Id);

            return response;
        }

        public ResponseEnvelope Sincronizar(dto.Usuario usuario)
        {
            var response = new ResponseEnvelope();

            if (usuario.Id.Equals(Guid.Empty) ||
                string.IsNullOrEmpty(usuario.Nome) ||
                string.IsNullOrEmpty(usuario.Apelido))
            {
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            repositorio.Sincronizar(usuario);

            return response;
        }

        public ResponseEnvelope Deletar(Guid usuarioId)
        {
            var response = new ResponseEnvelope();

            if (usuarioId.IsEmpty())
            {
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            repositorio.Deletar(usuarioId);

            return response;
        }

        public results.Usuario Obter(Guid usuarioId)
        {
            return repositorio.Obter(usuarioId);
        }

        public bool VerificarExistencia(Guid usuarioId)
        {
            var result = repositorio.Obter(usuarioId);

            return result is results.Usuario;
        }

    }
}
