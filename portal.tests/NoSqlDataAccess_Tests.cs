using System;
using System.Collections.Generic;
using System.IO;
using portal.webapi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using NUnit.Framework;

namespace portal.tests
{
    public class NoSqlDataAcessTests
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
            Assert.True(_configuration["TESTDBUSER"].Equals("test-user"));
            // NoSqlDataAccess noSqlDataAccess = new NoSqlDataAccess(_configuration);
            // bool result = noSqlDataAccess.Connect();
            //Assert.Pass();
        }
        [Test]
        public void TestEnvVarFail()
        {
            Assert.False(_configuration["TESTDBUSER"].Equals("test-usersss"));
            // NoSqlDataAccess noSqlDataAccess = new NoSqlDataAccess(_configuration);
            // bool result = noSqlDataAccess.Connect();
            //Assert.Pass();
        }
    }
}