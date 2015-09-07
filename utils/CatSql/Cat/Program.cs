using System;
using System.IO;
using System.Text;

namespace CatSql
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = ".";
            string outFile = "SqlCat.sql";            

            if (args.Length == 2)
            {
                directory = args[0];
                outFile = args[1];                                                
            }

            if (File.Exists(outFile))
            {
                File.Delete(outFile);
            }

            string[] files = Directory.GetFiles(directory, "*.sql");            

            foreach (string file in files)
            {
                File.AppendAllText(outFile, File.ReadAllText(file) + Environment.NewLine, Encoding.Unicode);
            }
        }
    }
}
