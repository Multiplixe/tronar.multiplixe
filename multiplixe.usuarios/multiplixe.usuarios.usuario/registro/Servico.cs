using adduo.helper.entries.entry_exceptions;
using multiplixe.classificador.client;
using multiplixe.comum.exceptions;
using multiplixe.comum.helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using adduohelper = adduo.helper;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.usuario.registro
{
    public class Servico
    {
        private Validador validador { get; }
        private Repositorio repositorio { get; }
        private Firebase registroFirebase { get; }
        private ClassificadorUsuarioClient classificador { get; }
        private exclusao.Servico rollback { get; }
        private inicio.Servico iniciadorFirebase { get; }

        public Servico(
            Validador validador,
            Repositorio repositorio,
            Firebase firebase,
            ClassificadorUsuarioClient classificador,
            inicio.Servico iniciadorFirebase,
            exclusao.Servico rollback)
        {
            this.validador = validador;
            this.repositorio = repositorio;
            this.registroFirebase = firebase;
            this.classificador = classificador;
            this.iniciadorFirebase = iniciadorFirebase;
            this.rollback = rollback;
        }



        public adduohelper.envelopes.ResponseEnvelope<dto.entries.UsuarioRegistro> Registrar(adduohelper.envelopes.RequestEnvelope<dto.entries.UsuarioRegistro> request)
        {
            var response = request.CreateResponse();

            var id = Guid.NewGuid();

            try
            {
                request.Item.UsuarioId = id;

                validador.Validar(request);

                registroFirebase.Registrar(request);

                var usuario = new dto.Usuario
                {
                    Id = request.Item.UsuarioId,
                    EmpresaId = request.Item.EmpresaId,
                    Nome = request.Item.Nome.Value,
                    Apelido = request.Item.Apelido.Value,
                    Email = request.Item.Email.Value,
                    DataCadastro = DateTimeHelper.Now()
                };

                var classificadorRequest = new adduohelper.envelopes.RequestEnvelope<dto.Usuario>(usuario);

                var tasks = new List<Task>();

                tasks.Add(Task.Run(() =>
                {
                    classificador.Registrar(classificadorRequest);
                }));

                tasks.Add(Task.Run(() =>
                {
                    repositorio.Registrar(usuario);
                }));

                tasks.Add(Task.Run(() =>
                {
                    iniciadorFirebase.Iniciar(id);
                }));

                Task.WaitAll(tasks.ToArray());
            }
            catch (EntriesException<dto.entries.UsuarioRegistro> entriesEx)
            {
                // ## TODO LOG

                rollback.Rollback(id);
                response = entriesEx.ResponseEnvelope;
            }
            catch (GRPCException grpcEx)
            {
                // ## TODO LOG

                rollback.Rollback(id);
                response.HttpStatusCode = grpcEx.HttpStatusCode;
            }
            catch (Exception ex)
            {
                // ## TODO LOG

                rollback.Rollback(id);
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

 
    }
}
