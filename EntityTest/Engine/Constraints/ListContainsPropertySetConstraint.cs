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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;
using TestMonkeys.EntityTest.Engine.Constraints.Helpers;
using TestMonkeys.EntityTest.Engine.HumanReadableMessages;
using TestMonkeys.EntityTest.Engine.Validators;

namespace TestMonkeys.EntityTest.Engine.Constraints
{
    public class ListContainsPropertySetConstraint : CustomMessageConstraint
    {
        private readonly OnListContainsFailure actionOnFailure;
        private readonly object expected;

        public ListContainsPropertySetConstraint(object expected, OnListContainsFailure actionOnFailure)
        {
            this.expected = expected;
            this.actionOnFailure = actionOnFailure;
        }

        protected override string DescriptionLine => "Expected object not found in list";

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            var cresult = new CustomConstraintResult(this, actual) {Status = ConstraintStatus.Error};
            if (!(actual is IList))
                throw new InvalidOperationException("Actual object should be a list");
            var actualAndDiff = new Dictionary<object, List<string>>();
            foreach (var actualItem in ((IList) actual))
            {
                var propertyValidator = new EntityEqualityConstraint(expected);
                if (propertyValidator.Matches(actualItem))
                {
                    cresult.Status = ConstraintStatus.Success;
                    return cresult;
                }
                actualAndDiff.Add(actualItem, propertyValidator.Differences);
            }
            switch (actionOnFailure)
            {
                case OnListContainsFailure.DoNothing:
                    break;
                case OnListContainsFailure.DisplayExpectedAndActualList:
                    messageList.Add("Expected item:{0}");
                    messageList.Add(Describe.Object(expected));
                    var position = 0;
                    messageList.Add("Actual List:");
                    foreach (var item in ((IList) actual))
                    {
                        messageList.Add($"Item[{position}]");
                        messageList.Add(Describe.Object(item));
                        position++;
                    }

                    break;
                case OnListContainsFailure.DisplayClosestMatch:
                    var biggestDifference = actualAndDiff.Max(x => x.Value.Count);
                    var smallestDifference = actualAndDiff.Min(x => x.Value.Count);
                    if (biggestDifference != smallestDifference || actualAndDiff.Count == 1)
                    {
                        var closestMatches = actualAndDiff.Where(x => x.Value.Count == smallestDifference);
                        messageList.Add("Expected item:");
                        messageList.Add(Describe.Object(expected));

                        messageList.Add("Closest matches:");
                        foreach (var closestMatch in closestMatches)
                        {
                            messageList.Add("Item Description:");
                            messageList.Add(Describe.Object(closestMatch.Key));
                            messageList.Add("Diff:");
                            foreach (var diff in closestMatch.Value)
                            {
                                messageList.Add(diff);
                            }
                        }
                    }
                    break;
            }

            cresult.MessageList = messageList;
            cresult.Status = ConstraintStatus.Failure;
            return cresult;
        }
    }
}