using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Converter
{
    internal class Program
    {
        #region Dictionary
        /*static readonly Dictionary<string, string> eng = new Dictionary<string, string>()
        {
            { "if", "IF" },
            { "else", "ELSE" },
            { "while", "WHILE" },
            { "for", "FOR" },
            { "true", "TRUE" },
            { "false", "FALSE" },
        };*/

        static readonly Dictionary<string, string> hun = new Dictionary<string, string>()
        {
            { "if", "Ha" },//ifelses
            { "else", "Különben" },
            { "while", "Amíg" },//loop
            { "for", "Ciklus" },
            { "true", "Igaz" },//logic
            { "false", "Hamis" },
            { "=", ":=" },
            { "!=", "≠" },
            { "==", "=" },
            { "int", "Egész" },//variables
            { "bool", "Logikai" },
            { "static void", "Eljárás" },
            
        };

        #endregion

        #region Read
        static int Read()
        {
            Console.WriteLine("1: Read from file\n2: Read form console\nq: exit");
            string inp = Console.ReadLine();
            int val;
            if (!int.TryParse(inp, out val) || val > 2 || val < 1)
            {
                Console.WriteLine("Invalid command!");
                Console.ReadKey();
                return -1;
            }
            return val;
        }

        static List<string> ReadFromFile(List<string> file)
        {
            Console.Clear();
            Console.WriteLine("The file must be encoded due to UTF8\nMust contain a valid c# code\nPut the file in the 'Pseudocode' folder");
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
            Console.WriteLine("Paste here a working c# code\nAt the end of it write 'STOP'");
            string line;
            while ((line = Console.ReadLine()) != "STOP")
            {
                file.Add(line);
            }
            if (file.Count > 0)
                Console.WriteLine("Input succesfully taken");
            else
                Console.WriteLine("The input was empty, or an ERROR occured");
            return file;
        }
        #endregion

        #region Processing
        static void RULines(List<string> file)
        { //Remove Unnecesary and Unused Lines
            bool comments = false; // true if want to remove comments
            Console.Clear();
            Console.WriteLine("Do you want comments in your pseudo code? (Y/n)\n");
            if (Console.ReadLine().ToLower().Trim() == "y")
                comments = true;
            string tline;
            if (!comments)
            {
                Console.WriteLine("Removing comments...");
                for (int i = file.Count - 1; i >= 0; i--)
                {
                    tline = file[i].Trim();
                    if (tline.Contains("//"))
                        file[i] = file[i].Remove(file[i].IndexOf("//"));
                    else if (tline.Contains("/*"))
                    {
                        file[i] = file[i].Remove(file[i].IndexOf("/*"));
                        while ((!tline.Contains("*/")) && i > 0)
                        {
                            file.RemoveAt(i);
                            i--;
                            tline = file[i].Trim();
                        }
                        if (file[i].IndexOf("*/") >= 0)
                            file[i] = file[i].Remove(file[i].IndexOf("*/"));
                    }
                }
            }
            Console.WriteLine("Removing unnecessary lines...");
            for (int i = file.Count - 1; i >= 0; i--)
            {
                file[i] = file[i].Replace(";", "");
                tline = file[i].Trim();
                if (tline.Length == 0)
                    file.RemoveAt(i);
                else
                    file[i] = file[i].Replace(";", "");
            }
            Console.WriteLine("Removing succesfully done");
        }

        static List<string> Convert(List<string> file)
        {
            List<string> code = new List<string>(file.Count);
            for (int i = 0; i < file.Count; i++)
            {

            }
            return file;
        }
        #endregion

        #region Write
        static int Write()
        {
            Console.Clear();
            Console.WriteLine("1: Write to file\n2: Write to console");
            string inp = Console.ReadLine();
            int val;
            if (!int.TryParse(inp, out val) || val > 2 || val < 1)
            {
                Console.WriteLine("Invalid command!");
                Console.ReadKey();
                return -1;
            }
            return val;
        }

        static void WriteToFile(List<string> pseudo)
        {
            Console.Clear();
            Console.WriteLine("The file with the pseudo code will be in the pseudocode folder");
            StreamWriter writer = new StreamWriter("../../Pseudocode/output.txt");
            foreach (var item in pseudo)
            {
                writer.WriteLine(pseudo);
            }
            writer.Close();
        }

        static void WriteToConsole(List<string> pseudo)
        {
            Console.Clear();
            foreach (var item in pseudo)
            {
                Console.WriteLine(item);
            }
        }
        #endregion

        static void Main()
        {
            List<string> file = new List<string>(4);
            int val = Read();
            if (val == -1) return;
            if (val == 1)
                file = ReadFromFile(file);
            else if (val == 2)
                file = ReadFromConsole(file);
            Console.ReadKey();

            RULines(file);
            Console.ReadKey();

            val = Write();
            if (val == -1) return;
            if (val == 1)
                WriteToFile(file);
            else if (val == 2)
                WriteToConsole(file);
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Thank you for using this program.\nCredits: Vilmos45");
            Console.ReadKey();
        }
    }
}

/** /
Alma
//körte
if()
lol
//lol
lol

stop
   éldfkgj ; 
lksdafjg nlkjdfn odfishgofd hg jkdf hjkdk lg dsfkh gkljdfh gjkf hkj dkjfg hu8i93475 896 iuwreéergßĐ[l];í$dsfofhg hg dfh ;
"oiuh"-


/**/