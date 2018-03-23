namespace Cdt312_assign_6
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    class ACOAlgorithm
    {
        public int NoAnts;
        public double EvaporationFactor;
        public List<Ant> Ants;
        public static double[,] Phermones;
        public static double[,] Distances;
        public static double[,] Heuristics;

        public ACOAlgorithm(int noAnts, List<City> allCities, double evapFac)
        {
            NoAnts = noAnts;
            EvaporationFactor = evapFac;
            InitialiseAnts(allCities[0]);
            InitialisePhermones(allCities.Count - 1);
            InitialiseDistances(allCities, allCities.Count - 1);
            InitialiseHeuristics(allCities.Count - 1);
        }

        public void InitialiseAnts(City initialCity)
        {
            Ants = new List<Ant>();
            for (var i = 0; i < NoAnts; i++)
            {
               Ant newAnt = new Ant(initialCity);
               Ants.Add(newAnt);
            }
        }

        /*
        * Desc: Function initialises the Phermones matrix, setting all value to 10.0.  
        */
        public void InitialisePhermones(int dim)
        {
            Phermones = new double[dim, dim];
            for (var i = 0; i < dim; i++)
            {
                for (var j = 0; j < dim; j++)
                {
                    Phermones[i, j] = 10.0;
                }
            }
        }

        public void InitialiseDistances(List<City> allCities, int dim)
        {
            Distances = new double[dim, dim];
            for (var i = 0; i < dim; i++)
            {
                for (var j = 0; j < dim; j++)
                {
                    Distances[i, j] = MathExtension.CalculateDistance(allCities[i], allCities[j]);
                }
            }
        }

        public void InitialiseHeuristics(int dim)
        {
            Heuristics = new double[dim, dim];
            for (var i = 0; i < dim; i++)
            {
                for (var j = 0; j < dim; j++)
                {
                    Heuristics[i, j] = (1.0 / Distances[i, j]);
                }
            }
        }

        public void UpdatePheromones(int i, int j)
        {
            double t = 0.0, visitedTerm = 0.0;
            for (int k = 0; k < Ants.Count - 1; k++)
            {
                if (Ants[k].HasVisitedEdge(i, j))
                {
                    visitedTerm += (1.0 / Ants[k].Cost);
                }
                visitedTerm += 0.0;
            }
            t = (1.0 - EvaporationFactor) * Phermones[i, j] + visitedTerm;
            Phermones[i, j] = t;
        }

        public int FindBest()
        {
            int idx = 0;
            for (int i = 1; i < NoAnts; i++)
            {
                if (Ants[i].Cost < Ants[idx].Cost)
                {
                    idx = i;
                }
            }
            return idx;
        }

        public void Run(List<City> allCities, int maxIterations)
        {
            int iterationBestIdx = 0;
            double bestCost = 0.0;
            List<City> bestPath = new List<City>();
            for (int i = 0; i < maxIterations; i++)
            {
                for (int j = 0; j < allCities.Count; j++)
                {
                    for (int k = 0; k < NoAnts; k++)
                    {
                        Ants[k].Transition(Phermones, Heuristics, allCities);
                    }
                }

                for (int j = 0; j < NoAnts; j++)
                {
                    Ants[j].Visited.Add(allCities[0]);
                    Ants[j].CalculateCost();
                }

                iterationBestIdx = FindBest();       

                for (int j = 0; j < Phermones.GetLength(0); j++)
                {
                    for (int k = 0; k < Phermones.GetLength(1); k++)
                    {
                        UpdatePheromones(j, k);
                    }
                }

                if (bestCost != 0.0)
                {
                    if (Ants[iterationBestIdx].Cost < bestCost)
                    {
                        bestCost = Ants[iterationBestIdx].Cost;
                        bestPath = new List<City>(Ants[iterationBestIdx].Visited);
                    }
                }
                else
                {
                    bestCost = Ants[iterationBestIdx].Cost;
                    bestPath = new List<City>(Ants[iterationBestIdx].Visited);
                }

                for (int j = 0; j < Ants.Count; j++)
                {
                    Ants[j].Visited.Clear();
                    Ants[j].Visited.Add(allCities[0]);
                    Ants[j].Cost = 0.0;
                }
            }
            Console.WriteLine("Best path cost found: {0}, after {1} iterations", bestCost, maxIterations);
        }
    }
}
