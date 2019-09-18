using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class ThreadInfo
    {
        public string Id { get; set; }
        public double ExecutionTime { get; set; }

        public List<MethodInfo> Methods = new List<MethodInfo>();
    }
}
