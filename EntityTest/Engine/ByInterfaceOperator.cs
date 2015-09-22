using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestMonkey.Assertion.Constraints;
using TestMonkey.Assertion.Constraints.Operators;
using TestMonkeys.EntityTest.Matchers;

namespace TestMonkeys.EntityTest.Engine
{
    public class ByInterfaceOperator:SelfResolvingOperator
    {
        private readonly Type type;

        public ByInterfaceOperator(Type type)
        {
            this.type = type;
        }

        public override void Reduce(ConstraintBuilder.ConstraintStack stack)
        {
            var top = stack.Pop();
            ((EntityComparisonMatcher) top).ByInterface(type);
            stack.Push(top);
        }
    }
}
