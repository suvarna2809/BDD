using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BDD_Framework_POC.PageObjects
{
   public class HomePage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.
                FromSeconds(30000));
            PageFactory.InitElements(driver, this);
        }


        [FindsBy(How = How.XPath, Using = "//title[contains(text(),'Find a Flight: Mercury Tours: ')]")]
        private IWebElement application_title { get; set; }

        [FindsBy(How = How.CssSelector, Using = "img[src='/images/masts/mast_flightfinder.gif']")]
        private IWebElement application { get; set; }

        public string HomePageDisplayed()
        {
            Thread.Sleep(10000);
            return driver.Title;
            //return application.Displayed;
        }     
    }
}
