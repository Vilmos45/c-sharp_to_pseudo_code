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
            Console.Write("Enter the name of the file:");
            StreamReader f = new StreamReader(Console.ReadLine(), Encoding.UTF8);
            while (!f.EndOfStream)
            {
                file.Add(f.ReadLine());
            }
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
            Console.WriteLine("Paste here a working c# code. \nAt the end of it write 'STOP', or a single dot '.'");
            string line;
            while((line = Console.ReadLine()) != "STOP" || line != ".")
            {
                file.Add(line);
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
                    file = ReadFromFile(file);
                if (val == 2)
                    file = ReadFromConsole(file);
            }

        }
    }
}
