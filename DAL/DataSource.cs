using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    /// <summary>
    /// All lists and data
    /// </summary>
    public class DataSource
    {
        internal static List<Drone> drones = new List<Drone>();
        internal static List<Station> stations = new List<Station>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcel> parcels = new List<Parcel>();
        internal static List<DroneCharge> droneCharges = new List<DroneCharge>();
        
        internal class Config
        {
            public static int Number_ID = 1;
            public static double Avaliable = 0.0001;
            public static double WeightLight = 0.00025;
            public static double WeightMedium = 0.0004;
            public static double WeightHeavy = 0.00055;
            public static double ChargingRate = 30;
        }

        /// <summary>
        /// The function initialise the data at the beginning
        /// </summary>
        public static void Initialize() {
            Random r = new Random();
            int l;
            
            //Station
            l = r.Next(2, 6);
            for (int i = 0; i < l; ++i) {
                int rid = r.Next();
                for (int h = 0; h < i; ++h) {
                    if (rid == stations[h].Id) {
                        h = -1;
                        rid = r.Next();
                    }
                }
                Station s = new Station();
                s.ChargeSlots = 2 + r.Next(0, 3);
                s.Id = rid;
                s.Name = s.Id;
                s.Longitude = r.NextDouble() + r.Next(-180, 180);
                s.Lattitude = r.NextDouble() + r.Next(-90, 90);
                stations.Add(s);
            }

            //Customer
            l = r.Next(10, 101);
            for (int i = 0; i < l; ++i) {
                int rid = r.Next();
                for (int h = 0; h < i; ++h) {
                    if (rid == customers[h].Id) {
                        h = -1;
                        rid = r.Next();
                    }
                }
                Customer c = new Customer();
                c.Id = rid;
                c.Name = ("MyNameIs" + i);
                string ph = "0537589982";
                rid = r.Next(1000, 10000);
                for (int h = 0; h < i; ++h) {
                    ph = "053758" + rid;
                    if (ph == customers[h].Phone) {
                        //it takes a random number from 1000 to 9999 and add it to "053758"
                        rid = r.Next();
                        h = -1;
                    }
                }
                c.Phone = ph;
                c.Longitude = r.NextDouble() + r.Next(-180, 180);
                c.Lattitude = r.NextDouble() + r.Next(-90, 90);
                customers.Add(c);
            }

            //Drones
            l = r.Next(5, 11);
            for (int i = 0; i < l; ++i) {
                int rid = r.Next();
                for (int h = 0; h < i; ++h) {
                    //to check if the id already exists
                    if (rid == drones[h].Id) {
                        i = -1;
                        rid = r.Next();
                    }
                }
                Drone d = new Drone();
                d.Id = rid;
                d.Model = ("Minip" + i);
                d.MaxWeight = (WeightCategories)(r.Next(0, 3));
                drones.Add(d);
            }

            ///Parcel
            l = r.Next(10, 1001);
            for (int i = 0; i < l; ++i) {
                Parcel p = new Parcel();
                p.Weight = (WeightCategories)(r.Next(0, 3));
                p.Id = Config.Number_ID;
                Config.Number_ID += 1;
                p.Requested = new DateTime(2021, r.Next(10, 13), r.Next(1, 28), r.Next(0, 24), r.Next(0, 60), r.Next(0, 60));
                while (DateTime.Compare(DateTime.Now, p.Requested) <= 0)//randomize the requested field
                            p.Requested = new DateTime(2021, r.Next(10, 13), r.Next(1, 28), r.Next(0, 24), r.Next(0, 60), r.Next(0, 60));

                foreach (var item in drones)//randomize a parcel 
                {
                    bool check = true;
                    for (int j = 0; j < i && check; j++)
                        if (parcels[j].DroneId == item.Id && DateTime.Compare(parcels[j].Scheduled, parcels[j].Delivered) >= 0)//checks that the id
                            check = false;

                    if (item.MaxWeight >= p.Weight && check) {//if the weight and id are legal it randomzies the datetimes fields
                        p.DroneId = item.Id;
                        p.Scheduled = new DateTime(2021, r.Next(10, 13), r.Next(1, 28), r.Next(0, 24), r.Next(0, 60), r.Next(0, 60));
                        while (DateTime.Compare(p.Scheduled, p.Requested) <= 0 || DateTime.Compare(DateTime.Now, p.Scheduled) <= 0)
                            p.Scheduled = new DateTime(2021, r.Next(10, 13), r.Next(1, 28), r.Next(0, 24), r.Next(0, 60), r.Next(0, 60));
                        
                        int rand = r.Next(0,3);
                        if (rand > 0) {
                            p.PickedUp = new DateTime(2021, r.Next(10, 13), r.Next(1, 28), r.Next(0, 24), r.Next(0, 60), r.Next(0, 60));
                            while (DateTime.Compare(p.PickedUp, p.Scheduled) <= 0 || DateTime.Compare(DateTime.Now, p.PickedUp) <= 0)
                                p.PickedUp = new DateTime(2021, r.Next(10, 13), r.Next(1, 28), r.Next(0, 24), r.Next(0, 60), r.Next(0, 60));

                            if (rand == 2) {
                                p.Delivered = new DateTime(2021, r.Next(10, 13), r.Next(1, 28), r.Next(0, 24), r.Next(0, 60), r.Next(0, 60));
                                while (DateTime.Compare(p.Delivered, p.PickedUp) <= 0 || DateTime.Compare(DateTime.Now, p.Delivered) <= 0)
                                    p.Delivered = new DateTime(2021, r.Next(10, 13), r.Next(1, 28), r.Next(0, 24), r.Next(0, 60), r.Next(0, 60));
                            }
                        }
                        break;
                    }
                }

                int ind = r.Next(0, customers.Count);
                p.TargetId = customers[ind].Id;
                ind = r.Next(0, customers.Count);
                //so that the sender and the target won't be the same customer
                while (p.TargetId == customers[ind].Id) {
                    ind = r.Next(0, customers.Count);
                }
                p.SenderId = customers[ind].Id;
                p.Priority = (Priorities)(r.Next(0, 3));
                parcels.Add(p);//add all the randomized fields as a last parcel
            }
        }
    }
}