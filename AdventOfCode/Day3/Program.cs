using System.Diagnostics;
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

            /*
            // Puzzle 2


            */

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

    private static int ProcessLine2(string input)
    {

        return 0;

    }



}