namespace Simplic.MessageBroker
{
    /// <summary>
    /// Container for message broker queue keys
    /// </summary>
    public static class MessageBrokerQueueKeys
    {
        /// <summary>
        /// Key for the global queue
        /// </summary>
        public const string GlobalQueueKey = "messagebroker:queue:global";

        /// <summary>
        /// Gets the key of the user specific queue
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>The key of the user queue</returns>
        public static string GetUserQueueKey(int userId) => $"messagebroker:queue:user:{userId}";
    }
}