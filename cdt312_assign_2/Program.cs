using System;
using System.Collections.Generic;
using System.Linq;

namespace cdt312_assign_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var allPaths = new List<Path>();
            var allCities = new List<Node>();
            ReadFileAndGenerateLists(allPaths, allCities);
            //PrintPathList(allPaths);
            //PrintCityList(allCities);
            Console.WriteLine("***GREEDY BEST FIRST SEARCH***");
            var solutionGreedy = new List<Node>(GreedyBestFirstSearch(allPaths, allCities));
            PrintCityList(solutionGreedy);
            Console.WriteLine("***A-STAR SEARCH***");
            var solutionAStar = new List<Node>(AStarSearch(allPaths, allCities));
            PrintCityList(solutionAStar);
            Console.ReadKey();
        }

        static void ReadFileAndGenerateLists(List<Path> allPaths, List<Node> allCities)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:.\Spain_map.txt");
            string line = null;
            string[] subStrings;
            int pathCost = 0;
            while ((line = file.ReadLine()) != null)
            {
                subStrings = line.Split(null);
                //generate cities
                if ((subStrings[0].Length > 0) && (int.TryParse(subStrings[1], out pathCost)))
                {
                    if (!(subStrings[0] == "COMMENT:"))
                    {
                        List<Node> visitedCities = new List<Node>();
                        Node newCity = new Node(null, visitedCities, 0, subStrings[0], pathCost, 0);
                        allCities.Add(newCity);
                    }
                   
                }
                //generate paths
                else if ((subStrings[0].Length > 0) && (int.TryParse(subStrings[2], out pathCost)))
                {
                    Path newPath = new Path(subStrings[0], subStrings[1], pathCost);
                    allPaths.Add(newPath);
                }
            }
            file.Close();
        }

        static void PrintCityList(List<Node> listToPrint)
        {
            if (listToPrint == null)
            {
                Console.WriteLine("List sent was null, cannot print list!");
                return;
            }

            Console.WriteLine("<----------------------->");

            foreach (Node city in listToPrint)
                Console.WriteLine("|Name: {0} Dist: {1}", city.cityName, city.straightDist);

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

            foreach (Path path in listToPrint)
                Console.WriteLine("|A: {0} B: {1} Cost: {2}", path.cityA, path.cityB, path.pathCost);

            Console.WriteLine("<----------------------->");
        }

        static List<Node> GetSuccessorsGBFS(Node current, List<Path> allPaths, List<Node> allCities)
        {
            var successors = new List<Node>();
            var availablePaths = new List<Path>();
            var index = 0;
            for (var i = 0; i < allPaths.Count; i++)
            {
                if (allPaths[i].cityA == current.cityName || allPaths[i].cityB == current.cityName)
                {
                    availablePaths.Add(allPaths[i]);
                }
            }
            for (var i = 0; i < availablePaths.Count; i++)
            {
                index = allCities.FindIndex(city => city.cityName == availablePaths[i].cityB);
                if(index > -1)
                {
                    successors.Add(CreateChildNode(current, allCities[index], availablePaths[i], 0));
                }
            }
            for (var i = 0; i < availablePaths.Count; i++)
            {
                index = allCities.FindIndex(city => city.cityName == availablePaths[i].cityA);
                if (index > -1)
                {
                    successors.Add(CreateChildNode(current, allCities[index], availablePaths[i], 0));
                }
            }
            return successors;
        }

        static List<Node> GetSuccessorsAStar(Node current, List<Path> allPaths, List<Node> allCities)
        {
            var successors = new List<Node>();
            var availablePaths = new List<Path>();
            var index = 0;
            for (var i = 0; i < allPaths.Count; i++)
            {
                if (allPaths[i].cityA == current.cityName || allPaths[i].cityB == current.cityName)
                {
                    availablePaths.Add(allPaths[i]);
                }
            }
            for (var i = 0; i < availablePaths.Count; i++)
            {
                index = allCities.FindIndex(city => city.cityName == availablePaths[i].cityB);
                if (index > -1)
                {
                    successors.Add(CreateChildNode(current, allCities[index], availablePaths[i], 1));
                }
            }
            for (var i = 0; i < availablePaths.Count; i++)
            {
                index = allCities.FindIndex(city => city.cityName == availablePaths[i].cityA);
                if (index > -1)
                {
                    successors.Add(CreateChildNode(current, allCities[index], availablePaths[i], 1));
                }
            }
            return successors;
        }

        static bool IsGoalState(Node current, string goalCity)
        {
            return (current.cityName.Equals(goalCity));
        }

        static Node CreateChildNode(Node current, Node city, Path currentPath, int heuristicType)
        {
            var newVisitedCitiesList = new List<Node>(current.visitedCities);
            newVisitedCitiesList.Add(current);
            var costEst = CalculateCostEstimation(heuristicType, city.straightDist, current.currentPathCost, currentPath.pathCost);
            var newNode = new Node(current, newVisitedCitiesList, current.currentPathCost + currentPath.pathCost, city.cityName, city.straightDist, costEst);
            return newNode;
        }

        //SLD - Straight Line Distance
        //CPC - Current  Path cost
        //PC - Path cost
        static int CalculateCostEstimation(int type, int SLD, int CTPC, int PC)
        {
            if (type == 0)
            {
                return SLD;
            }
            else
            {
                return ((CTPC + PC) + SLD);
            }
        }

        static List<Node> GreedyBestFirstSearch(List<Path> allPaths, List<Node> allCities)
        {
            var current = new Node();
            var frontier = InitList();
            var successors = new List<Node>();
            while (frontier.Count > 0)
            {
                current = frontier[0];
                frontier.RemoveAt(0);
                if (IsGoalState(current, "Valladolid"))
                {
                    current.visitedCities.Add(current);
                    break;
                }
                successors = GetSuccessorsGBFS(current, allPaths, allCities);
                for (int i = 0; i < successors.Count; i++)
                {
                        frontier.Add(successors[i]);
                }
                frontier = frontier.OrderBy(node => node.priority).ToList();
            }
            return current.visitedCities;
        }

        static List<Node> InitList()
        {
            var initialVisitedList = new List<Node>();
            var initialNode = new Node(null, initialVisitedList, 0, "Malaga", 162, 0);
            initialVisitedList.Add(initialNode);
            return initialVisitedList;
        }

        static List<Node> AStarSearch(List<Path> allPaths, List<Node> allCities)
        {
            var current = new Node();
            var frontier = InitList();
            var successors = new List<Node>();
            while (frontier.Count > 0)
            {
                current = frontier[0];
                frontier.RemoveAt(0);
                if (IsGoalState(current, "Valladolid"))
                {
                    current.visitedCities.Add(current);
                    break;
                }
                successors = GetSuccessorsAStar(current, allPaths, allCities);
                for (int i = 0; i < successors.Count; i++)
                {
                    frontier.Add(successors[i]);
                }
                frontier = frontier.OrderBy(node => node.priority).ToList();
            }
            return current.visitedCities;
        }
    }
}
