using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDD_Framework_POC.WebDriver
{
    public class Browser
    {
        public IWebDriver Driver;
        public void GetBrowser()
        {

            Driver = BootStrapWebDriver.Open();

        }

        public void QuitBrowser()
        {
            Driver.Close();
            Driver.Quit();

        }

    }
}
