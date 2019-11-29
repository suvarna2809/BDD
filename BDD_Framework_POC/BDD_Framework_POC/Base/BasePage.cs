using BDD_Framework_POC.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDD_Framework_POC.Base
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;
        protected Browser Browser;

        //[DebuggerStepThrough]
        protected BasePage(Browser webBrowser)
        {
            Browser = webBrowser;
            Driver = webBrowser.Driver;
        }
    }
}
