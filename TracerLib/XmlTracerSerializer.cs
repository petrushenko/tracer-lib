using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;

namespace TracerLib
{
    public class XmlTracerSerializer : ITracerSerializer
    {
        public string Serealize(TraceResult traceResult)
        {
            var aSerializer = new XmlSerializer(typeof(TraceResult));
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            aSerializer.Serialize(sw, traceResult); // pass an instance of A
            string xmlResult = sw.GetStringBuilder().ToString();
            return xmlResult;
        }
    }
}
