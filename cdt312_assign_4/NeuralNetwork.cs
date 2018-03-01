namespace Cdt312_assign_4
{
    using System;
    using System.Collections.Generic;
    //ANN made on assumptiton of a hidden layer.
    class NeuralNetwork
    {
        public Neuron[] Input;
        public Neuron[] Hidden;
        public Neuron[] Output;
        public double[,] weightMatrix;
        public double[] activationVector;
        public NeuralNetwork(int noInput, int noHidden, int noOutput)
        {
            Input = new Neuron[noInput];
            Hidden = new Neuron[noHidden];
            Output = new Neuron[noOutput];
        }

        public void InitialiseNetwork()
        {
            for (var i = 0; i < weightMatrix.Length - 1; i++)
            {
                for (var j = 0; i < weightMatrix.Length - 1; i++)
                {
                    weightMatrix[i, j] = NNMath.rng.NextDouble();
                }
            }
        }

        public void AssignInputValues(Passenger individual)
        {
            Input[0].Value = individual.Class;
            Input[1].Value = individual.Age;
            Input[2].Value = individual.Sex;
        }

        public void CalculateNextLayer()
        {

        }

    }
}
