using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMonkey.Assertion.Extensions.Engine.Validators
{
    public class ValidationResult
    {
        public bool Success { get; set; }

        public object Expected { get; set; }
        public object Actual { get; set; }
        public string Parent { get; set; }
        public string PropertyName { get; set; }
    }
}
