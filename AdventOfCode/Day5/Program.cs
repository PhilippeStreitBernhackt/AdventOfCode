using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    private record Mapping(long Dest, long Source, long Range);
    const string cSeeds = "seeds:";
    const string cSeedToSoilMap = "seed-to-soil map:";
    const string cSoilToFertilizerMap = "soil-to-fertilizer map:";
    const string cFertilizerToWaterMap = "fertilizer-to-water map:";
    const string cWaterToLightMap = "water-to-light map:";
    const string cLightToTemperatureMap = "light-to-temperature map:";
    const string cTemperatureToHumidityMap = "temperature-to-humidity map:";
    const string cHumidityToLocationMap = "humidity-to-location map:";

    static void Main()
    {
        
        string filePath = "..\\..\\..\\inputPuzzle1.txt";
        List<long> seeds = new List<long>();
        List<long> seedsP2 = new List<long>();

        List<Mapping> seedToSoilMap = new List<Mapping>();
        List<Mapping> soilToFertilizerMap = new List<Mapping>();
        List<Mapping> fertilizerToWaterMap = new List<Mapping>();
        List<Mapping> waterToLightMap = new List<Mapping>();
        List<Mapping> lightToTemperatureMap = new List<Mapping>();
        List<Mapping> temperatureToHumidityMap = new List<Mapping>();
        List<Mapping> humidityToLocationMap = new List<Mapping>();

        string[] lines = File.ReadAllLines(filePath);
        string currentList = "";

        foreach (string line in lines)
        {
            if(line != string.Empty){

                if (line.StartsWith(cSeeds))
                {
                    //Puzzle 1
                    currentList = cSeeds;
                    seeds = line.Split(' ').Skip(1).Select(long.Parse).ToList();

                    //Puzzle 2
                    for(int l = 0; l < seeds.Count; l++)
                    {
                        if(l % 2 == 1)
                        {
                            for(long x = seeds[l-1]; x < seeds[l-1] + seeds[l]; x++)
                            {
                                seedsP2.Add(x);
                            }
                        }
                    }
                }
                else if (line.StartsWith(cSeedToSoilMap)) { currentList = cSeedToSoilMap;}
                else if (line.StartsWith(cSoilToFertilizerMap)) { currentList = cSoilToFertilizerMap;}
                else if (line.StartsWith(cFertilizerToWaterMap)) { currentList = cFertilizerToWaterMap;}
                else if (line.StartsWith(cWaterToLightMap)) { currentList = cWaterToLightMap;}
                else if (line.StartsWith(cLightToTemperatureMap)) { currentList = cLightToTemperatureMap;}
                else if (line.StartsWith(cTemperatureToHumidityMap)) { currentList = cTemperatureToHumidityMap;}
                else if (line.StartsWith(cHumidityToLocationMap)) { currentList = cHumidityToLocationMap;}

                else
                {
                    long[] values = line.Split(' ').Select(long.Parse).ToArray();
                    Mapping map = new Mapping(values[0], values[1], values[2]);
                    switch (currentList)
                    {
                        case cSeedToSoilMap:
                            seedToSoilMap.Add(map);
                            break;
                        case cSoilToFertilizerMap:
                            soilToFertilizerMap.Add(map);
                            break;
                        case cFertilizerToWaterMap:
                            fertilizerToWaterMap.Add(map);
                            break;
                        case cWaterToLightMap:
                            waterToLightMap.Add(map);
                            break;
                        case cLightToTemperatureMap:
                            lightToTemperatureMap.Add(map);
                            break;
                        case cTemperatureToHumidityMap:
                            temperatureToHumidityMap.Add(map);
                            break;
                        case cHumidityToLocationMap:
                            humidityToLocationMap.Add(map);
                            break;
                    }
                }
            }
        }
       
        // Puzzle 1
        long lowest = -1;
        foreach (var seed in seeds)
        {
            long i = SourceToDestConverter(seedToSoilMap, seed);
            i = SourceToDestConverter(soilToFertilizerMap, i);
            i = SourceToDestConverter(fertilizerToWaterMap, i);
            i = SourceToDestConverter(waterToLightMap, i);
            i = SourceToDestConverter(lightToTemperatureMap, i);
            i = SourceToDestConverter(temperatureToHumidityMap, i);
            i = SourceToDestConverter(humidityToLocationMap, i);
            if(lowest == -1 || lowest > i) lowest = i;           
        }
        Console.WriteLine("Puzzle 1: " + lowest.ToString());

        // Puzzle 2
        lowest = -1;
        foreach (var seed in seedsP2)
        {
            long i = SourceToDestConverter(seedToSoilMap, seed);
            i = SourceToDestConverter(soilToFertilizerMap, i);
            i = SourceToDestConverter(fertilizerToWaterMap, i);
            i = SourceToDestConverter(waterToLightMap, i);
            i = SourceToDestConverter(lightToTemperatureMap, i);
            i = SourceToDestConverter(temperatureToHumidityMap, i);
            i = SourceToDestConverter(humidityToLocationMap, i);
            if(lowest == -1 || lowest > i) lowest = i;           
        }
        Console.WriteLine("Puzzle 2: " + lowest.ToString());

    }

    private static long SourceToDestConverter(List<Mapping> input, long source)
    {
        long ret = -1;
        foreach(Mapping mapping in input)
        {
            if(source >= mapping.Source && source < mapping.Source + mapping.Range)
            {
                ret = mapping.Dest + (source - mapping.Source);
            }
        }

        if(ret == -1) ret = source;
        return ret;
    }
}





