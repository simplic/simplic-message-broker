using MassTransit.RabbitMqTransport;
using Simplic.Configuration;
using MassTransit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.MessageBroker.RabbitMQ
{
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
