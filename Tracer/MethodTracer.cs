using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tracer
{
    public class MethodTracer
    {
        public Stopwatch Stopwatch = new Stopwatch();

        public List<MethodInfo> MethodChilds = new List<MethodInfo>();

        public string GetName()
        {
            StackTrace stackTrace = new StackTrace();
            return stackTrace.GetFrame(2).GetMethod().Name;
        }

        public string GetClassName()
        {
            StackTrace stackTrace = new StackTrace();
            return stackTrace.GetFrame(2).GetMethod().ReflectedType.Name;
        }
    }
}
