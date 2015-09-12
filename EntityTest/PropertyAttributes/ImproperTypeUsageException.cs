using System;

namespace TestMonkey.EntityTest.PropertyAttributes
{
    [Serializable]
    public class ImproperTypeUsageException : Exception
    {
        public ImproperTypeUsageException(Exception parentException, string message, params object[] args)
            : base(string.Format(message, args), parentException)
        {
        }
    }
}