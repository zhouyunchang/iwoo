using System;

namespace Cben.Events.Bus.Exceptions
{
    /// <summary>
    /// This type of events are used to notify for exceptions handled by Cben infrastructure.
    /// </summary>
    public class CbenHandledExceptionData : ExceptionData
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exception">Exception object</param>
        public CbenHandledExceptionData(Exception exception)
            : base(exception)
        {

        }
    }
}