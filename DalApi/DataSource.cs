using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
//using DalApi;

namespace Dal
{
    /// <summary>
    /// All lists and data
    /// </summary>
    public class DataSource
    {
        readonly static Random rando = new Random();
        internal static List<Drone> Drones = new List<Drone>();
        internal static List<Station> Stations = new List<Station>();
        internal static List<Customer> Customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();
        internal static List<DroneCharge> DroneCharges = new List<DroneCharge>();

        internal class Config
        {
            internal static int NumberID = 1;
            internal static double Avaliable = 0.0008;
            internal static double WeightLight = 0.0012;
            internal static double WeightMedium = 0.002;
            internal static double WeightHeavy = 0.0035;
            public static double ChargingRate = 15;
        }

        /// <summary>
        /// The function initialise the data at the beginning
        /// </summary>
        public static void Initialize()
        {

            int ran;
            //Station
            ran = rando.Next(2, 6);
            for (int i = 0; i < ran; ++i)
            {
                int rid = rando.Next();
                //h check if the random number is ranegal and if not h start over
                for (int h = 0; h < i; ++h)
                {
                    if (rid == Stations[h].Id)
                    {
                        h = -1;
                        rid = rando.Next();
                    }
                }
                Station adst = new Station();
                adst.ChargeSlots = 2 + rando.Next(0, 3);
                adst.Id = rid;
                adst.Name = $"{rid}";
                adst.Longitude = rando.NextDouble() + rando.Next(-180, 180);
                adst.Lattitude = rando.NextDouble() + rando.Next(-90, 90);
                Stations.Add(adst);
            }

            //Customer
            ran = rando.Next(10, 101);
            for (int i = 0; i < ran; ++i)
            {
                int rid = rando.Next();
                for (int h = 0; h < i; ++h)
                {
                    //h check if the random number is legal and if not h start over
                    if (rid == Customers[h].Id)
                    {
                        h = -1;
                        rid = rando.Next();
                    }
                }
                Customer cust = new Customer();
                cust.Id = rid;
                cust.Name = ("Customer" + i);
                string phone = "+972-582559635";
                rid = rando.Next(1000, 10000);
                //h check if the random number is legal and if not h start over
                for (int h = 0; h < i; ++h)
                {
                    phone = "+972-58671" + rid;
                    if (phone == Customers[h].Phone)
                    {
                        //it takes a random number from 1000 to 9999 and add it to "053758"
                        rid = rando.Next();
                        h = -1;
                    }
                }
                cust.Phone = phone;
                cust.Longitude = rando.NextDouble() + rando.Next(-180, 180);
                cust.Lattitude = rando.NextDouble() + rando.Next(-90, 90);
                Customers.Add(cust);
            }

            //Drones
            ran = rando.Next(5, 11);
            for (int i = 0; i < ran; ++i)
            {
                int rid = rando.Next();//rid = random id
                for (int h = 0; h < i; ++h)
                {
                    //to check if the id already exists
                    //h check if the random number is legal and if not h start over
                    if (rid == Drones[h].Id)
                    {
                        h = -1;
                        rid = rando.Next();
                    }
                }
                Drone d = new Drone();
                d.Id = rid;
                d.Model = ("Minip" + i);
                d.MaxWeight = (WeightCategories)(rando.Next(0, 3));
                Drones.Add(d);
            }

            //Parcel
            ran = rando.Next(10, 1001);
            for (int i = 0; i < ran; ++i)
            {
                Parcel p = new Parcel();
                p.Weight = (WeightCategories)(rando.Next(0, 3));
                p.Id = Config.NumberID;
                Config.NumberID += 1;
                //Randomize the requested field
                p.Requested = new DateTime(2021, rando.Next(10, 13), rando.Next(1, 28), rando.Next(0, 24), rando.Next(0, 60), rando.Next(0, 60));
                //As long as the "Requested" field precedes the current time
                while (DateTime.Now < p.Requested)
                    p.Requested = new DateTime(2021, rando.Next(10, 13), rando.Next(1, 28), rando.Next(0, 24), rando.Next(0, 60), rando.Next(0, 60));

                foreach (var item in Drones)
                {
                    bool check = true;
                    for (int j = 0; j < i && check; j++)
                        //If the drone is occupied by a parcel that has not yet been delivered
                        if (Parcels[j].DroneId == item.Id && Parcels[j].Delivered == null)
                            check = false;
                    //If the drone is not in the middle of delivery and also the parcel is a normal weight for the drone
                    if (item.MaxWeight >= p.Weight && check)
                    {
                        p.DroneId = item.Id;
                        //Randomize the "Scheduled" field
                        p.Scheduled = new DateTime(2021, rando.Next(10, 13), rando.Next(1, 28), rando.Next(0, 24), rando.Next(0, 60), rando.Next(0, 60));
                        //As long as the "Scheduled" field precedes the current time or As long as the "Scheduled" field precedes the "Requested" field
                        while ((p.Scheduled < p.Requested) || (DateTime.Now < p.Scheduled))
                            p.Scheduled = new DateTime(2021, rando.Next(10, 13), rando.Next(1, 28), rando.Next(0, 24), rando.Next(0, 60), rando.Next(0, 60));

                        //Grill a random number that characterizes the status of the parcel (associated, collected, supplied)
                        int rand = rando.Next(0, 3);
                        if (rand > 0)
                        {

                            //Randomize the "PickedUp" field
                            p.PickedUp = new DateTime(2021, rando.Next(10, 13), rando.Next(1, 28), rando.Next(0, 24), rando.Next(0, 60), rando.Next(0, 60));
                            //As long as the "PickedUp" field precedes the current time or As long as the "PickedUp" field precedes the "Scheduled" field
                            while ((p.PickedUp < p.Scheduled) || (DateTime.Now < p.PickedUp))
                                p.PickedUp = new DateTime(2021, rando.Next(10, 13), rando.Next(1, 28), rando.Next(0, 24), rando.Next(0, 60), rando.Next(0, 60));

                            if (rand == 2)
                            {
                                //Randomize the "Delivered" field
                                p.Delivered = new DateTime(2021, rando.Next(10, 13), rando.Next(1, 28), rando.Next(0, 24), rando.Next(0, 60), rando.Next(0, 60));
                                //As long as the "Delivered" field precedes the current time or As long as the "Delivered" field precedes the "PickedUp" field
                                while ((p.Delivered < p.PickedUp) || (DateTime.Now < p.Delivered))
                                    p.Delivered = new DateTime(2021, rando.Next(10, 13), rando.Next(1, 28), rando.Next(0, 24), rando.Next(0, 60), rando.Next(0, 60));
                                p.DroneId = 0;
                            }
                        }
                        //Since a drone is found for the parcel, it is possible to exit the loop
                        break;
                    }
                }

                int ind = rando.Next(0, Customers.Count);
                p.TargetId = Customers[ind].Id;

                ind = rando.Next(0, Customers.Count);
                //so that the sender and the target won't be the same customer
                while (p.TargetId == Customers[ind].Id)
                {
                    ind = rando.Next(0, Customers.Count);
                }
                p.SenderId = Customers[ind].Id;
                p.Priority = (Priorities)(rando.Next(0, 3));
                Parcels.Add(p);//add all the randomized fields as a last parcel
            }
        }
    }
}
