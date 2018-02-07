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
            List<Item> BFSsolution = new List<Item>();
            List<Item> DFSSolution = new List<Item>();
            int knapsackLimit = 0, BFSWeight = 0, BFSBenefit = 0, DFSWeight = 0, DFSBenefit = 0;
            ReadFileAndGenerateList(items, out knapsackLimit);
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            BFSsolution = BreadthFirstSearch(items, knapsackLimit);
            stopwatch.Stop();
            Console.WriteLine("BFS took: {0} ms", stopwatch.ElapsedMilliseconds);
            PrintList(BFSsolution);
            foreach (Item action in BFSsolution)
            {
                BFSWeight += action.itemWeight;
                BFSBenefit += action.itemBenefit;
            }
            Console.WriteLine("w: {0}, b: {1}", BFSWeight, BFSBenefit);
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
            DFSSolution = DepthFirstSearch(items, knapsackLimit);
            stopwatch.Stop();
            Console.WriteLine("######################");
            Console.WriteLine("DFS took: {0} ms", stopwatch.ElapsedMilliseconds);
            PrintList(DFSSolution);
            foreach (Item action in DFSSolution)
            {
                DFSWeight += action.itemWeight;
                DFSBenefit += action.itemBenefit;
            }
            Console.WriteLine("w: {0}, b: {1}", DFSWeight, DFSBenefit);
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
            Item initialAction = new Item(0, 0, 0);
            Node initialNode = new Node(null, 0, 0, newActionsList, initialAction);
            newQueue.Enqueue(initialNode);
            return newQueue;
        }

        static Stack<Node> InitStack()
        {
            Stack<Node> newStack = new Stack<Node>();
            List<Item> newActionsList = new List<Item>();
            Item initialAction = new Item(0, 0, 0);
            Node initialNode = new Node(null, 0, 0, newActionsList, initialAction);
            newStack.Push(initialNode);
            return newStack;
        }

        static List<Item> BreadthFirstSearch(List<Item> items, int knapsackLimit)
        {
            Node currentBest = new Node();
            Node parent = new Node();
            Queue<Node> frontier = InitQueue();
            List<Node> successors = new List<Node>();
            while (frontier.Count > 0)
            {
                parent = frontier.Dequeue();
                if(parent.action.itemNo != 0)
                {
                    if ((parent.weight < knapsackLimit) && (parent.benefit > currentBest.benefit))
                    {
                        currentBest = parent;
                    }
                    foreach (Node successor in GetSuccessors(parent, items))
                    {
                        frontier.Enqueue(successor);
                    }
                }
                else
                {
                    foreach (Node successor in GetSuccessors(parent, items))
                    {
                        frontier.Enqueue(successor);
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
                if ((allItems[i].itemNo != parent.action.itemNo) && !(parent.actions.Contains(allItems[i])))
                {
                    if(parent.weight + allItems[i].itemWeight < 420)
                    {
                        successors.Add(CreateChildNode(parent, allItems[i]));
                    }
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
            Node newChild = new Node(parent, newWeight, newBenefit, newActionsList, newAction);
            return newChild;
        }

        static List<Item> DepthFirstSearch(List<Item> items, int knapsackLimit)
        {
            Node currentBest = new Node();
            Node parent = new Node();
            Stack<Node> frontier = InitStack();
            List<Node> successors = new List<Node>();
            while (frontier.Count > 0)
            {
                parent = frontier.Pop();
                if (parent.action.itemNo != 0)
                {
                    if ((parent.weight < knapsackLimit) && (parent.benefit > currentBest.benefit))
                    {
                        currentBest = parent;
                    }
                    foreach (Node successor in GetSuccessors(parent, items))
                    {
                        frontier.Push(successor);
                    }
                }
                else
                {
                    foreach (Node successor in GetSuccessors(parent, items))
                    {
                        frontier.Push(successor);
                    }
                }
            }
            return currentBest.actions;
        }
    }
}

