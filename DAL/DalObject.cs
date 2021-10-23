using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
	public class DataSource
	{
		internal static List<Drone> drones = new List<Drone>();
		internal static List<Station> stations = new List<Station>();
		internal static List<Customer> customers = new List<Customer>();
		internal static List<Parcel> parcels = new List<Parcel>();

		internal class Config
		{
			public static int Number_ID = 0;
		}
		public static void AddStation(Station oj)
        {
			stations.Add(oj);
        }
		public static void AddDrone(Drone oj)
		{
			drones.Add(oj);
		}
		public static void AddParcel(Parcel oj)
		{
			parcels.Add(oj);
		}
		public static void AddCustomer(Customer oj)
		{
			customers.Add(oj);
		}
		public static double DistancePrint(double lat1, double lon1, char letter, int id)
		{
			double dis;
			if (letter == 'c') {
				Customer c = customers.Find(c => id == c.Id);
				int index = customers.IndexOf(c);
				dis = customers[index].DistanceTo(lat1, lon1, customers[index].Lattitude, customers[index].Longitude);
			}
			else {
				Station s = stations.Find(s => id == s.Id);
				int index = stations.IndexOf(s);
				dis = stations[index].DistanceTo(lat1, lon1, stations[index].Lattitude, stations[index].Longitude);
			}
			return dis;
		}
		
		public static void Initialize()
		{
			Random r = new Random();
			
			//Drones
			int l = r.Next(5, 11);
			for (int i = 0;i < l;++i)
			{
				int rid = r.Next();
				for (int h = 0;h < i;++h) {
					if (rid == drones[h].Id) {
						i -= 1;
						rid = r.Next();
					}
				}
				
				Drone d = new Drone();
				d.Id = rid;
				d.Model = ("Mark" + 1);
				d.MaxWeight = (WeightCategories)(r.Next(0, 3));
				d.Status = (DroneStatuses)(r.Next(0, 3));  //change the status so they wil be different 
				d.Battery = 100;
				drones.Add(d);
			}


			//Station
			l = r.Next(2, 6);
			for (int i = 0;i < l;++i)
			{
				int rid = r.Next();
				for (int h = 0;h < i;++h) {
					if (rid == stations[h].Id) {
						h =  -1;
						rid = r.Next();
					}
				}
				
				Station s = new Station();
				s.ChargeSlots = 2 + r.Next(0, 3);
				s.Id = rid;
				s.Name = "station" + s.Id;
				s.Longitude = r.NextDouble() + r.Next(-180, 180);
				s.Lattitude = r.NextDouble() + r.Next(-90, 90);
				stations.Add(s);
			}
					

			//Customer
			l = r.Next(10, 101);
			for (int i = 0;i < l;++i)
			{
				int rid = r.Next();
				for (int h = 0;h < i;++h) {
					if (rid == customers[h].Id) {
						h =  -1;
						rid = r.Next();
					}
				}
				
				Customer c = new Customer();
				c.Id = rid;
				c.Name = ("MyNameIs" + i);

				string ph = "0537589982";
				rid = r.Next(1000, 10000);
				for (int h = 0;h < i;++h) {
					ph = "053758" + rid;
					if (ph == customers[h].Phone)
					{
						rid = r.Next();
						h = -1;
					}
				}
				c.Phone = ph;
				c.Longitude = r.NextDouble() + r.Next(-180, 180);
				c.Lattitude = r.NextDouble() + r.Next(-90, 90);
				customers.Add(c);
			}

			//Parcel
			l = r.Next(10, 1001);
			for (int i = 0;i < l;++i)
			{
				Parcel p = new Parcel();
				p.Id = i;
				p.Requested = DateTime.Now;//should be created
				int len = customers.Count;
				for(int h = 0; h < len;++h)
				{
					int len2 = parcels.Count;
					for(int u = 0; u < len2; ++u)
					{
						if(customers[h].Id == parcels[u].Senderld)
						{
							h = -1;
							break;
						}
					}
					p.Senderld = customers[h].Id;
				}
				
				
				int Len = drones.Count;
				for(int h = 0; h < Len;++h)
				{
					if(drones[h].Status == DroneStatuses.Available)
					{
						p.Droneld = drones[h].Id;	
						p.Scheduled = DateTime.Now;
					}
				}
				
				
				Len = customers.Count;
				for(int h = 0; h < Len;++h)
				{
					int len2 = parcels.Count;
					for(int u = 0; u < len2; ++u)
					{
						if(customers[h].Id == parcels[u].Targetld)
						{
							h = -1;
							break;
						}
					}
					p.Targetld = customers[h].Id;
				}
				
				p.Weight = (WeightCategories)(r.Next(0, 3));
				p.priority = (Priorities)(r.Next(0, 3));
				
				
			}



		}

		
	}

	public class DalObject
	{
		public DalObject() { DataSource.Initialize(); }
		public int AddParcel()
		{

			DataSource.Config.Number_ID += 1;
			return 0;
		}

		public static void AddStation(Station oj)
		{
			DataSource.AddStation(oj);
		}
		public static void AddDrone(Drone oj)
		{
			DataSource.AddDrone(oj);
		}
		public static void AddParcel(Parcel oj)
		{
			DataSource.AddParcel(oj);
		}
		public static void AddCustomer(Customer oj)
		{
			DataSource.AddCustomer(oj);
		}
	}
}
