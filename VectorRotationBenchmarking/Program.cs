using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorRotationBenchmarking.Helpers;

namespace VectorRotationBenchmarking
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] vector = { 0, 2, 3 };
            double[] rotatedVector1 = RotationOperations.MatrixRotate(vector, 20);
            double[] rotatedVector2 = RotationOperations.MatrixRotate(rotatedVector1, 40);

            // double[] rotatedVector = rotatedVector1 * rotatedVector2;

            Console.WriteLine("Matrix-based Rotation (60 degrees):");
            Console.WriteLine($"1st rotation (+ 20 degrees): ({rotatedVector1[0]}, {rotatedVector1[1]}, {rotatedVector1[2]})");
            Console.WriteLine($"2nd rotation (+ 40 degrees): ({rotatedVector2[0]}, {rotatedVector2[1]}, {rotatedVector2[2]})");

            Console.ReadLine();
        }
    }
}
