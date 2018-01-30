using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Node
    {
        public State state;
        public Node parent;
        public Item action;
        public int pathCost;
        public Node()
        {
        }
        public Node(State newstate, Node newParent, Item newAction, int newPathCost)
        {
            state = newstate;
            parent = newParent;
            action = newAction;
            pathCost = newPathCost;
        }
    }
}
