using MassTransit;
using Moq;
using Simplic.MessageChannel;
using Simplic.Session;
using System;
using Unity;
using Xunit;

namespace Simplic.MessageBroker.Service.Test
{
    public class MessageBusTests
    {
        [Fact]
        public void MessageBusSend_UsingGenericAndAnonymus_SendsMessageIdAndUser()
        {
            var container = new UnityContainer();

            container.RegisterType<IMessageBus, MessageBus>();

            var channelPublisher = new Mock<IChannelPublisher>();
            var publishCalled = 0;
            channelPublisher.Setup(x => x.Publish(It.IsAny<string>(), It.Is<string>(y => y.Contains("MessageId") && y.Contains("UserId"))))
                .Callback(() =>
                {
                    publishCalled += 1;
                });
            container.RegisterInstance<IChannelPublisher>(channelPublisher.Object);

            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(x => x.CurrentSession).Returns(() =>
            {
                return new Session.Session()
                {
                    UserId = 0
                };
            });
            container.RegisterInstance<ISessionService>(sessionService.Object);

            var busControl = new Mock<IBusControl>();
            container.RegisterInstance<IBusControl>(busControl.Object);

            var messageBus = container.Resolve<IMessageBus>();
            messageBus.Send<TestInterface>(new { Name = "TestName", Value = "TestValue", MessageId = Guid.NewGuid() });

            Assert.Equal(1, publishCalled);
        }

        public interface TestInterface : ICommandBase
        {
            string Name { get; set; }
            string Value { get; set; }
        }
    }
}