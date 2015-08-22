using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestMonkey.Assertion.Extensions.Engine.PropertyRuleSet.Strategies;
using TestMonkey.Assertion.Extensions.Engine.Validators;
using TestMonkey.Assertion.Extensions.Framework.PropertyValidations;

namespace TestMonkey.Assertion.Extensions.Engine.PropertyRuleSet
{
    public abstract class PropertyMatchingStrategy:PropertyStrategy
    {

        public abstract List<ValidationResult> Validate(PropertyInfo propertyInfo, object actualObj, object expectedObj,
             string messagePropertyPrefix = null);


        


    }
}
