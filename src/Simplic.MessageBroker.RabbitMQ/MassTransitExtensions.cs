using MassTransit;
using MassTransit.RabbitMqTransport;
using Simplic.Configuration;

namespace Simplic.MessageBroker.RabbitMQ
{
    /// <summary>
    /// Extenstions for MassTransit objects
    /// </summary>
    public static class MassTransitExtensions
    {
        /// <summary>
        /// Initializes the RabbitMQ host for a MassTransitBus
        /// </summary>
        /// <param name="rabbitMQConfigurator"></param>
        /// <param name="configurationService"></param>
        public static void InitializeHost(
            this IRabbitMqBusFactoryConfigurator rabbitMQConfigurator,
            IConfigurationService configurationService)
        {
            rabbitMQConfigurator.Host(configurationService.GetValue<string>("Host", "RabbitMQ", ""), host =>
            {
                host.Username(configurationService.GetValue<string>("UserName", "RabbitMQ", ""));
                host.Password(configurationService.GetValue<string>("Password", "RabbitMQ", ""));
            });
        }
    }
}