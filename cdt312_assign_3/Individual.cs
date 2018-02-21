namespace Cdt312_assign_3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Individual
    {
        public double Fitness;
        public List<City> Cities;
        //new-constructor
        public Individual(List<City> space, double newFitness = 0.0)
        {
            Cities = new List<City>(space);
            Fitness = newFitness;
        }

        public void CalculateFitness()
        {
            double fit = 0.0;
            for (var i = 0; i < Cities.Count - 1; i++)
            {
                fit += Cities[i].GetDistance(Cities[i + 1]);
            }
            Fitness = fit;
        }

    }
}
