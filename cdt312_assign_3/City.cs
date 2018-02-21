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

        public City()
        {

        }
    
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

        public double GetDistance(City toCity)
        {
            return (Math.Pow((X - toCity.X), 2.0)) + (Math.Pow((Y - toCity.Y), 2.0));
        }
    }
}
