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
            var uri = new Uri(configurationService.GetByName("RabbitMQ").ConnectionString);
            rabbitMQConfigurator.Host(uri);

            //var connectionStringValues = configurationService.GetByName("RabbitMQ").ConnectionString.Split(';');

            //rabbitMQConfigurator.Host(connectionStringValues.Where(x => x.StartsWith("host=")).First().Replace("host=", ""), host =>
            //{
            //    host.Username(connectionStringValues.Where(x => x.StartsWith("username=") || x.StartsWith("user=")).First().Replace("username=", "").Replace("user=", ""));
            //    host.Password(connectionStringValues.Where(x => x.StartsWith("password=") || x.StartsWith("pwd=")).First().Replace("password=", "").Replace("pwd=", ""));
            //});
        }
    }
}