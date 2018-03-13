using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdt312_assign_6
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
            double a = (toCity.X - X) * (toCity.X - X);
            double b = (toCity.Y - Y) * (toCity.Y - Y);
            return Math.Sqrt(a + b);
        }
    }
}
