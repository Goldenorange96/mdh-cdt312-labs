namespace Cdt312_assign_6
{
    using System.Collections.Generic;
    using System;
    class Program
    {
        static void Main(string[] args)
        {
            int noRuns = 4;
            int maxIterations = 20;
            List<City> allCities = new List<City>();
            List<City> bestPath = new List<City>();
            Utils.ReadFileAndGenerateList(ref allCities);
            for (var i = 0; i < noRuns; i++)
            {
                ACOAlgorithm algorithm = new ACOAlgorithm(20, allCities, 0.03);
                algorithm.Run(allCities, maxIterations);
            }
            Console.ReadKey();
        }
    }
}
