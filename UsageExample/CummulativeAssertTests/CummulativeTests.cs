using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestMonkey.Assertion;
using TestMonkey.Assertion.Extensions;

namespace UsageExample.CummulativeAssertTests
{
    [TestClass]
    public class CummulativeTests
    {

        [TestMethod]
        public void CummulativeAssert_NoFailures1()
        {
            CummulativeAssert.That(true);
            CummulativeAssert.ShowFailures();
            Console.WriteLine(new StackTrace(true));
        }

        [TestMethod]
        public void CummulativeAssert_MultipleFailures1()
        {
            CummulativeAssert.That(false);
            CummulativeAssert.That(1, Is.EqualTo(2), "int equality");
            throw new Exception("interrupt");
            CummulativeAssert.That("true", Is.EqualTo("false"), "string equality");
            CummulativeAssert.ShowFailures();

        }

        [TestMethod]
        public void CummulativeAssert_MethodCalling1()
        {
            CummulativeAssert.That(false);
            CummulativeAssert.That(1, Is.EqualTo(2), "int equality");
            CummulativeAssert.That("true", Is.EqualTo("false"), "string equality");
        }

        [TestMethod]
        public void CummulativeAssert_MethodCalling2()
        {
            CummulativeAssert.That(false);
            CummulativeAssert.That(1, Is.EqualTo(2), "int equality");
            CummulativeAssert.That("true", Is.EqualTo("false"), "string equality");
            CummulativeAssert.ShowFailures();
        }

        [TestMethod]
        public void CummulativeAssert_MethodCalling()
        {
            CummulativeAssert_MethodCalling1();
            CummulativeAssert_MethodCalling2();
        }
    }
}