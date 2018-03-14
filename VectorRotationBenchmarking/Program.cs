using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorRotationBenchmarking.Benchmarks;
using VectorRotationBenchmarking.Helpers;

namespace VectorRotationBenchmarking
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] vector = { 0, 2, 3 };
            double[] rotatedVector1 = RotationOperations.MatrixRotateZAxis(vector, 20);
            double[] rotatedVector2 = RotationOperations.MatrixRotateZAxis(rotatedVector1, 40);

            Console.WriteLine("Matrix-based Rotation (60 degrees):");
            Console.WriteLine($"1st rotation (+ 20 degrees): ({rotatedVector1[0]}, {rotatedVector1[1]}, {rotatedVector1[2]})");
            Console.WriteLine($"2nd rotation (+ 40 degrees): ({rotatedVector2[0]}, {rotatedVector2[1]}, {rotatedVector2[2]})");

            Console.ReadLine();

            double[] rotatedQuaternion1 = RotationOperations.QuaternionRotateZAxis(vector, 20);
            double[] rotatedQuaternion2 = RotationOperations.QuaternionRotateZAxis(rotatedQuaternion1, 40);

            Console.WriteLine("Quaternion-based Rotation (60 degrees):");
            Console.WriteLine($"1st rotation (+ 20 degrees): ({rotatedQuaternion1[0]}, {rotatedQuaternion1[1]}, {rotatedQuaternion1[2]})");
            Console.WriteLine($"2nd rotation (+ 40 degrees): ({rotatedQuaternion2[0]}, {rotatedQuaternion2[1]}, {rotatedQuaternion2[2]})");

            Console.ReadLine();

            var matrixRotate = RotationOperations.MatrixRotateArbitraryAxis(vector, new double[] { 3, 4, 5 }, 30);
            Console.WriteLine($"Matrix Rotation Arbitrary: ({matrixRotate[0]}, {matrixRotate[1]}, {matrixRotate[2]})");
            matrixRotate = RotationOperations.QuaternionRotateArbitraryAxis(vector, new double[] { 3, 4, 5 }, 30);
            Console.WriteLine($"Quaternion Rotation Arbitrary: ({matrixRotate[0]}, {matrixRotate[1]}, {matrixRotate[2]})");

            //Console.ReadLine();

            //Console.WriteLine("Diagnostics:");
            //var summary = BenchmarkRunner.Run<MatrixVsQuaternionRotation>();

            Console.ReadLine();
        }
    }
}
