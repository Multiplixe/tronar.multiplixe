using multiplixe.comum.dto;
using multiplixe.comum.helper;
using multiplixe.enfileirador.client;
using multiplixe.youtube.dto.eventos;
using multiplixe.youtube.livehashtag.console.messages.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace multiplixe.youtube.livehashtag.console.messages
{
    public class Servico : BaseService
    {
        private List<string> enviados { get; set; }
        private EnfileiradorClient enfileirador { get; }

        public Servico(AppSettings settings, oauth2.Servico oauth2Servico, EnfileiradorClient enfileirador) : base(settings, oauth2Servico)
        {
            enviados = new List<string>();
            this.enfileirador = enfileirador;
        }

        public void Processar(string liveChatId, List<string> hashtags)
        {
            var pageToken = string.Empty;
            DateTime? offlineAt = null;

            while (!offlineAt.HasValue)
            {
                Console.WriteLine("------------------------------------------------------------------------------");

                var chat = ObterChat(liveChatId, pageToken);

                foreach (var item in chat.Items)
                {
                    offlineAt = chat.OfflineAt;

                    Console.WriteLine("Data/hora: {0}", DateTimeHelper.Now());
                    Console.WriteLine("usuário  : {0}", item.Snippet.AuthorChannelId);
                    Console.WriteLine("mensagem : {0}", item.Snippet.TextMessageDetails.MessageText);

                    if (ValidarItem(chat, item))
                    {
                        var HashtagValidacao = ValidarHashtag(item, hashtags);

                        if (HashtagValidacao.Valida)
                        {
                            Console.WriteLine("hashtag válida: {0}", HashtagValidacao.HashtagValidada);

                            EnfileirarParaTriador(item, liveChatId, HashtagValidacao.HashtagValidada);

                            RegistrarUsuarioProcessado(item);
                        }
                    }
                }

                pageToken = chat.NextPageToken;

                Thread.Sleep(settings.segundos_entre_consultas * 1000);
            }
        }

        private bool ValidarItem(Response chat, Item item)
        {
            return (!chat.OfflineAt.HasValue ||
                    item.Snippet.PublishedAt <= chat.OfflineAt.Value) &&
                    !enviados.Any(a => a.Equals(item.Snippet.AuthorChannelId));
        }

        private HashtagValida ValidarHashtag(Item item, List<string> hashtags)
        {
            string pattern = @"\#\w*";
            var rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            var matches = rgx.Matches(item.Snippet.TextMessageDetails.MessageText);

            var hashtagEncontrada = matches.Select(s => s.ToString())
                    .FirstOrDefault(a => hashtags.Contains(a));

            var hashtagValida = new HashtagValida();
            hashtagValida.Valida = !string.IsNullOrEmpty(hashtagEncontrada);
            hashtagValida.HashtagValidada = hashtagEncontrada;

            return hashtagValida;
        }

        private Response ObterChat(string liveChatId, string pageToken)
        {

            var messagesUrlPreparada = string.Format(settings.url.messages, liveChatId, settings.apiKey, pageToken);

            var messagesResponse = WebRequestHelper.GetExterno<Response>(messagesUrlPreparada, ObterHeader());
            messagesResponse.ThrownIfError();

            return messagesResponse.Item;
        }
        
        private void EnfileirarParaTriador(Item item, string liveChatId, string hashtag)
        {
                var evento = new Evento(item.Snippet.AuthorChannelId, liveChatId, hashtag);

            var envelope = new EnvelopeEvento<dto.eventos.Evento>
            {
                DataEvento = DateTimeHelper.Now(),
                EmpresaId = settings.empresa_id,
                Evento = evento
            };

            enfileirador.EnfileirarParaTriadorYoutube(envelope);
        }

        private void RegistrarUsuarioProcessado(Item item)
        {
            enviados.Add(item.Snippet.AuthorChannelId);
        }

    }
}
