using System;
using IDAL.DO
namespace DalObject
{
    public class DataSource
    {
        internal static Drone[] drones = new IDAL.DO.Drone[10];
        internal static Station[] stations = new IDAL.DO.Station[5];
        internal static Customer[] customers = new IDAL.DO.Customer[100];
        internal static Parcel[] parcels = new IDAL.DO.Parcel[1000];
         
        public static DistancePrint(double lat1, double lon1, char letter, int id)
        {
            if (letter == 'c')
                dis = DistanceTo(lat1, lon1,  
            else
                                 
        }
        
         public void Initialize()
         {
           ///IC 
            Random r = new Random();
            int l = r.next(5, 11);
            for(int i = 0;i < l;++i)
            {
                int rid = r.next();
                for(int h = 0;h < i;++h)
                {
                    if(rid == drones[h].Id)
                    {
                        rid = r.next();
                        h = -1;
                    }
                }
                drone[i].Id = rid;
                drone[i].Model = ("Mark" + 1);
                drone[i].MaxWeight = (WeightCategories)(r.next(0, 3));
                drone[i].Status = (DroneStatuses)(r.next(0, 3));  //change the status so they wil be different 
                drone[i].Battery = 100;
            }



            l = r.next(2, 11);
            for(int i = 0;i < l;++i)
            {
                int rid = r.next();
                for(int h = 0;h < i;++h)
                {
                    if(rid == stations[h].Id)
                    {
                        rid = r.next();
                        h = -1;
                    }
                }
                stations[i].Id = rid;
                stations[i].Name = ("PumbaNumber " + i);
                stations[i].Longitube = r.nextDouble(-180, 180);
                stations[i].Lattitube = r.nextDouble(-90, 90);
                int numberOsCells = 5;//to change
                stations[i].ChargeSlots = numberOsCells;
            }



            l = r.next(10, 101);
            for(int i = 0;i < l;++i)
            {
                int rid = r.next();
                for(int h = 0;h < i;++h)
                {
                    if(rid == stations[h].Id)
                    {
                        rid = r.next();
                        h = -1;
                    }
                }
                stations[i].Id = rid;
                customers.Name = ("mynameIs " + i);

                
                rid = r.next(1000, 10000);
                for(int h = 0;h < i;++h)
                {
                    string ph = "053758" + rid;
                    if(ph == customers[h].Phone)    
                    {
                        rid = r.next();
                        h = -1;
                    }
                }
                customers[i].Longitube = r.nextDouble(-180, 180);
                customers[i].Lattitube = r.nextDouble(-90, 90);
            }
             

            l = r.next(10, 1001);
            for(int i = 0;i < l;++i)
            {
                drone[i].Id = Confing.Parcels_Index;
                //need to add here the sender id, target id, drone id, 
                parcels[i].Longitube = r.nextDouble(-180, 180);
                parcels[i].Lattitube = r.nextDouble(-90, 90);

            }

            
            
         }

        internal class Confing
        {
            public static int Drones_Index = 0;
            public static int Parcels_Index = 0;
            public static int Drones_Index = 0;
            public static int Drones_Index = 0;
            public static int Number_ID = 0;       
        }
    }
    
    public class DalObject
    {
        
    }
} 
