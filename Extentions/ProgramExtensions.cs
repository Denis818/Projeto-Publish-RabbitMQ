using Integradora.Publish.RabbitMQ.Model;
using Integradora.Publish.RabbitMQ.PublishInterface;

namespace Integradora.Publish.RabbitMQ.Extentions
{
    public static class ProgramExtensions
    {
        public static void ConfigurationAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitMqConfiguration"));
        }

        public static void AddDependency(this IServiceCollection services)
        {
            services.AddSingleton<IPublishMessage, PublishMessage>();
        }
    }
}
