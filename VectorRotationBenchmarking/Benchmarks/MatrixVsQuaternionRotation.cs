using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Diagnosers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorRotationBenchmarking.Helpers;

namespace VectorRotationBenchmarking.Benchmarks
{
    [AllStatisticsColumn]
    [MemoryDiagnoser]
    // [HardwareCounters(HardwareCounter.TotalCycles, HardwareCounter.Timer)]
    public class MatrixVsQuaternionRotation
    {
        private readonly double[] vector = { 0, 2, 3 };

        [Params(1, 2, 3, 4)]
        public int Turns { get; set; }

        [Benchmark]
        public double[] MatrixRotate()
        {
            double[] rotatedMatrix = vector;
            for (int i = 0; i < Turns; i++)
            {
                rotatedMatrix = RotationOperations.MatrixRotateZAxis(rotatedMatrix, 15);
            }
            // var rotatedMatrix1 = RotationOperations.MatrixRotateZAxis(vector, 20);
            // var rotatedMatrix2 = RotationOperations.MatrixRotateZAxis(rotatedMatrix1, 40);
            return rotatedMatrix;
        }

        [Benchmark]
        public double[] QuaternionRotate()
        {
            double[] rotatedQuaternion = vector;
            for (int i = 0; i < Turns; i++)
            {
                rotatedQuaternion = RotationOperations.QuaternionRotateZAxis(rotatedQuaternion, 15);
            }
            // var rotatedQuaternion1 = RotationOperations.QuaternionRotateZAxis(vector, 20);
            // var rotatedQuaternion2 = RotationOperations.QuaternionRotateZAxis(rotatedQuaternion1, 40);
            return rotatedQuaternion;
        }
    }
}
