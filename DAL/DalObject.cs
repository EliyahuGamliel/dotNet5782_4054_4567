using System;
using IDAL.DO
namespace DalObject
{
    public class DataSource
    {
        internal static IDAL.DO.Drone[] drones = new IDAL.DO.Drone[10];
        internal static IDAL.DO.Station[] stations = new IDAL.DO.Station[5];
        internal static IDAL.DO.Customer[] customers = new IDAL.DO.Customer[100];
        internal static IDAL.DO.Parcel[] parcels = new IDAL.DO.Parcel[1000];
        
         public void Initialize()
         {
           ///IC 
            Random r = new Random();
            int l = r.next(2, 10);
            for(int i = 0;i < r;)
             
         }

        internal class Confing
        {
            public static int Drones_Index = 0;
            public static int Drones_Index = 0;
            public static int Drones_Index = 0;
            public static int Drones_Index = 0;
            public static int Number_ID = 0;       
        }
    }
    
    public class DalObject
    {
        
    }
} 
