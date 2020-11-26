using multiplixe.publicidade_banner.grpc.Protos;
using System;
using System.Collections.Generic;

namespace multiplixe.publicidade_banner.grpc
{
    public class Servico
    {
        private readonly Repositorio repositorio;
        private readonly Parametros parametros;

        public Servico(Repositorio repositorio, Parametros parametros)
        {
            this.repositorio = repositorio;
            this.parametros = parametros;
        }
        
        public List<BannerMessage> Obter(Guid usuarioId)
        {
            var results = repositorio.Obter(usuarioId);

            var response = new List<BannerMessage>();

            foreach (var result in results)
            {
                var message = result.ToMessage();
                message.Imagem = $"{parametros.URL_S3}/{message.Id}/image.jpg";
                message.Thumb = $"{parametros.URL_S3}/{message.Id}/thumb.jpg";
                response.Add(message);
            }

            return response;
        }
    }
}
