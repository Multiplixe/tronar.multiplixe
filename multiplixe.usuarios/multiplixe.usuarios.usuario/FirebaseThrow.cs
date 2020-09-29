using FirebaseAdmin.Auth;
using System;
using System.Net;
using adduohelper = adduo.helper;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.usuario
{
    public class FirebaseThrow<T> where T : dto.entries.UsuarioRegistro
    {
        public void Throw(Exception ex, adduohelper.envelopes.RequestEnvelope<T> request)
        {
            if (ex is FirebaseAuthException f)
            {
                ThrowFirebaseAuthException(f, request);
            }
            else if (ex is ArgumentException a)
            {
                ThrowArgumentException(a, request);
            }

        }

        private void ThrowFirebaseAuthException(FirebaseAuthException ex, adduohelper.envelopes.RequestEnvelope<T> request)
        {
            var response = request.CreateResponse();

            if (ex.ErrorCode == FirebaseAdmin.ErrorCode.AlreadyExists)
            {
                response.Item.Email.Status = adduohelper.entries.STATUS.INVALID;
                response.Item.Email.Code = adduohelper.entries.CODE.ALREADY;
                response.HttpStatusCode = HttpStatusCode.Conflict;
            }
            else if (ex.ErrorCode == FirebaseAdmin.ErrorCode.NotFound)
            {
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }

            Throw(response);
        }

        private void ThrowArgumentException(ArgumentException ex, adduohelper.envelopes.RequestEnvelope<T> request)
        {
            var response = request.CreateResponse();

            if (ex.Message == "Password must be at least 6 characters long.")
            {
                response.Item.Senha.Status = adduohelper.entries.STATUS.INVALID;
                response.Item.Senha.Code = adduohelper.entries.CODE.INVALID;
                response.HttpStatusCode = HttpStatusCode.BadRequest;
            }

            Throw(response);
        }

        private void Throw(adduohelper.envelopes.ResponseEnvelope<T> request)
        {
            throw new adduohelper.entries.entry_exceptions.EntriesException<T>(request);
        }
    }
}
