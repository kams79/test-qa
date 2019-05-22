using System;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ConsoleApp1
{
    public class Driver
    {
        private readonly IWebDriver _driver;

        private readonly string _path, _timestamp;
        
        public Driver()
        {
            _driver = new ChromeDriver();
            _path = AppDomain.CurrentDomain.BaseDirectory;
            _timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm");
        }
        

        public void Load(string link)
        {
            _driver.Navigate().GoToUrl(link);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            try
            {
                var load = _driver.FindElement(By.CssSelector("div.flex-item.ng-star-inserted.findflights"))
                    .GetAttribute("innerText");
                if (load != null)
                {
                    _driver.FindElement(By.CssSelector("div.flex-item.ng-star-inserted.findflights")).Click();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Bisa Langsung Isi");
            }
            finally
            {
                _driver.FindElement(By.XPath("//*[@id='wcaMainContent']")).Click();
            }

        }

        public void GetOrigin(string origin)
        {
            _driver.FindElement(By.XPath("//*[@id='home-origin-autocomplete-heatmap']")).SendKeys(origin);
            _driver.FindElement(By.XPath("//*[@id='wcaMainContent']/div[2]/form/div[1]/div[1]/div/div[1]/airasia-destination-autocomplete/div/div/div")).Click();

        }

        public void GetDestination(string destination)
        {
            _driver.FindElement(By.XPath("//*[@id='home-destination-autocomplete-heatmap']")).SendKeys(destination);
            _driver.FindElement(By.XPath("//*[@id='home-destination-autocomplete-heatmaplist-0']")).Click();

        }
        
        
        public void GetDateSingle()
        {
            _driver.FindElement(By.XPath("//*[@id='wcaMainContent']/div[2]/form/div[1]/div[2]/airasia-airasia-calendar/div[1]/div[2]/div[1]/div/div[1]/div[2]")).Click();
            _driver.FindElement(By.XPath("//*[@id='home-depart-date-heatmap']")).Clear();
            _driver.FindElement(By.XPath("//*[@id='home-depart-date-heatmap']")).SendKeys("01/08/2019");
            _driver.FindElement(By.XPath("//*[@id='wcaMainContent']/div[2]/form/div[1]/div[2]/airasia-airasia-calendar/div[1]/div[2]/div[3]/div/button")).Click();
            _driver.FindElement(By.XPath("//*[@id='home-flight-search-airasia-button-inner-button-select-flight-heatmap']")).Click();        }

        public void GetDeptArrTime()
        {
            
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var departime = _driver.FindElement(By.XPath("//*[@id='depart-desc-0']")).GetAttribute("textContent");
            var arrivtime = _driver.FindElement(By.XPath("//*[@id='arrival-desc-0']")).GetAttribute("textContent");
            var tanggal = _driver.FindElement(By.XPath("//*[@id='airasia-fare-calendar-div-calendar-date-0-7-heatmap']")).FindElement(By.CssSelector("div.date")).GetAttribute("textContent");
            
            using (StreamWriter writer = new StreamWriter(_path + _timestamp +".txt", true))
            {
                writer.WriteLine("==========================================");
                writer.WriteLine("{0}, {1}",departime, arrivtime);
                writer.WriteLine("Tunjukan Tanggal - {0}", tanggal);
                writer.WriteLine("==========================================");
            }
            
            Console.WriteLine("==========================================");
            Console.WriteLine("{0}, {1}",departime, arrivtime);
            Console.WriteLine("Tunjukan Tanggal - {0}", tanggal);
            Console.WriteLine("==========================================");
            Thread.Sleep(5000);
        }

        public void ScreenShoot()
        {
            var ss = ((ITakesScreenshot)_driver).GetScreenshot();
            ss.SaveAsFile(_path + _timestamp +"screenshot.png", ScreenshotImageFormat.Png);
        }

        public void StopDrv()
        {
            _driver.Quit();
        }
    }
}