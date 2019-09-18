using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class Tracer : ITracer
    {
        Stack<MethodTracer> MethodTracers = new Stack<MethodTracer>();

        Stack<ThreadInfo> Threads = new Stack<ThreadInfo>();

        ThreadInfo CurrentThread = null;

        MethodTracer CurrentMethodTracer = null;

        public List<MethodInfo> Methods = new List<MethodInfo>();

        public TraceResult GetTraceResult()
        {
            throw new NotImplementedException();
        }

        public void StartTrace()
        {
            if (CurrentMethodTracer != null)
            {
                CurrentMethodTracer.Stopwatch.Stop();
                MethodTracers.Push(CurrentMethodTracer);
            }
            CurrentMethodTracer = new MethodTracer();
            CurrentMethodTracer.Stopwatch.Start();            
        }

        public void StopTrace()
        {
            CurrentMethodTracer.Stopwatch.Stop();
            MethodInfo methodInfo = new MethodInfo();
            methodInfo.ClassName = CurrentMethodTracer.GetClassName();
            methodInfo.Name = CurrentMethodTracer.GetName();
            methodInfo.ExecutionTime = CurrentMethodTracer.Stopwatch.Elapsed.TotalMilliseconds;
            methodInfo.ChildMethods = CurrentMethodTracer.MethodChilds;
            if (MethodTracers.Count > 0)
            {
                CurrentMethodTracer = MethodTracers.Pop();
                CurrentMethodTracer.MethodChilds.Add(methodInfo);
            }
            else
            {
                Methods.Add(methodInfo);
            }
        }
    }
}
