namespace Cdt312_assign_4
{
    using System;
    using System.Windows;
    class NNMath
    {
        /* 
        * input: value corresponding to element in weight vector.
        * output: calculated new neuron value using sigmoid.
        * 
        * Function applies Sigmoid calculation on the provided value, return value will be in range [0,1]
        */

        static double[] CalcVectorMatrixProduct(double[] activationVector, double[,] weightMatrix)
        {
            double[] result = new double[3];

            return result;
        }

        static double CalcSigmoid(double val)
        {
            return (1.0 / (1.0 + (Math.Pow(Math.E, -val))));
        }
    }
}
