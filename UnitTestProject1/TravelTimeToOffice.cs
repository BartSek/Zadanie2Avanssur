using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TravelTimeCheck
{
    [TestClass]
    public class TravelTimeToOffice
    {

        private IWebDriver Driver { get; set; }

        
        public void TestBase(string address1, string address2, int expectedTravelTimeWalk, int expectedTravelDistanceWalk, int expectedTravelTimeBike, int expectedTravelDistanceBike)
        {
            Driver = GetChromeDriver();
            var googleMapsStartPage = new GoogleMapsStartPage(Driver);
            googleMapsStartPage.GoTo();
            Assert.IsTrue(googleMapsStartPage.IsVisible);

            googleMapsStartPage.FillOutDestinationAndSubmit(address1);
            googleMapsStartPage.Directions();
            googleMapsStartPage.FillOutStartingPoint(address2);
            googleMapsStartPage.SelectWalkTravel();
            Assert.IsTrue(googleMapsStartPage.TravelTime < expectedTravelTimeWalk);
            Assert.IsTrue(googleMapsStartPage.TravelDistance < expectedTravelDistanceWalk);
            googleMapsStartPage.SelectBikeTravel();
            Assert.IsTrue(googleMapsStartPage.TravelTime < expectedTravelTimeBike);
            Assert.IsTrue(googleMapsStartPage.TravelDistance < expectedTravelDistanceBike);

        }
        [TestMethod]
        public void TestMethod1()
        {
            TestBase("Chłodna 51, Warszawa", "Plac Defilad 1, Warszawa", 40, 3, 15, 3);

        }
        [TestMethod]
        public void TestMethod2()
        {
            TestBase("Plac Defilad 1, Warszawa", "Chłodna 51, Warszawa", 40, 3, 15, 3);

        }

        private IWebDriver GetChromeDriver()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return new ChromeDriver(outPutDirectory);
        }
    }
}
