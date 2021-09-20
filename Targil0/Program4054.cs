using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome4054();
            Welcome4567();
            Console.ReadKey();
        }

        static partial void Welcome4567();

        private static void Welcome4054()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("{0}, welcome to my first console application", name);
        }
    }
}
