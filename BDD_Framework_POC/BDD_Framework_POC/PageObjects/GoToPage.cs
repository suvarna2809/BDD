using BDD_Framework_POC.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDD_Framework_POC.PageObjects
{
   public class GoToPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public GoToPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30000));
            PageFactory.InitElements(driver, this);
        }

        public LoginPage GoToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
            return new LoginPage(driver);
        }
    }
}
