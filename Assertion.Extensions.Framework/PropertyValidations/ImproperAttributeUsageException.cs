﻿using System;

namespace TestMonkey.Assertion.Extensions.Framework.PropertyValidations
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