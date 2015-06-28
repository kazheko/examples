using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading
{
    class Cache
    {
        static void Main(string[] args)
        {
            var array = CreateArray(16000, 16000);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var sum = Sum(array);

            stopwatch.Stop();
            Console.WriteLine("time = {0}",stopwatch.Elapsed);
            Console.WriteLine("sum = {0}", sum);

            Console.ReadKey();
        }

        static int[,] CreateArray(int rowCount, int colCount)
        {
            var result = new int[rowCount, colCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    result[i, j] = 1;
                }
            }
            return result;
        }

        //static int Sum(int[,] array)
        //{
        //    int sum = 0;

        //    int rowCount = array.GetUpperBound(0) + 1;
        //    int colCount = array.GetUpperBound(1) + 1;

        //    for (int i = 0; i < rowCount; i++)
        //    {
        //        for (int j = 0; j < colCount; j++)
        //        {
        //            sum += array[i, j];
        //        }
        //    }
        //    return sum;
        //}

        static int Sum(int[,] array)
        {
            int rowCount = array.GetUpperBound(0) + 1;
            int colCount = array.GetUpperBound(1) + 1;

            var processorCount = Environment.ProcessorCount;
            var chunk = rowCount / processorCount;

            var threads = new Thread[processorCount];
            var results = new int[processorCount];
            for (int processorIndex = 0; processorIndex < processorCount; processorIndex++)
            {
                var start = processorIndex * chunk;
                var end = start + chunk;

                var temp = processorIndex;
                threads[processorIndex] = new Thread(() =>
                {
                    int localResult = 0;
                    for (int i = start; i < end; i++)
                    {
                        for (int j = 0; j < colCount; j++)
                        {
                            localResult += array[i, j];
                        }
                    }
                    results[temp] = localResult;
                });

                threads[processorIndex].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            return results.Sum();
        }
    }
}
