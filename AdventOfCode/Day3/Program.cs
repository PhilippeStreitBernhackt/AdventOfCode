using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
            int count = 0;
            Dictionary<int, string> input = new Dictionary<int, string>();
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Replace(".", "x");
                input.Add(++count, line);
            }

            result = ProcessLine1(input);
            Console.WriteLine("Result Puzzle 1: " + result.ToString());

            // Puzzle 2
            char[][] map = new char[input.Count][];
            for(int i = 1; i <= input.Count; i++)
            {
                char[] a = input[i].ToCharArray();
                map[i-1] = a;
            }

            int result2 = 0;
            result2 += ProcessLine2(map, input);
            Console.WriteLine("Result Puzzle 2: " + result2.ToString());

        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }

    private static int ProcessLine1(Dictionary<int, string> input)
    {
        int result = 0;
        foreach(KeyValuePair<int, string> kvp in input)
        {
            Regex regex = new Regex("\\d+");
            MatchCollection matches = regex.Matches(kvp.Value);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    int start = (match.Index > 0) ? start = match.Index - 1 : start = 0;
                    int len = (match.Index + match.Value.Length < kvp.Value.Length && match.Index > 0) ? match.Value.Length + 2 : match.Value.Length + 1;
                    bool found = HasSpecialChars(kvp.Value.Substring(start, len));
                    if (kvp.Key > 1 &! found) found = HasSpecialChars(input[kvp.Key-1].Substring(start, len));
                    if (kvp.Key < input.Count &! found) found = HasSpecialChars(input[kvp.Key+1].Substring(start, len));

                    if(found) result += int.Parse(match.Value);
                }
            }
            else
            {
                Console.WriteLine("Keine Zahlen gefunden.");
            }
        } 
        return result;       
    }

    private static bool HasSpecialChars(string check){
        return check.Any(ch => ! char.IsLetterOrDigit(ch));
    }

    private static int FindNumber(string s, string possible, ref List<int> numbers)
    {
        Regex regexNum = new Regex("\\d+");
        MatchCollection matches = regexNum.Matches(s);
        if(matches > 0)
        {
                foreach (Match match in matches)
                {
                    switch(match.Length)
                    {
                        case 1:
                        case 2:
                        case default:
                    }
                }
        }

        return matches.Count;
    }

    private static int ProcessLine2(char[][] map, Dictionary<int, string> input)
    {
        for(int i = 1; i <= input.Count; i++)
        {
            Regex regex = new Regex("\\*");
            MatchCollection matches = regex.Matches(input[i]);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    List<int> numbers = new List<int>();
                    int foundL1 = FindNumber(input[i-1].Substring(match.Index-1,3), input[i-1].Substring(match.Index-3,7), ref numbers);
                    int foundL2 = FindNumber(input[i].Substring(match.Index-1,3), input[i].Substring(match.Index-3,7), ref numbers);
                    int foundL3 = FindNumber(input[i+1].Substring(match.Index-1,3), input[i+1].Substring(match.Index-3,7), ref numbers);

                    if (foundL1+foundL2+foundL3==3)
                    {

                        Console.WriteLine(foundL1.ToString() + foundL2.ToString() + foundL3.ToString());

                        for(int y = i - 2; y <= i; y++)
                        {
                            for(int x = match.Index - 3; x <= match.Index + 3; x++)
                            {
                                //Console.Write(map[y][x]);
                            }
                            //Console.WriteLine();
                        }
                        //Console.WriteLine("------");

                    }
                }
            }
        }

        return 0;

    }

}