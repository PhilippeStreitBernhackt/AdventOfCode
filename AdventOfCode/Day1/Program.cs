using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;

class Program
{
    public static void Main()
    {
        try
        {
            // Puzzle 1
            using var sr = new StreamReader("..\\..\\..\\inputPuzzle1.txt");
            String ?line;
            int result = 0;
            while ((line = sr.ReadLine()) != null)
            {
                result += int.Parse(ProcessLine(line));
            }
            Console.WriteLine("Result Puzzle 1: " + result.ToString());

            // Puzzle 2
            using var sr2 = new StreamReader("..\\..\\..\\inputPuzzle2.txt");
            String ?line2;
            int result2 = 0;
            while ((line2 = sr2.ReadLine()) != null)
            {
                result2 += int.Parse(ProcessLine2(line2));
            }
            Console.WriteLine("Result Puzzle 2: " + result2.ToString());

        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }

    private static string ProcessLine(string input)
    {
        bool firstfound = false;
        string first = "";
        string last = "";

        foreach (char c in input)
        {
            if(c>='0' && c<='9')
            {
                if(!firstfound)
                {
                    firstfound = true;
                    first = c.ToString();
                }
                last = c.ToString();
            }
        }
        return first + last;
    }

    private static string ProcessLine2(string input)
    {
        bool firstfound = false;
        string first = "";
        string last = "";
        string line = "";

        line = Regex.Replace(input, "one", "on1e");
        line = Regex.Replace(line, "two", "tw2o");
        line = Regex.Replace(line, "three", "thre3e");
        line = Regex.Replace(line, "four", "fou4r");
        line = Regex.Replace(line, "five", "fiv5e");
        line = Regex.Replace(line, "six", "si6x");
        line = Regex.Replace(line, "seven", "sev7n");
        line = Regex.Replace(line, "eight", "eig8t");
        line = Regex.Replace(line, "nine", "nin9e");

        foreach (char c in line)
        {
            if(c>='0' && c<='9')
            {
                if(!firstfound)
                {
                    firstfound = true;
                    first = c.ToString();
                }
                last = c.ToString();
            }
        }
        return first + last;
    }

}