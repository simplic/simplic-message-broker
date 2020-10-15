using MassTransit;
using MassTransit.RabbitMqTransport;
using Simplic.Configuration;
using System;

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
            IConnectionConfigurationService configurationService)
        {
            var connectionString = configurationService.GetByName("RabbitMQ").ConnectionString;
            var uri = new Uri(connectionString);
            rabbitMQConfigurator.Host(uri);
        }
    }
}