namespace Cdt312_assign_4
{
    using System;
    using System.Collections.Generic;
    class Program
    {
        static void Main(string[] args)
        {
            List<Passenger> trainingCases = new List<Passenger>();
            List<Passenger> validationCases = new List<Passenger>();
            ReadFile(ref trainingCases, ref validationCases);
            //ListUtilities.PrintList(trainingCases);
            NeuralNetwork network = new NeuralNetwork(3, 3, 2, 1, trainingCases.Count);
            int noIterations = trainingCases.Count + validationCases.Count;
            int ret = 0, noCorrectCases = 0;
            for (var i = 0; i < trainingCases.Count - 1; i++)
            {
                network.TrainNetwork(trainingCases[i], i);
            }
            for (var i = 0; i < validationCases.Count - 1; i++)
            {
                ret = network.RunNetwork(validationCases[i]);
                if (ret == validationCases[i].Survived)
                {
                    noCorrectCases++;
                }
            }
            Console.WriteLine("Number of correct cases: {0}", noCorrectCases);
            //NeuralNetwork.PrintVector(NeuralNetwork.survived);
            Console.ReadKey();
        }

        static void ReadFile(ref List<Passenger> trainingCases, ref List<Passenger> validationCases)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:.\titanic.dat");
            string line = null;
            string[] subStrings;
            double substringOne = 0.0;
            int caseCount = 0;
            while ((line = file.ReadLine()) != null)
            {
                subStrings = line.Split(':');
                if (double.TryParse(subStrings[0], out substringOne))
                {
                    //if the case is training case or not
                    if (!(caseCount >= 1500))
                    {
                        trainingCases.Add(new Passenger(double.Parse(subStrings[0]), double.Parse(subStrings[1]), double.Parse(subStrings[2]), double.Parse(subStrings[3])));
                    }
                    else
                    {
                        validationCases.Add(new Passenger(double.Parse(subStrings[0]), double.Parse(subStrings[1]), double.Parse(subStrings[2]), double.Parse(subStrings[3])));
                    }
                    caseCount++;
                }
            }
            file.Close();
        }
    }
}
