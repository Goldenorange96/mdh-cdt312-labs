using System;
using System.Collections.Generic;

namespace cdt312_assign_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Path> allPaths = new List<Path>();
            List<City> allCities = new List<City>();
            ReadFileAndGenerateLists(allPaths, allCities);
            PrintPathList(allPaths);
            PrintCityList(allCities);
            Console.ReadKey();
        }

        static void ReadFileAndGenerateLists(List<Path> allPaths, List<City> allCities)
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

        static void PrintPathList(List<Path> listToPrint)
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

        static List<Node> GetSuccessors(Node parent, List<Item> allItems)
        {
            List<Node> successors = new List<Node>();
            for (int i = 0; i < allItems.Count; i++)
            {
                if ((allItems[i].itemNo != parent.action.itemNo) && !(parent.actions.Contains(allItems[i])))
                {
                    if (parent.weight + allItems[i].itemWeight < 420)
                    {
                        successors.Add(CreateChildNode(parent, allItems[i]));
                    }
                }
            }
            return successors;
        }

        static List<Node> GreedyBestFirstSearch(List<Path> allPaths, List<City> allCities)
        {
            List<Node> solution = new List<Node>();
            Node currentBest = new Node();
            Node parent = new Node();
            Stack<Node> frontier = new Stack<Node>();
            List<Node> successors = new List<Node>();
            while (frontier.Count > 0)
            {
                parent = frontier.Pop();
                if(parent.)

                //if (parent.action.itemNo != 0)
                //{
                //    if ((parent.weight < knapsackLimit) && (parent.benefit > currentBest.benefit))
                //    {
                //        currentBest = parent;
                //    }
                //    foreach (Node successor in GetSuccessors(parent, items))
                //    {
                //        frontier.Push(successor);
                //    }
                //}
                //else
                //{
                //    foreach (Node successor in GetSuccessors(parent, items))
                //    {
                //        frontier.Push(successor);
                //    }
                //}
            }
            return solution;


            return solution;
        }

        static List<Node> AStarSearch(List<Node> allPaths, List<City> allCities)
        {
            List<Node> solution = new List<Node>();
            return solution;
        }
    }
}
