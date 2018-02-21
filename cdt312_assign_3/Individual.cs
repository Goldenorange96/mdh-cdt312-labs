namespace Cdt312_assign_3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Individual
    {
        public double fitness;
        public List<City> cities;
        //new-constructor
        public Individual(List<City> space, double newFitness)
        {
            cities = new List<City>(space);
            fitness = newFitness;
        }

    }
}
