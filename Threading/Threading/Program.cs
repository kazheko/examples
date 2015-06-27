using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            uint start = 0, end = 240000;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = PrimesInRange(start, end);

            stopwatch.Stop();
            Console.WriteLine("[{0},{1}] - {2}", start, end, stopwatch.Elapsed);
            Console.WriteLine(result.Count());

            Console.ReadKey();
        }

        static bool IsPrime(uint item)
        {
            if (item == 2) return true;
            if (item % 2 == 0) return false;

            for (uint i = 3; i < item; i += 2)
            {
                if (item % i == 0) return false;
            }

            return true;
        }

        // example 1
        static IEnumerable<uint> PrimesInRange(uint start, uint end)
        {
            var result = new List<uint>();
            for (var i = start; i < end; i++)
            {
                if (IsPrime(i)) result.Add(i);
            }
            return result;
        }

        // example 2
        //static IEnumerable<uint> PrimesInRange(uint start, uint end)
        //{
        //    var result = new List<uint>();

        //    var countThread = Environment.ProcessorCount;
        //    var chunk = (end - start) / (uint)countThread;

        //    var threads = new Thread[countThread];

        //    var allDone = new ManualResetEvent(initialState: false);
        //    long temp = 0;

        //    for (uint i = 0; i < countThread; i++)
        //    {
        //        var chunkStart = start + chunk * i;
        //        var chunkEnd = chunkStart + chunk;

        //        threads[i] = new Thread(() =>
        //        {
        //            var stopwatch = new Stopwatch();
        //            stopwatch.Start();

        //            for (var j = chunkStart; j < chunkEnd; j++)
        //            {
        //                if (IsPrime(j))
        //                {
        //                    lock (result)
        //                    {
        //                        result.Add(j);
        //                    }
        //                }
        //            }

        //            stopwatch.Stop();
        //            Console.WriteLine("[{0},{1}] - {2}", chunkStart, chunkEnd, stopwatch.Elapsed);

        //            if (Interlocked.Increment(ref temp) == countThread) allDone.Set();

        //        });

        //        threads[i].Start();
        //    }

        //    allDone.WaitOne();

        //    return result;
        //}

        // example 3
        //private static IEnumerable<uint> PrimesInRange(uint start, uint end)
        //{
        //    var result = new List<uint>();

        //    uint chunk = 100;
        //    var actionCount = (end - start) / chunk;

        //    var allDone = new ManualResetEvent(initialState: false);
        //    long temp = 0;

        //    for (uint i = 0; i < actionCount; i++)
        //    {
        //        var chunkStart = start + chunk * i;
        //        var chunkEnd = chunkStart + chunk;

        //        ThreadPool.QueueUserWorkItem(obj =>
        //        {
        //            for (var j = chunkStart; j < chunkEnd; j++)
        //            {
        //                if (IsPrime(j))
        //                {
        //                    lock (result)
        //                    {
        //                        result.Add(j);
        //                    }
        //                }
        //            }
        //            if (Interlocked.Increment(ref temp) == actionCount) allDone.Set();
        //        });
        //    }

        //    allDone.WaitOne();

        //    return result;
        //}

        ////example 4
        //private static IEnumerable<uint> PrimesInRange(uint start, uint end)
        //{

        //    uint chunk = 100;
        //    var actionCount = (end - start) / chunk;

        //    var tasks = new Task<IEnumerable<uint>>[actionCount];

        //    for (uint i = 0; i < actionCount; i++)
        //    {
        //        var chunkStart = start + chunk * i;
        //        var chunkEnd = chunkStart + chunk;

        //        var task = new Task<IEnumerable<uint>>(() =>
        //        {
        //            var localResult = new List<uint>();
        //            for (var j = chunkStart; j < chunkEnd; j++)
        //            {
        //                if (IsPrime(j))
        //                {
        //                    localResult.Add(j);
        //                }
        //            }
        //            return localResult;
        //        });
        //        tasks[i] = task;
        //        task.Start();
        //    }

        //    Task.WaitAll(tasks);

        //    return tasks.SelectMany(t=>t.Result);
        //}

        // example 5.1
        //static IEnumerable<uint> PrimesInRange(uint start, uint end)
        //{
        //    var result = new List<uint>();
        //    Parallel.For(start, end, i =>
        //    {
        //        if (IsPrime((uint)i))
        //        {
        //            lock (result)
        //            {
        //                result.Add((uint)i);
        //            }
        //        }
        //    });
        //    return result;
        //}

        //// example 5.2
        //static IEnumerable<uint> PrimesInRange(uint start, uint end)
        //{
        //    var result = new List<uint>();
        //    Parallel.For(start, end, () => new List<uint>(), (i, state, localResult) =>
        //    {
        //        if (IsPrime((uint)i))
        //        {
        //            localResult.Add((uint)i);
        //        }
        //        return localResult;
        //    }, localResult =>
        //    {
        //        lock (result)
        //        {
        //            result.AddRange(localResult);
        //        }
        //    });
        //    return result;
        //}

        //// example 5.3
        //static IEnumerable<uint> PrimesInRange(uint start, uint end)
        //{
        //    var result = new List<uint>();
        //    Parallel.ForEach(Partitioner.Create(start, end), (range) =>
        //    {
        //        for (var i = range.Item1; i < range.Item2; i++)
        //        {
        //            if (IsPrime((uint) i))
        //            {
        //                lock (result)
        //                {
        //                    result.Add(Convert.ToUInt32(i));
        //                }
        //            }
        //        }
        //    });
        //    return result;
        //}

        //// example 6
        //static IEnumerable<uint> PrimesInRange(uint start, uint end)
        //{
        //    return Enumerable.Range((int)start, (int)(end - start))
        //        .AsParallel()
        //        .Select(Convert.ToUInt32)
        //        .Where(IsPrime)
        //        .ToList();
        //}
    }
}
