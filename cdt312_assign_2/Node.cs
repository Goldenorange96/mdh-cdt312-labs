using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cdt312_assign_2
{
    class Node
    {
        City currentCity;
        List<Node> visitedCities;
        int currentPathCost;
        public Node()
        {
        }
        public Node(City newCurrentCity, List<Node> newVisitedCities, int newPathCost)
        {
            currentCity = newCurrentCity;
            visitedCities = new List<Node>(newVisitedCities);
            currentPathCost = newPathCost;
        }
    }
}
