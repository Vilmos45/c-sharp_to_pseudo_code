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
        static List<string> ReadFromFile()
        {
            List<string> file = new List<string>();
            Console.Clear();
            Console.WriteLine("The file must be encoded due to UTF8\nMust contain a valid c# code\nPut the file in the bin of this code");
            Console.Write("Enter the name of the file:");
            StreamReader f = new StreamReader(Console.ReadLine(), Encoding.UTF8);
            while (!f.EndOfStream)
            {
                file.Add(f.ReadLine());
            }
            return file;
        }



        static void Main()
        {
            List<string> file = new List<string>();
            Console.WriteLine("1: Read from file\n2: Read form console\nq: exit");
            string inp = Console.ReadLine();
            if (int.TryParse(inp, out int val)) {
                if (val == 1)
                    file = ReadFromFile();

            }

        }
    }
}
