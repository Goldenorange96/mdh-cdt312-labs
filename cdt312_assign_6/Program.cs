namespace Cdt312_assign_6
{
    using System.Collections.Generic;
    using System;
    class Program
    {
        static void Main(string[] args)
        {
            int iterationBestIdx = 0;
            int maxIterations = 10;
            double bestCost = 0.0;
            List<City> allCities = new List<City>();
            List<City> bestPath = new List<City>();
            Utils.ReadFileAndGenerateList(ref allCities);
            ACOAlgorithm algorithm = new ACOAlgorithm(allCities.Count, 10, allCities, 0.10);
            for (int i = 0; i < maxIterations; i++)
            {
                for (int j = 0; j < allCities.Count - 2; j++)
                {
                    for (int k = 0; k < algorithm.NoAnts; k++)
                    {
                        algorithm.Ants[k].Transition(ACOAlgorithm.Phermones, ACOAlgorithm.Heuristics, allCities);
                    }
                }

                for (int j = 0; j < algorithm.NoAnts; j++)
                {
                    algorithm.Ants[j].Visited.Add(allCities[0]);
                }

                for (int j = 0; j < algorithm.NoAnts; j++)
                {
                    algorithm.Ants[j].CalculateCost();
                    iterationBestIdx = algorithm.FindBest();
                }

                for (int j = 0; j < ACOAlgorithm.Phermones.GetLength(0); j++)
                {
                    for (int k = 0; k < ACOAlgorithm.Phermones.GetLength(1); k++)
                    {
                        algorithm.UpdatePheromones(j, k);
                    }
                }
                if (bestCost != 0.0)
                {
                    if (algorithm.Ants[iterationBestIdx].Cost < bestCost)
                    {
                        bestCost = algorithm.Ants[iterationBestIdx].Cost;
                        bestPath = new List<City>(algorithm.Ants[iterationBestIdx].Visited);
                    }
                }
                else
                {
                    bestCost = algorithm.Ants[iterationBestIdx].Cost;
                    bestPath = new List<City>(algorithm.Ants[iterationBestIdx].Visited);
                }

                for (int j = 0; j < algorithm.Ants.Count-1; j++)
                {
                    algorithm.Ants[j].Visited.Clear();
                    algorithm.Ants[j].Cost = 0.0;
                    algorithm.Ants[j].Current = allCities[0];
                }
            }
            Console.WriteLine("Best path cost found: {0}, after {1} iterations", bestCost, maxIterations);
            Console.ReadKey();
        }
    }
}
