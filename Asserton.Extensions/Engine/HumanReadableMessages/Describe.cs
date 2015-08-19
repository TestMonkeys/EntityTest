namespace TestMonkey.Assertion.Extensions.Engine.HumanReadableMessages
{
    public class Describe
    {

        public static string Object(object obj){
            return new ObjectInspector(obj).Describe();
        }

    }
}
