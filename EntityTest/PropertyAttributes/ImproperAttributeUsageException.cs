using System;

namespace TestMonkey.EntityTest.PropertyAttributes
{
    [Serializable]
    public class ImproperAttributeUsageException : Exception
    {
        public ImproperAttributeUsageException(string message) : base(message)
        {
        }

        public ImproperAttributeUsageException(Exception parentException, string message)
            : base(message, parentException)
        {
        }
    }
}