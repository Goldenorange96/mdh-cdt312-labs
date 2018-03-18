
namespace Cdt312_assign_6
{
    using System.Collections.Generic;
    using System;
    class Ant
    {
        public double Cost;
        public List<City> Visited;
        public City Current;
        public Ant(City initialCity)
        {
            Cost = 0.0;
            Visited = new List<City>();
            Visited.Add(initialCity);
            Current = initialCity;
        }
        private void FindUnvisited(List<City> allCities, ref List<City> unvisited)
        {
            for(var i = 0; i < allCities.Count - 1; i++)
            {
                if (!Visited.Contains(allCities[i]))
                {
                    unvisited.Add(allCities[i]);
                }
            }
        }

        /*
         * Desc: Function will transition the ant from its current city to a city s, determine s using a probability rule.  
         */
        public void Transition(double[,] phermoneMat, double[,] heuristicMat, List<City> allCities)
        {
            List<City> unvisited = new List<City>();
            FindUnvisited(allCities, ref unvisited);
            double largestProp = 0.0, temp = 0.0;
            int idx = 0;
            for (var i = 0; i < unvisited.Count - 1; i++)
            {
                temp = CalculateProbability(unvisited[i], unvisited);
                if (temp > largestProp)
                {
                    largestProp = temp;
                    idx = i;
                }
            }
            Visited.Add(allCities[idx]);
            Current = allCities[idx];
        }
        /*
         * Desc: Function will calculate the probability that a ant will move to sent city. 
         */
        private double CalculateProbability(City toCity, List<City> unvisited)
        {
            double alpha = 0.5, beta = 1.0, numerator = 0.0, denominator = 0.0;
            numerator = Math.Pow(ACOAlgorithm.Phermones[Current.Id-1, toCity.Id-1], alpha) * Math.Pow(ACOAlgorithm.Heuristics[Current.Id-1, toCity.Id-1], beta);

            for (var i = 0; i < unvisited.Count - 1; i++)
            {
                denominator += Math.Pow(ACOAlgorithm.Phermones[Current.Id-1, unvisited[i].Id-1], alpha) * Math.Pow(ACOAlgorithm.Heuristics[Current.Id-1, unvisited[i].Id-1], beta);
            }

            return numerator / denominator;
        }

        public void CalculateCost()
        {
            Cost = 0.0;
            for (var i = 0; i < Visited.Count - 1; i++)
            {
                Cost += ACOAlgorithm.Distances[Visited[i].Id-1, Visited[i+1].Id-1];
            }
        }

        public bool HasVisitedEdge(int i, int j)
        {
            for (int k = 0; k < Visited.Count-1; k++)
            {
                if (Visited[i].Id == i && Visited[i + 1].Id == j)
                    return true;
            }
            return false;
        }
    }
}
