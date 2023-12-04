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
            List<string> cards = new List<string>();
            Dictionary<int,int> repeat = new Dictionary<int, int>();
            int result = 0;

            while ((line = sr.ReadLine()) != null)
            {
                result += ProcessLine1(line);
                cards.Add(line);
            }

            Console.WriteLine("Result Puzzle 1: " + result.ToString());

            int result2 = 0;
            foreach(string s in cards)
            {
                result2 += ProcessLine2(s, ref repeat);
            }

            Console.WriteLine("Result Puzzle 2: " + result2.ToString());


        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }

    private static int ProcessLine1(string line)
    {
        int result = 0;

        line = line.Replace("  ", " ");

        var game = line.Split(new string[] { ":" }, StringSplitOptions.None);
        var cards = game[1].Trim().Split(new string[] { "|" }, StringSplitOptions.None);
        var winning = cards[0].Trim().Split(new string[] { " " }, StringSplitOptions.None);
        var numbers = cards[1].Trim().Split(new string[] { " " }, StringSplitOptions.None);

        List<int> winningNumbers = new List<int>();
        foreach(string s in winning) winningNumbers.Add(int.Parse(s));
        foreach(string s in numbers)
        {
            if(winningNumbers.Contains(int.Parse(s)))
            {
                result = (result == 0) ? 1 : result*2;
            }
        } 

        return result;       
    }

    private static int ProcessLine2(string line, ref Dictionary<int,int> repeat)
    {
        int counter = 0;

        line = line.Replace("   ", " ");
        line = line.Replace("  ", " ");

        var game = line.Split(new string[] { ":" }, StringSplitOptions.None);
        int gameId = int.Parse(game[0].Trim().Split(new string[] { " " }, StringSplitOptions.None)[1]);
        var cards = game[1].Trim().Split(new string[] { "|" }, StringSplitOptions.None);
        var winning = cards[0].Trim().Split(new string[] { " " }, StringSplitOptions.None);
        var numbers = cards[1].Trim().Split(new string[] { " " }, StringSplitOptions.None);

        List<int> winningNumbers = new List<int>();
        foreach(string s in winning) winningNumbers.Add(int.Parse(s));

        // Count winning numbers
        foreach(string s in numbers)
        {
            if(winningNumbers.Contains(int.Parse(s))) counter += 1;
        } 

        // set copies of cards for each winning number
        int repeater = 0;
        if(repeat.ContainsKey(gameId)) repeater = repeat[gameId];
        for(int i = 1; i <= counter; i++)
        {
            if(repeat.ContainsKey(gameId+i)) { repeat[gameId+i] += 1 + repeater;} else { repeat.Add(gameId+i, 1 + repeater);}
        }

        return repeater + 1 ;       

    }



}