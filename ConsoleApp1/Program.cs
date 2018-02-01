using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;

namespace cdt312_assignments
{
    class Program
    {
        static void Main(string[] args)
        {
            //queue for created nodes.
            Queue<Node> frontier = null;
            //read info 
            List<Item> items = new List<Item>();
            List<Item> solution = new List<Item>();
            int knapsackLimit = 0;
            ReadFileAndGenerateList(items, out knapsackLimit);
            frontier = InitQueue();
            solution = BreadthFirstSearch(items, knapsackLimit);
            PrintList(solution);
            Console.ReadKey();
        }

        static void ReadFileAndGenerateList(List<Item> allItems, out int knapsackLimit)
        {
            knapsackLimit = 0;
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:.\knapsack.txt");
            string line = null;
            string[] subStrings;
            int id = 0;
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
                    Item newItem = new Item(Convert.ToInt32(subStrings[2]), id, Convert.ToInt32(subStrings[1]));
                    allItems.Add(newItem);
                }
            }
            file.Close();
        }

        static void PrintList(List<Item> listToPrint)
        {
            if (listToPrint == null)
            {
                Console.WriteLine("List sent was null, cannot print list!");
                return;
            }

            Console.WriteLine("<----------------------->");

            foreach (Item item in listToPrint)
                Console.WriteLine("id: {0} b: {1} w: {2}", item.itemNo, item.itemBenefit, item.itemWeight);

            Console.WriteLine("<----------------------->");
        }

        static Queue<Node> InitQueue()
        {
            Queue<Node> newQueue = new Queue<Node>();
            List<Item> newActionsList = new List<Item>();
            State initialState = new State(newActionsList, 0, 0);
            Node initialNode = new Node(initialState, null, null, 0);
            newQueue.Enqueue(initialNode);
            return newQueue;
        }

        static List<Item> BreadthFirstSearch(List<Item> items, int knapsackLimit)
        {
            int curItem = 0, i = 0, stateWeight = 0, stateBenefit = 0;
            Node currentBest = new Node();
            Node parent = new Node();
            List<Node> visited = new List<Node>();
            List<Node> children = new List<Node>();
            Queue<Node> frontier = InitQueue();
            while (frontier.Any())
            {
                parent = frontier.Dequeue();
                if (IsGoalState(parent.state.weight))
                {
                    return parent.state.actions;
                }

                if (curItem <= 0)
                {
                    while (i < 3)
                    {
                        Node childNode = CreateChildNode(parent, items[i]);
                        /*if (!visited.Contains(childNode))
                        {
                            frontier.Enqueue(childNode);
                        }*/
                        i++;
                    }
                    curItem += 2;
                }
                else
                {
                    if (curItem >= items.Count)
                    {
                        break;
                    }
                    i = 0;
                    while (i < 2)
                    {
                        Node childNode = CreateChildNode(parent, items[i]);
                        /*if (!visited.Contains(childNode))
                        {
                            frontier.Enqueue(childNode);
                        }*/
                        frontier.Enqueue(childNode);
                        i++;
                    }
                    curItem += 2;
                }
                Console.WriteLine("Actions to get to parent: ");
                PrintList(parent.state.actions);
                Console.WriteLine("Current weight of state: {0} and benefit: {1}", parent.state.weight, parent.state.benefit);
                visited.Add(parent);
            }
            return null;
        }

        static List<Node> GetSuccessors(Node parent, List<Item> possibleActions)
        {
            List<Node> successors = new List<Node>();

            for (int i = 0; i < possibleActions.Count; i++)
            {
                CreateChildNode(parent, possibleActions[i]);
            }

            return successors;
        }

        static Node CreateChildNode(Node parent, Item newAction)
        {
            List<Item> newActionsList = parent.state.actions;
            newActionsList.Add(newAction);
            int newWeight = 0, newBenefit = 0;
            foreach (Item action in newActionsList)
            {
                newWeight += action.itemWeight;
                newBenefit += action.itemBenefit;   
            }
            State newState = new State(newActionsList, newWeight, newBenefit);
            Node newChild = new Node(newState, parent, newAction, newAction.itemBenefit);
            return newChild;
        }

        static bool IsGoalState(int stateWeight)
        {
            if (stateWeight >= 420)
            {
                Console.WriteLine("Weight Limit exceeded/reached.");
                return true;
            }
            return false;
        }

        bool DepthFirstSearch()
        {
            return true;
        }

    }
}
