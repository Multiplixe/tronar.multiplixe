using multiplixe.comum.enums;
using System;
using System.Net;
using adduohelper = adduo.helper.envelopes;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.token
{
    public class Servico
    {
        private Repositorio repositorio { get; }
        public Servico(Repositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public adduohelper.ResponseEnvelope Registrar(dto.Token token)
        {
            var response = new adduohelper.ResponseEnvelope();

            try
            {
                ValidarParaRegistrar(token);

                repositorio.Registrar(token);
            }
            catch (ArgumentException a)
            {
                response.Error.Exception = a;
                response.HttpStatusCode = HttpStatusCode.BadRequest;
            }
            catch
            {
                // ##TODO LOG
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public void ValidarParaRegistrar(dto.Token token)
        {
            if (string.IsNullOrEmpty(token.Valor))
            {
                throw new ArgumentException("Valor inválido", "Valor");
            }
            else if (token.UsuarioId.Equals(Guid.Empty))
            {
                throw new ArgumentException("UsuarioId inválido", "UsuarioId");
            }

        }


        public adduohelper.ResponseEnvelope<dto.Token> Obter(Guid usuarioId)
        {
            return Obter(usuarioId, TipoTokenEnum.PushNotification);
        }

        public adduohelper.ResponseEnvelope<dto.Token> Obter(Guid usuarioId, TipoTokenEnum tipo)
        {
            var result = repositorio.Obter(usuarioId, tipo);

            var response = new adduohelper.ResponseEnvelope<dto.Token>();

            if (result != null)
            {
                response.Item = new dto.Token
                {
                    Tipo = (TipoTokenEnum)result.Tipo,
                    Valor = result.Valor,
                    UsuarioId = result.UsuarioId
                };

                response.HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }

            return response;
        }

        public void Delete(Guid usuarioId, TipoTokenEnum tipo)
        {
            repositorio.Delete(usuarioId, tipo);
        }

    }
}
