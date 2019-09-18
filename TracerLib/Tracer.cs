using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace TracerLib
{
    public class Tracer : ITracer
    {
        public Tracer()
        {
            ThreadTracers = new ConcurrentDictionary<int, ThreadTracer>();
            ThreadsInfo = new ConcurrentDictionary<int, ThreadInfo>();
        }

        private readonly ConcurrentDictionary<int, ThreadTracer> ThreadTracers;

        private readonly ConcurrentDictionary<int, ThreadInfo> ThreadsInfo;

        public TraceResult GetTraceResult()
        {
            return new TraceResult(ThreadsInfo);
        }

        private ThreadInfo GetThreadInfoById(int threadId)
        {
            ThreadInfo threadInfo;
            ThreadsInfo.TryGetValue(threadId, out threadInfo);      
            return threadInfo;
        }

        private ThreadTracer GetCurrentThreadTracer()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;         
            ThreadTracer threadTracer;
            ThreadTracers.TryGetValue(threadId, out threadTracer);
            return threadTracer;
        }

        public void StartTrace()
        {
            ThreadTracer threadTracer = GetCurrentThreadTracer();
            if (threadTracer == null)
            {
                int currentThreadId = Thread.CurrentThread.ManagedThreadId;
                threadTracer = new ThreadTracer();       
                ThreadTracers.TryAdd(currentThreadId, threadTracer);
                
            }
            threadTracer.StartTrace();
        }

        public void StopTrace()
        {
            ThreadTracer threadTracer = GetCurrentThreadTracer();
            threadTracer.StopTrace();
            int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            ThreadInfo threadInfo = GetThreadInfoById(currentThreadId);
            if (threadInfo == null)
            {
                List<MethodInfo> threadMethodInfos = threadTracer.GetThreadMethodList();
                threadInfo = new ThreadInfo(currentThreadId, threadMethodInfos);
                ThreadsInfo.TryAdd(currentThreadId, threadInfo);
            }
        }
    }
}
