using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace EncontrarImagenesNoUsadas
{
    class Program
    {
        static List<String> noUsadas = new List<string>();
        public static void Main()
        {
        
            IEnumerable<string> imagenes = Directory.EnumerateFiles(@"C:\Users\Windows\Source\Repos\ug\images").Select(x => x.Substring(x.LastIndexOf('\\') +1));
            foreach(string s in imagenes)
            {
                //Console.Write('.');
                Buscar(s);
            }
            Console.ReadKey();

            foreach (string f in noUsadas)
            {
                try
                {                    
                    File.Delete(@"C:\Users\Windows\Source\Repos\ug\images\" + f);                    
                }
                catch (Exception e) { Console.WriteLine("No se borró " + f + " " + e.Message); }
            }
        }

        static void Buscar(string searchWord)
        {
            string sourceFolder = @"C:\Users\Windows\Source\Repos\ug";
            

            List<string> allFiles = new List<string>();
            AddFileNamesToList(sourceFolder, allFiles);
            foreach (string fileName in allFiles)
            {
                var contents = File.ReadAllText(fileName);
                if (contents.Contains(searchWord))
                {
                    //Console.WriteLine(searchWord + "está en" + fileName);
                    return;
                }
            }
            noUsadas.Add(searchWord);
            Console.WriteLine(searchWord);
        }

        public static void AddFileNamesToList(string sourceDir, List<string> allFiles)
        {

            var fileEntries = Directory.GetFiles(sourceDir).Where(x => !x.EndsWith(".csproj") && !x.EndsWith(".sln") && !x.Contains(".git\\"));
            foreach (string fileName in fileEntries)
            {
                allFiles.Add(fileName);
            }

            //Recursion    
            string[] subdirectoryEntries = Directory.GetDirectories(sourceDir);
            foreach (string item in subdirectoryEntries)
            {
                // Avoid "reparse points"
                if ((File.GetAttributes(item) & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
                {
                    AddFileNamesToList(item, allFiles);
                }
            }

        }
    }
}

