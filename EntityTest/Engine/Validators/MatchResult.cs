using TestMonkey.EntityTest.Engine.PropertyRuleSet;

namespace TestMonkey.EntityTest.Engine.Validators
{
    public class MatchResult
    {
        public bool Success { get; set; }

        public object Expected { get; set; }
        public object Actual { get; set; }
        public ParentContext Parent { get; set; }
        public string PropertyName { get; set; }

        public string GetMessage()
        {
            
            if (string.IsNullOrEmpty(PropertyName) && Parent==null)
                return string.Format("Expected <{0}> but found <{1}>", Expected, Actual);
            return string.Format("Expected <{0}> but found <{1}> for <{2}{3}>", Expected, Actual, Parent,
                PropertyName ?? string.Empty);
        }
    }
}