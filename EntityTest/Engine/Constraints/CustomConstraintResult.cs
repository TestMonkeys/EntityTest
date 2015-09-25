using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework.Constraints;

namespace TestMonkeys.EntityTest.Engine.Constraints
{
    public class CustomConstraintResult:ConstraintResult
    {
        private readonly IConstraint constraint;

        public CustomConstraintResult(IConstraint constraint, object actualValue) : base(constraint, actualValue)
        {
            this.constraint = constraint;
        }

        public CustomConstraintResult(IConstraint constraint, object actualValue, ConstraintStatus status) : base(constraint, actualValue, status)
        {
            this.constraint = constraint;
        }

        public CustomConstraintResult(IConstraint constraint, object actualValue, bool isSuccess) : base(constraint, actualValue, isSuccess)
        {
            this.constraint = constraint;
        }

        public StringBuilder MessageBuilder { get; set; }

        public override void WriteActualValueTo(MessageWriter writer)
        {
            //base.WriteActualValueTo(writer);
        }

        public override void WriteMessageTo(NUnit.Framework.Constraints.MessageWriter writer)
        {
            writer.WriteMessageLine(constraint.Description);
            writer.WriteMessageLine(3, MessageBuilder.ToString());
        }
    }
}
