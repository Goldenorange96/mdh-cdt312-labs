using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
namespace ConsoleApp1
{
    class Node
    {
        public Node parent;
        public Item action;
        public int weight;
        public int benefit;
        public List<Item> actions;
        public Node()
        {
        }
        public Node(Node newParent, int newStateWeight, int newStateBenefit, List<Item> newActionsList, Item newAction)
        {
            weight = newStateWeight;
            benefit = newStateBenefit;
            actions = newActionsList;
            parent = newParent;
            action = newAction;
        }
    }
}
