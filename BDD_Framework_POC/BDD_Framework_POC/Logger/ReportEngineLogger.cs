using BDD_Framework_POC.Logger;
using Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static Io.Cucumber.Messages.TestResult.Types;
using static Reports.ReportEngine;

namespace BDD_Framework_POC.Logger
{ 
    //[DebuggerStepThrough]
    public class ReportEngineLogger
    {
        public static string TodayFolder;
        private string _reportPath;

        public ReportEngineLogger(string reportPath)
        {
            _reportPath = reportPath;
            ReportEngine.Instance.TestRunExecutionBeginTime = DateTime.Now.ToString();
            TodayFolder = DateTime.Now.ToString("ddMMyyyy_HHmmss");
        }

        public void Log(Status status, string details)
        {
            ReportEngine.Instance.AddStepStatus(GetReportLogLevel(status), details);
        }

        public void BuildSummaryReport()
        {
            ReportEngine.Instance.TestRunExecutionEndTime = DateTime.Now.ToString();
            //ReportEngine.Instance.BuildSummaryReport(_reportPath + TodayFolder);
            ReportEngine.Instance.BuildSummaryReport();
        }

        public void StartFeature(string FeatureInfoTitle)
        {
            ReportEngine.Instance.FeatureExecutionBeginTime = DateTime.Now.ToString();
            ReportEngine.Instance.CurrentFeature = FeatureInfoTitle;
            ReportEngine.Instance.ResultPath = _reportPath + TodayFolder + "\\" + FeatureInfoTitle;
            ReportEngine.Instance.ResultFileName = FeatureInfoTitle;
            ReportEngine.Instance.ClearScenarios();
        }

        private ReportEngine.ReportLogLevel GetReportLogLevel(Status Status)
        {
            switch (Status)
            {
                case Status.Pass: return ReportEngine.ReportLogLevel.PASS;
                case Status.Fail: return ReportEngine.ReportLogLevel.FAIL;
                case Status.Warning: return ReportEngine.ReportLogLevel.WARN;
                case Status.Info: return ReportEngine.ReportLogLevel.LOG;
                case Status.Error: return ReportEngine.ReportLogLevel.FAIL;
                default: return ReportEngine.ReportLogLevel.LOG;
            }
        }

        public void EndFeature(string FeatureInfoTitle, string ScenarioInfoTitle)
        {
            ReportEngine.Instance.FeatureExecutionEndTime = DateTime.Now.ToString();
            ReportEngine.Instance.CurrentFeature = FeatureInfoTitle;
            ReportEngine.Instance.AddFeature();
            //ReportEngine.Instance.BuildDetailedReport(FeatureInfoTitle, ScenarioInfoTitle);
            ReportEngine.Instance.BuildDetailedReport();
        }

        public void StartScenario(string ScenarioBrowser, string ScenarioInfoTitle)
        {
            ReportEngine.Instance.Browser = ScenarioBrowser;
            ReportEngine.Instance.ScenarioExecutionBeginTime = DateTime.Now.ToString();
            ReportEngine.Instance.CurrentScenario = ScenarioInfoTitle;
            ReportEngine.Instance.ClearTestSteps();
        }

        public void EndScenario(string ScenarioInfoTitle)
        {
            ReportEngine.Instance.ScenarioExecutionEndTime = DateTime.Now.ToString();
            ReportEngine.Instance.CurrentScenario = ScenarioInfoTitle;
            ReportEngine.Instance.AddScenario();
        }

    }
}
