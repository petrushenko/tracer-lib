using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TracerLib.Tests
{
    [TestClass]
    public class TracerLibTests
    {

        public Tracer Tracer = new Tracer();

        List<Thread> Threads = new List<Thread>();

        int ThreadsCount = 5;

        int MillisecondsTimeout = 100;

        private void Method()
        {
            Tracer.StartTrace();
            Thread.Sleep(MillisecondsTimeout);
            Tracer.StopTrace();

        }

        [TestMethod]
        public void ExecutionTime()
        {
            Method();
            TraceResult traceResult = Tracer.GetTraceResult();
            double methodTime = traceResult.ThreadsInfo[0].Methods[0].ExecutionTime;
            double threadTime = traceResult.ThreadsInfo[0].ExecutionTime;
            Assert.IsTrue(methodTime >= MillisecondsTimeout);
            Assert.IsTrue(threadTime >= MillisecondsTimeout);
        }

        [TestMethod]
        public void ThreadCount()
        {
            //creats (ThreadsCount - 1) thread
            for (int i = 0; i < ThreadsCount; i++)
            {
                Threads.Add(new Thread(Method));
            }

            foreach (Thread thread in Threads)
            {
                thread.Start();
                thread.Join();
            }

            TraceResult traceResult = Tracer.GetTraceResult();
            // (ThreadsCount - 1) and one main thread
            Assert.AreEqual(ThreadsCount, traceResult.ThreadsInfo.Count);
        }



    }
}
