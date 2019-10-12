using MongoDB;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Moq;

namespace portal.tests
{
    public class RepositoryTests
    {

        private IConfiguration _configuration { get; set; }

        [SetUp]
        public void Setup()
        {
            _configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
        }

        [Test]
        public void TestEnvVarPass()
        {

            // Assert.True(_configuration["TESTDBUSER"].Equals("test-user"));
            // NoSqlDataAccess noSqlDataAccess = new NoSqlDataAccess(_configuration);
            // bool result = noSqlDataAccess.Connect();
            //Assert.Pass();
        }
        [Test]
        public void TestEnvVarFail()
        {
            // Assert.False(_configuration["TESTDBUSER"].Equals("test-usersss"));
            // NoSqlDataAccess noSqlDataAccess = new NoSqlDataAccess(_configuration);
            // bool result = noSqlDataAccess.Connect();
            //Assert.Pass();
        }
    }
}