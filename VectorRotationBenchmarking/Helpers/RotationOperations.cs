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
            // Rotation Matrix for 3-dimensional vectors around the Z-axis
            double[,] value = { { Trig.Cos(Trig.DegreeToRadian(degrees)), -Trig.Sin(Trig.DegreeToRadian(degrees)), 0 },
                                { Trig.Sin(Trig.DegreeToRadian(degrees)),  Trig.Cos(Trig.DegreeToRadian(degrees)), 0 },
                                { 0                                     ,  0                                     , 1 } };
            // Build the matrix and return it
            return Matrix<double>.Build.DenseOfArray(value);
        }

        /// <summary>
        /// Rotates a vector around the z-axis by a number of degrees using the matrix-based method.
        /// </summary>
        /// <param name="vector">The 3-dimensional vector to be rotated.</param>
        /// <param name="degrees">The number of degrees which to rotate.</param>
        /// <returns>The resulting vector of the rotation.</returns>
        public static double[] MatrixRotateZAxis(double[] vector, double degrees)
        {
            // Get the rotation matrix
            var rotationMatrix = GetRotationMatrixZAxis(degrees);
            // Build the Vector<> object from the array passed in
            var vectorForm = Vector<double>.Build.DenseOfArray(vector);
            // Return the result of the rotation
            return (rotationMatrix * vectorForm).ToArray();
        }

        /// <summary>
        /// Rotates a vector around the z-axis by a number of degrees using the quaternion-based method.
        /// </summary>
        /// <param name="vector">The 3-dimensional vector to be rotated.</param>
        /// <param name="degrees">The number of degrees which to rotate.</param>
        /// <returns>The resulting vector of the rotation.</returns>
        public static double[] QuaternionRotateZAxis(double[] vector, double degrees)
        {
            // The quaternion representation of the z-axis
            var axis = new Quaternion(0, 0, 0, 1);
            // The r quaternion from the r * q * r' formula
            var r = new Quaternion(Trig.Cos(Trig.DegreeToRadian(degrees/2)), Trig.Sin(Trig.DegreeToRadian(degrees/2)) * axis.Q1,
                Trig.Sin(Trig.DegreeToRadian(degrees/2)) * axis.Q2, Trig.Sin(Trig.DegreeToRadian(degrees/2)) * axis.Q3);
            // The r' quaternion from the r * q * r' formula
            var rprime = r.Invert();
            // The quaternion representation of the vector to be rotated
            var q = new Quaternion(0, vector[0], vector[1], vector[2]);
            // The resulting quaternion after the rotation
            var result = r * q * rprime;
            // Return the resulting vector from the result quaternion
            return new double[] { result.Q1, result.Q2, result.Q3 };
        }
    }
}
