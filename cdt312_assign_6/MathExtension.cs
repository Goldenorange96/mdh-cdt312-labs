using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdt312_assign_6
{
    class MathExtension
    {
        public static void InitaliseMatrix(ref double[,] matrix, double value)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = value;
                }
            }
        }

        public static double CalculateDistance(City toCity, City fromCity)
        {
            double a = (toCity.X - fromCity.X) * (toCity.X - fromCity.X);
            double b = (toCity.Y - fromCity.Y) * (toCity.Y - toCity.Y);
            return Math.Sqrt(a + b);
        }
    }
}
