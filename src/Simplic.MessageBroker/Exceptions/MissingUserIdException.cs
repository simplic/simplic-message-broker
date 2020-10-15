using System;

namespace Simplic.MessageBroker
{
    /// <summary>
    /// Exception to represent a missing userid in a message header
    /// </summary>
    public class MissingUserIdException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the Simplic.MessageBroker.MissingUserIdException class.
        /// </summary>
        public MissingUserIdException() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the Simplic.MessageBroker.MissingUserIdException class
        /// with a specified error message
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MissingUserIdException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the Simplic.MessageBroker.MissingUserIdException class
        /// with a specified error message and a reference to the inner exception that is the cause
        /// of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference 
        /// (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        public MissingUserIdException(string message, Exception innerException) :base(message, innerException)
        {

        }
    }
}