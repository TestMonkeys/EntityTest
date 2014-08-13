// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints.Operators
{
    /// <summary>
    ///     Operator used to test for the presence of a named Property
    ///     on an object and optionally apply further tests to the
    ///     value of that property.
    /// </summary>
    public class PropOperator : SelfResolvingOperator
    {
        private readonly string name;

        /// <summary>
        ///     Constructs a PropOperator for a particular named property
        /// </summary>
        public PropOperator(string name)
        {
            this.name = name;

            // Prop stacks on anything and allows only 
            // prefix operators to stack on it.
            left_precedence = right_precedence = 1;
        }

        /// <summary>
        ///     Gets the name of the property to which the operator applies
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        ///     Reduce produces a constraint from the operator and
        ///     any arguments. It takes the arguments from the constraint
        ///     stack and pushes the resulting constraint on it.
        /// </summary>
        /// <param name="stack"></param>
        public override void Reduce(ConstraintBuilder.ConstraintStack stack)
        {
            if (RightContext == null || RightContext is BinaryOperator)
                stack.Push(new PropertyExistsConstraint(name));
            else
                stack.Push(new PropertyConstraint(name, stack.Pop()));
        }
    }
}