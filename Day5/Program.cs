using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    private record Mapping(long OutputStart, long InputStart, long Range);
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
        
        // Pfad zur Datei
        string filePath = "..\\..\\..\\inputPuzzle1.txt";

        // Dictionary für Seeds
        List<long> seeds = new List<long>();

        // Dictionaries für die verschiedenen Mapping-Listen
        List<Mapping> seedToSoilMap = new List<Mapping>();
        List<Mapping> soilToFertilizerMap = new List<Mapping>();
        List<Mapping> fertilizerToWaterMap = new List<Mapping>();
        List<Mapping> waterToLightMap = new List<Mapping>();
        List<Mapping> lightToTemperatureMap = new List<Mapping>();
        List<Mapping> temperatureToHumidityMap = new List<Mapping>();
        List<Mapping> humidityToLocationMap = new List<Mapping>();

        // Einlesen der Datei
        string[] lines = File.ReadAllLines(filePath);

        // Flag, um die entsprechende Liste zu erkennen
        string currentList = "";

        // Iteriere über jede Zeile der Datei
        foreach (string line in lines)
        {
            if(line != string.Empty){

                // Prüfe, ob die Zeile mit einem bekannten Listennamen beginnt
                if (line.StartsWith(seeds))
                {
                    currentList = seeds;
                    // Teile die Zeile bei Leerzeichen, um die Seeds zu extrahieren
                    seeds = line.Split(' ').Skip(1).Select(long.Parse).ToList();
                }
                else if (line.StartsWith(cSeedToSoilMap)) { currentList = cSeedToSoilMap;}
                else if (line.StartsWith(cSoilToFertilizerMap)) { currentList = cSoilToFertilizerMap;}
                else if (line.StartsWith(cFertilizerToWaterMap)) { currentList = cFertilizerToWaterMap;}
                else if (line.StartsWith(cWaterToLightMap)) { currentList = cWaterToLightMap;}
                else if (line.StartsWith(cLightToTemperatureMap)) { currentList = cLightToTemperatureMap;}
                else if (line.StartsWith(cTemperatureToHumidityMap)) { currentList = cTemperatureToHumidityMap;}
                else if (line.StartsWith(cHumidityToLocationMap)) { currentList = cHumidityToLocationMap;}

                // Fülle die entsprechende Liste basierend auf dem aktuellen Flag
                else
                {
                    long[] values = line.Split(' ').Select(long.Parse).ToArray();
                    Mapping map = new Mapping(values[0], values[1], values[2]);
                    switch (currentList)
                    {
                        case cSeedToSoilMap
                            seedToSoilMap.Add(map);
                            break;
                        case cSoilToFertilizerMap
                            soilToFertilizerMap.Add(map);
                            break;
                        case cFertilizerToWaterMap
                            fertilizerToWaterMap.Add(map);
                            break;
                        case cWaterToLightMap
                            waterToLightMap.Add(map);
                            break;
                        case cLightToTemperatureMap
                            cightToTemperatureMap.Add(map);
                            break;
                        case cTemperatureToHumidityMap
                            temperatureToHumidityMap.Add(map);
                            break;
                        case cHumidityToLocationMap
                            humidityToLocationMap.Add(map);
                            break;
                    }
                }
            }
        }

        Console.WriteLine("Seeds:");
        foreach (var seed in seeds)
        {
            Console.Write(seed + " ");
        }
    }
}





