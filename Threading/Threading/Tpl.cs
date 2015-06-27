using System;
using System.Threading.Tasks;

namespace Threading
{
    class TPL
    {
        private static void Main(string[] args)
        {
            var task1 = new Task<int>(() =>
            {
                var task2 = new Task(() => { throw new Exception("test 2"); });
                task2.Start();
                throw new Exception("test 1");
                Console.WriteLine("task1");
                return -1;
            });
            
            task1.Start();
            var result = task1.Result;

            Console.ReadKey();
        }
    }
}
