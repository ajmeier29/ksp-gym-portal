using NUnit.Framework;
using ksp_portal.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            ValuesController vc = new ValuesController();
            var test = vc.Get();

            Assert.Pass();
        }
    }
}