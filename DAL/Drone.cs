using System;

namespace IDAL
{
    namespace DO
    {
       public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status{ get; set; }
            public double Battery { get; set; }
           
            public override string ToString() 
            { return $"Id: {Id}\nModel: {Model}\nMaxWeight: {MaxWeight}\nStatus: {Status}\nBattery: {Battery}\n"; }

            public static Drone CreateDrone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery)
            {
                Drone ojct = new Drone();
                ojct.Id = id;
                ojct.Model = model;
                ojct.MaxWeight = maxWeight;
                ojct.Status = status;
                ojct.Battery = battery;

                return ojct;
            }
        }   
    }
}
