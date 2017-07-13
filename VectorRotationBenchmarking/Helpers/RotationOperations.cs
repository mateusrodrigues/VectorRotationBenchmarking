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
        private static Matrix<double> GetRotationMatrixZAxis(double degrees)
        {
            // Rotation Matrix for 3D vectors around the Z-axis
            double[,] value = { { Trig.Cos(Trig.DegreeToRadian(degrees)), -Trig.Sin(Trig.DegreeToRadian(degrees)), 0 },
                                { Trig.Sin(Trig.DegreeToRadian(degrees)),  Trig.Cos(Trig.DegreeToRadian(degrees)), 0 },
                                { 0                                     ,  0                                     , 1 } };

            return Matrix<double>.Build.DenseOfArray(value);
        }

        public static double[] MatrixRotateZAxis(double[] vector, double degrees)
        {
            var rotationMatrix = GetRotationMatrixZAxis(degrees);

            var vectorForm = Vector<double>.Build.DenseOfArray(vector);
            return (rotationMatrix * vectorForm).ToArray();
        }

        public static double[] QuaternionRotateZAxis(double[] vector, double degrees)
        {
            var axis = new Quaternion(0, 0, 0, 1);
            var r = new Quaternion(Trig.Cos(Trig.DegreeToRadian(degrees/2)), Trig.Sin(Trig.DegreeToRadian(degrees/2)) * axis.Q1,
                Trig.Sin(Trig.DegreeToRadian(degrees/2)) * axis.Q2, Trig.Sin(Trig.DegreeToRadian(degrees/2)) * axis.Q3);
            var rprime = r.Invert();
            var q = new Quaternion(0, vector[0], vector[1], vector[2]);

            var result = r * q * rprime;
            return new double[] { result.Q1, result.Q2, result.Q3 };
        }
    }
}
