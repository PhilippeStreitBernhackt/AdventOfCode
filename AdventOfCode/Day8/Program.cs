using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{

    private record Values(string Left, string Right);

    static void Main()
    {
        
        string filePath = "..\\..\\..\\inputPuzzle1.txt";
        Dictionary<string, Values> dict = new Dictionary<string, Values>();
        string[] lines = File.ReadAllLines(filePath);

        string commands = lines[0];
        for(int i = 2; i < lines.Length; i++)
        {
            dict.Add(lines[i].Substring(0, 3), new Values(lines[i].Substring(7, 3), lines[i].Substring(12, 3)  ));
        }
        
        Console.WriteLine("Puzzle 1: " + Solve1(commands, dict).ToString());
        Console.WriteLine("Puzzle 2: " + Solve2(commands, dict).ToString());
    }


    private static int Solve1(string commands, Dictionary<string, Values> dict)
    {
        string end = "ZZZ";
        string next = "AAA";

        bool endReached = false;
        int indexCommand = 0;
        int count = 0;
        while(!endReached)
        {   
            next = commands.Substring(indexCommand, 1) == "L" ? dict[next].Left : dict[next].Right;
            indexCommand++;
            count++;
            endReached = (next == end);
            if(indexCommand == commands.Length) indexCommand = 0;
        }

        return count;

    }

    private static long Solve2(string commands, Dictionary<string, Values> dict)
    {

        List<string> next = new List<string>();
        int indexCommand = 0;
        foreach(KeyValuePair<string, Values> x in dict)
        {
            if(x.Key.EndsWith("A")) 
            {
                next.Add(x.Key);
            }
        }

        long[] stepCounter = new long[next.Count];
        for(int i = 0; i < next.Count; i++)
        {
            string current = next[i];
            long count = 0;
            while (!current.EndsWith("Z"))
            {
                current = commands.Substring(indexCommand, 1) == "L" ? dict[current].Left : dict[current].Right;
                count++;
                indexCommand++;
                if(indexCommand == commands.Length) indexCommand = 0;
            }
            stepCounter[i] = count;
        }

        return CalculateLCM(stepCounter);

    }

    private static long FindGCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    
    private static long CalculateLCM(long a, long b)
    {
        return (a / FindGCD(a, b)) * b;
    }

    private static long CalculateLCM(long[] numbers)
    {
        long lcm = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            lcm = CalculateLCM(lcm, numbers[i]);
        }
        return lcm;
    }

}





