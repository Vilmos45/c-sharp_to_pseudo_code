using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    internal class Program
    {
        static List<string> ReadFromFile(List<string> file)
        {
            Console.Clear();
            Console.WriteLine("The file must be encoded due to UTF8\nMust contain a valid c# code\nPut the file in the bin of this code");
            Console.Write("\nEnter the name of the file:");
            StreamReader f = new StreamReader(Console.ReadLine(), Encoding.UTF8);
            while (!f.EndOfStream)
                file.Add(f.ReadLine());
            f.Close();
            if (file.Count > 0)
                Console.WriteLine("File succesfully taken as input");
            else
                Console.WriteLine("The file was empty, or not placed to the right directory");
            return file;
        }

        static List<string> ReadFromConsole(List<string> file)
        {
            Console.Clear();
            Console.WriteLine("Paste here a working c# code. \nAt the end of it write 'STOP'");
            string line;
            while((line = Console.ReadLine()) != "STOP")
            {
                file.Add(line);
            }
            return file;
        }

        static void RULines(List<string> file) { //Remove Unnecesary Lines
            bool comments = false; // true if want to remove comments
            Console.Clear();
            Console.WriteLine("Do you want comments in your pseudo code? (Y/n)");
            if (Console.ReadLine().ToLower().Trim() == "y")
                comments = true;
            Console.WriteLine("\nRemoving comments, and unnecessary lines...");
            string tline;
            if (!comments)
            {
                for (int i = file.Count - 1; i >= 0; i--) 
                {
                    tline = file[i].Trim().ToLower(); //trimmed line
                    if (tline.StartsWith("#") || tline.Length == 0)
                        file.RemoveAt(i);
                }
            }
            else
            {
                for (int i = file.Count - 1; i >= 0; i--)
                {
                    tline = file[i].Trim().ToLower();
                    if (tline.Length == 0)
                        file.RemoveAt(i);
                }
            }
        }

        static void Main()
        {
            List<string> file = new List<string>();
            Console.WriteLine("1: Read from file\n2: Read form console\nq: exit");
            string inp = Console.ReadLine();
            if (int.TryParse(inp, out int val)) {
                if (val == 1)
                    file = ReadFromFile(file);
                if (val == 2)
                    file = ReadFromConsole(file);
                RULines(file);
            }
        }
    }
}
