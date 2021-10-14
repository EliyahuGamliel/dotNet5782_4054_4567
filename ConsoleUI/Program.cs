using System;

namespace ConsoleUI
{
    class Program
    {
        static void MainMenu()
        {
            int choice;
            Console.WriteLine("enter 1 for adding");
            Console.WriteLine("enter 2 for update");
            Console.WriteLine("enter 3 to show");
            Console.WriteLine("enter 4 for the list");
            Console.WriteLine("enter 5 to exit");

            choice = Convert.ToInt32(Console.ReadLine());
            FirstMenu(choice);
        }
        static void FirstMenu(int choice)
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine("enter 1 for adding a station to the list");
                    Console.WriteLine("enter 2 for adding a drone");
                    Console.WriteLine("enter 3 for adding a customer");
                    Console.WriteLine("enter 4 for adding a package to the delivery list");
                    
                    break;
                case 2:
                    Console.WriteLine("enter 1 for assigning a package to a drone");
                    Console.WriteLine("enter 2 for picking up a package");
                    Console.WriteLine("enter 3 for dropping a package to a customer");
                    Console.WriteLine("enter 4 for sending a drone to the station for a battery charge");
                    Console.WriteLine("enter 5 for releasing a drone from charging");
                    break;
                case 3:
                    Console.WriteLine("enter 1 for station status");
                    Console.WriteLine("enter 2 for drone status");
                    Console.WriteLine("enter 3 for customer status");
                    Console.WriteLine("enter 4 for package status");
                    break;
                case 4:
                    Console.WriteLine("enter 1 for the stations's list");
                    Console.WriteLine("enter 2 for drones's list");
                    Console.WriteLine("enter 3 for customers's list");
                    Console.WriteLine("enter 4 for the packages's list");
                    Console.WriteLine("enter 5 fop the list of packages that hadn't been assigned to a drone");
                    Console.WriteLine("enter 6 fop the list of stations that have sper place for charging");
                    break;
                case 5:
                    Console.WriteLine("enter 5 to exit");
                    break;

            }
            int SecondChoice;
            SecondChoice = Convert.ToInt32(Console.ReadLine());
            SecondMenu(choice, SecondChoice);

        }
        static void SecondMenu(int num1, int num2)
        {
            switch (num1)
            {
                case 1:
                    adding(num2);
                    break;
                case 2:
                    update(num2);
                    break;
                case 3:
                    status(num2);
                    break;
                case 4:
                    lists(num2);
                    break;
            }
        }
        static void adding(int num)
        {
            Console.WriteLine("you chose some kind of adding");
        }
        static void update(int num)
        {
            Console.WriteLine("you chose some kind of update");
        }
        static void status(int num)
        {
            Console.WriteLine("you chose some kind of status");
        }
        static void lists(int num)
        {
            Console.WriteLine("you chose some kind of lists");
        }
        static void Main(string[] args)
        {
            MainMenu();
        }
    }
}
