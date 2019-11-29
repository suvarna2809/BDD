using BDD_Framework_POC.Logger;
using BDD_Framework_POC.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;

using System;
using TechTalk.SpecFlow;

namespace BDD_Framework_POC.StepDefinitions
{
    [Binding]
    public sealed class Login_Page_Stepdefintion
    {

        private readonly IWebDriver driver;
        private readonly Verify Assert;

        public Login_Page_Stepdefintion(Verify assert)
        {
            Assert = assert;
            driver = ScenarioContext.Current.Get<IWebDriver>("currentdriver");
        }


        [Given(@"Navigate to Myntra application '(.*)'")]
        public void GivenNavigateToMyntraApplication(string url)
        {
            GoToPage go_to_page = new GoToPage(driver);
            go_to_page.GoToUrl(url);
        }


        [When(@"the user enter the (.*) and (.*)")]
        public void WhenTheUserEnterTheUsernameAndPassword(string email, string  password)
        {
            LoginPage login = new LoginPage(driver);
            login.ProfileMouseHover();
            login.ClickOnLogin_link();
            login.EnterEmailAddress(email);
            login.EnterPassword(password);
        }


        [When(@"the user click on submit button")]
        public void WhenTheUserClickOnSubmitButton()
        {
            LoginPage login = new LoginPage(driver);
            login.ClickOnsubmit();
        }

        [Then(@"the Myntra home page displayed")]
        public void ThenTheFlipcartHomePageDisplayed()
        {
            HomePage home = new HomePage(driver);
            string actual = "Online Shopping for Women, Men, Kids Fashion & Lifestyle - Myntra";
            string expected = home.HomePageDisplayed();
            Assert.AreEqual(expected, actual, "The Mynthra Home Page Displayed");            
        }

        [When(@"the user enter the email id '(.*)'")]
        public void WhenTheUserEnterTheEmailId(string emailid)
        {
            LoginPage login = new LoginPage(driver);
            login.ProfileMouseHover();
            login.ClickOnLogin_link();
            login.EnterEmailAddress(emailid);
        }

        [When(@"the user click on submit button without entering the password")]
        public void WhenTheUserClickOnSubmitButtonWithoutEnteringThePassword()
        {
            LoginPage login = new LoginPage(driver);
            login.ClickOnsubmit();
        }

        [Then(@"the error message is displayed as '(Please enter password)'")]
        public void ThenTheErrorMessageIsDisplayedAs(string errormsg)
        {
            LoginPage login = new LoginPage(driver);
            var expected = login.GetPasswordErrorMsg();
            Assert.AreEqual(expected, errormsg, "The Mynthra Home Page Displayed");
        }

        [When(@"the user enter the password '(.*)'")]
        public void WhenTheUserEnterThePassword(string password)
        {
            LoginPage login = new LoginPage(driver);
            login.ProfileMouseHover();
            login.ClickOnLogin_link();
            login.EnterPassword(password);
        }

        [When(@"the user click on submit button without entering the email id")]
        public void WhenTheUserClickOnSubmitButtonWithoutEnteringTheEmailId()
        {
            LoginPage login = new LoginPage(driver);
            login.ClickOnsubmit();
        }

        [Then(@"the error message is displayed as '(Please Enter a Valid Email Id)'")]
        public void ThenTheErrorMessageIsDisplayedAsPleaseEnteraValidEmailId(string errormsg)
        {
            LoginPage login = new LoginPage(driver);
            var expected = login.GetEmailErrorMsg();
            Assert.AreEqual(expected, errormsg, "The Mynthra Home Page Displayed");
        }

    }
}
