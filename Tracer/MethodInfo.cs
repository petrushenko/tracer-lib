using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tracer
{
    public class MethodInfo
    {
        public Stopwatch Stopwatch = new Stopwatch();
        public string Name { get; set; }

        public string ClassName { get; set; }

        public double ExecutionTime { get; set; }

        public List<MethodInfo> ChildMethods = new List<MethodInfo>();
    }
}
