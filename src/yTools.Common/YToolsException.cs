using System;

namespace yTools.Common
{
    public class YToolsException : Exception
    {
        public YToolsException()
        {
        }

        public YToolsException(string message)
            : base(message)
        {
        }

        public YToolsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
