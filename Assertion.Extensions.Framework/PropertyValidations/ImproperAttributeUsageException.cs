using System;

namespace TestMonkey.Assertion.Extensions.Framework.PropertyValidations
{
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