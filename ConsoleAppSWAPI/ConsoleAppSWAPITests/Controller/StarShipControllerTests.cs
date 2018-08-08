using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleAppSWAPI.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppSWAPI.Model.DTO;
using ConsoleAppSWAPI.Model.Class;

namespace ConsoleAppSWAPI.Controller.Tests
{
    [TestClass()]
    public class StarShipControllerTests
    {

        [TestMethod]
        // validating a positive number
        public void ValidPositiveInputReturnsTrue()
        {
            string mglt = "1000000";

            Assert.IsTrue(InputValidate.mgltTypeValidate(mglt));
        }

        [TestMethod]
        // validating a negative number
        public void ValidNegativeInputReturnsFalse()
        {
            string mglt = "-1";

            Assert.IsFalse(InputValidate.mgltTypeValidate(mglt));
        }

        [TestMethod()]
        // validating if it's a number
        public void ValidStringInputReturnsFalse()
        {
            string mglt = "string123";

            Assert.IsFalse(InputValidate.mgltTypeValidate(mglt));
        }

        [TestMethod]
        public void CalculateNeededStopsDeathStarShip()
        {
            string mglt = "1000000";
            string resupply = (Convert.ToInt64(mglt) / (10 * 26304)).ToString();

            StarShip starShip = new StarShip()
            {
                Consumables = "3 years",
                Manufacturer = "Imperial Department of Military Research, Sienar Fleet Systems",
                Model = "DS-1 Orbital Battle Station",
                MGLT = "10",
                Name = "Death Star",
                Url = "http://swapi.co/api/starships/9/"
            };
            StarShipController starshipController = new StarShipController();

            Assert.AreEqual(resupply, starshipController.calculateSupply(starShip,mglt));
        }

    }
}