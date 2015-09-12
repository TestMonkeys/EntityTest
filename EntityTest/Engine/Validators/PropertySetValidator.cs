using System;
using System.Collections.Generic;
using System.Linq;
using TestMonkey.EntityTest.Engine.Constraints;
using TestMonkey.EntityTest.Engine.PropertyRuleSet.Strategies.Matching;

namespace TestMonkey.EntityTest.Engine.Validators
{
    public class PropertySetValidator : CustomMessageConstraint
    {
        private readonly object internalExpected;
        private readonly Type validationType;
        private bool isMatch;

        internal PropertySetValidator(object expected)
        {
            if (expected == null)
                throw new ArgumentNullException(nameof(expected), "Expected can't be null");
            internalExpected = expected;
            isMatch = true;
            validationType = expected.GetType();
            Differences = new List<string>();
        }

        public PropertySetValidator(object expected, Type validationType) : this(expected)
        {
            this.validationType = validationType;
        }

        internal List<string> Differences { get; set; }

        protected override string DescriptionLine => "Property Set is not equal";

        public override bool Matches(object actual)
        {
            var matcher = new EntityMatcher();
            var result = matcher.Compare(actual, internalExpected, validationType);
            isMatch = result.All(x => x.Success);
            foreach (var mismatch in result.Where(mismatch => !mismatch.Success))
            {
                messageBuilder.AppendFormat(mismatch.GetMessage()).Append(Environment.NewLine);
            }
            return isMatch;
        }
    }
}