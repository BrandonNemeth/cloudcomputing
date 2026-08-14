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
    class Philosopher {
        public string Name { get; }
        private readonly Fork _leftFork;
        private readonly Fork _rightFork;
        private readonly Random _random = new Random();
        private readonly int _mealsToEat;

        public Philosopher(string name, Fork leftFork, Fork rightFork, int mealsToEat = 3) {
            Name = name;
            _leftFork = leftFork;
            _rightFork = rightFork;
            _mealsToEat = mealsToEat;
        }
        public void Dine() {
            for (int i = 0; i < _mealsToEat; i++) {
                Think();
                Eat();
            }
            Console.WriteLine($"{Name} ha terminado de cenar.");
        }
        private void Think() {
            Console.WriteLine($"{Name} esta pensando...");
            Thread.Sleep(_random.Next(500, 1500));
        }
        private void Eat() {
            // Para evitar deadlock: el filosofo con id menor toma primero el tenedor con id menor
            Fork first = _leftFork.Id < _rightFork.Id ? _leftFork : _rightFork;
            Fork second = _leftFork.Id < _rightFork.Id ? _rightFork : _leftFork;

            first.PickUp(Name);
            second.PickUp(Name);

            Console.WriteLine($"{Name} esta comiendo...");
            Thread.Sleep(_random.Next(500, 1500));

            second.PutDown(Name);
            first.PutDown(Name);
        }
    }

    class MainApplication {
        private const int NumPhilosophers = 5;

        public static void Run() {
            string[] names = { "Socrates", "Platon", "Aristoteles", "Descartes", "Kant" };

            Fork[] forks = new Fork[NumPhilosophers];
            for (int i = 0; i < NumPhilosophers; i++) {
                forks[i] = new Fork(i);
            }

            Thread[] threads = new Thread[NumPhilosophers];
            for (int i = 0; i < NumPhilosophers; i++) {
                Fork left = forks[i];
                Fork right = forks[(i + 1) % NumPhilosophers];
                Philosopher philosopher = new Philosopher(names[i], left, right);
                threads[i] = new Thread(philosopher.Dine);
                threads[i].Start();
            }

            for (int i = 0; i < NumPhilosophers; i++) {
                threads[i].Join();
            }

            Console.WriteLine("Todos los filosofos han terminado de cenar.");
        }
    }

    class Program {
        static void Main(string[] args) {
            Console.WriteLine("La Cena de los Filosofos");
            MainApplication.Run();
        }
    }
}