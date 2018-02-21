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
            List<Individual> InitialPopulation = InitializePopulation(allCities);
            PrintCityList(InitialPopulation);
            for (int i = 0; i < InitialPopulation.Count-1; i++)
            {
                Console.WriteLine("Fitness for individual {0} is: {1}", i, CalculateFitness(InitialPopulation[i], allCities));
            }
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

        }
        /*
         * input: A Individual list
         * output: none
         * 
         * Function prints the given Individual list including all properties of each item in the list.
         */
        public static void PrintCityList(List<Individual> listToPrint)
        {
            if (listToPrint != null)
            {
                Console.WriteLine("<----------------------->");

                foreach (Individual Individual in listToPrint)
                {
                    Console.WriteLine("> Id: {0} X: {1} Y: {2} fitness: {3}", Individual.Id, Individual.X, Individual.Y, Individual.fitness);
                }

                Console.WriteLine("<----------------------->");
            }
            
        }
        /*
         * input: Individual list representing the space of cities
         * output: Individual list randomised. 
         * 
         * Function copies the space list and passes it to shuffleList.
         */
        public static Population InitializePopulation(List<City> space)
        {
            Individual newIndividual = new Individual(space, 0.0);
            for (var i = 0; i < space.Count - 1; i++)
            {
                ini
            }


            Population initialPopulation = new Population(space.Count, )
            List<Individual> initialPopulation = new List<Individual>(space, 0.0);
            initialPopulation.Insert(0, space[0]);
            ShuffleList(ref space, 1, (space.Count - 2));
            initialPopulation.Add(space[0]);
            return initialPopulation;
        }
        /*
        * input: a Individual list
        * output: Individual list shuffled. 
        * 
        * Function shuffles the list using swaps with randomised indexes, done using a rng.
        */
        public static void ShuffleList(ref List<City> listToShuffle, int start, int end)
        {
            Random rng = new Random();
            int j = rng.Next(start, end);
            Individual tmp;
            for (int i = start; i < end; i++)
            {
                tmp = new Individual(listToShuffle[j]);
                listToShuffle[j] = listToShuffle[i];
                listToShuffle[i] = tmp;
                j = rng.Next(start, end);
            }
        }

        /* 
         * input: a population and tournament size
         * output: the fittest individual found
         * 
         * Function takes a population, generates a tournamet based off the parameter and finds the fittest individual of it.
         */
        public static Individual PerformTournamentSelection(List<Individual> currentPopulation, int tournamentSize)
        {
            List<Individual> tournament = new List<Individual>();
            Random rng = new Random();
            int randIndex = rng.Next(1, currentPopulation.Count - 2);
            for (var i = 0; i <= tournamentSize; i++)
            {
                tournament.Add(currentPopulation[randIndex]);
                randIndex = rng.Next(1, currentPopulation.Count - 2);
            }
            return FindFittest(tournament);
        }


        /*
       * input: a population
       * output: the fittest individual found in the given population 
       * 
       * Function iterates over the population, compares fitness to find best individual.
       */
        public static Individual FindFittest(List<Individual> population)
        {
            Individual fittest = new Individual(population[0]);

            for (var i = 1; i < population.Count - 1; i++)
            {
                if (population[i].fitness >= fittest.fitness)
                {
                    fittest = population[i];
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
            Individual offspring = new Individual();



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

        /*
         * input: individual
         * output: fitness of individual
         * 
         * Function calculates the fitness of a provided individual. Fitness is based off the Euclidean distance.
         */
        public static double CalculateFitness(Individual individual, List<City> list)
        {
            var fitness = 0.0;
            for (var i = 0; i < list.Count - 1; i++)
            {
                fitness += (Math.Pow((list[i].X - individual.X), 2.0)) + (Math.Pow((list[i].Y - individual.Y), 2.0));  
            }
            return 1.0/fitness;
        }

    }
}
