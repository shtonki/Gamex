
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gamex.src.Controller.App;
using Gamex.src.Controller.Debug;

namespace Gamex.src.Util.Logging
{

    public static class Logger
    {
        public static ILogger Default { get; private set; }

        static Logger()
        {
            RegisterDefaultLogger(new FileLogger("log"));
        }

        public static void RegisterDefaultLogger(ILogger appendee)
        {
            if (appendee == null) return;

            if (Default == null)
            {
                Default = appendee;
                return;
            }

            var newDefault = new CompoundLogger(Default, appendee);
            Default = newDefault;
        }
    }


    public interface ILogger
    {
        /// <summary>
        /// Prints a line to the log. 
        /// Formatted like Console.WriteLine.
        /// </summary>
        /// <param name="logMessage">The message to log</param>
        /// <param name="arguments">Arguments to be formatted into the message</param>
        void Log(string logMessage, params object[] arguments);
    }

    public class CompoundLogger : ILogger
    {
        public ILogger LoggerA { get; }
        public ILogger LoggerB { get; }

        public CompoundLogger(ILogger loggerA, ILogger loggerB)
        {
            LoggerA = loggerA;
            LoggerB = loggerB;
        }

        public void Log(string logMessage, params object[] arguments)
        {
            LoggerA.Log(logMessage, arguments);
            LoggerB.Log(logMessage, arguments);
        }
    }

    public class FileLogger : ILogger
    {
        private StreamWriter Writer { get; }

        public FileLogger(string fileName)
        {
            try
            {
                Writer = new StreamWriter(fileName);
                AppController.OnExit += (_, __) =>
                {
                    Writer.Flush();
                    Writer.Close();
                };
            }
            // i'm here because when the designer uses files that use this logger
            // it crashes horribly since it can't open the file
            // microsoft won't tell me what error it is so i catch them all
            // write to a dummy and hope this never happens when one wants logging
            catch
            {
                Writer = StreamWriter.Null;
            }
        }

        public void Log(string logMessage, params object[] arguments)
        {
            Writer.WriteLine(String.Format(logMessage, arguments));
        }

        // todo d7i8 make the StreamWriter close gracefully on ugly game exit
    }

    public class HamsterLogger : ILogger
    {
        private List<string> Messages { get; set; } = new List<string>();

        public void Log(string logMessage, params object[] arguments)
        {
            Messages.Add(String.Format(logMessage, arguments));
        }

        public string[] ToLines()
        {
            return Messages.ToArray();
        }

        public override string ToString()
        {
            return ToLines().Aggregate("", (str, next) => str + (str == "" ? "" : Environment.NewLine) + next);
        }
    }

    public class WriteLogger : ILogger
    {
        public static WriteLogger stdout = new WriteLogger(System.Console.WriteLine);
        public static WriteLogger GamexConsole = new WriteLogger(DebugController.Log);

        private Action<string> LogFunc { get; }

        public WriteLogger(Action<string> logfunc)
        {
            LogFunc = logfunc;
        }

        public void Log(string logMessage, params object[] arguments)
        {
            System.Console.WriteLine(logMessage, arguments);
        }
    }
}
