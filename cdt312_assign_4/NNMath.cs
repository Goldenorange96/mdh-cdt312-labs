namespace Cdt312_assign_4
{
    using System;
    using System.Windows;
    class NNMath
    {
        public static Random rng = new Random();

        /* 
        * input: value corresponding to element in weight vector.
        * output: calculated new neuron value using sigmoid.
        * 
        * Function applies Sigmoid calculation on the provided value, return value will be in range [0,1]
        */

        public static double[] CalcVectorMatrixProduct(double[] activationVector, double[,] weightMatrix)
        {
            double[] result = new double[weightMatrix.GetLength(0)];
            for (var i = 0; i < weightMatrix.GetLength(0); i++)
            {
                for (var j = 0; j < weightMatrix.GetLength(1); j++)
                {
                    result[i] += (weightMatrix[i, j] * activationVector[j]);
                }
                result[i] += Params.Bias;
            }
            result = CalcSigmoid(result);
            return result;
        }

        public static double[] CalcSigmoid(double[] val)
        {
            double[] result = new double[val.Length];
            double tmp = 0.0;
            for (var i = 0; i < val.GetLength(0); i++)
            {
                tmp = Math.Pow(Math.E, -val[i]);
                result[i] = (1.0 / (1.0 + tmp));
            }
            return result;
        }
    }
}
