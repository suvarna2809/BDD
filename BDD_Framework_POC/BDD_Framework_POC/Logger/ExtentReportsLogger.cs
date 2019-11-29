using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using RelevantCodes.ExtentReports;
using System.IO;


namespace BDD_Framework_POC.Logger
{
    //[DebuggerStepThrough]
    public class ExtentReportsLogger
    {
        private ExtentReports _reports;
        private ExtentTest _test;
        private string MMddHHmmss;
        
        [ThreadStatic] public static string ReportFolderPath;

        public ExtentReportsLogger(string reportPath = @"C:\Logs\")
        {
            MMddHHmmss = DateTime.Now.ToString("MMddHHmmss"); 
            reportPath += MMddHHmmss + @"\";

            if (Directory.Exists(reportPath))
            {
                Directory.Delete(reportPath, true);
            }
            Directory.CreateDirectory(reportPath);

            string reportFileFullName = Path.GetFullPath(reportPath + "index.html");
            _reports = new ExtentReports(reportFileFullName, true, DisplayOrder.OldestFirst);
            ReportFolderPath = reportPath;
        }

        public ExtentReportsLogger EndTest()
        {
            _reports.EndTest(_test);
            return this;
        }

        public Guid ReportId 
        {
            get { return _reports.ReportId; }
        }

        public ExtentReportsLogger AddSystemInfo(Dictionary<string, string> info)
        {
            _reports.AddSystemInfo(info);
            return this;
        }

        public ExtentReportsLogger AddSystemInfo(string param, string value)
        {
            _reports.AddSystemInfo(param, value);
            return this;
        }

        public ExtentReportsLogger AddTestRunnerOutput(string log)
        {
            _reports.AddTestRunnerOutput(log);
            return this;
        }

        public ExtentReportsLogger Close()
        {
            _reports.Close();
            return this;
        }

        public ExtentReportsLogger Flush()
        {
            _reports.Flush();
            return this;
        }

        public ExtentReportsLogger LoadConfig(string filePath)
        {
            _reports.LoadConfig(filePath);
            return this;
        }

        public ExtentReportsLogger StartTest(string name, string description = "")
        {
            _test = _reports.StartTest(name, description);
            return this;
        }

        public ExtentReportsLogger AssignCategory(string[] tags)
        {
            _test.AssignCategory(tags);
            return this;
        }

        private static LogStatus GetLogStatus(Status Status)
        {
            return (LogStatus)Enum.Parse(typeof(LogStatus), Status.ToString());
        }

        public void Log(Status Status, Exception Exception)
        {
            _test.Log(GetLogStatus(Status), Exception);
        }

        public void Log(Status Status, string Details)
        {
            _test.Log(GetLogStatus(Status), Details);
        }
        public void Log(Status Status, string StepName, Exception Exception)
        {
            _test.Log(GetLogStatus(Status), StepName, Exception);
        }
        public void Log(Status Status, string StepName, string Details)
        {
            _test.Log(GetLogStatus(Status), StepName, Details);
        }

        public string AddScreenCapture(string imageRelPath)
        {
            return _test.AddScreenCapture(imageRelPath); 
        } 
    }
}
