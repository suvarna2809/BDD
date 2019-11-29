using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDD_Framework_POC.Logger
{
   public class Verify
    {
        private readonly bool _isReviewMode;
        [ThreadStatic] private static List<string> _failures;

        public static List<string> Failures
        {
            get
            {
                if (_failures == null)
                {
                    _failures = new List<string>();
                }

                return _failures;
            }
        }

        public Verify()
        {
            _isReviewMode = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["isReviewMode"] ?? "false");
        }

        private static void HandleAssertionException(AssertionException ae)
        {
            var stackTrace = new StackTrace(true);
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(ae.Message);
            string myOwnCodeFileName = stackTrace.GetFrame(0).GetFileName();
            var stackFrames = stackTrace.GetFrames();

            if (stackFrames != null)
            {
                foreach (var stackFrame in stackFrames)
                {
                    if (myOwnCodeFileName != null &&
                        ((0 == stackFrame.GetFileLineNumber()) || myOwnCodeFileName.Equals(stackFrame.GetFileName())))
                        continue;

                    var method = stackFrame.GetMethod();
                    var fileName = stackFrame.GetFileName();
                    if (method.DeclaringType != null && !string.IsNullOrWhiteSpace(fileName))
                    {
                        stringBuilder.AppendLine("at " + method.DeclaringType.FullName + method.Name + @"(...) in " +
                                                 new FileInfo(fileName).Name + ":line " +
                                                 stackFrame.GetFileLineNumber());
                    }
                }
            }

            Failures.Add(stringBuilder.ToString());
            TestSuiteLogger.Log(Status.Fail, stringBuilder.ToString().Replace("\n", "<\br>"));

        }

        public static void AssertResults()
        {
            if (Failures.Count != 0)
            {
                StringBuilder builder = new StringBuilder();
                Failures.ForEach(delegate (string obj) { builder.Append(obj); });
                _failures.Clear();
                Assert.Fail(builder.ToString());
            }
        }

        public static void CleanUp()
        {
            Failures.Clear();
        }

        public void AreEqual(object expected, object actual, string message)
        {
            if (_isReviewMode)
            {
                TestSuiteLogger.Log(Status.Pass, message + "<br> Expected: " + expected);
            }
            else
            {
                try
                {
                    Assert.AreEqual(expected, actual, message);
                    TestSuiteLogger.Log(Status.Pass, message + "<br> Value: " + expected);
                }
                catch (AssertionException ae)
                {
                    HandleAssertionException(ae);
                }
            }
        }

        public void AreNotEqual(object expected, object actual, string message)
        {
            if (_isReviewMode)
            {
                TestSuiteLogger.Log(Status.Pass, message + "<br> Expected: " + expected);
            }
            else
            {
                try
                {
                    Assert.AreNotEqual(expected, actual, message);
                    TestSuiteLogger.Log(Status.Pass, message + "<br> Value: " + expected);
                }
                catch (AssertionException ae)
                {
                    HandleAssertionException(ae);
                }
            }
        }

        public void Contains<T>(T expected, ISet<T> actual, string message)
        {
            if (_isReviewMode)
            {
                TestSuiteLogger.Log(Status.Pass, message + "<br> Expected: " + expected);
            }
            else
            {
                try
                {
                    TestSuiteLogger.Log(Status.Pass, message + "<br> Value: " + expected);
                    Assert.IsTrue(actual.Contains(expected), message);
                }
                catch (AssertionException ae)
                {
                    HandleAssertionException(ae);
                }
            }
        }

        public void AreEqual<T>(ISet<T> expected, ISet<T> actual, string message)
        {
            if (_isReviewMode)
            {
                TestSuiteLogger.Log(Status.Pass, message);
                foreach (T t in expected)
                {
                    TestSuiteLogger.Log(Status.Pass, "-->" + t);
                }
            }
            else
            {
                AreEqual(expected.Count, actual.Count, message + "(count)");

                // Note: this is only difference
                foreach (T t in expected)
                {
                    Contains(t, actual, t + message + "(in actual)");
                }

                foreach (T t in actual)
                {
                    Contains(t, expected, t + message + "(in expected)");
                }
            }
        }



        public void IsTrue(bool b, string message)
        {
            if (_isReviewMode)
            {
                TestSuiteLogger.Log(Status.Pass, "IsTrue: " + message);
            }
            else
            {
                try
                {
                    TestSuiteLogger.Log(Status.Pass, "IsTrue: " + message);
                    Assert.IsTrue(b, message);
                }
                catch (AssertionException ae)
                {
                    HandleAssertionException(ae);
                }
            }
        }

        public void IsFalse(bool b, string message)
        {
            if (_isReviewMode)
            {
                TestSuiteLogger.Log(Status.Pass, "IsFalse: " + message);
            }
            else
            {
                try
                {
                    TestSuiteLogger.Log(Status.Pass, "IsFalse: " + message);
                    Assert.IsFalse(b, message);
                }
                catch (AssertionException ae)
                {
                    HandleAssertionException(ae);
                }
            }
        }

        public void Fail(string message)
        {
            if (_isReviewMode)
            {
                TestSuiteLogger.Log(Status.Fail, message);
            }
            else
            {
                try
                {
                    TestSuiteLogger.Log(Status.Fail, message);
                    Assert.Fail(message);
                }
                catch (AssertionException ae)
                {
                    HandleAssertionException(ae);
                }
            }
        }

        public void Pass(string message)
        {
            if (_isReviewMode)
            {
                TestSuiteLogger.Log(Status.Pass, message);
            }
            else
            {
                try
                {
                    TestSuiteLogger.Log(Status.Pass, message);
                    Assert.Pass(message);
                }
                catch (AssertionException ae)
                {
                    HandleAssertionException(ae);
                }
            }
        }
    }
}
