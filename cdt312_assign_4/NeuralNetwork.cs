namespace Cdt312_assign_4
{
    using System;
    using System.Collections.Generic;
    //ANN made on assumptiton of a single hidden layer.
    class NeuralNetwork
    {
        public int NoLayers;
        public List<double[,]> weightMatrices;
        public List<double[]> activationVectors;
        public static byte[] survived;

        public NeuralNetwork(int newNoLayers, int newNoInput, int newNoHidden, int newNoOutput, Passenger firstInput, int noCases)
        {
            survived = new byte[noCases];
            weightMatrices = new List<double[,]>();
            activationVectors = new List<double[]>();
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
            int tmp = 0;
            for (var i = 0; i < dimX; i++)
            {
                for (var j = 0; j < dimY; j++)
                {
                    newWeightMatrix[i, j] = NNMath.rng.NextDouble();
                    tmp = NNMath.rng.Next(0, 2);
                    if (tmp != 0)
                    {
                        newWeightMatrix[i, j] *= -1.0;
                    }
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

        public void ProcessCase(Passenger individual, int caseNo)
        {
            AssignInputValues(individual);
            for (var i = 0; i < NoLayers - 1; i++)
            {
                activationVectors[i + 1] = NNMath.CalcVectorMatrixProduct(activationVectors[i], weightMatrices[i]);
            }
            if (activationVectors[2][0] >= 0.5)
            {
                Backpropegate(0.75);
                survived[caseNo] = 1;
            }
            else
            {
                Backpropegate(0.25);
                survived[caseNo] = 0;
            }
        }

        public static void PrintVector<T>(T[] vectorToPrint)
        {
            Console.WriteLine("*-----*");
            for (var i = 0; i < vectorToPrint.GetLength(0); i++)
            {
                Console.WriteLine("{0}", vectorToPrint[i]);
            }
            Console.WriteLine("*-----*");
        }

        public double RecalcWeightsOutput(double target)
        {
            double delta = (activationVectors[2][0] * (1 - activationVectors[2][0])) * (target - activationVectors[2][0]);
            weightMatrices[weightMatrices.Count - 1][0, 0] = 0.1 * delta * activationVectors[1][0];
            weightMatrices[weightMatrices.Count - 1][0, 1] = 0.1 * delta * activationVectors[1][1];
            return delta;
        }

        public void Backpropegate(double target)
        {
            int k = 0;
            double a = 0, b = 0, c = 0;
            double deltaOutput = RecalcWeightsOutput(target);
            double delta = 0.0;



            //for each activation vector
            for (var i = activationVectors.Count - 2; i >= 0; i--)
            {
                //for each value in i-th A-vector
                for (var j = 0; j < activationVectors[i].GetLength(0); j++)
                {
                    a = (1.0 - activationVectors[i][j]);
                    b = weightMatrices[weightMatrices.Count - 1][0, i] * deltaOutput;
                    delta = activationVectors[i][j] * a * b;
                }

                for (var l = 0; l < activationVectors[k].GetLength(0); l++)
                {
                    weightMatrices[k][0, l] = 0.1 * delta * activationVectors[k][l];
                }
            }

        }

    }
}
