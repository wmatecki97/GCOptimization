using System;
using System.Collections.Generic;
using System.Linq;

namespace GCMemoryOptimization
{
    class Program
    {
        static void Main(string[] args)
        {
            //StringAppend();
            //FitTheRulesExample();
            //ListExample();
            //EvenAndOddNumbersExample();
            //BoxingExample();
            //LineExample();
            //LinqExample();
            Finalyzers();

            GC.Collect(); //To be sure that collection happened
        }

        private static void Finalyzers()
        {
            for (int i = 0; i < 1000; i++)
            {
                var o = new NoFinalyzerObject(i);
                int id = o.Id;
                //o.Dispose();
            }
        }

        private static void LinqExample()
        {
            var numbers = Enumerable.Range(0, 100000).Where(e => e % 2 == 1);
            Random rand = new Random();
            var randomNumbers = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                randomNumbers.Add(rand.Next());
            }

            randomNumbers = randomNumbers.Where(x => numbers.ToList().Contains(x)).ToList();
        }

        private static void LineExample()
        {
            const int count = 10000;
            var lines = new List<StraightLine>(count);
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                //y = ax+b
                lines.Add(new StraightLine { A = rand.Next(-100, 100) / 100, B = rand.Next(-100, 100) });
            }
            lines = lines.Where(DoesLinePassesThroughThePoint).ToList();

            bool DoesLinePassesThroughThePoint(StraightLine line)
            {
                var x = 1;
                var y = 1;
                return y == line.A * x + line.B;
            }
        }

        private static void BoxingExample()
        {
            var bElements = Enumerable.Range(0, 100000).Where(e => HasNegativeHashCode(e)).ToList();

            bool HasNegativeHashCode(object element)
            {
                return element.GetHashCode() < 0;
            }
        }

        private static void ListExample()
        {
            var evenNumbers = new List<int>();
            for (int i = 0; i < 1000000; i++)
            {
                if (i % 2 == 0)
                    evenNumbers.Add(i);
            }
        }

        private static void EvenAndOddNumbersExample()
        {
            var evenNumbers = new List<int>(500000);
            for (int i = 0; i < 1000000; i++)
            {
                if (i % 2 == 0)
                    evenNumbers.Add(i);
            }

            var oddNumbers = new List<int>(500000);
            for (int i = 0; i < 1000000; i++)
            {
                if (i % 2 == 1)
                    oddNumbers.Add(i);
            }
        }

        private static void FitTheRulesExample()
        {
            Point o = new Point { X = 1 };

            //some long lasting operation
            string s = string.Empty;
            for(int i=0; i<2000; i++)
            {
                s += "" + i.ToString();
            }

            Console.WriteLine(o.X);
        }

        private static void StringAppend()
        {
            string s = string.Empty;
            Enumerable.Range(0, 10000).ToList().ForEach(x => s += ".");
            //StringBuilder sb = new StringBuilder("");
            //Enumerable.Range(0, 10000).ToList().ForEach(x => sb.Append("."));
        }
    }
}
