using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BDD_Framework_POC.WebDriver
{
    public class BootStrapWebDriver
    {
        public static IWebDriver Open()
        {

            IWebDriver driver;
            var browser = System.Configuration.ConfigurationManager.AppSettings["Browser"];
            switch (System.Configuration.ConfigurationManager.AppSettings["Browser"])
            {
                case "InternetExplorer":
                    InternetExplorerOptions options = new InternetExplorerOptions
                    {
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                        EnsureCleanSession = true
                    };
                    IWebDriver _driver =
                        new InternetExplorerDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                            options);
                    _driver.Manage().Cookies.DeleteAllCookies();
                    driver = new EventFiringWebDriver(_driver);
                    break;
                case "Firefox":
                    //FirefoxProfile profile = new FirefoxProfile();
                    //profile.SetPreference("dom.max_script_run_time", 60);
                    //driver = new EventFiringWebDriver(new FirefoxDriver(profile));
                    driver = new EventFiringWebDriver(new FirefoxDriver());
                    break;
                case "Chrome":
                    driver =
                        new EventFiringWebDriver(
                            new ChromeDriver(@"C:\Drivers"));
                    break;
                default:
                    driver = null;
                    break;
            }          
            driver.Manage().Cookies.DeleteAllCookies();
            TimeSpanFromSeconds = TimeSpan.FromSeconds(int.Parse(System.Configuration.ConfigurationManager.AppSettings["ImplicitTimeOut"]));
            driver.Manage().Timeouts().ImplicitWait = TimeSpanFromSeconds;
            driver.Manage().Timeouts().PageLoad =TimeSpan.FromSeconds(int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageLoadTimeout"]));
            driver.Manage().Window.Maximize();

            return driver;
        }

        public static TimeSpan TimeSpanFromSeconds;

        public static void Close(IWebDriver driver)
        {
            try
            {
                driver.Close();
                driver.Quit();
            }
            catch
            {
                // ignored
            }
        }

        [ThreadStatic] public static bool IsBrowserOpen;
    }
}
