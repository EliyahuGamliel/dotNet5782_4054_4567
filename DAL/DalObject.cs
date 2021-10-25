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
		
		/// <summary>
		/// this function initialise the data at the beginning
		/// </summary>
		public static void Initialize() {
			Random r = new Random();
			bool tof;
			//Drones
			int l = r.Next(5, 11);
			for (int i = 0;i < l;++i) {
				int rid = r.Next();
				for (int h = 0;h < i;++h) {///to check if the id already exists
					if (rid == drones[h].Id) {
						i -= 1;
						rid = r.Next();
					}
				}
				
				Drone d = new Drone();
				d.Id = rid;
				d.Model = ("Mark" + i);
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
				s.Name = s.Id;
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
					if (ph == customers[h].Phone) {//it takes a random number from 0000 to 9999 and add it to "053758"
						rid = r.Next();
						h = -1;
					}
				}
				c.Phone = ph;
				c.Longitude = r.NextDouble() + r.Next(-180, 180);
				c.Lattitude = r.NextDouble() + r.Next(-90, 90);
				customers.Add(c);
			}

			///Parcel
			int IndexOfSender = 0;
			l = r.Next(10, 1001);
			for (int i = 0;i < l;++i) {
				tof = true;
				Parcel p = new Parcel();
				p.Id = i;
				p.Requested = new DateTime(2021, r.Next(10, 13), r.Next(1, 28), r.Next(0, 24), r.Next(0, 60), r.Next(0, 60));
				int len = customers.Count;

				int ind = r.Next(0, len);
				p.TargetId = customers[ind].Id;
				IndexOfSender = ind;
				



				len = drones.Count;
				for(int h = 0; h < len;++h) {
					if(drones[h].Status == DroneStatuses.Delivery) {
						p.DroneId = drones[h].Id;
						p.Scheduled = new DateTime(2021, r.Next(10, 13), r.Next(1, 28), r.Next(0, 24), r.Next(0, 60), r.Next(0, 60));
						while (DateTime.Compare(p.Requested, p.Scheduled) > 0)
						{
							p.Scheduled = new DateTime(2021, r.Next(10, 13), r.Next(1, 28), r.Next(0, 24), r.Next(0, 60), r.Next(0, 60));//////////////
						}
					}
				}
				
				
				len = customers.Count;

				int ind2 = r.Next(0, len);
				while(ind2 == ind)//so that the sender and the target won't be the same customer
                {
					ind2 = r.Next(0, len);
				}
				p.SenderId = customers[ind2].Id;


				p.Weight = (WeightCategories)(r.Next(0, 3));
				p.priority = (Priorities)(r.Next(0, 3));

				parcels.Add(p);
			}

		}
	
	}

	public class DalObject
	{
		/// <summary>
		/// uses the function Initialize() in order to initialise the data
		/// </summary>
		public DalObject() { DataSource.Initialize(); }

		/// <summary>
		/// prints the distance between a given point and a customer/station
		/// </summary>
		/// <param name="lat1">the lattitude of place number 1</param>
		/// <param name="lon1">the longitude of place number 1</param>
		/// <param name="letter">if the user wants to check the distance from a station or a customer</param>
		/// <param name="id">the id of the customer/station</param>
		/// <returns></returns>
		public double DistancePrint(double lat1, double lon1, char letter, int id)
		{
			double dis;
			if (letter == 'c') {
				Customer c = DataSource.customers.Find(cu => id == cu.Id);
				int index = DataSource.customers.IndexOf(c);
				dis = DataSource.customers[index].DistanceTo(lat1, lon1, DataSource.customers[index].Lattitude, DataSource.customers[index].Longitude);///the sis between the customers
			}
			else {
				Station s = DataSource.stations.Find(st => id == st.Id);
				int index = DataSource.stations.IndexOf(s);
				dis = DataSource.stations[index].DistanceTo(lat1, lon1, DataSource.stations[index].Lattitude, DataSource.stations[index].Longitude);///the dis between the customer and the station
			}
			return dis;
		}

		public void AddStation(int Id, int Name, double Longitude, double Lattitude, int ChargeSlots) {
			Station s = new Station();
			s.Id = Id;
			s.Name = Name;
			s.Longitude = Longitude;
			s.Lattitude = Lattitude;
			s.ChargeSlots = ChargeSlots;
			DataSource.stations.Add(s);
		}
		
		public void AddDrone(int Id, string Model, int MaxWeight, int Status, double Battery) {
			Drone d = new Drone();
			d.Id = Id;
			d.Model = Model;
			d.MaxWeight = (WeightCategories)MaxWeight;
			d.Status = (DroneStatuses)Status;
			d.Battery = Battery;
			DataSource.drones.Add(d);
		}
		
		public int AddParcel(int Id, int Senderld, int Targetld, int Weight, int priority, int droneId) {
			DataSource.Config.Number_ID += 1;
			Parcel p = new Parcel();
			p.Id = Id;
			p.SenderId = Senderld;
			p.TargetId = Targetld;
			p.Weight = (WeightCategories)Weight;
			p.priority = (Priorities)priority;
			p.TargetId = Targetld;
			p.Requested = DateTime.Now;
			p.DroneId = droneId;
			DataSource.parcels.Add(p);
			return DataSource.Config.Number_ID;
		}
		
		public void AddCustomer(int Id, string Name, string Phone, double Longitude, double Lattitude) {
			Customer c = new Customer();
			c.Id = Id;
			c.Name = Name;
			c.Phone = Phone;
			c.Longitude = Longitude;
			c.Lattitude = Lattitude;
			DataSource.customers.Add(c);
		}

		public void AssignDroneParcel(int id) {
			Parcel p = DataSource.parcels.Find(pa => id == pa.Id);
			WeightCategories w = p.Weight;
			int index = DataSource.parcels.IndexOf(p);
			for (int i = 0; i < DataSource.drones.Count; i++) {
				if (DataSource.drones[i].MaxWeight >= w && DataSource.drones[i].Status == DroneStatuses.Available){///if the drone can pick up the parcel
					Drone d = DataSource.drones[i];
					d.Status = DroneStatuses.Delivery;
					DataSource.drones[i] = d;
					p.DroneId = d.Id;
					p.Scheduled = DateTime.Now;
					DataSource.parcels[index] = p;
					return;
				}
			}
		}
		/// <summary>
		/// the function takes care of picking up the drone using a drone
		/// </summary>
		/// <param name="id"></param>
public void PickUpDroneParcel(int id) {///
			Parcel p = DataSource.parcels.Find(pa => id == pa.Id);
			int index = DataSource.parcels.IndexOf(p);
			p.PickedUp = DateTime.Now;
			DataSource.parcels[index] = p;
		}
		/// <summary>
		/// the function takes care of delivering the parcel to the customer
		/// </summary>
		/// <param name="id">the id of the parcel that needs to be delivered</param>
		public void DeliverParcelCustomer(int id) {///deliverring the parcel
			Parcel p = DataSource.parcels.Find(pa => id == pa.Id);
			int index = DataSource.parcels.IndexOf(p);
			p.Delivered = DateTime.Now;
			DataSource.parcels[index] = p;
			Drone d = DataSource.drones.Find(dr => p.DroneId == dr.Id);
			index = DataSource.drones.IndexOf(d);
			d.Status = DroneStatuses.Available;
			DataSource.drones[index] = d;
		}
		/// <summary>
		/// sends the drone
		/// </summary>
		/// <param name="idDrone">the drone's id</param>
		/// <param name="idStation">the station's id</param>
		public void SendDrone(int idDrone, int idStation) { 
			DroneCharge dc = new DroneCharge();
			dc.DroneId = idDrone;
			dc.StationId = idStation;
			DataSource.droneCharges.Add(dc);
			Drone d = DataSource.drones.Find(dr => idDrone == dr.Id);
			int index = DataSource.drones.IndexOf(d);
			d.Status = DroneStatuses.Maintenance;
			DataSource.drones[index] = d;
			Station s = DataSource.stations.Find(st => idStation == st.Id);
			index = DataSource.stations.IndexOf(s);
			s.ChargeSlots -= 1;
			DataSource.stations[index] = s;
		}
		/// <summary>
		/// ///releases the drone from the charging cell
		/// </summary>
		/// <param name="id">the id of the drone that needs releasing</param>
		public void ReleasDrone(int id) {
			int index;
			DroneCharge dc = DataSource.droneCharges.Find(drch => id == drch.DroneId);
			int stationId = dc.StationId;
			int droneId = dc.DroneId;
			DataSource.droneCharges.Remove(dc);
			Drone d = DataSource.drones.Find(dr => droneId == dr.Id);
			index = DataSource.drones.IndexOf(d);
			d.Status = DroneStatuses.Available;
			d.Battery = 100;
			DataSource.drones[index] = d;
			Station s = DataSource.stations.Find(st => stationId == st.Id);
			index = DataSource.stations.IndexOf(s);
			s.ChargeSlots += 1;
			DataSource.stations[index] = s;
		}
		/// <summary>
		/// prints the item
		/// </summary>
		/// <param name="Id">the id of the item</param>
		/// <param name="num">the number of what needs to be printed</param>
		/// <returns></returns>
		public string PrintById(int Id, int num) {
			switch (num)
			{
				case 1:
					Station s = DataSource.stations.Find(st => Id == st.Id);
					return s.ToString();
					
				case 2:
					Drone d = DataSource.drones.Find(dr => Id == dr.Id);
					return d.ToString();
					
				case 3:
					Customer c = DataSource.customers.Find(cu => Id == cu.Id);
					return c.ToString();
					
				case 4:
					Parcel p = DataSource.parcels.Find(pa => Id == pa.Id);
					return p.ToString();
			}
			return " ";
		}
		
		public Station[] PrintListStation() {
			return DataSource.stations.ToArray();
		}

		public Drone[] PrintListDrone() {
			return DataSource.drones.ToArray();
		}

		public Customer[] PrintListCustomer() {
			return DataSource.customers.ToArray();
		}

		public Parcel[] PrintListParcel() {
			return DataSource.parcels.ToArray();
		}

		public Parcel[] PrintListParcelDrone() {
			return DataSource.parcels.FindAll(pa => 0 == pa.DroneId).ToArray();
		}

		public Station[] PrintListStationCharge() {
			return DataSource.stations.FindAll(st => 0 != st.ChargeSlots).ToArray();
		}
	}
}
