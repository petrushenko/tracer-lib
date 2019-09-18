using TracerLib;
using System.Threading;
using System.Collections.Generic;
using System;

namespace test
{

    public class Foo
    {
        private Bar _bar;

        private Tracer _tracer;

        internal Foo(Tracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _bar.InnerMethod();
            _tracer.StopTrace();
        }
    }

    public class Bar
    {
        private Tracer _tracer;

        internal Bar(Tracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }
    }

    public class Program
    {
        public Tracer Tracer;
        static List<Thread> List = new List<Thread>();
        public static void Main(string[] args)
        {

            Program program = new Program();

            program.Tracer = new Tracer();
            Thread myThread = new Thread(new ParameterizedThreadStart(Thread2));
            myThread.Start(program.Tracer);

            Thread myThread2 = new Thread(new ParameterizedThreadStart(Thread2));
            myThread2.Start(program.Tracer);

            List.Add(myThread);
            List.Add(myThread2);

            foreach (var thread in List)
            {
                thread.Join();
            }

            Foo foo = new Foo(program.Tracer);
            foo.MyMethod();
            TraceResult traceResult = program.Tracer.GetTraceResult();
            JsonTracerSerializer serializer = new JsonTracerSerializer();
            string json = serializer.Serealize(traceResult);
            XmlTracerSerializer xmlTracerSerializer = new XmlTracerSerializer();
            string xml = xmlTracerSerializer.Serealize(traceResult);
            Console.WriteLine(xml);
            //Console.WriteLine(json);
        }

        static void Thread2(object o)
        {
            Tracer tracer = (Tracer)o;
            Foo foo = new Foo(tracer);
            foo.MyMethod();
        }

    }
}
