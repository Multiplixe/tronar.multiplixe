using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace multiplixe.facebook.triador.ws
{
    public class Worker : BackgroundService
    {
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private const string QueueName = "teste-sw-v1";

        public Worker()
        {
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            using (var sw = File.AppendText("c:/log/sw.txt"))
            {
                sw.WriteLine("-----------------------------------------------------------------");
                sw.WriteLine("Start 1");
                sw.WriteLine("-----------------------------------------------------------------");
            }

            _connectionFactory = new ConnectionFactory()
            {
                DispatchConsumersAsync = true,
                HostName = "localhost"
            };

            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: QueueName, exclusive: false); ;
            //_channel.BasicQos(0, 1, false);

            using (var sw = File.AppendText("c:/log/sw.txt"))
            {
                sw.WriteLine("-----------------------------------------------------------------");
                sw.WriteLine("Start 2");
                sw.WriteLine("-----------------------------------------------------------------");
            }

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);

            using (var sw = File.AppendText("c:/log/sw.txt"))
            {
                sw.WriteLine("-----------------------------------------------------------------");
                sw.WriteLine("ExecuteAsync 1");
            }

            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);

            using (var sw = File.AppendText("c:/log/sw.txt"))
            {
                sw.WriteLine("-----------------------------------------------------------------");
                sw.WriteLine("ExecuteAsync 2");
            }

            consumer.Received += async (bc, ea) =>
            {
                using (var sw = File.AppendText("c:/log/sw.txt"))
                {
                    sw.WriteLine("-----------------------------------------------------------------");
                    sw.WriteLine("ExecuteAsync 4");
                }

                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                _channel.BasicAck(ea.DeliveryTag, false);

                Console.WriteLine(message);

                using (var sw = File.AppendText("c:/log/sw.txt"))
                {
                    sw.WriteLine("-----------------------------------------------------------------");
                    sw.WriteLine(message);
                }
            };

            using (var sw = File.AppendText("c:/log/sw.txt"))
            {
                sw.WriteLine("-----------------------------------------------------------------");
                sw.WriteLine("ExecuteAsync 3");
            }


            await Task.CompletedTask;

        }


        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection.Close();

            await base.StopAsync(cancellationToken);
        }

    }
}
