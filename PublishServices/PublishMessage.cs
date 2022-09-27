using Integradora.Publish.RabbitMQ.Model;
using Integradora.Publish.RabbitMQ.PublishInterface;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;

namespace Integradora.Publish.RabbitMQ
{
    public class PublishMessage : IPublishMessage
    {
        public RabbitMqConfiguration RabbitMqConfiguration { get; }

        public PublishMessage(IOptions<RabbitMqConfiguration> rabbitMqConfiguration)
        {
            RabbitMqConfiguration = rabbitMqConfiguration.Value;
        }
         
        public void  PublishMessageRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = RabbitMqConfiguration.HostName,

                UserName = RabbitMqConfiguration.UserName,

                Password = RabbitMqConfiguration.Password,

                VirtualHost = RabbitMqConfiguration.VirtualHost
            };

            try
            {
                using var connection =   factory.CreateConnection();

                using var channel =  connection.CreateModel();

                channel.QueueDeclare(queue: RabbitMqConfiguration.QueueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";

                var body = Encoding.UTF8.GetBytes(message);

                 channel.BasicPublish(exchange: "",
                                     routingKey: RabbitMqConfiguration.QueueName,
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine("[x] Enviado com Sucesso. {0} \n---------", message);

            }
            catch (BrokerUnreachableException)
            {
                Console.WriteLine("[*] Error: Sem Conexão");
            }
        }
    }
}
