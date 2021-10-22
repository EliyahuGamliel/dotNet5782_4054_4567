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
				bool check = true;
				int rid = r.Next();
				for (int h = 0;h < i;++h) 
					if (rid == drones[h].Id) {
						i -= 1;
						check = false;
					}
				
				if (check)
				{
					Drone d = new Drone();
					d.Id = rid;
					d.Model = ("Mark" + 1);
					d.MaxWeight = (WeightCategories)(r.Next(0, 3));
					d.Status = (DroneStatuses)(r.Next(0, 3));  //change the status so they wil be different 
					d.Battery = 100;
					drones.Add(d);
				}
			}


			//Station
			l = r.Next(2, 6);
			for (int i = 0;i < l;++i)
			{
				bool check = true;
				int rid = r.Next();
				for (int h = 0;h < i;++h) 
					if (rid == drones[h].Id) {
						i -= 1;
						check = false;
					}
				
				if (check)
				{
					Station s = new Station();
					int numberOsCells = 5;//to change
					s.ChargeSlots = numberOsCells;
					s.Id = rid;
					//s.Name = 
					s.Longitube = r.NextDouble() + r.Next(-180, 180);
					s.Lattitube = r.NextDouble() + r.Next(-90, 90);
					stations.Add(s);
				}
			}
					

			//Customer
			l = r.Next(10, 101);
			for (int i = 0;i < l;++i)
			{
				int rid = r.Next();
				for (int h = 0;h < i;++h)
				{
					if (rid == stations[h].Id)
					{
						h = -1;
					}
				}
				stations[i].Id = rid;
				customers[i].Name = ("MyNameIs" + i);


				rid = r.Next(1000, 10000);
				for (int h = 0;h < i;++h)
				{
					string ph = "053758" + rid;
					if (ph == customers[h].Phone)
					{
						rid = r.Next();
						h = -1;
					}
				}
				customers[i].Longitube = r.NextDouble() + r.Next(-180, 180);
				customers[i].Lattitube = r.NextDouble() + r.Next(-90, 90);
				
				bool check = true;
				int rid = r.Next();
				for (int h = 0;h < i;++h) 
					if (rid == drones[h].Id) {
						i -= 1;
						check = false;
					}
				
				if (check)
				{
					Station s = new Station();
					int numberOsCells = 5;//to change
					s.ChargeSlots = numberOsCells;
					s.Id = rid;
					//s.Name = 
					s.Longitube = r.NextDouble() + r.Next(-180, 180);
					s.Lattitube = r.NextDouble() + r.Next(-90, 90);
					stations.Add(s);
				}
			}

			//Parcel
			l = r.Next(10, 1001);
			for (int i = 0;i < l;++i)
			{
				drones[i].Id = Config.Parcels_Index;
				//need to add here the sender id, target id, drone id, 

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
