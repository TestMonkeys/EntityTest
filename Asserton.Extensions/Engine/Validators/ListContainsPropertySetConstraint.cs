using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TestMonkey.Assertion.Extensions.Engine.Constraints;
using TestMonkey.Assertion.Extensions.Engine.HumanReadableMessages;

namespace TestMonkey.Assertion.Extensions.Engine.Validators
{
    public class ListContainsPropertySetConstraint:CustomMessageConstraint
    {
        private readonly object expected;
        private readonly OnListContainsFailure actionOnFailure;

        public ListContainsPropertySetConstraint(object expected, OnListContainsFailure actionOnFailure)
        {
            this.expected = expected;
            this.actionOnFailure = actionOnFailure;
        }
        protected override string DescriptionLine
        {
            get { return "Expected object not found in list"; }
        }

        public override bool Matches(object actual)
        {
            if (!(actual is IList))
                throw new InvalidOperationException("Actual object should be a list");
            var actualAndDiff=new Dictionary<object, List<string>>();
            foreach (object actualItem in ((IList)actual))
            {
                var propertyValidator = new PropertySetValidator(expected);
                if (propertyValidator.Matches(actualItem))
                    return true;
                actualAndDiff.Add(actualItem,propertyValidator.Differences);

            }
            switch (actionOnFailure)
            {
                case OnListContainsFailure.DoNothing:
                    break;
                case OnListContainsFailure.DisplayExpectedAndActualList:
                    messageBuilder.Append("Expected item:").Append(Environment.NewLine);
                    messageBuilder.Append(Describe.Object(expected)).Append(Environment.NewLine);
                    messageBuilder.Append(Environment.NewLine);
                    int position = 0;
                    messageBuilder.Append("Actual List:").Append(Environment.NewLine);
                    foreach(var item in ((IList)actual)){
                        messageBuilder.Append("Item[").Append(position).Append("]").Append(Environment.NewLine);
                        messageBuilder.Append(Describe.Object(item)).Append(Environment.NewLine);
                        position++;
                    }

                    break;
                case OnListContainsFailure.DisplayClosestMatch:
                    var biggestDifference=actualAndDiff.Max(x => x.Value.Count);
                    var smallestDifference = actualAndDiff.Min(x => x.Value.Count);
                    if (biggestDifference!=smallestDifference || actualAndDiff.Count==1)
                    {
                        var closestMatches = actualAndDiff.Where(x => x.Value.Count == smallestDifference);
                        messageBuilder.Append("Expected item:").Append(Environment.NewLine);
                        messageBuilder.Append(Describe.Object(expected)).Append(Environment.NewLine);
                        messageBuilder.Append(Environment.NewLine);
                        messageBuilder.Append("Closest matches:").Append(Environment.NewLine);
                        foreach (var closestMatch in closestMatches)
                        {
                            messageBuilder.Append("Item Description:").Append(Environment.NewLine);
                            messageBuilder.Append(Describe.Object(closestMatch.Key)).Append(Environment.NewLine);
                            messageBuilder.Append("Diff:").Append(Environment.NewLine);
                            foreach (var diff in closestMatch.Value)
                            {
                                messageBuilder.AppendLine(diff);
                            }
                            messageBuilder.Append(Environment.NewLine);
                        }
                    }
                    break;
            }

            return false;
        }
    }
}
