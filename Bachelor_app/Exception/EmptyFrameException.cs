using System;

namespace Bachelor_app
{
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
