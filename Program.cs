using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCMemoryOptimization
{
    class Program
    {
        static void Main(string[] args)
        {
            //StringAppend();
            //InitializeBeforeUse();
            //SetCollectionsSize();
            //ReuseCollections();
            //BoxingExample();
            //ClassVsStruct();
            //ToListInsideLinq();
            //Finalyzers();
            GC.Collect(); //To be sure that collection happened
        }

        private static void Finalyzers()
        {
            for (int i = 0; i < 1000; i++)
            {
                var o = new FinalyzerObject(i);
                int id = o.Id;
                //o.Dispose();
            }
        }

        private static void ToListInsideLinq()
        {
            var numbers = Enumerable.Range(0, 100000).Where(e => e % 2 == 1).ToList();

            Random rand = new Random();
            var randomNumbers = new List<int>();
            for (int i = 0; i < 10000; i++)
            {
                randomNumbers.Add(rand.Next());
            }

            randomNumbers = randomNumbers.Where(x => numbers.Contains(x)).ToList();
        }

        private static void ClassVsStruct()
        {
            const int count = 10000;
            var lines = new List<StraightLineStruct>(count);
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                //y = ax+b
                var line = new StraightLineStruct { A = rand.Next(-100, 100) / 100, B = rand.Next(-100, 100) };
                lines.Add(line);
            }
            lines = lines.Where(DoesLinePassesThroughThePoint).ToList();

            bool DoesLinePassesThroughThePoint(StraightLineStruct line)
            {
                var x = 1;
                var y = 1;
                return y == line.A * x + line.B;
            }
        }

        private static void BoxingExample()
        {
            var bElements = Enumerable.Range(0, 100000).Where(e => HasNegativeHashCode(e)).ToList();

            bool HasNegativeHashCode<T>(T element)
            {
                return element.GetHashCode() < 0;
            }
        }

        private static void SetCollectionsSize()
        {
            int count = 10000;
            var evenNumbers = new List<int>(count/2);
            for (int i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                    evenNumbers.Add(i);
            }
        }

        private static void ReuseCollections()
        {
            var numbers = new List<int>(500000);
            for (int i = 0; i < 1000000; i++)
            {
                if (i % 2 == 0)
                    numbers.Add(i);
            }

            //var oddNumbers = new List<int>(500000);
            numbers.Clear();
            for (int i = 0; i < 1000000; i++)
            {
                if (i % 2 == 1)
                    numbers.Add(i);
            }
        }

        private static void InitializeBeforeUse()
        {
            //some long lasting operation
            string s = string.Empty;
            for(int i=0; i<2000; i++)
            {
                s += "" + i.ToString();
            }

            Point o = new Point { X = 1 };
            Console.WriteLine(o.X);
        }

        private static void StringAppend()
        {
            //string s = string.Empty;
            //Enumerable.Range(0, 10000).ToList().ForEach(x => s += ".");
            StringBuilder sb = new StringBuilder("");
            Enumerable.Range(0, 10000).ToList().ForEach(x => sb.Append("."));
        }
    }
}
