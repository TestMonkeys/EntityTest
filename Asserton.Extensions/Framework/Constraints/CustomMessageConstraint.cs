using System.Text;

namespace TestMonkey.Assertion.Extensions.Framework.Constraints
{
#if NUnit
    public abstract class CustomMessageConstraint : NUnit.Framework.Constraints.Constraint
#else
    public abstract class CustomMessageConstraint : Assertion.Constraints.Constraint
#endif
    {
        protected readonly StringBuilder messageBuilder;

        protected CustomMessageConstraint()
        {
            messageBuilder = new StringBuilder();
        }

        protected abstract string DescriptionLine { get; }

#if NUnit
        public override void WriteDescriptionTo(NUnit.Framework.Constraints.MessageWriter writer)
        {
            writer.WriteMessageLine(DescriptionLine);
        }

        public override void WriteMessageTo(NUnit.Framework.Constraints.MessageWriter writer)
        {
            WriteDescriptionTo(writer);
            writer.WriteMessageLine(3, messageBuilder.ToString());
        }
#else
        public override void WriteDescriptionTo(Assertion.Constraints.MessageWriter writer)
        {
            writer.WriteMessageLine(DescriptionLine);
        }

        public override void WriteMessageTo(Assertion.Constraints.MessageWriter writer)
        {
            WriteDescriptionTo(writer);
            writer.WriteMessageLine(0, messageBuilder.ToString());
        }
#endif
    }
}