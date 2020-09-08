using MassTransit.RabbitMqTransport;
using Simplic.Configuration;
using MassTransit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.MessageBroker
{
    public static class MassTransitExtensions
    {
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




        //Insert into ESS_MS_Intern_Config(PlugInName, UserName, ConfigName, ConfigValue, Description, ContentType, IsEditable, UserCanOverwrite)
        //    Values('RabbitMQ', '', 'Host', 'rabbitmq://localhost/', 'Gibt die Hostadresse des RabbitMQ servers an', 0, 1, 0 );


        //Insert into ESS_MS_Intern_Config(PlugInName, UserName, ConfigName, ConfigValue, Description, ContentType, IsEditable, UserCanOverwrite)
        //    Values('RabbitMQ', '', 'UserName', 'guest', 'Gibt den RabbitMQ Benutzernamen an', 0, 1, 0 );


        //Insert into ESS_MS_Intern_Config(PlugInName, UserName, ConfigName, ConfigValue, Description, ContentType, IsEditable, UserCanOverwrite)
        //    Values('RabbitMQ', '', 'Password', 'guest', 'Gibt das RabbitMQ Passwort an', 0, 1, 0 );
    }
}
