using NUnitLite;
using System;
using System.Reflection;
using NUnit.Common;
using Microsoft.Extensions.Configuration;


namespace portal.tests
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var mongoDbUser = Environment.GetEnvironmentVariable("MONGODBUSER"); 
            var mongoDbPassword = Environment.GetEnvironmentVariable("MONGODBPASSWORD"); 
            var mongoDbHostname = Environment.GetEnvironmentVariable("MONGODBHOSTNAME"); 
            var mongoDbConnectionString = Environment.GetEnvironmentVariable("MONGODBCONNECTIONSTRING"); 
            Console.WriteLine($"mongoDbUser: {mongoDbUser}");
            Console.WriteLine($"mongoDbPassword: {mongoDbPassword}");
            Console.WriteLine($"mongoDbHostname: {mongoDbHostname}");
            Console.WriteLine($"mongoDbConnectionString: {mongoDbConnectionString}"); 
            // var test = new AutoRun(typeof(Program).GetTypeInfo().Assembly)
            //     .Execute(args, new ExtendedTextWrapper(Console.Out), Console.In);
            // MongoDb_IntegrationTests tests = new MongoDb_IntegrationTests(args);
            // // tests.ConnectionTest();
            // if (tests.RunTests())
            // {
            //     Console.WriteLine("integration tests passed as all vars were not null");
            //     return 0;
            // }
            // else
            // {
            //     throw new Exception("An integration Test failed");
            // }
            return 0;
        }
    }
}