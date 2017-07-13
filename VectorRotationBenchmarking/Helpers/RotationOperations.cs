using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorRotationBenchmarking.Helpers
{
    public static class RotationOperations
    {
        private static Matrix<double> GetRotationMatrix(double degrees)
        {
            double[,] value = { { Trig.Cos(Trig.DegreeToRadian(degrees)), -Trig.Sin(Trig.DegreeToRadian(degrees)), 0 },
                                { Trig.Sin(Trig.DegreeToRadian(degrees)),  Trig.Cos(Trig.DegreeToRadian(degrees)), 0 },
                                { 0                                     ,  0                                     , 1 } };

            return Matrix<double>.Build.DenseOfArray(value);
        }

        public static double[] MatrixRotate(double[] vector, double degrees)
        {
            var rotationMatrix = GetRotationMatrix(degrees);

            var vectorForm = Vector<double>.Build.DenseOfArray(vector);
            return (rotationMatrix * vectorForm).ToArray();
        }
    }
}
