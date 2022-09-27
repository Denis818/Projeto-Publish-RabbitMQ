using Integradora.Publish.RabbitMQ.PublishInterface;

namespace Brunsker.Integradora.RabbitMQ
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IPublishMessage _publishMessage;

        public Worker(ILogger<Worker> logger, IPublishMessage publishMessage)
        {
            _logger = logger;
            _publishMessage = publishMessage;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("O SERVIÇO ESTÁ INICIANDO.");

            stoppingToken.Register(() => _logger.LogInformation("\nTarefa de segundo plano está parando.\n"));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("[*] Iniciando Envio de Mensagens ... : {time}", DateTimeOffset.Now);

                _publishMessage.PublishMessageRabbitMQ();

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}