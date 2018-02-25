namespace Cdt312_assign_3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Individual
    {
        public double PathDistance;
        public List<City> Cities;
        //new-constructor
        public Individual()
        {

        }

        public Individual(List<City>space, double newFitness = 0.0)
        {
            Cities = new List<City>(space);
            //CalculatePathDist();
        }

        public void CalculatePathDist()
        {
            double dist = 0.0;
            for (var i = 0; i < Cities.Count - 2; i++)
            {
                dist += Cities[i].GetDistance(Cities[i + 1]);
            }
            PathDistance = dist;
        }

        public void VerifySolution()
        {
            var occurence = 0;

            foreach (City city in Cities)
            {
                foreach (City other in Cities)
                {
                    if (city.Id == other.Id && city.Id != 1)
                    {
                        occurence++;
                        if (occurence > 1)
                        {
                            Console.WriteLine("SOLUTION WAS NOT CORRECT DUPLICATE ID FOUND!");
                            break;
                        }
                    }
                }
                occurence = 0;
            }
        }
    }
}
