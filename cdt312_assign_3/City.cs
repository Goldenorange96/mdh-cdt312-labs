using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdt312_assign_3
{
    class City
    {
        public int Id;
        public double X;
        public double Y;

        public City(City newCity)
        {
            Id = newCity.Id;
            X = newCity.X;
            Y = newCity.Y;
        }

        public City(int newId, double newX, double newY, double newFitness)
        {
            Id = newId;
            X = newX;
            Y = newY;
        }
    }
}
