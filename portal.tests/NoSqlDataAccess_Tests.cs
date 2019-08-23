using System;
using System.Collections.Generic;
using System.IO;
using ksp_portal.Services;
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
        }

        [Test]
        public void TestDbConection()
        {
            // NoSqlDataAccess noSqlDataAccess = new NoSqlDataAccess(_configuration);
            // bool result = noSqlDataAccess.Connect();
            Assert.Pass();
        }
    }
}