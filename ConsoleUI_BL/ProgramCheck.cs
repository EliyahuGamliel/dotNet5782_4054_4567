using System;
using BO;

namespace ConsoleUI_BL
{
    partial class Program
    {

        /// <summary>
        /// keep getting numbers till it gets a legal one
        /// </summary>
        /// <returns> a legal int </returns>
        private static int GetInt()
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
        private static double GetDouble()
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

        /// <summary>
        /// gets a number as a string
        /// </summary>
        /// <returns></returns>
        private static string GetPhone()
        {
            string num = Console.ReadLine();
            int check;
            bool error = true;
            if (num.Length != 14 || num[0] != '+' || num[1] != '9' || num[2] != '7' || num[3] != '2' || num[4] != '-' || num[5] != '5') 
                error = false;
            else 
            {
                string output = num.Substring(num.IndexOf("+") + 6, 4);
                error = Int32.TryParse(output, out check);
                if (error) 
                {
                    output = num.Substring(num.IndexOf("+") + 10, 4); 
                    error = Int32.TryParse(output, out check);
                }
            }
            while (!error)
            {
                Console.WriteLine("You didnt enter a valid phone, please try again (+972-5????????)");
                num = Console.ReadLine();
                if (num.Length != 14 || num[0] != '+' || num[1] != '9' || num[2] != '7' || num[3] != '2' || num[4] != '-' || num[5] != '5') 
                    error = false;
                else 
                {
                    string output = num.Substring(num.IndexOf("+") + 6, 4);
                    error = Int32.TryParse(output, out check);
                    if (error) 
                    {
                        output = num.Substring(num.IndexOf("+") + 10, 4); 
                        error = Int32.TryParse(output, out check);
                    }
                }
            }
            return num;
        }
    }
}