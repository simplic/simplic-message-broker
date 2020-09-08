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


            container.RegisterInstance<IBusControl>(bus, new ContainerControlledLifetimeManager());
            container.RegisterInstance<IBus>(bus, new ContainerControlledLifetimeManager());


            bus.Start();
            return container;
        }

    }
}
