using multiplixe.enfileirador.client;
using System;
using System.Collections.Generic;
using coredto = multiplixe.comum.dto;
using coreenums = multiplixe.comum.enums;

namespace multiplixe.notificador.client
{
    public class NotificadorClient
    {
        private EnfileiradorClient enfileirador { get; }

        public NotificadorClient(EnfileiradorClient enfileirador)
        {
            this.enfileirador = enfileirador;
        }

        public void EnviarEmail(Guid empresaId, string nome, string email, string titulo, List<string> paragrafos)
        {
            var notificacao = new coredto.Notificacao()
            {
                EmpresaId = empresaId,
                Nome = nome,
                Destinatario = email,
                Titulo = titulo,
                Paragrafos = paragrafos,
                Tipo = coreenums.TipoNotificacaoEnum.email
            };

            enfileirador.EnfileirarParaNotificadorEmail(notificacao);
        }

        public void EnviarPush(string nome, string token, string titulo, string mensagem)
        {
            var notificacao = new coredto.Notificacao()
            {
                Nome = nome,
                Destinatario = token,
                Titulo = titulo,
                Tipo = coreenums.TipoNotificacaoEnum.push
            };

            notificacao.Paragrafos.Add(mensagem);

            enfileirador.EnfileirarParaNotificadorEmail(notificacao);
        }

        public void TwitchPubSub(coredto.UsuarioParaProcessar usuarioParaProcessar)
        {
            enfileirador.EnfileirarParaNotificadorTwichPubSub(usuarioParaProcessar);
        }
    }
}
