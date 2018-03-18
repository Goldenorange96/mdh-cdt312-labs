namespace Cdt312_assign_6
{
    using System.Collections.Generic;
    class ACOAlgorithm
    {
        public int NoAnts;
        public double EvaporationFactor;
        public Ant[] Ants;
        public static double[,] Phermones;
        public static double[,] Distances;
        public static double[,] Heuristics;

        public ACOAlgorithm(int dim, int noAnts, List<City> allCities, double evapFac)
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
            Ants = new Ant[NoAnts];
            for (var i = 0; i < NoAnts; i++)
            {
                Ants[i] = new Ant(initialCity);
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
                    if (!(j == i))
                    {
                        Phermones[i, j] = 10.0;
                    }
                    else
                    {
                        Phermones[i, j] = 0.0;
                    }

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
                    if (!(j == i))
                    {
                        Distances[i, j] = MathExtension.CalculateDistance(allCities[j], allCities[i]);
                    }
                    else
                    {
                        Distances[i, j] = 0.0;
                    }
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
                    Heuristics[i, j] = 1.0 / Distances[i, j];
                }
            }
        }

        public void UpdatePheromone()
        {
            double t = 0.0;
            for (int i = 0; i < Phermones.GetLength(0); i++)
            {
                for (int j = 0; j < Phermones.GetLength(1); i++)
                {
                    Phermones[i,j] = (1.0 - EvaporationFactor) + 
                }
            }
            for (int i = 0; i < Ants.Length; i++)
            {
                double temp = 0.0;
                if ()
                {
                    temp += 1.0 / Ants[i].Cost;
                }
            }

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

    }
}
