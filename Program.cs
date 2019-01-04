using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Sample04
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }
        public void Run()
        {
            Console.WriteLine("Start Run");
            // create data source
            int[] data = new int[100000];
            for (int i = 0; i < 100000; i++)
                data[i] = i;

            double result01 = 0;
            foreach (int i in data)
                result01 += DoSomeWork(i);
            Console.WriteLine("Result: {0}", result01);

            double result02 = 0;
            object locker = new Object();
            Parallel.ForEach<int, double>(data,
            () =>
            {
                double tls = 0.0;
                return tls;
            },
            (item, pls, i, tls) =>
            {
                tls += DoSomeWork(item);
                return tls;

            },
            (tls) =>
            {
                result02 += tls;
            });
            Console.WriteLine("Result: {0}", result02);

            Console.WriteLine("End Run");
            Console.ReadLine();
        }
        private double DoSomeWork(int index)
        {
            return Math.Sin(index) + Math.Sqrt(index) * Math.Pow(index, 0.14);
        }
    }
}
