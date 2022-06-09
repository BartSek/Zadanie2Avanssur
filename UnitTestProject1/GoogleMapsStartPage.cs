using OpenQA.Selenium;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TravelTimeCheck
{
    internal class GoogleMapsStartPage
    {
        private IWebDriver Driver { get; set; }

        public GoogleMapsStartPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public bool IsVisible {
            get
            {
                return Driver.Title.Contains("Mapy Google");
            }
            internal set { }
        }

        public int TravelTime {
            get
            {   
                var timeText = Driver.FindElement(By.XPath("//div[contains(text(),' min')]")).GetDomProperty("innerText");
                timeText = timeText.Remove(2);
                return int.Parse(timeText);
            } 
            internal set { }
        }
        public double TravelDistance
        {
            get
            {
                double distance;
                var distanceText = Driver.FindElement(By.XPath("//div[contains(text(),' km')]")).GetDomProperty("innerText");
                distanceText = distanceText.Remove(3).Replace(',', '.');
                double.TryParse(distanceText, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out distance);
                return distance;
            }
            internal set { }
        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://www.google.com/maps/");
        }

        internal void FillOutDestinationAndSubmit(string destination)
        {
            Driver.FindElement(By.Id("searchboxinput")).SendKeys(destination);
            Driver.FindElement(By.Id("searchboxinput")).SendKeys(Keys.Enter);
        }

        internal void SelectWalkTravel()
        {
            Driver.FindElement(By.XPath("//img[contains(@aria-label,'Pieszo')]")).Click();
        }

        internal void SelectBikeTravel()
        {
            Driver.FindElement(By.XPath("//img[contains(@aria-label,'Na rowerze')]")).Click();
        }

        internal void Directions()
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Driver.FindElement(By.XPath("//button[@data-value='Wyznacz trasę']")).Click();
            
        }

        internal void FillOutStartingPoint(string startingPoint)
        {
            //Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Driver.FindElement(By.XPath("//input[contains(@placeholder,'Wybierz')]")).SendKeys(startingPoint);
            //Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Driver.FindElement(By.XPath("//input[contains(@placeholder,'Wybierz')]")).SendKeys(Keys.Enter);
        }
    }
}