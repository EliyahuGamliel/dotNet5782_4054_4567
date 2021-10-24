using System;
using DalObject;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            DalObject.DalObject data = new DalObject.DalObject();
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
                    Console.WriteLine("Enter 4 for parcel status");
                    break;

                case 4:
                    Console.WriteLine("Enter 1 for the stations's list");
                    Console.WriteLine("Enter 2 for drones's list");
                    Console.WriteLine("Enter 3 for customers's list");
                    Console.WriteLine("Enter 4 for the packages's list");
                    Console.WriteLine("Enter 5 for the list of packages that hadn't been assigned to a drone");
                    Console.WriteLine("Enter 6 for the list of stations that have sper place for charging");
                    break;
                    
                case 5:
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

        static void adding(int num) {
            int id;
            double longitude;
            double latitude;
            string name;
            switch (num)
            {
                case 1:
                    int chargeslots;
                    Console.WriteLine("Enter Id: ");
                    Int32.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter Name: ");
                    name = Console.ReadLine();
                    Console.WriteLine("Enter Longitude: ");
                    Double.TryParse(Console.ReadLine(), out longitude);
                    Console.WriteLine("Enter Latitude: ");
                    Double.TryParse(Console.ReadLine(), out latitude);
                    Console.WriteLine("Enter ChargeSlots: ");
                    Int32.TryParse(Console.ReadLine(), out chargeslots);
                    DalObject.AddStation(id, name, longitude, latitude, chargeslots);
                    break;
                    
                case 2:
                    int maxw;
                    int ds;
                    double battery;
                    Console.WriteLine("Enter Id: ");
                    Int32.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter model: ");
                    string model = Console.ReadLine();
                    Console.WriteLine("Enter maxWeight: ");
                    Int32.TryParse(Console.ReadLine(), out maxw);
                    Console.WriteLine("Enter status: ");
                    Int32.TryParse(Console.ReadLine(), out ds);
                    Console.WriteLine("Enter battery: ");
                    Double.TryParse(Console.ReadLine(), out battery);
                    DalObject.DalObject.AddDrone(id, model, (WeightCategories)maxw, (DroneStatuses)ds, battery);
                    break;
                    
                case 3:
                    Console.WriteLine("Enter the id: ");
                    Int32.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter the name: ");
                    name = Console.ReadLine();
                    Console.WriteLine("Enter the phone: ");
                    string phone = Console.ReadLine();
                    Console.WriteLine("Enter the longitude: ");
                    Double.TryParse(Console.ReadLine(), out longitude);
                    Console.WriteLine("Enter the lattitude: ");
                    Double.TryParse(Console.ReadLine(), out latitude);
                    DalObject.DalObject.AddCustomer(id, name, phone, longitude, latitude);
                    break;
                    
                case 4:
                    int senderId;
                    int targetld;
                    int wh;
                    int pr;
                    int droneld;
                    Console.WriteLine("Enter Id: ");
                    Int32.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter senderId: ");
                    Int32.TryParse(Console.ReadLine(), out senderId);
                    Console.WriteLine("Enter targetId: ");
                    Int32.TryParse(Console.ReadLine(), out targetld);
                    Console.WriteLine("Enter weight: ");
                    Int32.TryParse(Console.ReadLine(), out wh);
                    Console.WriteLine("Enter Priority: ");
                    Int32.TryParse(Console.ReadLine(), out pr);
                    Console.WriteLine("Enter droneId: ");
                    Int32.TryParse(Console.ReadLine(), out droneld);
                    DalObject.DalObject.AddParcel(id, senderId, targetld, (WeightCategories)wh, (Priorities)pr, droneld);
                    break;
            }
        }

        
        static void update(int num)
        {
            Console.WriteLine("you chose some kind of update");
        }

        
        static void status(int num)
        {
            Console.WriteLine("Enter the Id: ");
            Int32.TryParse(Console.ReadLine(), out ID);
            DalObject.DalObject.PrintById(ID, num);
        }

        
        static void lists(int num)
        {
            Console.WriteLine("Enter 1 for the stations's list");
            Console.WriteLine("Enter 2 for drones's list");
            Console.WriteLine("Enter 3 for customers's list");
            Console.WriteLine("Enter 4 for the packages's list");
            Console.WriteLine("Enter 5 fop the list of packages that hadn't been assigned to a drone");
            Console.WriteLine("Enter 6 fop the list of stations that have sper place for charging");
        }
        
        
        static void coordinateMa() {
            double lat;
            double lon;
            int ID;
            char ch;
            Console.WriteLine("Enter the lattitude: ")
            Double.TryParse(Console.ReadLine(), out lat);
            Console.WriteLine("Enter the longitude: ");
            Double.TryParse(Console.ReadLine(), out lon);
            Console.WriteLine("Do you want to mesure the distance from a customer or from a station? (c/s)");
            Char.TryParse(Console.ReadLine(), out ch);
            if (ch == 'c') {
                Console.WriteLine("Enter the Id of customer");
                Int32.TryParse(Console.ReadLine(), out ID);
                Console.WriteLine(DalObject.DalObject.DistancePrint(lat, lon, ch, ID));
            }
            else {
                Console.WriteLine("Enter the Id of station");
                Int32.TryParse(Console.ReadLine(), out ID);
                Console.WriteLine(DalObject.DalObject.DistancePrint(lat, lon, ch, ID));
            }
        }
    }
}
