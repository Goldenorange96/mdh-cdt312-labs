using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdt312_assign_3
{
    class Population
    {
        public List<Individual> Individuals;
        public int PopulationSize;
        public Population(int newPopulationSize, List<Individual> newIndividuals)
        {
            PopulationSize = newPopulationSize;
            Individuals = new List<Individual>(newIndividuals);
        }
    }
}
