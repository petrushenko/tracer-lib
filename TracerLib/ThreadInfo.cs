using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    public class ThreadInfo
    {
        public ThreadInfo(int id, List<MethodInfo> threadMethods)
        {
            Id = id;
            Methods = new List<MethodInfo>();
            Methods = threadMethods;
        }

        public int Id { get; private set; }

        private double executionTime;
        public double ExecutionTime
        {
            get
            {
                foreach (MethodInfo method in Methods)
                {
                    executionTime += method.ExecutionTime;
                }
                return executionTime;
            }
            private set
            {
                executionTime = value;
            }
        }

        public List<MethodInfo> Methods { get; private set; }
    }
}
