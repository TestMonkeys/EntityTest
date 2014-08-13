using System;

namespace TestMonkey.Assertion.Extensions.Framework.Properties
{
    public class ImproperTypeUsageException : Exception
    {
        public ImproperTypeUsageException(Exception parentException, string message, params object[] args)
            : base(string.Format(message, args), parentException)
        {
        }
    }
}