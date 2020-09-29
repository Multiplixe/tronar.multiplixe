using multiplixe.classificador.client;
using multiplixe.usuarios.client;
using System;
using System.Collections.Generic;
using System.Linq;
using adduohelper = adduo.helper.envelopes;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.api.consultas
{
    public class Dashboard
    {
        private ClassificadorClient classificadorClient { get; }
        private PerfilClient perfilClient { get; }

        public Dashboard(
            ClassificadorClient classificadorClient,
            PerfilClient perfilClient)
        {
            this.classificadorClient = classificadorClient;
            this.perfilClient = perfilClient;
        }

        public adduohelper.ResponseEnvelope<comum_dto.Dashboard> Obter(Guid usuarioId)
        {
            var response = new adduohelper.ResponseEnvelope<comum_dto.Dashboard>();

            var responsePerfil = perfilClient.ObterPerfisConectados(usuarioId);

            if (responsePerfil.Success)
            {
                response.Item.TemConexaoRedeSocial = responsePerfil.Item.TemConexao;
                response.Item.Classificacao = new comum_dto.classificacao.Classificacao();

                if (responsePerfil.Item.TemConexao)
                {
                    PreparaClassificacao(usuarioId, response);
                    PreparaRedeSocial(responsePerfil.Item.Perfis, response);
                    PreparaRedeSocialOrdenacao(response);
                }
            }

            return response;
        }

        private void PreparaClassificacao(Guid usuarioId, adduohelper.ResponseEnvelope<comum_dto.Dashboard> response)
        {
            var responseClassificacao = classificadorClient.ObterClassificacao(usuarioId);

            response.HttpStatusCode = responseClassificacao.HttpStatusCode;

            if (responseClassificacao.Success)
            {
                response.Item.Classificacao = responseClassificacao.Item;
            }
        }

        private void PreparaRedeSocial(List<comum_dto.Perfil> perfils, adduohelper.ResponseEnvelope<comum_dto.Dashboard> response)
        {
            foreach (var redeSocial in perfils.Select(s => s.RedeSocial).Distinct())
            {
                var pontuacaoRedeSocial = response.Item.Classificacao.RedesSociais.FirstOrDefault(f => f.Id == (int)redeSocial);

                if (pontuacaoRedeSocial != null)
                {
                    pontuacaoRedeSocial.Conectado = true;
                }
            }

        }

        private void PreparaRedeSocialOrdenacao(adduohelper.ResponseEnvelope<comum_dto.Dashboard> response)
        {
            response.Item.Classificacao.RedesSociais = response.Item.Classificacao.RedesSociais
                                                            .OrderByDescending(o => o.Pontos)
                                                            .ThenByDescending(t => t.Conectado)
                                                            .ToList();
        }

    }
}
