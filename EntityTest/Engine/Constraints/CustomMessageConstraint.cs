using System.Text;
using TestMonkey.Assertion.Constraints;

namespace TestMonkey.EntityTest.Engine.Constraints
{
#if NUnit
    public abstract class CustomMessageConstraint : NUnit.Framework.Constraints.Constraint
#else
    public abstract class CustomMessageConstraint : Constraint
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
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WriteMessageLine(DescriptionLine);
        }

        public override void WriteMessageTo(MessageWriter writer)
        {
            WriteDescriptionTo(writer);
            writer.WriteMessageLine(0, messageBuilder.ToString());
        }
#endif
    }
}