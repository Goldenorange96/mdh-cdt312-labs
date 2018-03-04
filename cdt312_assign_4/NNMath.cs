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
            double[] result = new double[3];

            for (var i = 0; i < weightMatrix.Length; i++)
            {
                for (var j = 0; j < weightMatrix.Length; i++)
                {
                    result[i] += weightMatrix[i, j] * activationVector[j];
                }
            }
            result = CalcSigmoid(result);
            return result;
        }

        public static double[] CalcSigmoid(double[] val)
        {
            double[] result = new double[val.Length];
            for (var i = 0; i < val.Length; i++)
            {
                result[i] = (1.0 / (1.0 + (Math.Pow(Math.E, -val[i]))));
            }

            return result;
        }
    }
}
