using System;
using Dal;
using DalApi;
using DO;
namespace ConsoleUI
{
    class Program
    {
        static DalApi.IDal data;

        /// <summary>
        /// The main function
        /// </summary>
        static void Main(string[] args) {
            data = new Dal.DalObject();
            MainMenu();
        }

        /// <summary>
        /// The MainMenu
        /// </summary>
        static void MainMenu() {
            int choice;
            do {
                Console.WriteLine("Enter 1 for adding");
                Console.WriteLine("Enter 2 for update");
                Console.WriteLine("Enter 3 to show by Id");
                Console.WriteLine("Enter 4 for the list");
                Console.WriteLine("Enter 5 to exit");
                Int32.TryParse(Console.ReadLine(), out choice);
                FirstMenu(choice);
            } while (choice != 5);
        }
        /// <summary>
        /// The first part of the menu
        /// </summary>
        /// <param name="choice">The first choice of the user</param>
        static void FirstMenu(int choice) {
            switch (choice) {
                case 1:
                    Console.WriteLine("Enter 1 for adding a station to the list");
                    Console.WriteLine("Enter 2 for adding a drone");
                    Console.WriteLine("Enter 3 for adding a customer");
                    Console.WriteLine("Enter 4 for adding a parcel to the delivery list");
                    break;

                case 2:
                    Console.WriteLine("Enter 1 for assigning a parcel to a drone");
                    Console.WriteLine("Enter 2 for picking up a parcel");
                    Console.WriteLine("Enter 3 for dropping a parcel to a customer");
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
                    Console.WriteLine("Enter 4 for the parcel's list");
                    Console.WriteLine("Enter 5 for the list of parcels that hadn't been assigned to a drone");
                    Console.WriteLine("Enter 6 for the list of stations that have sper place for charging");
                    break;

                case 5:
                    Console.WriteLine("Bye Bye!");
                    return;
            }
            int secondChoice;
            Int32.TryParse(Console.ReadLine(), out secondChoice);
            SecondMenu(choice, secondChoice);
        }

        /// <summary>
        /// The second part of the menu
        /// </summary>
        /// <param name="choice">The first choice of the user</param>
        /// <param name="secondChoice">The second choice of the user</param>
        static void SecondMenu(int choice, int secondChoice) {
            switch (choice) {
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

        /// <summary>
        /// The function takes care of the existing addition options
        /// </summary>
        /// <param name="num">The second choice of the user</param>
        static void adding(int num) {
            switch (num) {
                //For adding a station
                case 1:
                    Station s = new Station();
                    Console.WriteLine("Enter the longitude of the station");
                    s.Longitude = GetDouble();
                    Console.WriteLine("Enter the lattitude of the station");
                    s.Lattitude = GetDouble();
                    Console.WriteLine("Enter the Station's Name: ");
                    s.Name = Console.ReadLine();
                    Console.WriteLine("Enter Station Id: ");
                    s.Id = GetInt();
                    Console.WriteLine("Enter ChargeSlots: ");
                    s.ChargeSlots = GetInt();
                    s.Active = true;
                    data.AddStation(s);
                    break;

                //For adding a drone
                case 2:
                    Drone d = new Drone();
                    Console.WriteLine("Enter drone's Id: ");
                    d.Id = GetInt();
                    Console.WriteLine("Enter the drone's model: ");
                    d.Model = Console.ReadLine();
                    Console.WriteLine("Enter the max weight of the drone (1\\2\\3)");
                    d.MaxWeight = (WeightCategories)GetInt();
                    d.Active = true;
                    data.AddDrone(d);
                    break;

                //For adding a customer
                case 3:
                    Customer c = new Customer();
                    Console.WriteLine("Enter the longitude of the customer");
                    c.Longitude = GetDouble();
                    Console.WriteLine("Enter the lattitude of the customer");
                    c.Lattitude = GetDouble();
                    Console.WriteLine("Enter the customer's Name: ");
                    c.Name = Console.ReadLine();
                    Console.WriteLine("Enter customer's Id: ");
                    c.Id = GetInt();
                    Console.WriteLine("Enter customer's phone number: ");
                    c.Phone = Console.ReadLine();
                    c.Active = true;
                    data.AddCustomer(c);
                    break;

                //For adding a parcel
                case 4:
                    Parcel p = new Parcel();
                    Console.WriteLine("Enter parcel's Id: ");
                    p.Id = GetInt();
                    Console.WriteLine("Enter the drone Id for the parcel");
                    p.DroneId = GetInt();
                    Console.WriteLine("Enter the target Id for the parcel");
                    p.TargetId = GetInt();
                    Console.WriteLine("Enter the sender Id for the parcel");
                    p.SenderId = GetInt();
                    Console.WriteLine("Enter the max weight of the drone (1\\2\\3)");
                    p.Weight = (WeightCategories)GetInt();
                    p.Active = true;
                    data.AddParcel(p);
                    break;
            }
        }

        /// <summary>
        /// The function takes care of the existing update options
        /// </summary>
        /// <param name="num">The second choice of the user</param>
        static void update(int num) {
            int id;
            switch (num) {
                //For associating a parcel with a drone
                case 1:
                    int idDrone;
                    Console.WriteLine("Enter Id of Parcel: ");
                    Int32.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter Id of Drone: ");
                    Int32.TryParse(Console.ReadLine(), out idDrone);
                    //data.AssignDroneParcel(idDrone, idDrone);
                    break;

                //For collection of a parcel by the drone
                case 2:
                    Console.WriteLine("Enter Id of Parcel: ");
                    Int32.TryParse(Console.ReadLine(), out id);
                    //data.PickUpDroneParcel(id);
                    break;

                //For delivering a parcel to the customer
                case 3:
                    Console.WriteLine("Enter Id of Parcel: ");
                    Int32.TryParse(Console.ReadLine(), out id);
                    //data.DeliverParcelCustomer(id);
                    break;

                //For sending a drone for charging at a base station
                case 4:
                    int idStation;
                    Console.WriteLine("Enter Id of Drone: ");
                    Int32.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter Id of Station: ");
                    Int32.TryParse(Console.ReadLine(), out idStation);
                    //data.SendDrone(id, idStation);
                    break;

                //For releasing a drone from charging at a base station
                case 5:
                    Console.WriteLine("Enter Id of Drone: ");
                    Int32.TryParse(Console.ReadLine(), out id);
                    //data.ReleasDrone(id);
                    break;
            }
        }

        static void status(int num) {
            int ID;
            Console.WriteLine("Enter the Id: ");
            Int32.TryParse(Console.ReadLine(), out ID);
            switch (num) {

                case 1:
                    Console.WriteLine(data.GetStationById(ID));
                    break;

                case 2:
                    Console.WriteLine(data.GetDroneById(ID));
                    break;

                case 3:
                    Console.WriteLine(data.GetCustomerById(ID));
                    break;

                case 4:
                    Console.WriteLine(data.GetParcelById(ID));
                    break;
            }
        }

        /// <summary>
        /// The function allows you to view a selected list
        /// </summary>
        /// <param name="num">The second choice of the user</param>
        static void lists(int num) {
            switch (num) {
                //For displaying a list of base stations
                case 1:
                    foreach (var item in data.GetStationByFilter(s => s.Active))
                        Console.WriteLine(item);
                    break;

                //For displaying a list of drones
                case 2:
                    foreach (var item in data.GetDroneByFilter(d => d.Active))
                        Console.WriteLine(item);
                    break;

                //For displaying a list of customer
                case 3:
                    foreach (var item in data.GetCustomerByFilter(c => c.Active))
                        Console.WriteLine(item);
                    break;

                //For displaying a list of parcels
                case 4:
                    foreach (var item in data.GetParcelByFilter(p => p.Active))
                        Console.WriteLine(item);
                    break;

                //To display a list of parcels that have not yet been associated with a drone
                case 5:
                    foreach (var item in data.GetParcelByFilter(p => p.Active && p.Scheduled == null))
                        Console.WriteLine(item);
                    break;

                //For displaying base stations with available charging stations
                case 6:
                    foreach (var item in data.GetStationByFilter(s => s.Active && s.ChargeSlots > 0))
                        Console.WriteLine(item);
                    break;
            }
        }

        /// <summary>
        /// keep getting numbers till it gets a legal one
        /// </summary>
        /// <returns> a legal int </returns>
        private static int GetInt() {
            int num;
            bool error = Int32.TryParse(Console.ReadLine(), out num);
            while (!error) {
                Console.WriteLine("you didnt enter an int, please try again");
                error = Int32.TryParse(Console.ReadLine(), out num);
            }
            return num;
        }

        /// <summary>
        /// keep getting numbers till it gets a legal one
        /// </summary>
        /// <returns>a legal double</returns>
        private static double GetDouble() {
            double num;
            bool error = Double.TryParse(Console.ReadLine(), out num);
            while (!error) {
                Console.WriteLine("You didnt enter a double, please try again");
                error = Double.TryParse(Console.ReadLine(), out num);
            }
            return num;
        }
    }
}