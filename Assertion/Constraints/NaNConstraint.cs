// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     NaNConstraint tests that the internalActual value is a double or float NaN
    /// </summary>
    public class NaNConstraint : Constraint
    {
        /// <summary>
        ///     Test that the internalActual value is an NaN
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public override bool Matches(object actual)
        {
            internalActual = actual;

            return actual is double && double.IsNaN((double) actual)
                   || actual is float && float.IsNaN((float) actual);
        }

        /// <summary>
        ///     Write the constraint description to a specified writer
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("NaN");
        }
    }
}