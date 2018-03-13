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

        public NeuralNetwork(int newNoLayers, int newNoInput, int newNoHidden, int newNoOutput, int noCases)
        {
            weightMatrices = new List<double[,]>();
            activationVectors = new List<double[]>();
            InitialiseWeightMatrix(newNoInput - 1, newNoInput);
            InitialiseWeightMatrix(newNoHidden - 1, newNoHidden);
            InitialiseActivationVectors(newNoInput, newNoHidden, newNoOutput);
            NoLayers = newNoLayers;
        }

        public void InitialiseActivationVectors(int noInput, int noHidden, int noOutput)
        {
            double[] newInputVector = new double[noInput+2];
            activationVectors.Add(newInputVector);
            double[] newHiddenVector = new double[noHidden+1];
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

        public void TrainNetwork(Passenger individual, int caseNo)
        {
            AssignInputValues(individual);
            for (var i = 0; i < NoLayers - 1; i++)
            {
                activationVectors[i + 1] = NNMath.CalcVectorMatrixProduct(activationVectors[i], weightMatrices[i]);
            }
            //Console.WriteLine("Output: {0}", activationVectors[2][0]);
            if (individual.Survived < 0)
            {
                Backpropegate(0.25);
            }
            else
            {
                Backpropegate(0.75);
            }
        }

        public double RunNetwork(Passenger individual)
        {
            AssignInputValues(individual);
            for (var i = 0; i < NoLayers - 1; i++)
            {
                activationVectors[i + 1] = NNMath.CalcVectorMatrixProduct(activationVectors[i], weightMatrices[i]);
            }
            int expected = GetSurvivedValue(activationVectors[2][0]);
            if (expected == 1)
            {
                Console.WriteLine("Ex: {0} Act:{1}", individual.Survived, activationVectors[2][0]);
            }
            return GetSurvivedValue(activationVectors[2][0]);
        }

        public int GetSurvivedValue(double val)
        {
            if (val >= 0.5)
            {
                return 1;
            }
            return -1;
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

        public static void PrintMatrix<T>(T[,] matrixToPrint)
        {
            Console.WriteLine("*** Printing Matrix ***");
            for (var i = 0; i < matrixToPrint.GetLength(0); i++)
            {
                for (var j = 0; j < matrixToPrint.GetLength(1); j++)
                {
                    Console.Write(" {0} ", matrixToPrint[i,j]);
                }
                Console.WriteLine("");
            }
        }

        public void Backpropegate(double target)
        {
            double outputError = activationVectors[2][0] * (1.0 - activationVectors[2][0]) * (target - activationVectors[2][0]);
            for (var i = 0; i < weightMatrices[1].GetLength(1); i++)
            {
                //if (i != weightMatrices[1].GetLength(1) - 1)
                //{
                    weightMatrices[1][0, i] += Params.LearningRate * outputError * activationVectors[1][i];
                //}
                //else
                //{
                    //weightMatrices[1][0, i] += Params.LearningRate * outputError * Params.Bias;
                //}
            }

            for (var i = 0; i < weightMatrices[0].GetLength(0); i++)
            {
                double errorHidden = activationVectors[1][i] * (1.0 - activationVectors[1][i]) * weightMatrices[1][0, i] * outputError;
                for (var j = 0; j < weightMatrices[0].GetLength(1); j++)
                {
                    //if (j != weightMatrices[0].GetLength(1) - 1)
                    //{
                        weightMatrices[0][i, j] += Params.LearningRate * errorHidden * activationVectors[0][j];
                    //}
                    //else
                    //{
                    //    weightMatrices[0][i, j] += Params.LearningRate * errorHidden * Params.Bias;
                    //}
                }
                
            }
        }
    }
}
