using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Converter
{
    internal class Program
    {
        #region Dictionary
        private static readonly Dictionary<string, string> eng = new Dictionary<string, string>()
        {
            { "if", "IF" },
            { "else", "ELSE" },
            { "while", "WHILE" },
            { "for", "FOR" },
            { "true", "TRUE" },
            { "false", "FALSE" },
        };

        private static readonly Dictionary<string, string> hun = new Dictionary<string, string>()
        {
            // Metódusok
            { "static void", "Eljárás" },
            { "void", "Eljárás" },
            { "static", "" },

            // Típusok
            { "int", "Egész:" },
            { "bool", "Logikai:" },
            { "string", "Szöveg:" },
            { "List<", "Lista<" },

            // Logikai literalok
            { "true", "Igaz" },
            { "false", "Hamis" },

            // Kontroll szerkezetek
            { "if", "Ha" },
            { "else", "Különben" },
            { "while", "Amíg" },
            { "for", "Ciklus" },
            { "foreach", "Ciklus minden elemre" },

            // Operátorok
            { "==", "=" },
            { "!=", "≠" },
            { "=", ":=" },
            { "=>", "->" },

            // Gyakoribb metódus-hívások C#-ból → pszeudokód
            { ".Add(", ".Hozzáad(" },
            { ".Remove(", ".Töröl(" },
            { ".RemoveAt(", ".TörölIndexnél(" },
            { ".Replace(", ".Helyettesít(" },
            { ".Contains(", ".Tartalmaz(" },
            { ".Count", ".Hossz" },
            { ".Length", ".Hossz" },
            { ".Sort(", ".Rendez(" },
            { ".ToString(", ".Szöveggé(" },
            //.TRIM, cw

            // Egyéb gyakori C# elemek
            { "return", "Visszaad: " },
            { "new ", "Új " },
            { "break", "Törés" },

            // Blokkok
            { "{", "" },
            { "}", "Vége" },
        };
        #endregion

        #region Read
        private static int ReadOptions()
        {
            Console.Clear();
            Console.WriteLine("1: Read from file\n2: Read from console\nq: exit");

            string inp = Console.ReadLine().Trim().ToLower();

            if (inp == "q")
                return -1;

            if (int.TryParse(inp, out int val) && (val < 3 && -2 < val))
                return val;

            Console.WriteLine("Invalid command!");
            return 0;
        }


        private static List<string> ReadFromFile(List<string> file)
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

        private static List<string> ReadFromConsole(List<string> file)
        {
            Console.Clear();
            Console.WriteLine("Paste here a working c# code\nAt the end of it write 'STOP':\n");
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
        private static void RULines(List<string> file, bool comments = false)
        { //Remove Unnecesary and Unused Lines
            Console.Clear();
            if (!comments)
            {
                Console.WriteLine("Do you want comments in your pseudo code? (Y/n):");
                if (Console.ReadLine().ToLower().Trim() == "y")
                    comments = true;
            }
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
            }
            Console.WriteLine("Removing succesfully done");
        }

        private static List<string> Convert(List<string> file, int e = 0)
        {
            RULines(file);
            Console.Clear();
            Console.WriteLine("Which language do you want your pseudo code to be translated?");
            Console.Write("hun/eng: ");
            string mode = Console.ReadLine().Trim().ToLower();
            Dictionary<string, string> table;
            List<string> code = new List<string>(file.Count);

            if (mode == "eng")
            {
                Console.WriteLine("Converting to English...");
                table = eng;
            }
            else
            {
                Console.WriteLine("Converting to Hungarian...");
                table = hun;
            }
            for (int i = e; i < file.Count; i++)
            {
                string line = file[i];
                foreach (var itm in table)
                    line = line.Replace(itm.Key, itm.Value);

                code.Add(line);
            }
            Console.WriteLine("Conversion done.");
            Console.ReadKey();
            RULines(code, false);
            return code;
        }


        #endregion

        #region Write
        private static int WriteOptions()
        {
            Console.Clear();
            Console.WriteLine("1: Write to file\n2: Write to console\nq: exit");
            string inp = Console.ReadLine().Trim().ToLower();
            if (inp == "q")
                return -1;
            if (int.TryParse(inp, out int val) && (val < 3 && -2 < val))
                return val;

            Console.WriteLine("Invalid command!");
            return 0;
        }

        private static void WriteToFile(List<string> pseudo, string path = "../../output.txt")
        {
            Console.Clear();
            Console.WriteLine("The file with the pseudo code will be in the pseudocode folder\nTo the following path: " + path + "\n\n");
            StreamWriter writer = new StreamWriter(path);
            foreach (var item in pseudo)
            {
                writer.WriteLine(pseudo);
            }
            writer.Close();
        }

        private static void WriteToConsole(List<string> pseudo)
        {
            Console.WriteLine();
            Console.Clear();
            Console.WriteLine($"Your new code is {pseudo.Count} lines\nYour code in pseudo code:\n\n");
            foreach (string item in pseudo)
            {
                Console.WriteLine(item);
            }
        }
        #endregion

        static List<string> Read(List<string> file)
        {
            int val;
            do
            {
                val = ReadOptions();
                if (val == -1)
                    Environment.Exit(0);
                if (val == 1)
                    return ReadFromFile(file);
                if (val == 2)
                    return ReadFromConsole(file);
                Console.ReadKey();
            } while (val == 0);
            return file;
        }

        static void Write(List<string> file)
        {
            int val;
            do
            {
                val = WriteOptions();
                if (val == -1)
                    Environment.Exit(0);
                Console.ReadKey();
                if (val == 1)
                    WriteToFile(file);
                else if (val == 2)
                    WriteToConsole(file);
            } while (val == 0);
            Console.ReadKey();
        }

        private static void Main()
        {
            List<string> file = new List<string>();

            file = Read(file);
            file = Convert(file);
            Write(file);

            Console.Clear();
            Console.WriteLine("Thank you for using this program.\nCredits: Vilmos45");
            Console.ReadKey();
        }
    }
}