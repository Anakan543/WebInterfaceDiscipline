namespace laborotorna3
{
    internal class Program
    {
        static void WriteMessage1()
        {
            Console.WriteLine("Start WriteMessage1");
            Thread.Sleep(3000);
            Console.WriteLine("Hello! From WriteMessage1");
        }
        static void WriteMessage2()
        {
            Console.WriteLine("Start WriteMessage2");
            Thread.Sleep(3000);
            Console.WriteLine("Hello! From WriteMessage2");
        }

        static void SumTwoNumber(double x1, double x2) {
            Console.WriteLine("Start SumTwoNumber");
            Thread.Sleep(3000);
            Console.WriteLine($"Sum two number {x1} + {x2} = {x1 + x2}");
        }

        static void CountFrom0toNumber(int number)
        {
            Console.WriteLine("Start CountFrom0toNumber");
            for (int i = 0; i <= number; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine(i);
            }

        }
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(new ThreadStart(WriteMessage1));
            Thread thread2 = new Thread(new ThreadStart(WriteMessage2));

            Task task1 = Task.Factory.StartNew(() => SumTwoNumber(25, 25));
            Task task2 = Task.Factory.StartNew(() => CountFrom0toNumber(10));
            Task.WaitAll(task1, task2);

            thread1.Start();
            thread2.Start();

            Thread.Sleep(3000);
            Console.WriteLine("The End");
        }
    }
}
