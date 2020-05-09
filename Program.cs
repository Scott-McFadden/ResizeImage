using System;
using System.IO;
using System.Threading.Tasks;

namespace ResizeImage
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(args[0], "*.jpg");
            string d = args[0] + "\\modified";
            Directory.CreateDirectory(d);

            foreach (string currentFile in files)
            {
                string fileName = Path.GetFileName(currentFile);
                ResizeImage r = new ResizeImage(currentFile);

                r.AdjustSizeBasedOnWidth(500);
                r.Dir = d;
                r.ProcessTheFiles();
                r.SaveBoth(Path.GetFileNameWithoutExtension(currentFile));
                
            }

        }
    }
}
