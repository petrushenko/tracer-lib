using System.Collections.Concurrent;
using System.Collections.Generic;

namespace TracerLib
{
    public class TraceResult
    {
        public TraceResult(List<ThreadInfo> threadsInfo)
        {
            ThreadsInfo = threadsInfo;
        }

        public List<ThreadInfo> ThreadsInfo { get; private set; }
    }
}
