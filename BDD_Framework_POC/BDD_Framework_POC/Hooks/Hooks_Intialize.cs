using BDD_Framework_POC.Logger;
using BDD_Framework_POC.WebDriver;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace BDD_Framework_POC.Hooks
{
    [Binding]
    public sealed class Hooks_Intialize
    {
        private IWebDriver _driver;
        private readonly IObjectContainer _objectContainer;
        ScenarioContext _scenarioContext;
        FeatureContext _featureContext;

        public Hooks_Intialize(ScenarioContext scenarioContext, FeatureContext featureContext, IObjectContainer objectContainer)
        {
            //objectContainer.RegisterInstanceAs(new Browser());
            objectContainer.RegisterInstanceAs(new Browser());
            objectContainer.RegisterInstanceAs(new Verify());
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }


        [BeforeTestRun]
        public static void BeforeTestRunMethod()
        {
            TestSuiteLogger.BeforeTestRun();
        }

        [AfterTestRun]
        public static void AfterTestRunMethod()
        {
            TestSuiteLogger.AfterTestRun();
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            //string featureTitle = FeatureContext.Current.FeatureInfo.Title;
            // Console.WriteLine("BeforeStep" + Thread.CurrentThread.ManagedThreadId);
            string featureInfoTitle = FeatureContext.Current.FeatureInfo.Title;
            string featureInfoDescription = FeatureContext.Current.FeatureInfo.Description;
            string[] featureInfoTags = FeatureContext.Current.FeatureInfo.Tags;
            TestSuiteLogger.BeforeFeature(featureInfoTitle, featureInfoDescription, featureInfoTags);
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            TestSuiteLogger.AfterFeature();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            string scenarioInfoTitle = ScenarioContext.Current.ScenarioInfo.Title;
            string[] scenarioInfoTags = ScenarioContext.Current.ScenarioInfo.Tags;
            string scenarioBrowser = "Chrome";
            TestSuiteLogger.BeforeScenario(scenarioInfoTitle, scenarioBrowser, scenarioInfoTags);
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            _driver.Manage().Window.Maximize();
            ScenarioContext.Current.Add("currentdriver", _driver);
            //_objectContainer.Resolve<Browser>().GetBrowser();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver?.Quit();
            TestSuiteLogger.AfterScenario();
            //_objectContainer.Resolve<Browser>().QuitBrowser();
        }
    }
}
