using System;

namespace Simplic.MessageBroker
{
    /// <summary>
    /// Interface to have base values for every command
    /// </summary>
    public interface ICommandBase
    {
        Guid MessageId { get; set; }

        int UserId { get; set; }
    }
}