using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cdt312_assign_2
{
    class Path
    {
        public string cityA;
        public string cityB;
        public int pathCost;
        public Path() { }
        public Path(string newCityA, string newCityB, int newPathCost)
        {
            cityA = newCityA;
            cityB = newCityB;
            pathCost = newPathCost;
        }
    }
}
