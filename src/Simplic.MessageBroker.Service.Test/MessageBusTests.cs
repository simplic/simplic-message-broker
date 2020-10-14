using MassTransit;
using Moq;
using Simplic.MessageChannel;
using Simplic.Session;
using Unity;
using Xunit;

namespace Simplic.MessageBroker.Service.Test
{
    public class MessageBusTests
    {
        [Fact]
        public void MessageBusSend_UsingGenericAndAnonymus_IncrementsUserAndClobalTaskQueue()
        {
            var container = new UnityContainer();

            container.RegisterType<IMessageBus, MessageBus>();

            #region Mock IMessageChannel

            var messageChannel = new Mock<IMessageChannel>();
            var incrementCalled = 0;
            messageChannel.Setup(x => x.StringIncrement(It.IsAny<string>(), It.IsAny<double>()))
                .Callback(() =>
                {
                    incrementCalled += 1;
                });
            container.RegisterInstance<IMessageChannel>(messageChannel.Object);

            #endregion Mock IMessageChannel

            #region Mock ISessionService

            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(x => x.CurrentSession).Returns(() =>
            {
                return new Session.Session()
                {
                    UserId = 0
                };
            });
            container.RegisterInstance<ISessionService>(sessionService.Object);

            #endregion Mock ISessionService

            var busControl = new Mock<IBusControl>();
            container.RegisterInstance<IBusControl>(busControl.Object);

            // Send a message via messagebus
            var messageBus = container.Resolve<IMessageBus>();
            messageBus.Send<TestInterface>(new { Name = "TestName", Value = "TestValue" });

            //Since the increment is called once for the user and once for the global queue it needs to be 2
            Assert.Equal(2, incrementCalled);
        }

        public interface TestInterface
        {
            string Name { get; set; }
            string Value { get; set; }
        }
    }
}