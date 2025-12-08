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


        private static readonly Dictionary<string, string> hunif = new Dictionary<string, string>()
        {
            { "if", "Ha" },//ifelses
            { "else", "Különben" },
            { "while", "Amíg" },//loop
            { "foreach", "Ciklus minden lemere" },
            { "for", "Ciklus" },
            { "static void", "Eljárás" },
            { "static", "Függvény" }
        };

        #endregion

        #region Read
        private static int Read()
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
        private static void RULines(List<string> file)
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

        private static List<string> Convert(List<string> file, int e = 0)
        {
            Console.Clear();
            Console.WriteLine("Wich language do you want your pseudo code to be translated?");
            Console.Write("hun/eng: ");
            List<string> code = new List<string>(file.Count);
            string row;
            if (Console.ReadLine().Trim().ToLower() == "eng")
            {
                Console.WriteLine("Converting to english\nStarted to convert...");
                for (int i = e; i < file.Count; i++)
                {
                    row = file[i].Trim();
                    foreach (var item in hunif)
                    {
                        if (!row.Contains(item.Key))
                        {
                            foreach (var itm in hun)
                                file[i] = file[i].Replace(itm.Key, itm.Value);
                            break;
                        }
                    }
                    code.Add(file[i]);

                }
            }
            else
            {
                Console.WriteLine("Converting to hungarian\nStarted to convert...");
                for (int i = e; i < file.Count; i++)
                {
                    row = file[i].Trim();

                    foreach (var item in hunif)
                    {
                        if (!row.Contains(item.Key))
                        {
                            foreach (var itm in hun)
                                file[i] = file[i].Replace(itm.Key, itm.Value);
                            break;
                        }
                    }
                    code.Add(file[i]);
                }
            }
            Console.WriteLine("Converting succesfully done");
            return code;
        }

        #endregion

        #region Write
        private static int Write()
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

        private static void WriteToFile(List<string> pseudo, string path = "../../Pseudocode/output.txt")
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
            Console.Clear();
            Console.WriteLine("Your code in pseudo code:\n\n");
            foreach (var item in pseudo)
                Console.WriteLine(item);
        }
        #endregion

        private static void Main()
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

            file = Convert(file);
            Console.ReadKey();

            val = Write();
            if (val == -1) return; // Enviroment.exit(0);
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