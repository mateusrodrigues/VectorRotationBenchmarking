using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VectorRotationBenchmarking.Helpers
{
    public static class RotationOperations
    {
        /// <summary>
        /// Rotates a vector around the z-axis by a number of degrees using the matrix-based method.
        /// </summary>
        /// <param name="vector">The 3-dimensional vector to be rotated.</param>
        /// <param name="degrees">The number of degrees which to rotate.</param>
        /// <returns>The resulting vector of the rotation.</returns>
        public static double[] MatrixRotateZAxis(double[] vector, double degrees)
        {
            var angleInRadians = Trig.DegreeToRadian(degrees);
            // Rotation Matrix for 3-dimensional vectors around the Z-axis
            double[,] value = { { Trig.Cos(angleInRadians), -Trig.Sin(angleInRadians), 0 },
                                { Trig.Sin(angleInRadians),  Trig.Cos(angleInRadians), 0 },
                                { 0                       ,  0                       , 1 } };

            // Get the rotation matrix
            var rotationMatrix = Matrix<double>.Build.DenseOfArray(value);
            // Build the Vector<> object from the array passed in
            var vectorForm = Vector<double>.Build.DenseOfArray(vector);
            // Return the result of the rotation
            return (rotationMatrix * vectorForm).ToArray();
        }

        /// <summary>
        /// Rotates a vector around an arbitraty axis represented by a unit vector using Rodrigue's rotation formula.
        /// </summary>
        /// <param name="vector">The 3-dimensional vector to be rotated.</param>
        /// <param name="axis">The unit vector representation of the axis which to rotate around.</param>
        /// <param name="degrees">The number of degrees which to rotate.</param>
        /// <returns>The resulting vector of the rotation.</returns>
        public static double[] MatrixRotateArbitraryAxis(double[] vector, double[] axis, double degrees)
        {
            // Turn the vector array into a System.Numerics Vector3 object
            var vectorObject = new Vector3((float)vector[0], (float)vector[1], (float)vector[2]);
            // Turn the axis array into a System.Numerics NORMALIZED Vector3 object
            var axisUnitVector = Vector3.Normalize(new Vector3((float)axis[0], (float)axis[1], (float)axis[2]));

            // Take the cross product between the axisUnitVector and vector to be used later
            // in the Rodrigues' rotation formula
            var crossProduct = Vector3.Cross(axisUnitVector, vectorObject);

            // First member of the Rodrigues' rotation formula: V*cos(theta)
            var firstMember = vectorObject * (float)Trig.Cos(Trig.DegreeToRadian(degrees));
            // Second member of the Rodrigues' rotation formula: (K x V)*sin(theta)
            var secondMember = crossProduct * (float)Trig.Sin(Trig.DegreeToRadian(degrees));
            // Third member of the Rodrigues' rotation formula: K * (K . V) * (1 - cos(theta))
            var thirdMember = axisUnitVector * Vector3.Dot(axisUnitVector, vectorObject) * (1 - (float)Trig.Cos(Trig.DegreeToRadian(degrees)));
            // Summing all members together
            var result = firstMember + secondMember + thirdMember;
            
            return new double[] { result.X, result.Y, result.Z };
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

        /// <summary>
        /// Rotates a vector around an arbitrary axis denoted by an unit vector using the quaternion-based method.
        /// </summary>
        /// <param name="vector">The 3-dimensional vector to be rotated.</param>
        /// <param name="axis">The arbitrary axis of rotation - this vector has to be UNITARY.</param>
        /// <param name="degrees">The number of degrees which to rotate.</param>
        /// <returns>The resulting vector of the rotation.</returns>
        public static double[] QuaternionRotateArbitraryAxis(double[] vector, double[] axis, double degrees)
        {
            // The quaternion representation of the rotation axis
            var unitAxis = new Quaternion(0, axis[0], axis[1], axis[2]).Normalize();
            // The r quaternion from the r * q * r' formula
            var r = new Quaternion(Trig.Cos(Trig.DegreeToRadian(degrees / 2)), Trig.Sin(Trig.DegreeToRadian(degrees / 2)) * unitAxis.Q1,
                Trig.Sin(Trig.DegreeToRadian(degrees / 2)) * unitAxis.Q2, Trig.Sin(Trig.DegreeToRadian(degrees / 2)) * unitAxis.Q3);
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
