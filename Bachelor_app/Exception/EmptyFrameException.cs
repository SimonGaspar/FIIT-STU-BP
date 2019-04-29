using System;

namespace Bachelor_app
{
    /// <summary>
    /// Own exception, when frame from camera is empty/null.
    /// </summary>
    public class EmptyFrameException : Exception
    {
        public EmptyFrameException()
        {
        }

        public EmptyFrameException(string message)
        : base(message)
        {
        }

        public EmptyFrameException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
