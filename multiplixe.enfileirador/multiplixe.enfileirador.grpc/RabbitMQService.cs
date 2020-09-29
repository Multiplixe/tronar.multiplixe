using multiplixe.enfileirador.core;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiplixe.enfileirador.grpc
{
    public class RabbitMQService
    {
        public void Enfileirar(string nomeDaFila, string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new Exception("Enfileirador: JSON vazio.");
            }

            try
            {
                var fila = Factory.Obtem(nomeDaFila);

                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Data {0}", DateTime.Now.ToString());
                Console.WriteLine("Fila {0}", nomeDaFila);
                Console.WriteLine("json {0}", json);

                var factory = new ConnectionFactory() { HostName = fila.HostName };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: fila.Nome,
                                         durable: fila.Durable,
                                         exclusive: fila.Exclusive,
                                         autoDelete: fila.AutoDelete,
                                         arguments: null);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = fila.Persistent;

                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "",
                                         routingKey: fila.Nome,
                                         basicProperties: properties,
                                         body: body);

                    Console.WriteLine("enfileirou");
                }
            }
            catch (Exception ex)
            {
                // # TODO log
                Console.Write("ERRO  {0}", ex.Message);
            }
        }
    }
}
