using System;
using IDAL;
using IDAL.DO;
using DalObject;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            DalObject.DalObject i = new DalObject.DalObject();

            MainMenu();
            //
        }

        static void MainMenu()
        {
            int choice;
            Console.WriteLine("Enter 1 for adding");
            Console.WriteLine("Enter 2 for update");
            Console.WriteLine("Enter 3 to show");
            Console.WriteLine("Enter 4 for the list");
            Console.WriteLine("Enter 5 to show the distance from the station/customer");
            Console.WriteLine("Enter 6 to exit");





            choice = Convert.ToInt32(Console.ReadLine());
            FirstMenu(choice);
        }

        static void FirstMenu(int choice)
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter 1 for adding a station to the list");
                    Console.WriteLine("Enter 2 for adding a drone");
                    Console.WriteLine("Enter 3 for adding a customer");
                    Console.WriteLine("Enter 4 for adding a package to the delivery list");
                    break;

                case 2:
                    Console.WriteLine("Enter 1 for assigning a package to a drone");
                    Console.WriteLine("Enter 2 for picking up a package");
                    Console.WriteLine("Enter 3 for dropping a package to a customer");
                    Console.WriteLine("Enter 4 for sending a drone to the station for a battery charge");
                    Console.WriteLine("Enter 5 for releasing a drone from charging");
                    break;

                case 3:
                    Console.WriteLine("Enter 1 for station status");
                    Console.WriteLine("Enter 2 for drone status");
                    Console.WriteLine("Enter 3 for customer status");
                    Console.WriteLine("Enter 4 for package status");
                    break;

                case 4:
                    Console.WriteLine("Enter 1 for the stations's list");
                    Console.WriteLine("Enter 2 for drones's list");
                    Console.WriteLine("Enter 3 for customers's list");
                    Console.WriteLine("Enter 4 for the packages's list");
                    Console.WriteLine("Enter 5 fop the list of packages that hadn't been assigned to a drone");
                    Console.WriteLine("Enter 6 fop the list of stations that have sper place for charging");
                    break;
                case 5:
                    Console.WriteLine("enter the latitude and then the longitude");
                    coordinateMa();

                    break;
                case 6:
                    Console.WriteLine("Enter 5 to exit");
                    break;

            }
            int secondChoice;
            Int32.TryParse(Console.ReadLine(), out secondChoice);
            SecondMenu(choice, secondChoice);

        }

        static void SecondMenu(int choice, int secondChoice)
        {
            switch (choice)
            {
                case 1:
                    adding(secondChoice);
                    break;
                case 2:
                    update(secondChoice);
                    break;
                case 3:
                    status(secondChoice);
                    break;
                case 4:
                    lists(secondChoice);
                    break;
            }
        }

        static void adding(int num)
        {
            int id;
            double longitude;
            double latitude;
            string name;
            switch (num)
            {
                case 1:
                    Console.WriteLine("enter the following: id, name, longitude, latitude, chargeslots.");
                    
                    Int32.TryParse(Console.ReadLine(), out id);
                    name = Console.ReadLine();
                    Double.TryParse(Console.ReadLine(), out longitude);
                    Double.TryParse(Console.ReadLine(), out latitude);
                    int chargeslots;
                    Int32.TryParse(Console.ReadLine(), out chargeslots);
                    DataSource.AddStation(Station.CreateStation(id, name, longitude, latitude, chargeslots));
                    break;
                case 2:
                    Console.WriteLine("enter the following: id, model, maxWeight, status, battery.");
                    int maxw;
                    int ds;
                    Int32.TryParse(Console.ReadLine(), out id);
                    string model = Console.ReadLine();
                    Int32.TryParse(Console.ReadLine(), out maxw);
                    Int32.TryParse(Console.ReadLine(), out ds);
                    double battery;
                    Double.TryParse(Console.ReadLine(), out battery);

                    DalObject.DalObject.AddDrone(Drone.CreateDrone(id, model, (WeightCategories)maxw, (DroneStatuses)ds, battery));
                    break;
                case 3:
                    Console.WriteLine("enter the following: id, name, phone, longitude, Lattitude.");
                    Int32.TryParse(Console.ReadLine(), out id);
                    name = Console.ReadLine();
                    string phone = Console.ReadLine();
                    Double.TryParse(Console.ReadLine(), out longitude);
                    Double.TryParse(Console.ReadLine(), out latitude);

                    DalObject.DalObject.AddCustomer(Customer.CreateCustomer(id, name, phone, longitude, latitude));

                    break;
                case 4:
                    //CreateParcel(int id, int senderId, int targetld, WeightCategories weight, Priorities Priority, int droneld)
                    Console.WriteLine("enter the following: id, senderId, targetld, weight, Priority, droneld.");
                    Int32.TryParse(Console.ReadLine(), out id);
                    int senderId;
                    Int32.TryParse(Console.ReadLine(), out senderId);
                    int targetld;
                    Int32.TryParse(Console.ReadLine(), out targetld);
                    int wh;
                    Int32.TryParse(Console.ReadLine(), out wh);
                    int pr;
                    Int32.TryParse(Console.ReadLine(), out pr);
                    int droneld;
                    Int32.TryParse(Console.ReadLine(), out droneld);
                    DalObject.DalObject.AddParcel(Parcel.CreateParcel(id, senderId, targetld, (WeightCategories)wh, (Priorities)pr, droneld));
                    break;
            }
        }

        static void coordinateMa()
        {
            double lat;
            int ID;
            Double.TryParse(Console.ReadLine(), out lat);
            double lon;
            Double.TryParse(Console.ReadLine(), out lon);
            Console.WriteLine("do you want to mesure the distance from a customer or from a station? (c/s)");
            char ch;
            Char.TryParse(Console.ReadLine(), out ch);
            if (ch == 'c')
            {
                Console.WriteLine("enter the id");
                Int32.TryParse(Console.ReadLine(), out ID);
                Console.WriteLine(DataSource.DistancePrint(lat, lon, ch, ID));
            }
            else
            {
                Console.WriteLine("enter the id");
                Int32.TryParse(Console.ReadLine(), out ID);
                Console.WriteLine(DataSource.DistancePrint(lat, lon, ch, ID));
            }
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
    }
}
