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
            NeuralNetwork network = new NeuralNetwork(3, 4, 3, 1, trainingCases.Count);
            int noCorrectCases = 0, survived = 0, totalSurvived = 0;
            for (var i = 0; i < trainingCases.Count - 1; i++)
            {
                network.TrainNetwork(trainingCases[i], i);
            }
            NeuralNetwork.PrintMatrix(network.weightMatrices[0]);
            NeuralNetwork.PrintMatrix(network.weightMatrices[1]);
            for (var i = 0; i < validationCases.Count - 1; i++)
            {
                if (network.RunNetwork(validationCases[i]) == validationCases[i].Survived)
                {
                    if (network.RunNetwork(validationCases[i]) == 1)
                    {
                        survived++;
                    }
                    noCorrectCases++;
                }
            }

            totalSurvived = validationCases.FindAll(x => x.Survived > 0).Count;
            Console.WriteLine("Total survived in validation cases: {0}", totalSurvived);
            Console.WriteLine("Number of survived individuals: {0}", survived);
            Console.WriteLine("Number of dead cases: {0}", noCorrectCases - survived);
            Console.WriteLine("Number of correct cases: {0}", noCorrectCases);
            Console.WriteLine("Correct survived percentage: {0}", (double)survived / totalSurvived);
            Console.WriteLine("Correct percentage: {0}", (double)noCorrectCases/validationCases.Count);
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
