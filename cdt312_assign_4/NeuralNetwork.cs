namespace Cdt312_assign_4
{
    using System.Collections.Generic;
    //ANN made on assumptiton of a single hidden layer.
    class NeuralNetwork
    {
        public int NoLayers;
        public List<double[,]> weightMatrices;
        public List<double[]> activationVectors;
        public NeuralNetwork(int newNoLayers, int newNoInput, int newNoHidden, int newNoOutput, Passenger firstInput)
        {
            InitialiseWeightMatrix(newNoInput - 1, newNoInput);
            InitialiseWeightMatrix(newNoHidden - 1, newNoHidden);
            InitialiseActivationVectors(newNoInput, newNoHidden, newNoOutput);
            AssignInputValues(firstInput);
            NoLayers = newNoLayers;
        }

        public void InitialiseActivationVectors(int noInput, int noHidden, int noOutput)
        {
            double[] newInputVector = new double[noInput];
            activationVectors.Add(newInputVector);
            double[] newHiddenVector = new double[noHidden];
            activationVectors.Add(newHiddenVector);
            double[] newOutputVector = new double[noOutput];
            activationVectors.Add(newOutputVector);
        }

        public void InitialiseWeightMatrix(int dimX, int dimY)
        {
            double[,] newWeightMatrix = new double[dimX, dimY];
            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; i < 3; j++)
                {
                    newWeightMatrix[i, j] = NNMath.rng.NextDouble() * -NNMath.rng.Next(0, 1);
                }
            }
            weightMatrices.Add(newWeightMatrix);
        }

        public void AssignInputValues(Passenger individual)
        {
                activationVectors[0][0] = individual.Class;
                activationVectors[0][1] = individual.Age;
                activationVectors[0][2] = individual.Sex;
        }

        public void ProcessCase(Passenger individual)
        {
            AssignInputValues(individual);
            for (var i = 0; i < NoLayers; i++)
            {
                activationVectors[i + 1] = NNMath.CalcVectorMatrixProduct(activationVectors[i], weightMatrices[i]);
            }
        }

    }
}
