using System;
using System.Threading;
using System.Xml.Serialization;
using TracerLib;

namespace ConsoleApp
{
    class Program
    {
        public class Foo
        {
            private Bar _bar;
            private ITracer _tracer;

            internal Foo(ITracer tracer)
            {
                _tracer = tracer;
                _bar = new Bar(_tracer);
            }

            public void MyMethod()
            {
                _tracer.StartTrace();
                _bar.InnerMethod();
                _tracer.StopTrace();
            }
        }

        public class Bar
        {
            private ITracer _tracer;

            internal Bar(ITracer tracer)
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

        public void Method(object o)
        {
            Tracer tracer = (Tracer)o;
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            Thread thread = new Thread(new ParameterizedThreadStart(program.Method));

            ITracer tracer = new Tracer();
            Foo foo = new Foo(tracer);
            foo.MyMethod();
            thread.Start(tracer);
            thread.Join();

            var xmlTracerSerializer = new XmlTracerSerializer();
            var jsonTracerSerializer = new JsonTracerSerializer();
            TraceResult traceResult = tracer.GetTraceResult();
            var xml = xmlTracerSerializer.Serealize(traceResult);
            var json = jsonTracerSerializer.Serealize(traceResult);

            var fs = new FilePrinter("trace.json");
            fs.Print(xml);
            fs.Print(json);

            ConsolePrinter cp = new ConsolePrinter();
            cp.Print(xml);
            cp.Print(json);

            Console.ReadKey();
        }
    }
}
