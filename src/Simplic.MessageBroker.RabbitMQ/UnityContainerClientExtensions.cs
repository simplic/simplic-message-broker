﻿using GreenPipes;
using MassTransit;
using Simplic.Configuration;
using Simplic.Session;
using System;
using System.Linq;
using Unity;

namespace Simplic.MessageBroker.RabbitMQ
{
    /// <summary>
    /// Extensions for a client based unity container
    /// </summary>
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
            IConnectionConfigurationService connectionConfigurationService,
            ISessionService sessionService)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.InitializeHost(connectionConfigurationService);

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
    }
}