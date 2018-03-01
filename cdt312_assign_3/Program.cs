namespace Cdt312_assign_3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    class Program
    {
        public static Random rngGlobal = new Random();

        public static void Main(string[] args)
        {
            List<City> allCities = new List<City>();
            ReadFileAndGenerateList(ref allCities);
            Population initialPopulation = InitializePopulation(allCities, GAParameters.PopulationSize);
            EvolvePopulation(initialPopulation, GAParameters.Generations, GAParameters.TournamentSize);
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
                    City newCity = new City(id, float.Parse(subStrings[1]), float.Parse(subStrings[2]), 0.0);
                    allCities.Add(newCity);
                }
            }
            file.Close();
            allCities.Add(allCities[0]);

        }
      
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
            for (var i = 0; i < populationSize; i++)
            {
                newIndividual = new Individual(space);
                newIndividual.Cities = ShuffleList(space, 1, space.Count - 2);
                newIndividual.CalculatePathDist();
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
            List<City> randomisedList = new List<City>(listToShuffle);
            int j = rngGlobal.Next(start, end);
            City tmp = new City();
            for (int i = start; i < end; i++)
            {
                tmp = listToShuffle[j];
                listToShuffle[j] = listToShuffle[i];
                listToShuffle[i] = tmp;
                j = rngGlobal.Next(start, end);
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
            int randIndex = rngGlobal.Next(0, currentPopulation.PopulationSize - 1);
            for (var i = 0; i < tournamentSize; i++)
            {
                tmp.Add(currentPopulation.Individuals[randIndex]);
                randIndex = rngGlobal.Next(0, currentPopulation.PopulationSize - 1);
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
                if (population.Individuals[i].PathDistance < fittest.PathDistance)
                {
                    fittest = population.Individuals[i];
                }
            }
            return fittest;
        }

        public static List<Individual> PerformCrossover(Individual firstParent, Individual secondParent)
        {
            /*
             * Order representation
             * 1. Pick a range from parent 1.
             * 2. Input the cities from that range into child 1.
             * 3. Input the remainder from parent 2. 
             */

            List<Individual> offsprings = new List<Individual>();
            List<City> firstOffspring = Enumerable.Repeat<City>(null, 52).ToList();
            List<City> secondOffspring = Enumerable.Repeat<City>(null, 52).ToList();

            int start = rngGlobal.Next(1, firstParent.Cities.Count - 2);
            int end = rngGlobal.Next(start, firstParent.Cities.Count - 2);

            firstOffspring[0] = firstParent.Cities[0];
            secondOffspring[0] = secondParent.Cities[0];
            for (var i = start; i <= end; i++)
            {
                firstOffspring[i] = firstParent.Cities[i];
            }

            for (var i = 0; i < secondParent.Cities.Count; i++)
            {
                if (!firstOffspring.Contains(secondParent.Cities[i]))
                {
                    for (var j = 0; j < firstOffspring.Count; j++)
                    {
                        if (firstOffspring[j] == null)
                        {
                            firstOffspring[j] = secondParent.Cities[i];
                            break;
                        }
                    }
                }
            }

            //start = rngGlobal.Next(1, firstParent.Cities.Count - 2);
            //end = rngGlobal.Next(start, firstParent.Cities.Count - 2);

            for (var i = start; i <= end; i++)
            {
                secondOffspring[i] = secondParent.Cities[i];
            }

            for (var i = 0; i < secondParent.Cities.Count; i++)
            {
                if (!secondOffspring.Contains(firstParent.Cities[i]))
                {
                    for (var j = 0; j < secondOffspring.Count; j++)
                    {
                        if (secondOffspring[j] == null)
                        {
                            secondOffspring[j] = firstParent.Cities[i];
                            break;
                        }
                    }
                }
            }
           
            firstOffspring.Add(firstParent.Cities[0]);
            secondOffspring.Add(secondParent.Cities[0]);
            Individual offspring1 = new Individual(firstOffspring);
            offspring1.CalculatePathDist();
            offsprings.Add(offspring1);
            Individual offspring2 = new Individual(secondOffspring);
            offspring2.CalculatePathDist();
            offsprings.Add(offspring2);
            return offsprings;
        }

        public static void PrintCityList(List<City> listToPrint)
        {
            foreach (City city in listToPrint)
            {
                Console.WriteLine("City: {0}", city.Id);
            }
        }

        public static void EvolvePopulation(Population population, int generations, int tournamentSize)
        {
            Population newPopulation = new Population(population.PopulationSize, population.Individuals);
            int worstIdx = 0;
            double worstFit = 0.0;
            Console.WriteLine("Best {0}", FindFittest(newPopulation).PathDistance);
            for (var i = 0; i < generations; i++)
            {
                //Individual firstParent = PerformTournamentSelection(newPopulation, tournamentSize);
                //Individual secondParent = PerformTournamentSelection(newPopulation, tournamentSize);
                Individual firstParent = newPopulation.Individuals[rngGlobal.Next(0, newPopulation.PopulationSize - 1)];
                Individual secondParent = newPopulation.Individuals[rngGlobal.Next(0, newPopulation.PopulationSize - 1)];
                while (firstParent == secondParent)
                {
                    //secondParent = PerformTournamentSelection(newPopulation, tournamentSize);
                    //firstParent = PerformTournamentSelection(newPopulation, tournamentSize);
                    secondParent = newPopulation.Individuals[rngGlobal.Next(0, newPopulation.PopulationSize - 1)];
                    firstParent = newPopulation.Individuals[rngGlobal.Next(0, newPopulation.PopulationSize - 1)];
                }

                List<Individual> offsprings = new List<Individual>(PerformCrossover(firstParent, secondParent));
                offsprings[0] = Mutate(offsprings[0]);
                offsprings[1] = Mutate(offsprings[1]);

                for (var j = 0; j < newPopulation.PopulationSize - 1; j++)
                {
                    if (newPopulation.Individuals[j].PathDistance > worstFit)
                    {
                        worstFit = newPopulation.Individuals[j].PathDistance;
                        worstIdx = j;
                    }
                }

                if (offsprings[0].PathDistance < offsprings[1].PathDistance)
                {
                    newPopulation.Individuals[worstIdx] = offsprings[0];
                }
                else
                {
                    newPopulation.Individuals[worstIdx] = offsprings[1];
                }

                if (i % 5000 == 0)
                {
                    Console.WriteLine("Best {0}", FindFittest(newPopulation).PathDistance);
                }

                worstFit = 0.0;
            }
            Individual fittest = FindFittest(newPopulation);
            PrintCityList(fittest.Cities);
            fittest.VerifySolution();
            Console.WriteLine("Best dist: {0}", FindFittest(newPopulation).PathDistance);
        }
        //only do swap mutation
        public static Individual Mutate(Individual offspring)
        {
            int noMutations = rngGlobal.Next(1, 4);
            int randIdx1 = rngGlobal.Next(1, offspring.Cities.Count - 2), randIdx2 = rngGlobal.Next(1, offspring.Cities.Count - 2);
            for (var i = 0; i < noMutations; i++)
            {
                //while (randIdx1 == randIdx2)
                //{

                //}
                City tmp = offspring.Cities[randIdx1];
                offspring.Cities[randIdx1] = offspring.Cities[randIdx2];
                offspring.Cities[randIdx2] = tmp;
                randIdx1 = rngGlobal.Next(1, offspring.Cities.Count - 2);
                randIdx2 = rngGlobal.Next(1, offspring.Cities.Count - 2);
            }
            offspring.CalculatePathDist();
            return offspring;
        }
    }
}
