using System;
using IBL.BO;

namespace ConsoleUI
{
    partial class Program
    {

        /// <summary>
        /// keep getting numbers till it gets a legal one
        /// </summary>
        /// <returns> an legal int </returns>
        static int GetInt()
        {
            int num;
            bool error = Int32.TryParse(Console.ReadLine(), out num);
            while(!error)
            {
                Console.WriteLine("you didnt enter an int, please try again");
                error = Int32.TryParse(Console.ReadLine(), out num);
            }
            return num;
        }

        /// <summary>
        /// keep getting numbers till it gets a legal one
        /// </summary>
        /// <returns>a legal double</returns>
        static double GetDouble()
        {
            double num;
            bool error = Double.TryParse(Console.ReadLine(), out num);
            while (!error)
            {
                Console.WriteLine("You didnt enter a double, please try again");
                error = Double.TryParse(Console.ReadLine(), out num);
            }
            return num;
        }

    
        static string GetStringInt()
        {
            string num = Console.ReadLine();
            int check;
            bool error = Int32.TryParse(num, out check);
            while (!error)
            {
                Console.WriteLine("You didnt enter a number only, please try again");
                num = Console.ReadLine();
                error = Int32.TryParse(num, out check);
            }
            return num;
        }
    }
}