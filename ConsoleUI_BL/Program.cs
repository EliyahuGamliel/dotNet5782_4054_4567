using System;
using System.Collections.Generic;
using IBL;
using IBL.BO;

namespace ConsoleUI_BL
{
    partial class Program
    {
        private static IBL.IBL logic;

        /// <summary>
        /// The main function
        /// </summary>
        static void Main(string[] args)
        {
            logic = new IBL.BL();
            MainMenu();
        }

        enum MenuOptions { Exit, Add, Update, Status, ShowList}
        enum Adding { Exit, Station, Drone, Customer, Parcel }
        enum Update { Exit, DroneModel, StationDetails, CustomerDetails, SendDrone, ReleaseDrone, AssignParcel, PickParcel, DeliverParcel }
        enum List { Exit, Stations, Drones, Customers, Parcels, UnAssignmentParcels, AvailableChargingStations }
        enum Status { Exit, Stations, Drones, Customers, Parcels }

        /// <summary>
        /// The MainMenu
        /// </summary>
        private static void MainMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("\nEnter 0 to exit");
                Console.WriteLine("Enter 1 for adding");
                Console.WriteLine("Enter 2 for update");
                Console.WriteLine("Enter 3 to show by Id");
                Console.WriteLine("Enter 4 for print list");
                
                choice = GetInt();
                FirstMenu(choice);
            } while ((MenuOptions)choice != MenuOptions.Exit);   
        }
        /// <summary>
        /// The first part of the menu
        /// </summary>
        /// <param name="choice">The first choice of the user</param>
        private static void FirstMenu(int choice)
        {
            try
            {
                bool legal = true;
                switch ((MenuOptions)choice)
                {
                    case MenuOptions.Add:
                        Console.WriteLine("Enter 1 for adding a station");
                        Console.WriteLine("Enter 2 for adding a drone");
                        Console.WriteLine("Enter 3 for adding a customer");
                        Console.WriteLine("Enter 4 for adding a parcel");
                        Console.WriteLine("Enter 0 to exit");
                        break;

                    case MenuOptions.Update:
                        Console.WriteLine("Enter 1 for update drone's name");
                        Console.WriteLine("Enter 2 for update station's data");
                        Console.WriteLine("Enter 3 for update customer's data");
                        Console.WriteLine("Enter 4 for sending a drone to the station for a battery charge");
                        Console.WriteLine("Enter 5 for releasing a drone from charging");
                        Console.WriteLine("Enter 6 for assign a parcel to a drone");
                        Console.WriteLine("Enter 7 for picking up a parcel");
                        Console.WriteLine("Enter 8 for delivery a parcel by drone");
                        Console.WriteLine("Enter 0 to exit");
                        break;

                    case MenuOptions.Status:
                        Console.WriteLine("Enter 1 for station status");
                        Console.WriteLine("Enter 2 for drone status");
                        Console.WriteLine("Enter 3 for customer status");
                        Console.WriteLine("Enter 4 for parcel status");
                        Console.WriteLine("Enter 0 to exit");
                        break;

                    case MenuOptions.ShowList:
                        Console.WriteLine("Enter 1 for the stations's list");
                        Console.WriteLine("Enter 2 for drones's list");
                        Console.WriteLine("Enter 3 for customers's list");
                        Console.WriteLine("Enter 4 for the parcel's list");
                        Console.WriteLine("Enter 5 for the list of parcels that hadn't been assigned to a drone");
                        Console.WriteLine("Enter 6 for the list of stations that have sper place for charging");
                        Console.WriteLine("Enter 0 to exit");
                        break;

                    case MenuOptions.Exit:
                        Console.WriteLine("Bye Bye!");
                        return;
                    
                    default:
                        Console.WriteLine("Enter only numbers between 0-5!\n");
                        legal = false;
                        break;
                }
                if (legal) {
                    int secondChoice;
                    secondChoice = GetInt();
                    if(secondChoice == 0)
                    {
                        return;
                    }
                    SecondMenu(choice, secondChoice);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ho no! {0}", e);
            }
        }
        
        /// <summary>
        /// The second part of the menu
        /// </summary>
        /// <param name="choice">The first choice of the user</param>
        /// <param name="secondChoice">The second choice of the user</param>
        private static void SecondMenu(int choice, int secondChoice)
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
            switch ((Adding)num)
            {
                case Adding.Exit:
                    break;

                //For adding a station
                case Adding.Station:
                    Station s = new Station();
                    Console.WriteLine("Enter Station Id: ");
                    s.Id = GetInt();
                    Console.WriteLine("Enter Station Name: ");
                    s.Name = GetInt();
                    Console.WriteLine("Enter the longitude of the station");
                    s.Location.Longitude = GetDouble();
                    Console.WriteLine("Enter the latitude of the station");
                    s.Location.Lattitude = GetDouble();
                    Console.WriteLine("Enter Charge Slots: ");
                    s.ChargeSlots = GetInt();
                    s.DCharge.Clear();
                    System.Console.WriteLine(logic.AddStation(s)); 
                    break;
                
                //For adding a drone
                case Adding.Drone:
                    int idStation;
                    DroneList d = new DroneList();
                    Console.WriteLine("Enter Id: ");
                    d.Id = GetInt();
                    Console.WriteLine("Enter model of Drone: ");
                    d.Model = Console.ReadLine();
                    Console.WriteLine("Enter the number of maxWeight: \n0) Light\n1) Medium\n2) Heavy");
                    d.MaxWeight = (WeightCategories)GetInt();
                    Console.WriteLine("Enter Id of Station to charge the Drone: ");
                    idStation = GetInt();
                    System.Console.WriteLine(logic.AddDrone(d, idStation)); 
                    break;

                //For adding a customer
                case Adding.Customer:
                    Customer c = new Customer();
                    Console.WriteLine("Enter the Id: ");
                    c.Id = GetInt();
                    Console.WriteLine("Enter the longitude of the customer");
                    c.Location.Longitude = GetDouble();
                    Console.WriteLine("Enter the latitude of the customer ");
                    c.Location.Lattitude = GetDouble();
                    Console.WriteLine("Enter the name: ");
                    c.Name = Console.ReadLine();
                    Console.WriteLine("Enter the phone: ");
                    c.Phone = GetStringInt();
                    System.Console.WriteLine(logic.AddCustomer(c)); 
                    break;

                //For adding a parcel
                case Adding.Parcel:
                    Parcel p = new Parcel();
                    Console.WriteLine("Enter senderId: ");
                    int SenderId = GetInt();
                    Console.WriteLine("Enter targetId: ");
                    int TargetId = GetInt();
                    Console.WriteLine("Enter the number of weight: \n0) Light\n1) Medium\n2) Heavy");
                    p.Weight = (WeightCategories)GetInt();
                    Console.WriteLine("Enter the number of priority: \n0) Normal\n1) Fast\n2) Emergency");
                    p.Priority = (Priorities)GetInt();
                    System.Console.WriteLine(logic.AddParcel(p, SenderId, TargetId)); 
                    break;

                default: 
                    System.Console.WriteLine("Enter only numbers between 0-4!\n");
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
            string input;
            bool success;
            switch ((Update)num)
            {
                case Update.Exit:
                    return;

                case Update.DroneModel:
                    Console.WriteLine("Enter Id of Drone: ");
                    id = GetInt();
                    Console.WriteLine("Enter new Model to Drone: ");
                    string modelDrone = Console.ReadLine();
                    System.Console.WriteLine(logic.UpdateDrone(id ,modelDrone));
                    break;

                case Update.StationDetails:
                    int nameStation = -1;
                    int chargeSlots = -1;
                    Console.WriteLine("Enter Id of Station: ");
                    id = GetInt();
                    Console.WriteLine("Enter a new name to Station: ");
                    input = Console.ReadLine();
                    if (input != "") {
                        success = int.TryParse(input, out nameStation);
                        while (!success) {
                            Console.WriteLine("you didnt enter an int, please try again");
                            input = Console.ReadLine();
                            if (input != "")
                                success = int.TryParse(input, out nameStation);
                            else {
                                nameStation = -1;
                                break;
                            }
                        }
                    }
                    Console.WriteLine("Enter a total amount of charge slots: ");
                    input = Console.ReadLine();
                    if (input != "")
                    {
                        success = int.TryParse(input, out chargeSlots);
                        while (!success)
                        {
                            Console.WriteLine("you didnt enter an int, please try again");
                            input = Console.ReadLine();
                            if (input != "")
                                success = int.TryParse(input, out chargeSlots);
                            else
                            {
                                chargeSlots = -1;
                                break;
                            }
                        }
                    }
                    System.Console.WriteLine(logic.UpdateStation(id, nameStation, chargeSlots));
                    break;

                case Update.CustomerDetails:
                    Console.WriteLine("Enter Id of Customer: ");
                    id = GetInt();
                    Console.WriteLine("Enter a new name to Customer: ");
                    string nameCustomer = Console.ReadLine();
                    Console.WriteLine("Enter a new phone to Customer: ");
                    string phoneCustomer = "";
                    int pho;
                    input = Console.ReadLine();
                    if (input != "")
                    {
                        success = Int32.TryParse(input, out pho);
                        while (!success)
                        {
                            Console.WriteLine("you didnt enter an number only, please try again");
                            input = Console.ReadLine();
                            if (input != "")
                                success = int.TryParse(input, out pho);
                            else
                            {
                                phoneCustomer = "";
                                break;
                            }
                        }
                        if (success)
                        {
                            phoneCustomer = pho.ToString();
                        }
                    }
                    else
                        phoneCustomer = "";
                    System.Console.WriteLine(logic.UpdateCustomer(id, nameCustomer, phoneCustomer));
                    break;
                case Update.SendDrone:
                    Console.WriteLine("Enter Id of Drone: ");
                    id = GetInt();
                    System.Console.WriteLine(logic.SendDrone(id));
                    break;
                case Update.ReleaseDrone:
                    Console.WriteLine("Enter Id of Drone: ");
                    id = GetInt();
                    Console.WriteLine("Enter How long was the drone charging: ");
                    double time = GetDouble();
                    System.Console.WriteLine(logic.ReleasDrone(id, time));
                    break;
                case Update.AssignParcel:
                    Console.WriteLine("Enter Id of Drone: ");
                    id = GetInt();
                    System.Console.WriteLine(logic.AssignDroneParcel(id));
                    break;
                case Update.PickParcel:
                    Console.WriteLine("Enter Id of Drone: ");
                    id = GetInt();
                    System.Console.WriteLine(logic.PickUpDroneParcel(id));
                    break;
                case Update.DeliverParcel:
                    Console.WriteLine("Enter Id of Drone: ");
                    id = GetInt();
                    System.Console.WriteLine(logic.DeliverParcelCustomer(id));
                    break;

                default: 
                    System.Console.WriteLine("Enter only numbers between 0-8!\n");
                    break;
            }
        }

        
        static void status(int num) {
            Console.WriteLine("Enter the Id: ");
            int ID = GetInt();
            switch ((Status)num)
            {
                case Status.Exit:
                    return;

                case Status.Stations:
                    Console.WriteLine(logic.GetStationById(ID));
                    break;

                case Status.Drones:
                    Console.WriteLine(logic.GetDroneById(ID));
                    break;
                
                case Status.Customers:
                    Console.WriteLine(logic.GetCustomerById(ID));
                    break;

                case Status.Parcels:
                    Console.WriteLine(logic.GetParcelById(ID));
                    break;

                default: 
                    System.Console.WriteLine("Enter only numbers between 0-4!\n");
                    break;
            }
        }

        /// <summary>
        /// The function allows you to view a selected list
        /// </summary>
        /// <param name="num">The second choice of the user</param>
        
        static void lists(int num) {
            switch ((List)num)
            {
                case List.Exit:
                    return;

                //For displaying a list of base stations
                case List.Stations:
                    foreach (var item in logic.GetStations())
                        Console.WriteLine(item);
                    break;
                
                //For displaying a list of drones
                case List.Drones:
                    foreach (var item in logic.GetDrones())
                        Console.WriteLine(item);
                    break;
                
                //For displaying a list of customer
                case List.Customers:
                    foreach (var item in logic.GetCustomers())
                        Console.WriteLine(item);
                    break;
            
                //For displaying a list of parcels
                case List.Parcels:
                    foreach (var item in logic.GetParcels())
                        Console.WriteLine(item);
                    break;

                //To display a list of parcels that have not yet been associated with a drone
                case List.UnAssignmentParcels:
                    foreach (var item in logic.GetParcelDrone())
                        Console.WriteLine(item);
                    break;
                    
                //For displaying base stations with available charging stations
                case List.AvailableChargingStations:
                    foreach (var item in logic.GetStationCharge())
                        Console.WriteLine(item);
                    break;

                default: 
                    System.Console.WriteLine("Enter only numbers between 0-6!\n");
                    break;
            }  
        } 
    }
}