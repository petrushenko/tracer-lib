using System.Collections.Concurrent;
using System.Collections.Generic;

namespace TracerLib
{
    public class TraceResult
    {
        public TraceResult(ConcurrentDictionary<int, ThreadInfo> threadsInfo)
        {
            ThreadsInfo = new List<ThreadInfo>();
            ThreadsInfo.AddRange(threadsInfo.Values);
        }

        public List<ThreadInfo> ThreadsInfo { get; private set; }
    }
}
