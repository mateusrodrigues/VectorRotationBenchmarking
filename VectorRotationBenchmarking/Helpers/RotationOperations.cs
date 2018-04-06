using System;
using System.Numerics;

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
            // Convert the angle from degrees to radians
            var angleInRadians = degrees * Math.PI / 180;

            // Rotation Matrix for 3-dimensional vectors around the Z-axis
            double[,] rotationMatrix = { { Math.Cos(angleInRadians), -Math.Sin(angleInRadians), 0 },
                                         { Math.Sin(angleInRadians),  Math.Cos(angleInRadians), 0 },
                                         { 0                       ,  0                       , 1 } };

            // Calculate and return the result of the rotation
            return new double[]{ (rotationMatrix[0, 0] * vector[0] + rotationMatrix[0, 1] * vector[1] + rotationMatrix[0, 2] * vector[2]),
                                 (rotationMatrix[1, 0] * vector[0] + rotationMatrix[1, 1] * vector[1] + rotationMatrix[1, 2] * vector[2]),
                                 (rotationMatrix[2, 0] * vector[0] + rotationMatrix[2, 1] * vector[2] + rotationMatrix[2, 2] * vector[2]) };
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
            // Convert the angle from degrees to radians
            var angleInRadians = degrees * Math.PI / 180;

            // Turn the vector array into a System.Numerics Vector3 object
            var vectorObject = new Vector3((float)vector[0], (float)vector[1], (float)vector[2]);
            // Turn the axis array into a System.Numerics NORMALIZED Vector3 object
            var axisUnitVector = Vector3.Normalize(new Vector3((float)axis[0], (float)axis[1], (float)axis[2]));

            // Take the cross product between the axisUnitVector and vector to be used later
            // in the Rodrigues' rotation formula
            var crossProduct = Vector3.Cross(axisUnitVector, vectorObject);

            // First member of the Rodrigues' rotation formula: V*cos(theta)
            var firstMember = vectorObject * (float)Math.Cos(angleInRadians);
            // Second member of the Rodrigues' rotation formula: (K x V)*sin(theta)
            var secondMember = crossProduct * (float)Math.Sin(angleInRadians);
            // Third member of the Rodrigues' rotation formula: K * (K . V) * (1 - cos(theta))
            var thirdMember = axisUnitVector * Vector3.Dot(axisUnitVector, vectorObject) * (1 - (float)Math.Cos(angleInRadians));
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
            // Convert the angle from degrees to radians
            // already dividing the angle by two according to the
            // quaternion rotation formula
            var angleInRadians = (degrees / 2.0) * Math.PI / 180;
            // The quaternion representation of the z-axis
            // var axis = new Quaternion(0, 0, 0, 1);
            var axis = new System.Numerics.Quaternion(0, 0, 1, 0);
            // The r quaternion from the r * q * r' formula
            var r = new System.Numerics.Quaternion(
                (float)Math.Sin(angleInRadians) * axis.X,
                (float)Math.Sin(angleInRadians) * axis.Y,
                (float)Math.Sin(angleInRadians) * axis.Z,
                (float)Math.Cos(angleInRadians)
            );
            // The r' quaternion from the r * q * r' formula
            // var rprime = r.Invert();
            var rprime = System.Numerics.Quaternion.Inverse(r);
            // The quaternion representation of the vector to be rotated
            var q = new System.Numerics.Quaternion((float)vector[0], (float)vector[1], (float)vector[2], 0);
            // The resulting quaternion after the rotation
            var result = r * q * rprime;
            // Return the resulting vector from the result quaternion
            return new double[] { result.X, result.Y, result.Z };
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
            // Convert the angle from degrees to radians
            // already dividing the angle by two according to the
            // quaternion rotation formula
            var angleInRadians = (degrees / 2.0) * Math.PI / 180;
            // The quaternion representation of the rotation axis
            var unitAxis = System.Numerics.Quaternion.Normalize(new System.Numerics.Quaternion((float)axis[0], (float)axis[1], (float)axis[2], 0));
            // The r quaternion from the r * q * r' formula
            var r = new System.Numerics.Quaternion(
                (float)Math.Sin(angleInRadians) * unitAxis.X,
                (float)Math.Sin(angleInRadians) * unitAxis.Y,
                (float)Math.Sin(angleInRadians) * unitAxis.Z,
                (float)Math.Cos(angleInRadians)
            );
            // The r' quaternion from the r * q * r' formula
            var rprime = System.Numerics.Quaternion.Inverse(r);
            // The quaternion representation of the vector to be rotated
            var q = new System.Numerics.Quaternion((float)vector[0], (float)vector[1], (float)vector[2], 0);
            // The resulting quaternion after the rotation
            var result = r * q * rprime;
            // Return the resulting vector from the result quaternion
            return new double[] { result.X, result.Y, result.Z };
        }
    }
}
