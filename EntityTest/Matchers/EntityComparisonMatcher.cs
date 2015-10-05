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
using NUnit.Framework.Constraints;
using TestMonkeys.EntityTest.Engine.Constraints;
using TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Matching;

namespace TestMonkeys.EntityTest.Matchers
{
    public class EntityComparisonMatcher : CustomMessageConstraint
    {
        private readonly object internalExpected;
        private bool isMatch;
        private Type validationType;

        internal EntityComparisonMatcher(object expected)
        {
            if (expected == null)
                throw new ArgumentNullException(nameof(expected), "Expected can't be null");
            internalExpected = expected;
            isMatch = true;
            validationType = expected.GetType();
            Differences = new List<string>();
        }

        public EntityComparisonMatcher(object expected, Type validationType) : this(expected)
        {
            this.validationType = validationType;
        }

        internal List<string> Differences { get; set; }
        protected override string DescriptionLine => "Property Set is not equal";
        public override string Description => "Property Set is not equal";

        public EntityComparisonMatcher ByInterface(Type byInterface)
        {
            validationType = byInterface;
            return this;
        }

        public bool Matches(object actual)
        {
            var matcher = new EntityMatchingStrategy();
            var result = matcher.Compare(actual, internalExpected, validationType);
            isMatch = result.All(x => x.Success);
            foreach (var mismatch in result.Where(mismatch => !mismatch.Success))
            {
                messageList.Add(mismatch.GetMessage());
            }
            return isMatch;
        }

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            var cresult = new CustomConstraintResult(this, actual) {Status = ConstraintStatus.Error};

            var matcher = new EntityMatchingStrategy();
            var result = matcher.Compare(actual, internalExpected, validationType);
            cresult.Status = result.All(x => x.Success) ? ConstraintStatus.Success : ConstraintStatus.Failure;

            foreach (var mismatch in result.Where(mismatch => !mismatch.Success))
            {
                messageList.Add(mismatch.GetMessage());
            }
            cresult.MessageList = messageList;
            return cresult;
        }
    }
}