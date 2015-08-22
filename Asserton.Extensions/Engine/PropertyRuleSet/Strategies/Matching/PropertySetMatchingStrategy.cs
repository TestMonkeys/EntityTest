using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestMonkey.Assertion.Extensions.Engine.Validators;

namespace TestMonkey.Assertion.Extensions.Engine.PropertyRuleSet.Strategies.Matching
{
    class PropertySetMatchingStrategy:PropertyMatchingStrategy
    {
        public override List<ValidationResult> Validate(PropertyInfo propertyInfo, object actualObj, object expectedObj,
            string messagePropertyPrefix = null)
        {
            var result = new List<ValidationResult>();

           

            return result;
        }


        
    }
}
