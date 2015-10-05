using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace EntityTest.Test.PropertySetValidatorTests
{
    public class MessageCheck
    {
        private readonly List<string> lines;

        public MessageCheck(string description = null)
        {
            lines = new List<string>();
            if (!string.IsNullOrEmpty(description))
                lines.Add(description);
        }

        public void AddPropertyLine(string expected, string actual, string propertyName)
        {
            lines.Add(string.Format("Expected <{0}> but found <{1}> for <{2}>",
                                    expected, actual, propertyName));
        }

        public void AddObjectLine(string expected, string actual, string propertyName)
        {
            lines.Add(string.Format("Expected <{0}> but found <{1}> for <{2}>",
                                    expected, actual, propertyName));
        }

        public void AddObjectLine(string expected, string actual)
        {
            lines.Add(string.Format("Expected <{0}> but found <{1}>",
                                    expected, actual));
        }

        public void Check(Exception e)
        {
            string message = e.Message;
            List<string> actual =
                message.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
            Assert.That(actual[0].Trim(), Is.EqualTo(lines[0]));
            foreach (var expected in lines)
            {
                Assert.IsTrue(actual.Any(x=>x.Trim().Contains(expected)),
                              "did not find " + expected);
                actual.Remove(actual.First(x => x.Trim().Contains(expected)));
            }
            Assert.That(actual.Any(), Is.False,
                        "actual lines that were not expected:" + string.Join(Environment.NewLine, actual));
        }
    }
}