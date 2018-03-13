namespace Cdt312_assign_6
{
    using System.Collections.Generic;
    class ACOAlgorithm
    {
        public static double[,] Phermones;
        public static double[,] Distances;
        public static double[,] Heuristics;
        public int NoAnts;
        public Ant[] Ants;

        public ACOAlgorithm(int dim, int noAnts, List<City> allCities)
        {
            NoAnts = noAnts;
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

    }
}
