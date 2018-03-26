
namespace Cdt312_assign_6
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
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
            for(var i = 1; i < allCities.Count-1; i++)
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
            List<double> probabilities = new List<double>();
            FindUnvisited(allCities, ref unvisited);
            if (unvisited.Count < 1)
            {
                return;
            }
            for (var i = 0; i < unvisited.Count; i++)
            {
                probabilities.Add(CalculateProbability(unvisited[i], unvisited));
            }    
            Visited.Add(unvisited[PickCity(probabilities)]);
            Current = Visited.Last();
            return;
        }

        private bool VerifyProbabilityList(List<double> props)
        {
            int idx = props.FindIndex(x => x == double.NaN);
            if (idx == -1)
            {
                return true;
            }
            return false;
        }

        private int PickCity(List<double> probabilities)
        {
            double propSum = 0.0;
            for (var i = 0; i < probabilities.Count; i++)
            {
                propSum += probabilities[i];
            }
            double threshold = MathExtension.rng.NextDouble() * propSum;
            double sum = 0.0;
            for(var i = 0; i < probabilities.Count; i++)
            {
                sum += probabilities[i];
                if (threshold <= sum)
                {
                    return i;
                }
            }
            return -1;
        }
        /*
         * Desc: Function will calculate the probability that a ant will move to sent city. 
         */
        private double CalculateProbability(City toCity, List<City> unvisited)
        {
            double alpha = 2.0, beta = 3.0, denominator = 0.0;
            double numerator = ((Math.Pow(ACOAlgorithm.Phermones[Current.Id - 1, toCity.Id - 1], alpha)) * (Math.Pow(ACOAlgorithm.Heuristics[Current.Id - 1, toCity.Id - 1], beta)));

            for (var i = 0; i < unvisited.Count; i++)
            {
                if (unvisited[i].Id != toCity.Id)
                {
                    denominator += (Math.Pow(ACOAlgorithm.Phermones[Current.Id - 1, unvisited[i].Id - 1], alpha)) * (Math.Pow(ACOAlgorithm.Heuristics[Current.Id - 1, unvisited[i].Id - 1], beta));
                }
            }
            return (numerator / denominator);
        }

        public void CalculateCost()
        {
            Cost = 0.0;
            for (var i = 0; i < Visited.Count-1; i++)
            {
                Cost += ACOAlgorithm.Distances[Visited[i].Id-1, Visited[i+1].Id-1];
            }
        }

        public bool HasVisitedEdge(int i, int j)
        {
            for (int k = 0; k < Visited.Count-1; k++)
            {
                if (Visited[k].Id == i && Visited[k + 1].Id == j)
                    return true;
            }
            return false;
        }
    }
}
