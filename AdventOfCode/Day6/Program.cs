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
            int[,] input = new int[,]{
                                {56, 334},
                                {71, 1135},
                                {79, 1350},  
                                {99, 2430}}; 

            long timeRace = 0;
            long recordDist = 0;
            long wins = 0;
            long result = 1;
            for (long i = 0; i < input.GetLength(0); i++)
            {

                timeRace = input[i,0];
                recordDist = input[i,1];
                wins = 0;

                for(long hold = 1; hold < timeRace; hold++)
                {
                    long dist = (timeRace - hold) * hold;
                    if(dist > recordDist) wins++;
                }

                result *= wins;

            }
            Console.WriteLine("Puzzle 1: " + result.ToString());

            // Puzzle 2
            long result2 = 1;
            timeRace = 56717999;
            recordDist = 334113513502430;
            wins = 0;

            for(long hold = 1; hold < timeRace; hold++)
            {
                long dist = (timeRace - hold) * hold;
                if(dist > recordDist) wins++;
            }

            result2 = wins;
            Console.WriteLine("Puzzle 2: " + result2.ToString());

        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }

}