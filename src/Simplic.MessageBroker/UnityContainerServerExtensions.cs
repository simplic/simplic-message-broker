using MassTransit;
using Simplic.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;

namespace Simplic.MessageBroker
{
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
            IConfigurationService configurationService
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
            });

            container.RegisterInstance<IBusControl>(bus);
            container.RegisterInstance<IBus>(bus);

            bus.Start();
            return container;
        }

        /// <summary>
        /// get only loadable types
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