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
            // var test = new AutoRun(typeof(Program).GetTypeInfo().Assembly)
            //     .Execute(args, new ExtendedTextWrapper(Console.Out), Console.In);
            MongoDb_IntegrationTests tests = new MongoDb_IntegrationTests(args);
            // tests.ConnectionTest();
            if (tests.RunTests()){
                Console.WriteLine("integration tests passed as all vars were not null");
                return 0;
            } else {
                throw new Exception("An integration Test failed");
            }
            
        }
    }
}