using System;

namespace Simplic.MessageBroker
{
    /// <summary>
    /// Interface to have base values for every command
    /// </summary>
    public interface ICommandBase
    {
        /// <summary>
        /// Gets or sets the message id
        /// </summary>
        Guid MessageId { get; set; }

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        int UserId { get; set; }
    }
}