using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

namespace TracerLib
{
    public class JsonTracerSerializer : ITracerSerializer
    {
        public string Serealize(TraceResult traceResult)
        {
            JArray threadJArray = new JArray();
            foreach (ThreadInfo thread in traceResult.ThreadsInfo)
            {
                JObject threadJObject = GetThreadJObject(thread);
                threadJArray.Add(threadJObject);
            }
            JObject resultJObject = new JObject
            {
                {"threads", threadJArray }
            };
            StringWriter stringWriter = new StringWriter();
            using (var jsonWriter = new JsonTextWriter(stringWriter))
            {
                jsonWriter.Formatting = Formatting.Indented;
                resultJObject.WriteTo(jsonWriter);
            }
            return stringWriter.ToString();
        }

        private JObject GetMethodJObject(MethodInfo methodInfo)
        {
            return new JObject
            {
                {"name", JToken.FromObject(methodInfo.Name) },
                {"class", JToken.FromObject(methodInfo.ClassName) },
                {"time", JToken.FromObject(methodInfo.ExecutionTime) },
                {"methods", new JArray(JToken.FromObject(methodInfo.ChildMethods)) }
            };
        }

        private JObject GetThreadJObject(ThreadInfo threadInfo)
        {
            JArray methodJArray = new JArray();
            foreach (MethodInfo method in threadInfo.Methods)
            {
                JObject methodJObject = GetMethodJObject(method);
                methodJArray.Add(methodJObject);
            }
            return new JObject
            {
                {"id", threadInfo.Id },
                {"time", threadInfo.ExecutionTime },
                {"methods", methodJArray }
            };
        }
    }
}
