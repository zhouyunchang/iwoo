using System;
using System.Runtime.Serialization;
using Cben.Logging;

namespace Cben.Authorization
{
    /// <summary>
    /// This exception is thrown on an unauthorized request.
    /// </summary>
    [Serializable]
    public class CbenAuthorizationException : CbenException, IHasLogSeverity
    {
        /// <summary>
        /// Severity of the exception.
        /// Default: Warn.
        /// </summary>
        public LogSeverity Severity { get; set; }

        /// <summary>
        /// Creates a new <see cref="CbenAuthorizationException"/> object.
        /// </summary>
        public CbenAuthorizationException()
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// Creates a new <see cref="CbenAuthorizationException"/> object.
        /// </summary>
        public CbenAuthorizationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="CbenAuthorizationException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public CbenAuthorizationException(string message)
            : base(message)
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// Creates a new <see cref="CbenAuthorizationException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public CbenAuthorizationException(string message, Exception innerException)
            : base(message, innerException)
        {
            Severity = LogSeverity.Warn;
        }
    }
}