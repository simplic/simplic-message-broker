using GreenPipes;
using MassTransit;
using MassTransit.Testing;
using Simplic.Configuration;
using Simplic.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;

namespace Simplic.MessageBroker.RabbitMQ
{
    /// <summary>
    /// Extensions for a client server unity container
    /// </summary>
    public static class UnityContainerServerExtensions
    {
        /// <summary>
        /// Initializes MassTransit for a server
        /// </summary>
        /// <param name="container"></param>
        /// <param name="configurationService"></param>
        /// <returns></returns>
        public static IUnityContainer InitializeMassTransitForServer(
            this IUnityContainer container,
            IConfigurationService configurationService,
            ISessionService sessionService
        )
        {
            var consumerTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetLoadableTypes())
                .Where(t => typeof(IConsumer).IsAssignableFrom(t))
                .ToList();

            foreach (var consumer in consumerTypes)
            {
                if (consumer.GetCustomAttributes().Any(x => x.GetType() == typeof(QueueAttribute)))
                {
                    Console.WriteLine($"Consumer found {consumer.FullName}");
                    container.RegisterType(consumer);
                }
            }
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.InitializeHost(configurationService);

                if (consumerTypes.Any())
                {
                    var consumers = new Dictionary<string, Type>();
                    foreach (var consumerType in consumerTypes)
                    {
                        var attributes = consumerType.GetCustomAttributes(typeof(QueueAttribute), true);
                        if (attributes.Any())
                        {
                            var queueName = ((QueueAttribute)attributes[0]).Name;
                            consumers.Add(queueName, consumerType);
                        }
                    }

                    Console.WriteLine($"Consumers found: {consumers.Count}");

                    foreach (var consumer in consumers)
                        cfg.ReceiveEndpoint(consumer.Key, ec => ec.Consumer(consumer.Value, x => { return container.Resolve(x); }));
                }

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

            var timeout = configurationService.GetValue<int>("ConnectionTimeout", "RabbitMQ", "");

            bus.Start(TimeSpan.FromSeconds(timeout));

            return container;
        }

        /// <summary>
        /// Get only loadable types
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}