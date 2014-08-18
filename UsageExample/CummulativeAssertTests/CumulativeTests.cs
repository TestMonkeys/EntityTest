using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestMonkey.Assertion;
using TestMonkey.Assertion.Extensions;

namespace UsageExample.CummulativeAssertTests
{
    [TestClass]
    public class CumulativeTests
    {

        [TestMethod]
        public void CumulativeAssert_NoFailures1()
        {
            CumulativeAssert.That(true);
            CumulativeAssert.ShowFailures();
            Console.WriteLine(new StackTrace(true));
        }

        [TestMethod]
        public void CumulativeAssert_MultipleFailuresExample()
        {
            CumulativeAssert.That(false,"Boolean failure");
            CumulativeAssert.That(true, "Boolean passed");
            CumulativeAssert.That(1, Is.EqualTo(2), "int equality");
            CumulativeAssert.That("true", Is.EqualTo("false"), "string equality");
            CumulativeAssert.ShowFailures();

        }

        [TestMethod]
        public void CumulativeAssert_MethodCalling1()
        {
            CumulativeAssert.That(false);
            CumulativeAssert.That(1, Is.EqualTo(2), "int equality");
            CumulativeAssert.That("true", Is.EqualTo("false"), "string equality");
        }

        [TestMethod]
        public void CumulativeAssert_MethodCalling2()
        {
            CumulativeAssert.That(false);
            CumulativeAssert.That(1, Is.EqualTo(2), "int equality");
            CumulativeAssert.That("true", Is.EqualTo("false"), "string equality");
            CumulativeAssert.ShowFailures();
        }

        [TestMethod]
        public void CumulativeAssert_MethodCalling()
        {
            CumulativeAssert_MethodCalling1();
            CumulativeAssert_MethodCalling2();
        }
    }
}