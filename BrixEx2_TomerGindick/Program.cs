using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrixEx2_TomerGindick
{
    class Program
    {
        private static Dictionary<char, int> _primeNumbersMap = new Dictionary<char, int>
        {
            {'a', 2 }, {'b', 3 }, {'c', 5 }, {'d', 7 }, {'e', 11 }, {'f', 13 }, {'g', 17 }, {'h', 19 }, {'i', 23 }, 
            {'j', 29 }, {'k', 31 }, {'l', 37 }, {'m', 41 }, {'n', 43 }, {'o', 47 }, {'p', 53 }, {'q', 59 }, {'r', 61 }, 
            {'s', 67 }, {'t', 71 }, {'u', 73 }, {'v', 79 }, {'w', 83 }, {'x', 89 }, {'y', 97 }, {'z', 101 },
            {'0', 103 }, {'1', 107 }, {'2', 109 }, {'3', 113 }, {'4', 127 }, {'5', 131 }, {'6', 137 }, {'7', 139 },
            {'8', 149 }, {'9', 151 }
        };

        private static Dictionary<int, string> _stringSignatureMap = new Dictionary<int, string>();

        static void Main(string[] args)
        {
            ReadFile();

            int stringLength = int.Parse(ConfigurationManager.AppSettings.Get("stringLength"));
            Console.WriteLine($"Please write a {stringLength} characters long alphanumerical string:");
            string input = Console.ReadLine();
            int inputSignature = CreateStringSignature(input);

            if (!_stringSignatureMap.ContainsKey(inputSignature))
            {
                Console.WriteLine($"No anagram found for input string {input}");
            }
            else
            {
                Console.WriteLine($"String {_stringSignatureMap[inputSignature]} is an anagram to input string {input}");
            }
        }

        private static void ReadFile()
        {
            Console.WriteLine("Loading file...");
            var stringsFilePath = ConfigurationManager.AppSettings.Get("stringsFileName");
            using (var streamReader = new StreamReader(stringsFilePath))
            {
                string stringLine;
                while ((stringLine = streamReader.ReadLine()) != null)
                {
                    int stringLineSignature = CreateStringSignature(stringLine);
                    if (_stringSignatureMap.ContainsKey(stringLineSignature))
                    {
                        // In an edge case where another word in the file is an anagram of a previous one, 
                        // we override the new one instead of the older one
                        _stringSignatureMap[stringLineSignature] = stringLine;
                    }
                    else
                    {
                        _stringSignatureMap.Add(stringLineSignature, stringLine);
                    }
                }
            }
        }

        private static int CreateStringSignature(string currentStringLine)
        {
            int signature = 1;
            foreach (char c in currentStringLine)
            {
                signature *= _primeNumbersMap[c];
            }
            return signature;
        }
    }
}
