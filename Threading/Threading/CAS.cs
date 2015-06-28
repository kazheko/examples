using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading
{
    class Cas
    {
        static void Main(string[] args)
        {
            string x = "1", y = "2";
            DoWithCas(ref x, temp=>temp+y);
        }

        static void Operation(ref string x, string y)
        {
            string temp, result;
            do
            {
                temp = x;
                result = temp + y;
            } while (Interlocked.CompareExchange(ref x, result, temp) != temp);
        }

        static void DoWithCas<T>(ref T location, Func<T, T> func) 
            where T : class 
        {
            T temp, result;
            do
            {
                temp = location;
                result = func(temp);
            } while (Interlocked.CompareExchange<T>(ref location, result, temp) != temp);
        }
    }
}
