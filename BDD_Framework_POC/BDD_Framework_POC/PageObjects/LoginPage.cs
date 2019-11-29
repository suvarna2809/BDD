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
   public class LoginPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30000));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement email { get; set; }

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password { get; set; }

        [FindsBy(How = How.ClassName, Using = "login-login-button")]
        private IWebElement submit { get; set; }

        [FindsBy(How = How.ClassName, Using = "desktop-userTitle")]
        private IWebElement profile { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text()='log in']")]
        private IWebElement login_button { get; set; }

        [FindsBy(How = How.XPath, Using = "//p[text()='Please enter password']")]
        private IWebElement pswd_err_msg { get; set; }

        [FindsBy(How = How.XPath, Using = "//p[text()='Please Enter a Valid Email Id']")]
        private IWebElement email_err_msg { get; set; }

        public LoginPage ClickOnLogin_link()
        {
            login_button.Click();
            Thread.Sleep(100);
            return this;
        }


        public LoginPage EnterEmailAddress(string email_address)
        {
            Thread.Sleep(50);
            email.Click();
            email.SendKeys(email_address);
            return this;
        }

        public LoginPage EnterPassword(string passwordValue)
        {
            password.Click();
            password.SendKeys(passwordValue);
            return this;
        }

        public void ProfileMouseHover()
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("desktop-userTitle")));
            Actions action = new Actions(driver);
            action.MoveToElement(profile).Perform();
            Thread.Sleep(4000);
        }
        public HomePage ClickOnsubmit()
        {
            submit.Click();
            Thread.Sleep(100);
            return new HomePage(driver);
        }

        public string GetPasswordErrorMsg()
        {
            return pswd_err_msg.Text;
        }

        public string GetEmailErrorMsg()
        {
            return email_err_msg.Text;
        }
    }
}
