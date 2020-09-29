using comum_dto =  multiplixe.comum.dto;
using coreenums = multiplixe.comum.enums;
using multiplixe.comum.helper;
using multiplixe.api.dto.settings;
using multiplixe.api.interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace multiplixe.api.log_eventos
{
    public class LogEventoService<T> where T : comum_dto.EventoBase
    {
        private string caminhoArquivoLog = string.Empty;

        private ILogEventoSettings<T> settings { get; }
        private coreenums.RedeSocialEnum redeSocial { get; }
        private EmpresaSettings empresaSettings { get; }

        public LogEventoService(
            coreenums.RedeSocialEnum redeSocial,
            EmpresaSettings empresaSettings,
            ILogEventoSettings<T> settings)
        {
            this.settings = settings;
            this.redeSocial = redeSocial;
            this.empresaSettings = empresaSettings;

            PreparaCaminhoArquivoLog();
        }

        private void PreparaCaminhoArquivoLog()
        {
            var caminhoEmpresa = $"c:/log/{empresaSettings.Id}";

            if (!Directory.Exists(caminhoEmpresa))
            {
                Directory.CreateDirectory(caminhoEmpresa);
            }

            caminhoArquivoLog = caminhoEmpresa;
        }

        private string ObterCaminhoArquivoLog(string arquivo)
        {
            return string.Concat(caminhoArquivoLog, "/", redeSocial, "-", arquivo, ".txt");
        }

        public void LogarRequestInicial(HttpRequest request)
        {
            var textos = new List<string>();

            foreach (var key in request.Query.Keys)
            {
                textos.Add(string.Format("{0}:{1}", key, request.Query[key].ToString()));
            }

            Logar(textos, "request-inicial");
        }

        public void LogarEvento(T evento)
        {
            if (settings.LogarEvento)
            {
                var json = SerializadorHelper.Serializar(evento);
                Logar(new List<string> { json }, "eventos");
            }
        }

        private void Logar(List<string> textos, string arquivo)
        {
            using (var sw = File.AppendText(ObterCaminhoArquivoLog(arquivo)))
            {
                sw.WriteLine("-----------------------------------------------------------------");
                sw.WriteLine("");
                sw.WriteLine(DateTimeHelper.Now().ToString());

                foreach (var texto in textos)
                {
                    sw.WriteLine(texto);
                }

                sw.WriteLine("");
            }
        }
    }
}
