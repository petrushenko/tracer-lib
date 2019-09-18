using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TracerLib
{
    [CollectionDataContract]
    [Serializable]
    public class TraceResult
    {
        public TraceResult(List<ThreadInfo> threadsInfo)
        {
            ThreadsInfo = new List<ThreadInfo>();
            ThreadsInfo = threadsInfo;
        }

        [DataMember]
        public List<ThreadInfo> ThreadsInfo { get; private set; }
    }
}
