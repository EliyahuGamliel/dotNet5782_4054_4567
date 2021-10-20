using System;
using IDAL.DO;

namespace DalObject
{
	public class DataSource
	{
		internal static Drone[] drones = new IDAL.DO.Drone[10];
		internal static Station[] stations = new IDAL.DO.Station[5];
		internal static Customer[] customers = new IDAL.DO.Customer[100];
		internal static Parcel[] parcels = new IDAL.DO.Parcel[1000];

		internal class Config
		{
			public static int Drones_Index = 0;
			public static int Parcels_Index = 0;
			public static int Customers_Index = 0;
			public static int Stations_Index = 0;
			public static int Number_ID = 0;
		}

		public static double DistancePrint(double lat1, double lon1, char letter, int id)
		{
			double dis;
			if (letter == 'c')
				dis = customers[IndexOfCustomerId(id)].DistanceTo(lat1, lon1, customers[IndexOfCustomerId(id)].Lattitube, customers[IndexOfCustomerId(id)].Longitube);
			else
				dis = stations[IndexOfCustomerId(id)].DistanceTo(lat1, lon1, stations[IndexOfStationId(id)].Lattitude, stations[IndexOfStationId(id)].Longitude);
			return dis;
		}

		internal static int IndexOfCustomerId(int CSID)
		{
			for (int i = 0;i <= Config.Customers_Index; ++i) {
				if (customers[i].Id == CSID) {
					return i;
				}
			}
			return 0;
		}

		internal static int IndexOfStationId(int CSID)
		{
			for (int i = 0; i <= Config.Customers_Index; ++i)
			{
				if (stations[i].Id == CSID) {
					return i;
				}
			}
			return 0;
		}
		
		public void Initialize()
		{
			///IC 
			Random r = new Random();
			int l = r.Next(5, 11);
			for (int i = 0;i < l;++i)
			{
				int rid = r.Next();
				for (int h = 0;h < i;++h)
				{
					if (rid == drones[h].Id)
					{
						rid = r.Next();
						h = -1;
					}
				}
				drones[i].Id = rid;
				drones[i].Model = ("Mark" + 1);
				drones[i].MaxWeight = (WeightCategories)(r.Next(0, 3));
				drones[i].Status = (DroneStatuses)(r.Next(0, 3));  //change the status so they wil be different 
				drones[i].Battery = 100;
			}



			l = r.Next(2, 11);
			for (int i = 0;i < l;++i)
			{
				int rid = r.Next();
				for (int h = 0;h < i;++h)
				{
					if (rid == stations[h].Id)
					{
						rid = r.Next();
						h = -1;
					}
				}
				stations[i].Id = rid;
				stations[i].Name = $"StationNumber{i}";
				stations[i].Longitude = r.NextDouble() + r.Next(-180, 180);
				stations[i].Lattitude = r.NextDouble() + r.Next(-90, 90);
				int numberOsCells = 5;//to change
				stations[i].ChargeSlots = numberOsCells;
			}



			l = r.Next(10, 101);
			for (int i = 0;i < l;++i)
			{
				int rid = r.Next();
				for (int h = 0;h < i;++h)
				{
					if (rid == stations[h].Id)
					{
						rid = r.Next();
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
			}


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

	}
}
