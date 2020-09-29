using System;
using System.Collections.Generic;
using System.Text;
using empresasgrpc = multiplixe.empresas.client;
using coredto = multiplixe.comum.dto;
using multiplixe.empresas.client;
using System.IO;
using System.Linq;

namespace multiplixe.notificador.email.console
{
    public class Notificador
    {
        private EmpresaClient empresaClient { get; }
        private SmtpService smtpService { get; }

        public Notificador(EmpresaClient empresaClient, SmtpService smtpService)
        {
            this.empresaClient = empresaClient;
            this.smtpService = smtpService;
        }


        public void Enviar(coredto.Notificacao notificacao)
        {
            var html = PrepararHtml(notificacao);

            this.smtpService.Send(notificacao.Destinatario, notificacao.Nome, notificacao.Titulo, html);
        }

        private string PrepararHtml(coredto.Notificacao notificacao)
        {
            var responseEmpresaUrls = empresaClient.ObterUrls(notificacao.EmpresaId);

            var local = Directory.GetCurrentDirectory();

            var caminhoHtml = string.Concat(local, $"/email-templates/padrao.html");

            var template = System.IO.File.ReadAllText(caminhoHtml);
            var html = new StringBuilder(template);

            html.Replace("#BODY#", string.Join(null, notificacao.Paragrafos.Select(s => $"<p>{s}</p>")));
            html.Replace("#NOME#", notificacao.Nome);
            html.Replace("#URLAPP#", responseEmpresaUrls.Item.UrlApp);
            html.Replace("#EMPRESAID#", notificacao.EmpresaId.ToString());

            return html.ToString();
        }

    }
}
