using System;

namespace TracerLib
{
    public class ConsolePrinter : IPrinter
    {
        public void Print(string serializedResult)
        {
            Console.WriteLine(serializedResult);
        }
    }
}
