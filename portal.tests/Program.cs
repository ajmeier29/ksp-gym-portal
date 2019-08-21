using NUnitLite;
using System;
using System.Reflection;
using NUnit.Common;


namespace portal.tests
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Console.WriteLine("Hellow world new test");
            return new AutoRun(typeof(Program).GetTypeInfo().Assembly)
                .Execute(args, new ExtendedTextWrapper(Console.Out), Console.In);
        }
    }
}