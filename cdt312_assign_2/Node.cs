using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cdt312_assign_2
{
    class Node
    {
        public Node prevCity;
        public List<Node> visitedCities;
        public string cityName;
        public int currentPathCost;
        public int straightDist;
        public int priority;
        public Node()
        {
        }
        public Node(Node newPrevCity, List<Node> newVisitedCities, int newPathCost, string newCityName, int newStraightDist, int newPriority)
        {
            prevCity = newPrevCity;
            visitedCities = newVisitedCities;
            currentPathCost = newPathCost;
            cityName = newCityName;
            straightDist = newStraightDist;
            priority = newPriority;
        }
    }
}
