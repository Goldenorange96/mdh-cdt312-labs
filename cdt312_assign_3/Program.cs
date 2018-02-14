namespace Cdt312_assign_3
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        public static void Main(string[] args)
        {
            List<Node> allCities = new List<Node>();
            ReadFileAndGenerateList(ref allCities);
            Console.ReadKey();
        }

        public static void ReadFileAndGenerateList(ref List<Node> allCities)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:.\berlin52.tsp");
            string line = null;
            string[] subStrings;
            int id = 0;
            while ((line = file.ReadLine()) != null)
            {
                subStrings = line.Split(null);
                if (int.TryParse(subStrings[0], out id))
                {
                    Node newNode = new Node(id, double.Parse(subStrings[1]), double.Parse(subStrings[2]));
                    allCities.Add(newNode);
                }

            }
            file.Close();

        }
        
        /*
         * input: A node list
         * output: none
         * 
         * Function prints the given node list including all properties of each item in the list.
         */
        public static void PrintCityList(List<Node> listToPrint)
        {
            if (listToPrint != null)
            {
                Console.WriteLine("<----------------------->");

                foreach (Node city in listToPrint)
                {
                    Console.WriteLine("> Id: {0} X: {1} Y: {2}", city.Id, city.X, city.Y);
                }

                Console.WriteLine("<----------------------->");
            }
            
        }

        public static List<Node> InitializePopulation(List<Node> space)
        {
            List<Node> initialPopulation = new List<Node>();

            return initialPopulation;
        }

        public static void PerformSelection()
        {

        }

        public static void Mutate()
        {
        }

        /*
         * input: individual
         * output: fitness of individual
         * 
         * Function calculates the fitness of a provided individual. Fitness is based off the Euclidean distance.
         */
        public static double CalculateFitness(Node individual)
        {
            double fitness = 0.0;
            return fitness;
        }

    }
}
