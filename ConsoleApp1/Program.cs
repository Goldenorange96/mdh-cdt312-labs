using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cdt312_assignments
{
    class Program
    {
        static void Main(string[] args)
        {
            //closed used to keep all visited nodes.
            List<State> closed = new List<State>();
            //queue for created nodes.
            Queue<State> open;
            //read info 
            List<State> allItems = new List<State>();
            ReadFileAndGenerateList(allItems);
            InitQueue(out open);
            BreadthFirstSearch(open, allItems);
            //PrintList(allItems);
        }

        static void ReadFileAndGenerateList(List<State> allItems)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:.\knapsack.txt");
            string line = null;
            string[] subStrings;
            int id, itemBenefit, itemWeight;

            while((line = file.ReadLine()) != null)           
            {
                subStrings = line.Split(null);
                if (!int.TryParse(subStrings[0], out id))
                {
                }
                else
                {
                    id = Convert.ToInt32(subStrings[0]);
                    itemBenefit = Convert.ToInt32(subStrings[1]);
                    itemWeight = Convert.ToInt32(subStrings[2]);
                    State newState = new State(id, itemWeight, itemBenefit, null);
                    allItems.Add(newState);
                }
            }
            file.Close();
        }

        static void PrintList(List<State> listToPrint)
        {
            foreach (State item in listToPrint)
                Console.WriteLine("id: {0} b: {1} w: {2}", item.itemNo, item.itemBenefit, item.itemWeight);
        }

        static void InitQueue(out Queue<State> newQueue)
        {
            State initialState = new State(0, 0, 0, null);
            newQueue = new Queue<State>();
            newQueue.Enqueue(initialState);
        }

        static bool BreadthFirstSearch(Queue<State> stateQueue, List<State> allItems)
        {
            //get initial state
            State childState = null;
            State parentState = stateQueue.Dequeue();
            int curItem = 0;
            while (curItem < allItems.Count)
            {
                for (int i = curItem; i < curItem+2; i++)
                {
                    childState = allItems[i];
                    childState.parentNode = parentState;
                    stateQueue.Enqueue(childState);
                    curItem++;
                }
                parentState = stateQueue.Dequeue();
            }

            return true;
        }

        bool DepthFirstSearch()
        {
            return true;
        }

        bool IsGoalState()
        {
            return true;
        }
    }

    class State
    {
        public State parentNode;
        public int itemNo;
        public int itemWeight;
        public int itemBenefit;
        public State(int newItemNo, int newItemWeight, int newItemBenefit, State newParentNode)
        {
            itemNo = newItemNo;
            itemWeight = newItemWeight;
            itemBenefit = newItemBenefit;
            parentNode = newParentNode;
        }
    }

}
