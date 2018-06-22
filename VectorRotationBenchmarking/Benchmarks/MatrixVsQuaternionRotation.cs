using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
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
#if NET471
    [MemoryDiagnoser]
    [HardwareCounters(HardwareCounter.TotalCycles, HardwareCounter.Timer)]
#endif
    [HtmlExporter]
    [MarkdownExporter]
    [CsvExporter]
    [CsvMeasurementsExporter]
    public class MatrixVsQuaternionRotation
    {
        private readonly double[] vector = { 0, 2, 3 };

        [Params(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15)]
        public int Turns { get; set; }

        [Benchmark]
        public double[] MatrixRotate()
        {
            double[] rotatedMatrix = vector;
            for (int i = 0; i < Turns; i++)
            {
                rotatedMatrix = RotationOperations.MatrixRotateZAxis(rotatedMatrix, 15);
            }

            return rotatedMatrix;
        }

        [Benchmark]
        public double[] MatrixRotateArbitrary()
        {
            double[] rotatedMatrix = vector;
            for (int i = 0; i < Turns; i++)
            {
                rotatedMatrix = RotationOperations.MatrixRotateArbitraryAxis(rotatedMatrix, new double[] { 3, 4, 5 }, 15);
            }
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

            return rotatedQuaternion;
        }

        [Benchmark]
        public double[] QuaternionRotateArbitrary()
        {
            double[] rotatedQuaternion = vector;
            for (int i = 0; i < Turns; i++)
            {
                rotatedQuaternion = RotationOperations.QuaternionRotateArbitraryAxis(rotatedQuaternion, new double[] { 3, 4, 5 }, 15);
            }
            return rotatedQuaternion;
        }
    }
}
