using System;
using System.Collections.Generic;

namespace cdt312_assign_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Node> allPaths = new List<Node>();
            List<City> allCities = new List<City>();
            ReadFileAndGenerateLists(allPaths, allCities);
            PrintPathList(allPaths);
            PrintCityList(allCities);
            Console.ReadKey();
        }

        static void ReadFileAndGenerateLists(List<Node> allPaths, List<City> allCities)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:.\Spain_map.txt");
            string line = null;
            string[] subStrings;
            int pathCost = 0;
            while ((line = file.ReadLine()) != null)
            {
                subStrings = line.Split(null);
                if ((subStrings[0].Length > 0) && (int.TryParse(subStrings[1], out pathCost)))
                {
                    City newCity = new City(subStrings[0], pathCost);
                    allCities.Add(newCity);
                }
                else if ((subStrings[0].Length > 0) && (int.TryParse(subStrings[2], out pathCost)))
                {
                    Node newPath = new Node(subStrings[0], subStrings[1], pathCost);
                    allPaths.Add(newPath);
                }
            }
            file.Close();
        }

        static void PrintCityList(List<City> listToPrint)
        {
            if (listToPrint == null)
            {
                Console.WriteLine("List sent was null, cannot print list!");
                return;
            }

            Console.WriteLine("<----------------------->");

            foreach (City city in listToPrint)
                Console.WriteLine("|Name: {0} Dist: {1}|", city.name, city.straightDist);

            Console.WriteLine("<----------------------->");
        }

        static void PrintPathList(List<Node> listToPrint)
        {
            if (listToPrint == null)
            {
                Console.WriteLine("List sent was null, cannot print list!");
                return;
            }

            Console.WriteLine("<----------------------->");

            foreach (Node node in listToPrint)
                Console.WriteLine("|From: {0} To: {1} Cost: {2}|", node.fromCity, node.toCity, node.pathCost);

            Console.WriteLine("<----------------------->");
        }
    }
}
