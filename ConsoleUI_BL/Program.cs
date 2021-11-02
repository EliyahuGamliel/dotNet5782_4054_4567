using System;
using IBL.BO;

namespace ConsoleUI
{
    class Program
    {
        static IBL.IBL logic;

        /// <summary>
        /// The main function
        /// </summary>
        static void Main(string[] args)
        {
            logic = new IBL.BL();
            MainMenu();
        }

        /// <summary>
        /// keep getting numbers till it gets a legal one
        /// </summary>
        /// <returns> an legal int </returns>
        static int GetInt()
        {
            int num;
            Console.WriteLine("Enter Station Id: ");
            bool error = Int32.TryParse(Console.ReadLine(), out num);
            while(!error)
            {
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
            Console.WriteLine("Enter Station Id: ");
            bool error = Double.TryParse(Console.ReadLine(), out num);
            while (!error)
            {
                error = Double.TryParse(Console.ReadLine(), out num);
            }
            return num;
        }


        /// <summary>
        /// The MainMenu
        /// </summary>
        static void MainMenu()
        {
            int choice;
            do
            {
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
        static void FirstMenu(int choice)
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter 1 for adding a station");
                    Console.WriteLine("Enter 2 for adding a drone");
                    Console.WriteLine("Enter 3 for adding a customer");
                    Console.WriteLine("Enter 4 for adding a parcel");
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

        /// <summary>
        /// The function takes care of the existing addition options
        /// </summary>
        /// <param name="num">The second choice of the user</param>
        static void adding(int num) {
            int id;
            Location location = new Location();
            double longitude, latitude;
            string name;
            switch (num)
            {
                //For adding a station
                case 1:
                    Station s = new Station();
                    Console.WriteLine("Enter Station Id: ");
                    Int32.TryParse(Console.ReadLine(), out s.Id);
                    Console.WriteLine("Enter Station Name: ");
                    //Int32.TryParse(Console.ReadLine(), out name1);
                    Console.WriteLine("Enter Station Longitude: ");
                    //Double.TryParse(Console.ReadLine(), out longitude);
                    Console.WriteLine("Enter Station Latitude: ");
                    //Double.TryParse(Console.ReadLine(), out latitude);
                    location.Longitude = longitude;
                    location.Lattitude = latitude;
                    Console.WriteLine("Enter Charge Slots: ");
                    Int32.TryParse(Console.ReadLine(), out chargeslots);
        
                    logic.AddStation(s);
                    break;
                
                //For adding a drone
                case 2:
                    Drone d = new Drone();
                    Console.WriteLine("Enter Id: ");
                    Int32.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter Model: ");
                    string model = Console.ReadLine();
                    Console.WriteLine("Enter the number of maxWeight: \n0) Light\n1) Medium\n2) Heavy");
                    Int32.TryParse(Console.ReadLine(), out maxw);
                    logic.AddDrone(d);
                    break;

                //For adding a customer
                case 3:
                    Customer c = new Customer();
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
                    logic.AddCustomer(c);
                    break;

                //For adding a parcel
                case 4:
                    Parcel p = new Parcel();
                    int senderId, targetId, wh, pr, droneId;
                    Console.WriteLine("Enter Id: ");
                    Int32.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter senderId: ");
                    Int32.TryParse(Console.ReadLine(), out senderId);
                    Console.WriteLine("Enter targetId: ");
                    Int32.TryParse(Console.ReadLine(), out targetId);
                    Console.WriteLine("Enter the number of weight: \n0) Light\n1) Medium\n2) Heavy");
                    Int32.TryParse(Console.ReadLine(), out wh);
                    Console.WriteLine("Enter the number of priority: \n0) Normal\n1) Fast\n2) Emergency");
                    Int32.TryParse(Console.ReadLine(), out pr);
                    Console.WriteLine("Enter droneId: ");
                    Int32.TryParse(Console.ReadLine(), out droneId);
                    logic.AddParcel(p);
                    break;
            }
        }
        
        /// <summary>
        /// The function takes care of the existing update options
        /// </summary>
        /// <param name="num">The second choice of the user</param>
        static void update(int num)
        {
            int id;
            switch (num)
            {
                //For associating a parcel with a drone
                case 1:
                    Console.WriteLine("Enter Id of Parcel: ");
                    Int32.TryParse(Console.ReadLine(), out id);
                    logic.AssignDroneParcel(id);
                    break;
                
                //For collection of a parcel by the drone
                case 2:
                    Console.WriteLine("Enter Id of Parcel: ");
                    Int32.TryParse(Console.ReadLine(), out id);
                    logic.PickUpDroneParcel(id);
                    break;

                //For delivering a parcel to the customer
                case 3:
                    Console.WriteLine("Enter Id of Parcel: ");
                    Int32.TryParse(Console.ReadLine(), out id);
                    logic.DeliverParcelCustomer(id);
                    break;

                //For sending a drone for charging at a base station
                case 4:
                    int idStation;
                    Console.WriteLine("Enter Id of Drone: ");
                    Int32.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("Enter Id of Station: ");
                    Int32.TryParse(Console.ReadLine(), out idStation);
                    logic.SendDrone(id, idStation);
                    break;
                
                //For releasing a drone from charging at a base station
                case 5:
                    Console.WriteLine("Enter Id of Drone: ");
                    Int32.TryParse(Console.ReadLine(), out id);
                    logic.ReleasDrone(id);
                    break;
            }
        }

        
        static void status(int num) {
            int ID;
            Console.WriteLine("Enter the Id: ");
            Int32.TryParse(Console.ReadLine(), out ID);
            Console.WriteLine(logic.PrintById(ID, num));
        }

        /// <summary>
        /// The function allows you to view a selected list
        /// </summary>
        /// <param name="num">The second choice of the user</param>
        static void lists(int num) {
            switch (num)
            {
                //For displaying a list of base stations
                case 1:
                    foreach (var item in logic.PrintListStation())
                        Console.WriteLine(item.ToString());
                    break;
                
                //For displaying a list of drones
                case 2:
                    foreach (var item in logic.PrintListDrone())
                        Console.WriteLine(item.ToString());
                    break;
                
                //For displaying a list of customer
                case 3:
                    foreach (var item in logic.PrintListCustomer())
                        Console.WriteLine(item.ToString());
                    break;

                //For displaying a list of parcels
                case 4:
                    foreach (var item in logic.PrintListParcel())
                        Console.WriteLine(item.ToString());
                    break;
                
                //To display a list of parcels that have not yet been associated with a drone
                case 5:
                    foreach (var item in logic.PrintListParcelDrone())
                        Console.WriteLine(item.ToString());
                    break;

                //For displaying base stations with available charging stations
                case 6:
                    foreach (var item in logic.PrintListStationCharge())
                        Console.WriteLine(item.ToString());
                    break;
            }
        }    
    }
}