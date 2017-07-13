# VectorRotationBenchmarking

Benchmarking mathematical vector rotation through matrix-based and quaternion-based methods using BenchmarkDotNet to obtain memory-related, CPU-related and time-related information on these ops.

## Running the Project

1. Download Visual Studio 2017
2. Open the VectorRotationBenchmarking.sln solution file
3. Build the solution as Release, not Debug
4. Go to your bin\Release folder in the project's folder on Windows Explorer and **run VectorRotationBenchmarking.exe as admin** for the Hardware Counters to work

It should take a while on average hardware. By the end of the run, you should have the results of the benchmark on your screen as well as in the BenchmarkDotNet.Artifacts folder inside bin\Release.

## Works On My Machine (tm)

``` ini

BenchmarkDotNet=v0.10.8, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-5500U CPU 2.40GHz (Broadwell), ProcessorCount=4
Frequency=2338338 Hz, Resolution=427.6542 ns, Timer=TSC
  [Host]     : Clr 4.0.30319.42000, 32bit LegacyJIT-v4.7.2098.0
  DefaultJob : Clr 4.0.30319.42000, 32bit LegacyJIT-v4.7.2098.0
```
 |           Method |       Mean |    Error |   StdDev |   StdErr |        Min |         Q1 |     Median |         Q3 |        Max |        Op/s |  Gen 0 | Allocated | Timer/Op | TotalCycles/Op |
 |----------------- |-----------:|---------:|---------:|---------:|-----------:|-----------:|-----------:|-----------:|-----------:|------------:|-------:|----------:|---------:|---------------:|
 |     MatrixRotate | 1,611.6 ns | 24.62 ns | 21.83 ns | 5.833 ns | 1,571.6 ns | 1,594.1 ns | 1,613.7 ns | 1,621.5 ns | 1,658.5 ns |   620,502.9 | 0.6008 |    1264 B |       48 |           7322 |
 | QuaternionRotate |   799.8 ns | 14.59 ns | 12.94 ns | 3.457 ns |   781.9 ns |   789.9 ns |   797.8 ns |   806.3 ns |   827.4 ns | 1,250,267.5 | 0.3119 |     656 B |       21 |           3318 |
