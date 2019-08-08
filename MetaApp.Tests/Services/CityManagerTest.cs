using System;
using System.Collections.Generic;
using System.Text;
using MetaApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetaApp.Tests.Services
{
    [TestClass]
    public class CityManagerTest
    {
        [TestMethod]
        public void Validate_Null()
        {
            string[] args = null;
            var result = new CityManager(args).Validate();

            Assert.AreEqual("Parameters cannot be empty.", result);
        }

        [TestMethod]
        public void Validate_Empty()
        {
            string[] args = new string[0];
            var result = new CityManager(args).Validate();

            Assert.AreEqual("Parameters cannot be empty.", result);
        }

        [TestMethod]
        public void Validate_WrongParameters()
        {
            string[] args = new string[] {"some_param"};
            var result = new CityManager(args).Validate();

            Assert.AreEqual("Wrong parameters.", result);
        }

        [TestMethod]
        public void Validate_OnlyWeather()
        {
            string[] args = new string[] { "weather" };
            var result = new CityManager(args).Validate();

            Assert.AreEqual("Wrong parameters.", result);
        }

        [TestMethod]
        public void Validate_WeatherAndWrongCity()
        {
            string[] args = new string[] { "weather", "-city" };
            var result = new CityManager(args).Validate();

            Assert.AreEqual("Wrong parameters.", result);
        }

        [TestMethod]
        public void Validate_NoCityNames()
        {
            string[] args = new string[] { "weather", "--city" };
            var result = new CityManager(args).Validate();

            Assert.AreEqual("Please provide a city list", result);
        }

        [TestMethod]
        public void Validate_OneCity()
        {
            string[] args = new string[] { "weather", "--city", "Vilnius" };
            var result = new CityManager(args).Validate();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Validate_TwoCities()
        {
            string[] args = new string[] { "weather", "--city", "Vilnius", "Kyiv" };
            var result = new CityManager(args).Validate();

            Assert.IsNull(result);
        }
    }
}
