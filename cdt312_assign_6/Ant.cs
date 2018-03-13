
namespace Cdt312_assign_6
{
    using System.Collections.Generic;
    class Ant
    {
        public List<City> Visited;
        public City Current;
        public Ant(City initialCity)
        {
            Visited = new List<City>();
            Visited.Add(initialCity);
            Current = initialCity;
        }
        /*
         * Desc: Function will transition the ant from its current city to a city s, determine s using a probability rule.  
         */
        public void Transition(double[,] phermoneMat, double[,] heuristicMat)
        {
            
        }
        /*
       * Desc: Function will calculate the probability that a ant will move to sent city. 
       */
        private double CalculateProbability(City toCity, List<City> allCities)
        {
            double alpha = 0.0, beta = 0.0, numerator = 0.0, denominator = 0.0;
            numerator = (ACOAlgorithm.Phermones[Current.Id, toCity.Id] * alpha) * (ACOAlgorithm.Heuristics[Current.Id, toCity.Id] * beta);
            for (var i = 0; i < allCities.Count; i++)
            {

            }
            

            return 0.0;
        }

    }
}
