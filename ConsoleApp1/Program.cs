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
            List<Item> items = new List<Item>();
            List<Item> solution = new List<Item>();
            int knapsackLimit = 0, weight = 0, benefit = 0;
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            solution = BreadthFirstSearch(items, knapsackLimit);
            stopwatch.Stop();
            Console.WriteLine("BFS took: {0} ms", stopwatch.ElapsedMilliseconds);
            PrintList(solution);
            foreach (Item action in solution)
            {
                weight += action.itemWeight;
                benefit += action.itemBenefit;
            }
            Console.WriteLine("w: {0}, b: {1}", weight, benefit);
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
                    Item newItem = new Item(Convert.ToInt32(subStrings[1]), id, Convert.ToInt32(subStrings[2]));
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
            //State initialState = new State(newActionsList, 0, 0);
            //Item initialAction = new Item(0, 0, 0);
            Node initialNode = new Node(null, 0, 0, newActionsList, 0, 0, 0);
            
            newQueue.Enqueue(initialNode);
            return newQueue;
        }

        static List<Item> BreadthFirstSearch(List<Item> items, int knapsackLimit)
        {
            ReadFileAndGenerateList(items, out knapsackLimit);
            Node currentBest = new Node();
            Node parent = new Node();
            Queue<Node> frontier = InitQueue();
            while (frontier.Count > 0)
            {
                parent = frontier.Dequeue();
                if (parent.weight < knapsackLimit)
                {
                    if ((parent.weight < knapsackLimit) && (parent.benefit > currentBest.benefit))
                    {
                        currentBest = parent;
                    }

                    foreach (Node item in GetSuccessors(parent, items))
                    {
                        frontier.Enqueue(item);
                    }
                }
               
            }
            return currentBest.actions;
        }

        static List<Node> GetSuccessors(Node parent, List<Item> allItems)
        {
            List<Node> successors = new List<Node>();
            for (int i = 0; i < allItems.Count; i++)
            {
                if ((allItems[i].itemNo != parent.itemNo) && !(parent.actions.Contains(allItems[i])))
                {
                    successors.Add(CreateChildNode(parent, allItems[i]));
                }
            }
            return successors;
        }

        static Node CreateChildNode(Node parent, Item newAction)
        {
            List<Item> newActionsList = new List<Item>(parent.actions);
            newActionsList.Add(newAction);
            int newWeight = parent.weight + newAction.itemWeight;
            int newBenefit = parent.benefit + newAction.itemBenefit;
            Node newChild = new Node(parent, newWeight, newBenefit, newActionsList, newAction.itemNo, newAction.itemWeight, newAction.itemBenefit);
            return newChild;
        }

        static bool IsGoalState(Node current, Node best, int limit)
        {
            if ((current.weight < limit) && (current.benefit > best.benefit))
            {
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
