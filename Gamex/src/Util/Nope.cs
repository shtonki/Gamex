using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Gamex.src.testland;

namespace Gamex.src.Util
{
    class Nope
    {
        /// <summary>
        /// Returns the name of the method which called it.
        /// </summary>
        /// <returns></returns>
        public static string MethodNameOfCaller(int depth = 1)
        {
            var currentMethodName = MethodBase.GetCurrentMethod().Name;
            var st = new StackTrace();
            return st.GetFrames()
                .Select(frame => frame.GetMethod().Name)
                .SkipWhile(name => name != currentMethodName)
                .ToArray()[depth];
        }
    }
}
