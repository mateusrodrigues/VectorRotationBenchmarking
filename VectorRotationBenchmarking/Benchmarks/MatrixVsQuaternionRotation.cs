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
    [HardwareCounters(HardwareCounter.TotalCycles, HardwareCounter.Timer)]
    public class MatrixVsQuaternionRotation
    {
        private readonly double[] vector = { 0, 2, 3 };

        [Benchmark]
        public double[] MatrixRotate()
        {
            var rotatedMatrix1 = RotationOperations.MatrixRotateZAxis(vector, 20);
            var rotatedMatrix2 = RotationOperations.MatrixRotateZAxis(rotatedMatrix1, 40);
            return rotatedMatrix2;
        }

        [Benchmark]
        public double[] QuaternionRotate()
        {
            var rotatedQuaternion1 = RotationOperations.QuaternionRotateZAxis(vector, 20);
            var rotatedQuaternion2 = RotationOperations.QuaternionRotateZAxis(rotatedQuaternion1, 40);
            return rotatedQuaternion2;
        }
    }
}
