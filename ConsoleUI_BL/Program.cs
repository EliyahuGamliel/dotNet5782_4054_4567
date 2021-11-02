﻿using System;
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
                Console.WriteLine("Enter 4 for the list");
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
                    Console.WriteLine("Enter 1 for assigning a parcel to a drone");
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
            secondChoice = GetInt();
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
            switch (num)
            {
                //For adding a station
                case 1:
                    Station s = new Station();
                    Console.WriteLine("Enter Station Id: ");
                    s.Id = GetInt();
                    Console.WriteLine("Enter Station Name: ");
                    s.Name = GetInt();
                    Console.WriteLine("Enter Station Longitude: ");
                    s.Location.Longitude = GetDouble();
                    Console.WriteLine("Enter Station Latitude: ");
                    s.Location.Lattitude = GetDouble();
                    Console.WriteLine("Enter Charge Slots: ");
                    s.ChargeSlots = GetInt();
                    s.DCharge.Clear();
                    logic.AddStation(s);
                    break;
                
                //For adding a drone
                case 2:
                    Drone d = new Drone();
                    Console.WriteLine("Enter Id: ");
                    d.Id = GetInt();
                    Console.WriteLine("Enter the number of maxWeight: \n0) Light\n1) Medium\n2) Heavy");
                    d.MaxWeight = (WeightCategories)GetInt();
                    Console.WriteLine("Enter Id of Station to charge the Drone: ");
                    d. = Console.ReadLine();
                    logic.AddDrone(d);
                    break;

                //For adding a customer
                case 3:
                    Customer c = new Customer();
                    Console.WriteLine("Enter the id: ");
                    c.Id = GetInt();
                    Console.WriteLine("Enter the name: ");
                    c.Name = Console.ReadLine();
                    Console.WriteLine("Enter the phone: ");
                    c.Phone = Console.ReadLine(); //Check
                    logic.AddCustomer(c);
                    break;

                //For adding a parcel
                case 4:
                    Parcel p = new Parcel();
                    Console.WriteLine("Enter senderId: ");
                    p.SenderId = GetInt();
                    Console.WriteLine("Enter targetId: ");
                    p.TargetId = GetInt();
                    Console.WriteLine("Enter the number of weight: \n0) Light\n1) Medium\n2) Heavy");
                    p.Weight = (WeightCategories)GetInt();
                    Console.WriteLine("Enter the number of priority: \n0) Normal\n1) Fast\n2) Emergency");
                    p.Priority = (Priorities)GetInt();
                    p.Drone = new Drone();
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
                    Console.WriteLine("Enter Id of Drone: ");
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
            Console.WriteLine("Enter the Id: ");
            int ID = GetInt();
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