using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

class Program
{

        private const string red = "red";
        private const string green = "green";
        private const string blue = "blue";

    public static void Main()
    {
        try
        {

            Dictionary<string, int> maxCubes = new Dictionary<string, int>()
            {
                { red, 12 },
                { green, 13 },
                { blue, 14 }
            };

            // Puzzle 1
            using var sr = new StreamReader("..\\..\\..\\inputPuzzle.txt");
            String ?line;
            int result = 0;
            while ((line = sr.ReadLine()) != null)
            {
                result += ProcessLine1(line, maxCubes);
            }
            Console.WriteLine("Result Puzzle 1: " + result.ToString());

            // Puzzle 2
            using var sr2 = new StreamReader("..\\..\\..\\inputPuzzle.txt");
            String ?line2;
            int result2 = 0;
            while ((line2 = sr2.ReadLine()) != null)
            {
                result2 += ProcessLine2(line2);
            }
            Console.WriteLine("Result Puzzle 2: " + result2.ToString());

        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }

    private static int ProcessLine1(string input, Dictionary<string, int> max)
    {

        var game = input.Split(new string[] { ": " }, StringSplitOptions.None);

        var sets = game[1].Split(new string[] { "; " }, StringSplitOptions.None);
        foreach(string set in sets)
        {
            var cubes = set.Split(new string[] { ", " }, StringSplitOptions.None);
            Dictionary<string, int> revealed = new Dictionary<string, int>()
            {
                { red, 0 },
                { green, 0 },
                { blue, 0 }
            };

            foreach(string cube in cubes)
            {
                var numCubes = cube.Split(new string[] { " " }, StringSplitOptions.None);
                revealed[numCubes[1]] = int.Parse(numCubes[0]);
            }
            if(revealed[red] > max[red] || revealed[green] > max[green] || revealed[blue] > max[blue] )
                return 0;
        }

        return int.Parse(Regex.Replace(game[0], "Game ", ""));

    }

    private static int ProcessLine2(string input)
    {

        var game = input.Split(new string[] { ": " }, StringSplitOptions.None);
        int result = 0;

        var sets = game[1].Split(new string[] { "; " }, StringSplitOptions.None);
        Dictionary<string, int> minimalSet = new Dictionary<string, int>()
        {
            { red, 0 },
            { green, 0 },
            { blue, 0 }
        };
        foreach(string set in sets)
        {
            var cubes = set.Split(new string[] { ", " }, StringSplitOptions.None);
            foreach(string cube in cubes)
            {
                var numCubes = cube.Split(new string[] { " " }, StringSplitOptions.None);
                if(minimalSet[numCubes[1]] < int.Parse(numCubes[0]))
                    minimalSet[numCubes[1]] = int.Parse(numCubes[0]);
            }
        }

        result = (minimalSet[red] > 0) ? minimalSet[red] : 0;
        result = (minimalSet[green] > 0 && result > 0) ? result * minimalSet[green] : minimalSet[green];
        result = (minimalSet[blue] > 0 && result > 0) ? result * minimalSet[blue] : minimalSet[blue];

        return result;

    }



}