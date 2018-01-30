using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class State
    {
        public int weight;
        public int benefit;
        public List<Item> actions;
        public State(List<Item> newActions, int newWeight, int newBenefit)
        {
            actions = newActions;
            weight = newWeight;
            benefit = newBenefit;
        }
    }
}
