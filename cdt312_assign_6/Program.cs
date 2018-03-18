namespace Cdt312_assign_6
{
    using System.Collections.Generic;
    class Program
    {
        static void Main(string[] args)
        {
            int globalBestIdx = 0, iterationBestIdx = 0;
            int maxIterations = 100;
            List<City> allCities = new List<City>();
            Utils.ReadFileAndGenerateList(ref allCities);
            ACOAlgorithm algorithm = new ACOAlgorithm(allCities.Count, 25, allCities, 0.10);
            for (int i = 0; i < maxIterations; i++)
            {
                for (int j = 0; j < allCities.Count; j++)
                {
                    for (int k = 0; k < algorithm.NoAnts; k++)
                    {
                        algorithm.Ants[k].Transition(ACOAlgorithm.Phermones, ACOAlgorithm.Heuristics, allCities);
                    }
                }
                for (int j = 0; j < algorithm.NoAnts; j++)
                {
                    algorithm.Ants[i].CalculateCost();
                    iterationBestIdx = algorithm.FindBest();
                }

                for (int j = 0; j < allCities.Count-1; j++)
                {
                    for (int k = 0; k < allCities.Count - 1; k++)
                    {
                        ACOAlgorithm.Phermones[j, k] = ;
                    }

                }

                if (algorithm.Ants[iterationBestIdx].Cost < algorithm.Ants[globalBestIdx].Cost)
                {
                    globalBestIdx = iterationBestIdx;
                }
            }
        }
    }
}
