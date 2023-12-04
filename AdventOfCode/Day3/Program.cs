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
            long result2 = 0;
            result2 += ProcessLine2(input);
            Console.WriteLine("Result Puzzle 2: " + result2);

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
        MatchCollection matches = regexNum.Matches(possible);
        int found = 0;
        if(matches.Count > 0)
        {
                foreach (Match match in matches)
                {
                   bool foundValidNum = false;
                   switch(match.Length)
                    {
                        case 1:
                            if(match.Index>1 && match.Index<5) foundValidNum = true;
                            break;
                        case 2:
                            if(match.Index>0 && match.Index<5) foundValidNum = true;
                            break;
                        case 3:
                            foundValidNum = true;
                            break;
                    }
                    if(foundValidNum)
                    {
                        numbers.Add(int.Parse(match.Value));
                        found += 1;
                    }

                }
        }

        return found;
    }

    private static long ProcessLine2(Dictionary<int, string> input)
    {
        long result = 0;

        for(int i = 1; i <= input.Count; i++)
        {
            Regex regex = new Regex("\\*");
            MatchCollection matches = regex.Matches(input[i]);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    long product = 0;
                    List<int> numbers = new List<int>();
                    int found = FindNumber(input[i-1].Substring(match.Index-1,3), input[i-1].Substring(match.Index-3,7), ref numbers);
                    found += FindNumber(input[i].Substring(match.Index-1,3), input[i].Substring(match.Index-3,7), ref numbers);
                    found += FindNumber(input[i+1].Substring(match.Index-1,3), input[i+1].Substring(match.Index-3,7), ref numbers);

                    if (found==2)
                    { 
                        product = numbers[0]*numbers[1];
                    }
                    result += product;
                }
            }
        }

        return result;

    }

}