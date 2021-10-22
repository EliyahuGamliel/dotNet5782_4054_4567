using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
	public class DataSource
	{
		internal static list<Drone> drones = new list<Drone>();
		internal static list<Station> stations = new list<Station>();
		internal static list<Customer> customers = new list<Customer>();
		internal static list<Parcel> parcels = new list<Parcel>();

		internal class Config
		{
			public static int Number_ID = 0;
		}

		public static double DistancePrint(double lat1, double lon1, char letter, int id)
		{
			double dis;
			if (letter == 'c') {
				Customer c = customers.find(c => id == c.Id);
				int index = customers.indexof(c);
				dis = customers[index].DistanceTo(lat1, lon1, customers[index].Lattitube, customers[index].Longitube);
			}
			else {
				Station s = stations.find(s => id == s.Id);
				int index = stations.indexof(s);
				dis = stations[index].DistanceTo(lat1, lon1, stations[index].Lattitude, stations[index].Longitude);
			}
			return dis;
		}
		
		public void Initialize()
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
					if (rid == statins[h].Id) {
						h =  -1;
						rid = r.Next();
					}
				}
				
				Station s = new Station();
				s.ChargeSlots = 2 + r.Next(0, 3);
				s.Id = rid;
				s.Name = "station" + s.Id;
				s.Longitube = r.NextDouble() + r.Next(-180, 180);
				s.Lattitube = r.NextDouble() + r.Next(-90, 90);
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


				rid = r.Next(1000, 10000);
				for (int h = 0;h < i;++h) {
					string ph = "053758" + rid;
					if (ph == customers[h].Phone)
					{
						rid = r.Next();
						h = -1;
					}
				}
				c.Phone = ph;
				c.Longitube = r.NextDouble() + r.Next(-180, 180);
				c.Lattitube = r.NextDouble() + r.Next(-90, 90);
				customers.Add(c);
			}

			//Parcel
			l = r.Next(10, 1001);
			for (int i = 0;i < l;++i)
			{
				Parcel p = new Parcel();
				p.Id = Config.Parcels_Index;
				p.Requested = DateTime.now;//should be created
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
				
				
				int len = drones.Count;
				for(int h = 0; h < len;++h)
				{
					if(drones[h].DroneStatuses == DroneStatuses.Available)
					{
						p.Droneld = drones[h].Id;	
						p.Scheduled = DateTime.now;
					}
				}
				
				
				int len = customers.Count;
				for(int h = 0; h < len;++h)
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
		public DalObject() { DataSource.Initialize() }
		public int AddParcel() { 
			
			DataSource.Config.Number_ID += 1;
			
	}
}
