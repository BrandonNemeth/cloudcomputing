using System;
using System.Threading;

namespace CenadeFilosofos {

    class Fork {
        public int Id { get; }
        private readonly object _lock = new object();

        public Fork(int id) {
            Id = id;
        }

        public void PickUp(string philosopherName) {
            Monitor.Enter(_lock);
            Console.WriteLine($"  {philosopherName} toma el tenedor {Id}");
        }

        public void PutDown(string philosopherName) {
            Console.WriteLine($"  {philosopherName} suelta el tenedor {Id}");
            Monitor.Exit(_lock);
        }
    }
    class MainApplication {
        public static void Run() {}

    }

    class Program {
        static void Main(string[] args) {
            Console.WriteLine("=== La Cena de los Filosofos ===");
            MainApplication.Run();
        }
    }
}