using adduo.helper.envelopes;
using multiplixe.compartilhador.grpc.Protos;
using System;
using System.Collections.Generic;

namespace multiplixe.compartilhador.client.parsers
{
    public class PostObterLista
    {
        public ConsultaRequest Request(object o)
        {
            return new ConsultaRequest
            {
                EmpresaId = Guid.Empty.ToString(), // não remover isso, o GRPC dá erro com NULL
                UsuarioId = Guid.Empty.ToString(), // não remover isso, o GRPC dá erro com NULL
                PostId = "o.ID-XYZ"
            };
        }

        public ResponseEnvelope<List<object>> Response(ObterListaResponse response)
        {
            var lista = new List<object>();

            foreach (var item in response.Posts)
            {
                lista.Add(item); //parse
            }

            return new ResponseEnvelope<List<object>>
            {
                Item = lista
            };
        }
    }
}
