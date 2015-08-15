using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMonkey.Assertion.Extensions.Engine.HumanReadableMessages
{
    public class Describe
    {

        public static string Object(object obj){
            return new ObjectInspector(obj).Describe();
        }

    }
}
