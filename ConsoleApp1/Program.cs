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
            //queue for created nodes.
            Queue<State> open = null;
            //read info 
            List<State> allItems = new List<State>();
            int knapsackLimit = 0;
            ReadFileAndGenerateList(allItems, out knapsackLimit);
            InitQueue(out open);
            BreadthFirstSearch(open, allItems);
            //PrintList(allItems);
        }

        static void ReadFileAndGenerateList(List<State> allItems, out int knapsackLimit)
        {
            knapsackLimit = 0;
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:.\knapsack.txt");
            string line = null;
            string[] subStrings;
            int id, itemBenefit, itemWeight;

            while((line = file.ReadLine()) != null)           
            {
                subStrings = line.Split(null);
                if (!int.TryParse(subStrings[0], out id))
                {
                    if (subStrings[0] == "MAXIMUM")
                    {
                        knapsackLimit = Convert.ToInt32(subStrings[2]);
                    }
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
            State parentState = null;
            //closed used to keep all visited nodes.
            List<State> closed = new List<State>();
            int curItem = 0;
            while (stateQueue.Count > 0)
            {
                parentState = stateQueue.Dequeue();
                //special case root children
                if (curItem == 0)
                {
                    for (int i = 1; i < 4; i++)
                    {
                        childState = allItems[i];
                        childState.parentNode = parentState;
                        stateQueue.Enqueue(childState);
                        closed.Add(parentState);
                    }
                    curItem += 3;
                }

                for (int i = curItem; i < curItem + 2; i++)
                {
                    childState = allItems[i];
                    childState.parentNode = parentState;
                    if (closed.Contains(childState))
                    {

                    }
                    else
                    {
                        stateQueue.Enqueue(childState);
                    }

                }
            }
            Console.ReadKey();
            return true;
        }

        //static bool IsGoal(List<State> closed)
        //{
        //    if()
        //}

        bool DepthFirstSearch()
        {
            return true;
        }

        bool IsGoalState(int knapsackLimit, List<State> closed)
        {
            int currentWeight = 0;
            for (int i = 0; i < closed.Count; i++)
                currentWeight += closed[i].itemWeight;

            if (currentWeight > knapsackLimit)
                return false;

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
