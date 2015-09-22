#region Copyright

// Copyright 2015 Constantin Pascal
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using TestMonkeys.EntityTest.Engine.Constraints;
using TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Matching;

namespace TestMonkeys.EntityTest.Engine.Validators
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