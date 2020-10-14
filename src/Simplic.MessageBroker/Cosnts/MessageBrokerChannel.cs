namespace Simplic.MessageBroker
{
    /// <summary>
    /// Container for message broker channel names
    /// </summary>
    public static class MessageBrokerChannel
    {
        /// <summary>
        /// Message channel name of the global queue
        /// </summary>
        public const string GlobalMessageChannel = "messagebroker:queue:global";

        /// <summary>
        /// Gets the message channel name of the user specific queue
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>The name of the user message channel</returns>
        public static string GetUserMessageChannel(int userId) => $"messagebroker:queue:user:{userId}";
    }
}