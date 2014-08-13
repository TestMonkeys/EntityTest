// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     AndConstraint succeeds only if both members succeed.
    /// </summary>
    public class AndConstraint : BinaryConstraint
    {
        private FailurePoint failurePoint;

        /// <summary>
        ///     Create an AndConstraint from two other constraints
        /// </summary>
        /// <param name="left">The first constraint</param>
        /// <param name="right">The second constraint</param>
        public AndConstraint(Constraint left, Constraint right) : base(left, right)
        {
        }

        /// <summary>
        ///     Apply both member constraints to an internalActual value, succeeding
        ///     succeeding only if both of them succeed.
        /// </summary>
        /// <param name="actual">The internalActual value</param>
        /// <returns>True if the constraints both succeeded</returns>
        public override bool Matches(object actual)
        {
            internalActual = actual;

            failurePoint = left.Matches(actual)
                               ? right.Matches(actual)
                                     ? FailurePoint.None
                                     : FailurePoint.Right
                               : FailurePoint.Left;

            return failurePoint == FailurePoint.None;
        }

        /// <summary>
        ///     Write a description for this contraint to a MessageWriter
        /// </summary>
        /// <param name="writer">The MessageWriter to receive the description</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            left.WriteDescriptionTo(writer);
            writer.WriteConnector("and");
            right.WriteDescriptionTo(writer);
        }

        /// <summary>
        ///     Write the internalActual value for a failing constraint test to a
        ///     MessageWriter. The default implementation simply writes
        ///     the raw value of internalActual, leaving it to the writer to
        ///     perform any formatting.
        /// </summary>
        /// <param name="writer">The writer on which the internalActual value is displayed</param>
        public override void WriteActualValueTo(MessageWriter writer)
        {
            switch (failurePoint)
            {
                case FailurePoint.Left:
                    left.WriteActualValueTo(writer);
                    break;
                case FailurePoint.Right:
                    right.WriteActualValueTo(writer);
                    break;
                default:
                    base.WriteActualValueTo(writer);
                    break;
            }
        }

        private enum FailurePoint
        {
            None,
            Left,
            Right
        };
    }
}