using GreenPipes;
using MassTransit;
using Simplic.Configuration;
using Simplic.Session;
using System.Linq;
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
            IConfigurationService configurationService,
            ISessionService sessionService)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.InitializeHost(configurationService);

                var session = sessionService.CurrentSession;

                cfg.ConfigurePublish(publishPipeConfigurator => publishPipeConfigurator.UseExecute(ctx =>
                {
                    ctx.Headers.Set("UserId", session.UserId);
                    ctx.Headers.Set("TenantId", string.Join(",", session.Organizations.Where(x => x.IsActive)));
                }));

                cfg.ConfigureSend(ISendPipeConfigurator => ISendPipeConfigurator.UseExecute(ctx =>
                {
                    ctx.Headers.Set("UserId", session.UserId);
                    ctx.Headers.Set("TenantId", string.Join(",", session.Organizations.Where(x => x.IsActive)));
                }));
            });


            container.RegisterInstance<IBusControl>(bus);
            container.RegisterInstance<IBus>(bus);


            bus.Start();
            return container;
        }

    }
}
