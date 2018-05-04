using System;
using System.Text;
using Gamex.src.Util.Logging;
using Gamex.src.Util;

namespace Gamex.src.testland
{
    class TestMain
    {
        public static void AllTests()
        {
            var result = new TestResult("Gamex Tests");

            result.LogResult(new GameEntityTest().Run());
            result.LogResult(new CoordinateTest().Run());

            Logger.Default.Log(result.ToString());
        }
    }

    interface GamexTest
    {
        TestResult Run();
    }
    

    class TestResult
    {
        public string Title { get; private set; }
        public bool Passed { get; private set; } = true;

        private HamsterLogger log = new HamsterLogger();

        public TestResult()
        {
            Title = Nope.MethodNameOfCaller(2);
        }

        public TestResult(string title)
        {
            Title = title;
        }
        

        public void LogSuccess(string logstring)
        {
            log.Log(logstring);
        }

        public void LogFailure(string logstring)
        {
            log.Log(logstring);
            Passed = false;
        }

        public void LogFailureInCase(object o)
        {
            LogFailure(String.Format("Case {1} failed.", Title, o.ToString()));
        }

        private const string LogResultIndentString = "    ";
        public void LogResult(TestResult result)
        {
            log.Log(LogResultIndentString + result.GetInfo());
            foreach (var line in result.ToLines())
            {
                log.Log(LogResultIndentString + line);
            }
        }

        public string[] ToLines()
        {
            return log.ToLines();
        }

        public string GetInfo()
        {
            var sb = new StringBuilder();

            sb.Append(String.Format("{0} {1}", Title, Passed ? "Passed" : "Failed"));

            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(GetInfo());
            foreach (var v in ToLines())
            {
                sb.AppendLine(v);
            }

            return sb.ToString();
        }
    }
}
