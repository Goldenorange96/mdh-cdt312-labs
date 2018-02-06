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
        public int itemNo;
        public int itemWeight;
        public int itemBenefit;
        public int weight;
        public int benefit;
        public int depth;
        public List<Item> actions;
        public Node()
        {
        }
        public Node(Node newParent, int newStateWeight, int newStateBenefit, List<Item> newActionsList, int newItemNo, int newItemWeight, int newItemBenefit, int newDepth)
        {
            weight = newStateWeight;
            benefit = newStateBenefit;
            actions = newActionsList;
            parent = newParent;
            itemNo = newItemNo;
            itemWeight = newItemWeight;
            itemBenefit = newItemBenefit;
            depth = newDepth;
        }
    }
}
