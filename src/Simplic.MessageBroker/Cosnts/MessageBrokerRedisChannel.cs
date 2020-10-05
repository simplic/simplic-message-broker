using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.MessageBroker
{
    public static class MessageBrokerRedisChannel
    {
        public const string EnqueueMessageChannel = "messagebroker:queue:enqueue";
        public const string CompleteMessageChannel = "messagebroker:queue:complete";
    }
}
