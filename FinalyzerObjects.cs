using System;
using System.Collections.Generic;
using System.Threading;

namespace GCMemoryOptimization
{
    class NoFinalyzerObject
    {
        public int Id { get; set; }
        private List<int> _list = new List<int>(10000);
        public NoFinalyzerObject(int id)
        {
            Id = id;
        }
    }

    class FinalyzerObject
    {
        public int Id { get; set; }
        private List<int> _list = new List<int>(10000);
        
        public FinalyzerObject(int id)
        {
            Id = id;
        }

        ~FinalyzerObject()
        {
            Console.WriteLine($"Object with id: {Id} Finalyzed in generation {GC.GetGeneration(this)}");
        }
    }

    class DisposableObject : IDisposable
    {
        public int Id { get; set; }
        private List<int> _list = new List<int>(10000);

        public DisposableObject(int id)
        {
            Id = id;
        }

        public void Dispose()
        {
            Console.WriteLine($"Object with id: {Id} Finalyzed in generation {GC.GetGeneration(this)}");
        }
    }

    class DisposableFinalyzerObject : IDisposable
    {
        public int Id { get; set; }
        private List<int> _list = new List<int>(10000);

        public DisposableFinalyzerObject(int id)
        {
            Id = id;
        }

        public void Dispose()
        {
            Finalyze();
            GC.SuppressFinalize(this);
        }

        ~DisposableFinalyzerObject()
        {
            Finalyze();
        }
        private void Finalyze()
        {
            Console.WriteLine($"Object with id: {Id} Finalyzed in generation {GC.GetGeneration(this)}");
        }
    }
}
