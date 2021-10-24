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
		internal static List<DroneCharge> droneCharges = new List<DroneCharge>();

		internal class Config {
			public static int Number_ID = 0;
		}
		
		
		public static void Initialize() {
			Random r = new Random();
			
			//Drones
			int l = r.Next(5, 11);
			for (int i = 0;i < l;++i) {
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
			for (int i = 0;i < l;++i) {
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
			for (int i = 0;i < l;++i) {
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
					if (ph == customers[h].Phone) {
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
			for (int i = 0;i < l;++i) {
				Parcel p = new Parcel();
				p.Id = i;
				//p.Requested = DateTime.Now;
				//p.Requested = DateTime.Now.AddDays(new Random().Next(-10));
				p.Requested = new DateTime(2021, rand.Next(10, 12), rand.Next(0, 28), rand.Next(0, 24), rand.Next(0, 60), rand.Next(0, 60));
				int len = customers.Count;
				for(int h = 0; h < len;++h) {
					int len2 = parcels.Count;
					for(int u = 0; u < len2; ++u) {
						if(customers[h].Id == parcels[u].Senderld) {
							h = -1;
							break;
						}
					}
					p.Senderld = customers[h].Id;
				}
				
				
				len = drones.Count;
				for(int h = 0; h < len;++h) {
					if(drones[h].Status == DroneStatuses.Delivery) {
						p.Droneld = drones[h].Id;	
						//p.Scheduled = DateTime.Now;
						p.Scheduled = new DateTime(2021, rand.Next(10, 12), rand.Next(0, 28), rand.Next(0, 24), rand.Next(0, 60), rand.Next(0, 60));
						//p.Scheduled = DateTime.Now.AddDays(new Random().Next(1000));
					}
				}
				
				
				len = customers.Count;
				for(int h = 0; h < Len;++h) {
					int len2 = parcels.Count;
					for(int u = 0; u < len2; ++u) {
						if(customers[h].Id == parcels[u].Targetld) {
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
		
		public static double DistancePrint(double lat1, double lon1, char letter, int id)
		{
			double dis;
			if (letter == 'c') {
				Customer c = DataSource.customers.Find(c => id == c.Id);
				int index = DataSource.customers.IndexOf(c);
				dis = DataSource.customers[index].DistanceTo(lat1, lon1, DataSource.customers[index].Lattitude, DataSource.customers[index].Longitude);
			}
			else {
				Station s = DataSource.stations.Find(s => id == s.Id);
				int index = DataSource.stations.IndexOf(s);
				dis = DataSource.stations[index].DistanceTo(lat1, lon1, DataSource.stations[index].Lattitude, DataSource.stations[index].Longitude);
			}
			return dis;
		}

		public static void AddStation(int Id, string Name, double Longitude, double Lattitude, int ChargeSlots) {
			Station s = new Station();
			s.Id = Id;
			s.Name = Name;
			s.Longitude = Longitude;
			s.Lattitude = Lattitude;
			s.ChargeSlots = ChargeSlots;
			DataSource.stations.Add(s);
		}
		
		public static void AddDrone(int Id, string Model, WeightCategories MaxWeight, DroneStatuses Status, double Battery) {
			Drone d = new Drone();
			d.Id = Id;
			d.Model = Model;
			d.MaxWeight = MaxWeight;
			d.Status = Status;
			d.Battery = Battery;
			DataSource.drones.Add(d);
		}
		
		public static int AddParcel(int Id, int Senderld, int Targetld, WeightCategories Weight, Priorities priority, DateTime Requested, int Droneld, DateTime Scheduled, DateTime PickedUp, DateTime Delivered) {
			DataSource.Config.Number_ID += 1;
			Parcel p = new Parcel();
			p.Id = Id;
			p.Senderld = Senderld;
			p.Targetld = Targetld;
			p.Weight = Weight;
			p.priority = priority;
			p.Targetld = Targetld;
			p.Requested = Requested;
			p.Scheduled = Scheduled;
			p.PickedUp = PickedUp;
			p.Delivered = Delivered;
			DataSource.parcels.Add(p);
			return DataSource.Config.Number_ID;
		}
		
		public static void AddCustomer(int Id, string Name, string Phone, double Longitude, double Lattitude) {
			Customer c = new Customer();
			c.Id = Id;
			c.Name = Name;
			c.Phone = Phone;
			c.Longitude = Longitude;
			c.Lattitude = Lattitude;
			DataSource.customers.Add(c);
		}
		
		public static void PrintById(int Id, int num) {
			switch (num)
			{
				case 1:
					Station s = DataSource.stations.Find(s => id == s.Id);
					s.ToString();
					
				case 2:
					Drone d = DataSource.drones.Find(d => id == d.Id);
					d.ToString();
					
				case 3:
					Customer c = DataSource.customers.Find(c => id == c.Id);
					c.ToString();
					
				case 4:
					Parcel p = DataSource.parcels.Find(p => id == p.Id);
					p.ToString();
			}
			
		}
		
		public static void PrintList(int num) {
			int i = 0;
			switch (num)
			{
				case 1:
					for (; i < DataSource.stations.Count; i++)
						DataSource.stations[i].ToString();
					
				case 2:
					for (; i < DataSource.drones.Count; i++)
						DataSource.drones[i].ToString();
					
				case 3:
					for (; i < DataSource.customers.Count; i++)
						DataSource.customers[i].ToString();
					
				case 4:
					for (; i < DataSource.parcels.Count; i++)
						DataSource.parcels[i].ToString();
					
				case 5:
					for (; i < DataSource.parcels.Count; i++)
						if (DataSource.parcels[i].DroneId == 0)
							DataSource.parcels[i].ToString();
					
				case 6: //to fix
					for (; i < DataSource.stations.Count; i++) {
						int id = DataSource.stations[i].Id;
						int count = 0;
						for (int j = 0; j < DataSource.droneCharges.Count; j++) 
							if (DataSource.droneCharges[j].StationId == id)
								count += 1;
						if (count == DataSource.stations[i].ChargeSlots)
							DataSource.stations[i].ToString();
					}
			}
			
		}
	}
}
