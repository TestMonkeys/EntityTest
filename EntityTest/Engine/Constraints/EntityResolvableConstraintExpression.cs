

using System;
using TestMonkey.Assertion.Constraints;

namespace TestMonkeys.EntityTest.Engine.Constraints
{
    public class EntityResolvableConstraintExpression:ResolvableConstraintExpression
    {
        public ResolvableConstraintExpression ByInterface(Type byInterface)
        {
            return Append(new ByInterfaceOperator(byInterface));
        }

    }
}
