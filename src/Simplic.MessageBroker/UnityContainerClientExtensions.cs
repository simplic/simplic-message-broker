using MassTransit;
using Simplic.Configuration;
using Unity;
using Unity.Lifetime;

namespace Simplic.MessageBroker
{
    public static class UnityContainerClientExtensions
    {
        /// <summary>
        /// Initializes MassTransit for a Client
        /// </summary>
        /// <param name="container"></param>
        /// <param name="configurationService"></param>
        /// <returns></returns>
        public static IUnityContainer InitializeMassTransitForClient(
            this IUnityContainer container,
            IConfigurationService configurationService)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.InitializeHost(configurationService);
            });


            container.RegisterInstance<IBusControl>(bus);
            container.RegisterInstance<IBus>(bus);


            bus.Start();
            return container;
        }

    }
}
