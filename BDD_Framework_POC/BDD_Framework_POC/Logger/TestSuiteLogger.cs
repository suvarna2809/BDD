using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BDD_Framework_POC.Logger
{
    class TestSuiteLogger
    {
        private static ExtentReportsLogger _extentReportsLogger;
        private static ReportEngineLogger _reportEngineLogger;

        [ThreadStatic] public static string FeatureInfoTitle;
        [ThreadStatic] public static string FeatureInfoDescription;
        [ThreadStatic] public static string[] FeatureInfoTags;

        [ThreadStatic] public static string ScenarioInfoTitle;
        [ThreadStatic] private static string ScenarioBrowser;
        [ThreadStatic] public static string[] ScenarioInfoTags;


        [ThreadStatic] private static string StepInfoStepDefinitionType;
        [ThreadStatic] private static string StepInfoText;
        [ThreadStatic] private static string StepInfoMultilineText;
        [ThreadStatic] private static string StepInfoTable;

        public static void BeforeTestRun()
        {
            // Console.WriteLine("AfterTestRun" + Thread.CurrentThread.ManagedThreadId);
            string reportPath = System.Configuration.ConfigurationManager.AppSettings["logFolder"] ?? @"C:\Logs\";
            _extentReportsLogger = new ExtentReportsLogger(reportPath);
            _reportEngineLogger = new ReportEngineLogger(reportPath);
        }

        public static void AfterTestRun()
        {
            // Console.WriteLine("AfterTestRun" + Thread.CurrentThread.ManagedThreadId);
            _extentReportsLogger.Flush().Close();
            _reportEngineLogger.BuildSummaryReport();
        }

        public static void BeforeFeature(string featureInfoTitle, string featureInfoDescription, string[] featureInfoTags)
        {
            FeatureInfoTitle = featureInfoTitle;
            FeatureInfoDescription = featureInfoDescription;
            FeatureInfoTags = featureInfoTags;
            _reportEngineLogger.StartFeature(featureInfoTitle);
        }

        public static void AfterFeature()
        {
            _reportEngineLogger.EndFeature(FeatureInfoTitle, ScenarioInfoTitle);
        }

        public static void BeforeScenario(string scenarioInfoTitle, string scenarioBrowser, string[] scenarioInfoTags)
        {
            ScenarioInfoTitle = scenarioInfoTitle;
            ScenarioBrowser = scenarioBrowser;
            ScenarioInfoTags = scenarioInfoTags;



            Console.WriteLine("Feature: " + FeatureInfoTitle);
            Console.WriteLine(FeatureInfoDescription);
            Console.WriteLine("\r\nScenario: " + ScenarioInfoTitle);

            _extentReportsLogger.StartTest(ScenarioInfoTitle, "")
                .AssignCategory(FeatureInfoTags)
                .AssignCategory(ScenarioInfoTags);

            _reportEngineLogger.StartScenario(ScenarioBrowser, ScenarioInfoTitle);
        }

        public static void AfterScenario()
        {
            _reportEngineLogger.EndScenario(ScenarioInfoTitle);

            Console.WriteLine(Verify.Failures.Count() == 0 ? "PASS" : "FAIL");

            foreach (var failure in Verify.Failures)
            {
                Console.WriteLine(failure);
            }

            _extentReportsLogger.EndTest();

            // Console.WriteLine("ReportTestEnd" + Thread.CurrentThread.ManagedThreadId);
            if ((System.Configuration.ConfigurationManager.AppSettings["FlushReport"] ?? "Default") == "Default")
            {
                _extentReportsLogger.Flush();
            }

            Verify.AssertResults();
            //Verify.CleanUp();
        }

        public static void BeforeStep(string stepInfoStepDefinitionType, string stepInfoText, string stepInfoMultilineText, string stepInfoTable)
        {
            StepInfoStepDefinitionType = stepInfoStepDefinitionType;
            StepInfoText = stepInfoText;
            StepInfoMultilineText = stepInfoMultilineText;
            StepInfoTable = stepInfoTable;
        }

        public static void AfterStep()
        {
            if ((System.Configuration.ConfigurationManager.AppSettings["FlushReport"] ?? "Default") == "Step")
            {
                _extentReportsLogger.Flush();
            }
        }

        public static void Log(Status status, string Details)
        {
            var html = "<span style='font-size: 14px;font-weight:bold;color:" +
                (status == Status.Pass ? "green" : "red") +
                "'>" + Details.Replace("\n", "<\br>") + "</span>";

            _extentReportsLogger.Log(status, html);
            _reportEngineLogger.Log(status, html);
        }

        public static void Log(Status status, Exception exception)
        {
            _extentReportsLogger.Log(status, exception);
            _reportEngineLogger.Log(status, exception.Message + "</br>" + exception.StackTrace);
        }



        public static void LogBeforePageMethod(string methodInfo)
        {
            _extentReportsLogger.Log(Status.Info, "<PRE><span style='color:green;font-size: 14px;padding-left: 2cm;'>" + methodInfo + "</span></PRE>");
            // background-color:lightblue;
            Console.WriteLine(methodInfo);
        }

        public static void LogAfterPageMethodError(Exception exception, string imagePath)
        {
            string imageRelPath = "./" + Path.GetFileName(imagePath);
            string message = exception.Message + "<PRE>" + exception.StackTrace + "</PRE>" + _extentReportsLogger.AddScreenCapture(imageRelPath);
            _extentReportsLogger.Log(Status.Error, message);
        }

        public static void LogAfterePageMethodSuccess(string retType, string imagePath)
        {
            string message = "PageObject: " + retType +
                (string.IsNullOrWhiteSpace(imagePath) ? "" :
                _extentReportsLogger.AddScreenCapture("./" + Path.GetFileName(imagePath)));

            _extentReportsLogger.Log(Status.Info, message);
        }

        public static void LogBeforeStepDefination(string stepText, string methodInfoStr)
        {
            _extentReportsLogger.Log(Status.Info,
                "<PRE><span style='font-size: 14px;font-weight:bold;color:blue'>" + stepText + "</span></PRE>" +
                "<PRE><span style='font-size: 13px;color:#808080;'>" + methodInfoStr + "</span></PRE>");

            _reportEngineLogger.Log(Status.Info,
                "<span style='font-size: 14px;font-weight:bold;color:blue'>" + stepText.Replace("\n", "<\br>") + "</span>");
        }

        public static void LogBindingWarning(string text)
        {
            _extentReportsLogger.Log(Status.Warning, text);
            _reportEngineLogger.Log(Status.Warning, text);
        }

        public static void LogPreviousBindingMethodSkipped()
        {
            _extentReportsLogger.Log(Status.Warning, "SKIPPED");
            _reportEngineLogger.Log(Status.Warning, "SKIPPED");
        }

        public static void LogPreviousBindingMethodPending()
        {
            _extentReportsLogger.Log(Status.Warning, "PENDING");
            _reportEngineLogger.Log(Status.Warning, "PENDING");
        }

        public static void LogBindingError(BindingException ex)
        {
            _extentReportsLogger.Log(Status.Error, ex);
            _reportEngineLogger.Log(Status.Error, ex.Message + "</br>" + ex.StackTrace);
        }

        internal static void LogError(Exception ex)
        {
            _extentReportsLogger.Log(Status.Error, ex);
            _reportEngineLogger.Log(Status.Error, ex.Message + "</br>" + ex.StackTrace);
        }
    }
}
