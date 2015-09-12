using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMonkey.Assertion.Extensions.Engine.Validators
{
    public class MatchResult
    {
        public bool Success { get; set; }

        public object Expected { get; set; }
        public object Actual { get; set; }
        public string Parent { get; set; }
        public string PropertyName { get; set; }

        public string GetMessage()
        {
            if (String.IsNullOrEmpty(PropertyName) && string.IsNullOrEmpty(Parent))
                return string.Format("Expected <{0}> but found <{1}>", Expected, Actual);
            return string.Format("Expected <{0}> but found <{1}> for <{2}{3}>", Expected, Actual, Parent ?? string.Empty, PropertyName ?? string.Empty);

            
        }
    }
}
