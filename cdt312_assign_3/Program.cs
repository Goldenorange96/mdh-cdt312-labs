namespace Cdt312_assign_3
{
    using System;
    using System.Collections.Generic;
    class Program
    {

        public static void Main(string[] args)
        {
            List<City>allCities = new List<City>();
            ReadFileAndGenerateList(ref allCities);
            Population InitialPopulation = InitializePopulation(allCities, allCities.Count);
            //PrintCityList(InitialPopulation);
            Console.ReadKey();
        }
        /*
         * input: reference to a list representing the space.
         * output: reference to a list containing all cities.
         * 
         * Function reads from hardcoded file and generates a Individual list containing file contents
         */
        public static void ReadFileAndGenerateList(ref List<City> allCities)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:.\berlin52.tsp");
            string line = null;
            string[] subStrings;
            int id = 0;
            while ((line = file.ReadLine()) != null)
            {
                subStrings = line.Split(null);
                if (int.TryParse(subStrings[0], out id))
                {
                    City newCity = new City(id, double.Parse(subStrings[1]), double.Parse(subStrings[2]), 0.0);
                    allCities.Add(newCity);
                }

            }
            file.Close();
            allCities.Add(allCities[0]);

        }
        /*
         * input: A Individual list
         * output: none
         * 
         * Function prints the given Individual list including all properties of each item in the list.
         */
        //public static void PrintCityList(List<Individual> listToPrint)
        //{
        //    if (listToPrint != null)
        //    {
        //        Console.WriteLine("<----------------------->");

        //        foreach (Individual Individual in listToPrint)
        //        {
        //            Console.WriteLine("> Id: {0} X: {1} Y: {2} fitness: {3}", Individual.Id, Individual.X, Individual.Y, Individual.fitness);
        //        }

        //        Console.WriteLine("<----------------------->");
        //    }
            
        //}
        /*
         * input: Individual list representing the space of cities
         * output: Individual list randomised. 
         * 
         * Function copies the space list and passes it to shuffleList.
         */
        public static Population InitializePopulation(List<City> space, int populationSize)
        {
            List<Individual> pop = new List<Individual>();
            Individual newIndividual;
            for (var i = 0; i < populationSize - 1; i++)
            {
                newIndividual = new Individual(space);
                newIndividual.Cities = ShuffleList(space, 1, space.Count - 2);
                newIndividual.CalculateFitness();
                pop.Add(newIndividual);
            }
            Population initialPopulation = new Population(populationSize, pop);
            return initialPopulation;
        }

        /*
        * input: a Individual list
        * output: Individual list shuffled. 
        * 
        * Function shuffles the list using swaps with randomised indexes, done using a rng.
        */
        public static List<City> ShuffleList(List<City> listToShuffle, int start, int end)
        {
            Random rng = new Random();
            List<City> randomisedList = new List<City>(listToShuffle);
            int j = rng.Next(start, end);
            City tmp = new City();
            for (int i = start; i < end; i++)
            {
                tmp = listToShuffle[j];
                listToShuffle[j] = listToShuffle[i];
                listToShuffle[i] = tmp;
                j = rng.Next(start, end);
            }
            return randomisedList;
        }

        /* 
         * input: a population and tournament size
         * output: the fittest individual found
         * 
         * Function takes a population, generates a tournamet based off the parameter and finds the fittest individual of it.
         */
        public static Individual PerformTournamentSelection(Population currentPopulation, int tournamentSize)
        {
            List<Individual> tmp = new List<Individual>();
            Random rng = new Random();
            int randIndex = rng.Next(0, currentPopulation.PopulationSize);
            for (var i = 0; i <= tournamentSize; i++)
            {
                tmp.Add(currentPopulation.Individuals[randIndex]);
                randIndex = rng.Next(0, currentPopulation.PopulationSize - 1);
            }
            Population tournament = new Population(tournamentSize, tmp);
            return FindFittest(tournament);
        }

        /*
       * input: a population
       * output: the fittest individual found in the given population 
       * 
       * Function iterates over the population, compares fitness to find best individual.
       */
        public static Individual FindFittest(Population population)
        {
            Individual fittest = population.Individuals[0];
            for (var i = 1; i < population.PopulationSize - 1; i++)
            {
                if (population.Individuals[i].Fitness >= fittest.Fitness)
                {
                    fittest = population.Individuals[i];
                }
            }
            return fittest;
        }

        public static bool CheckTerminationCriteria()
        {
            return true;
        }

        public static Individual PerformCrossover(List<Individual> firstParent, List<Individual> secondParent)
        {
            Individual offspring;



            return offspring;
        }

        public static List<Individual> EvolvePopulation()
        {
            List<Individual> newPopulation = new List<Individual>();



            return newPopulation;
        }
        //only do swap mutation
        public static void Mutate()
        {
        }

    }
}
