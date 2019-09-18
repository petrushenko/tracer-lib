using System.IO;

namespace TracerLib
{
    public class FilePrinter : IPrinter
    {
        public void Print(string serializedResult)
        {
            File.WriteAllText(PathToSave, serializedResult);
        }

        public string PathToSave { get; private set; }

        public FilePrinter(string path)
        {
            PathToSave = path;
        }
    }
}
