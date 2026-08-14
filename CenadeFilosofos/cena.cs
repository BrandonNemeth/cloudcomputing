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
        private const int NumPhilosophers = 5;
        public static void Run()
        {
            string[] names = { "Socrates", "Platon", "Aristoteles", "Descartes", "Kant" };

            Fork[] forks = new Fork[NumPhilosophers];
            for (int i = 0; i < NumPhilosophers; i++) {
                forks[i] = new Fork(i);
            }
        }

    }

    class Program {
        static void Main(string[] args) {
            Console.WriteLine("La Cena de los Filosofos");
            MainApplication.Run();
        }
    }
}