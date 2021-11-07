using System;
using System.Collections.Generic;
using IBL.BO;

namespace ConsoleUI
{
    partial class Program
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
                Console.WriteLine("Enter 4 for print list");
                Console.WriteLine("Enter 5 to exit");
                choice = GetInt();
                FirstMenu(choice);
            } while (choice != 5);   
        }
        /// <summary>
        /// The first part of the menu
        /// </summary>
        /// <param name="choice">The first choice of the user</param>
        static void FirstMenu(int choice)
        {
            try
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
                        Console.WriteLine("Enter 1 for update drone's name");
                        Console.WriteLine("Enter 2 for updae station's data");
                        Console.WriteLine("Enter 3 for update customer's data");
                        Console.WriteLine("Enter 4 for sending a drone to the station for a battery charge");
                        Console.WriteLine("Enter 5 for releasing a drone from charging");
                        Console.WriteLine("Enter 6 for assign a parcel to a drone");
                        Console.WriteLine("Enter 7 for picking up a parcel");
                        Console.WriteLine("Enter 8 for delivery a parcel by drone");
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
                secondChoice = GetInt();
                SecondMenu(choice, secondChoice);
            }
            catch (Exception e)
            {
                Console.WriteLine("ho no! {0} happend", e);
            }
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
            switch (num)
            {
                //For adding a station
                case 1:
                    Station s = new Station();
                    Console.WriteLine("Enter Station Id: ");
                    s.Id = GetInt();
                    Console.WriteLine("Enter Station Name: ");
                    s.Name = GetInt();
                    Console.WriteLine("Enter Station Location - (Longitude and Latitude): ");
                    s.Location.Longitude = GetDouble();
                    s.Location.Lattitude = GetDouble();
                    Console.WriteLine("Enter Charge Slots: ");
                    s.ChargeSlots = GetInt();
                    s.DCharge.Clear();
                    System.Console.WriteLine(logic.AddStation(s)); 
                    break;
                
                //For adding a drone
                case 2:
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
                case 3:
                    Customer c = new Customer();
                    Console.WriteLine("Enter the Id: ");
                    c.Id = GetInt();
                    Console.WriteLine("Enter Customer Location - (Longitude and Latitude): ");
                    c.Location.Longitude = GetDouble();
                    c.Location.Lattitude = GetDouble();
                    Console.WriteLine("Enter the name: ");
                    c.Name = Console.ReadLine();
                    Console.WriteLine("Enter the phone: ");
                    c.Phone = GetStringInt();
                    System.Console.WriteLine(logic.AddCustomer(c)); 
                    break;

                //For adding a parcel
                case 4:
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
                //
                case 1:
                    Console.WriteLine("Enter Id of Drone: ");
                    id = GetInt();
                    Console.WriteLine("Enter new Model to Drone: ");
                    string modelDrone = Console.ReadLine();
                    logic.UpdateDrone(id ,modelDrone);
                    break;
                
                //
                case 2:
                    Console.WriteLine("Enter Id of Station: ");
                    id = GetInt();
                    Console.WriteLine("Enter a new name to Station: ");
                    int nameStation = GetInt();
                    Console.WriteLine("Enter a total amount of charge slots: ");
                    int chargeSlots = GetInt();
                    logic.UpdateStation(id, nameStation, chargeSlots);
                    break;

                //
                case 3:
                    Console.WriteLine("Enter Id of Customer: ");
                    id = GetInt();
                    Console.WriteLine("Enter a new name to Customer: ");
                    string nameCustomer = Console.ReadLine();
                    Console.WriteLine("Enter a new phone to Customer: ");
                    string phoneCustomer = GetStringInt();
                    logic.UpdateCustomer(id, nameCustomer, phoneCustomer);
                    break;

                //
                case 4:
                    Console.WriteLine("Enter Id of Drone: ");
                    id = GetInt();
                    logic.SendDrone(id);
                    break;
                
                //
                case 5:
                    Console.WriteLine("Enter Id of Drone: ");
                    id = GetInt();
                    Console.WriteLine("Enter How long was the drone charging: ");
                    double time = GetDouble();
                    logic.ReleasDrone(id, time);
                    break;
                
                //
                case 6:
                    Console.WriteLine("Enter Id of Drone: ");
                    id = GetInt();
                    logic.AssignDroneParcel(id);
                    break;

                //
                case 7:
                    Console.WriteLine("Enter Id of Drone: ");
                    id = GetInt();
                    logic.PickUpDroneParcel(id);
                    break;

                //
                case 8:
                    Console.WriteLine("Enter Id of Drone: ");
                    id = GetInt();
                    logic.DeliverParcelCustomer(id);
                    break;
            }
        }

        
        static void status(int num) {
            Console.WriteLine("Enter the Id: ");
            int ID = GetInt();
            switch (num)
            {
                //
                case 1:
                    Console.WriteLine(logic.GetStationById(ID));
                    break;
                
                //
                case 2:
                    Console.WriteLine(logic.GetDroneById(ID));
                    break;
                
                //
                case 3:
                    Console.WriteLine(logic.GetCustomerById(ID));
                    break;

                //
                case 4:
                    Console.WriteLine(logic.GetParcelById(ID));
                    break;
            }
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
                    foreach (var item in logic.GetStations())
                        Console.WriteLine(item.ToString());
                    break;
                
                //For displaying a list of drones
                case 2:
                    foreach (var item in logic.GetDrones())
                        Console.WriteLine(item.ToString());
                    break;
                
                //For displaying a list of customer
                case 3:
                    foreach (var item in logic.GetCustomers())
                        Console.WriteLine(item.ToString());
                    break;

                //For displaying a list of parcels
                case 4:
                    foreach (var item in logic.GetParcels())
                        Console.WriteLine(item.ToString());
                    break;
                
                //To display a list of parcels that have not yet been associated with a drone
                case 5:
                    foreach (var item in logic.GetParcelDrone())
                        Console.WriteLine(item.ToString());
                    break;

                //For displaying base stations with available charging stations
                case 6:
                    foreach (var item in logic.GetStationCharge())
                        Console.WriteLine(item.ToString());
                    break;
                
            }
        }    
    }
}