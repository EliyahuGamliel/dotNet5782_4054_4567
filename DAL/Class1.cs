using System;

namespace IDAL
{
    namespace DO
    {
        public enum WeightCategories {Light, Medium, Heavy}
        
        public enum DroneStatuses {Available, Maintenance, Delivery}
        
        public enum Priorities {Normal, Fast, Emergency}
        
        public struct Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitube { get; set; }
            public double Lattitube{ get; set; }
        }
        
        public struct Parcel
        {
            public int Id { get; set; }
            public int Senderld { get; set; }
            public int Targetld { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Lattitube{ get; set; }
            public datetime Requested{ get; set; }
            public int Droneld{ get; set; }
            public Datetime Scheduled{ get; set; }
            public Datetime PickedUp{ get; set; }
            public Datetime Delivered{ get; set; }
        }
        
       public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status{ get; set; }
            public double Battery{ get; set; }
        }
        
        public struct Station
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public double Longitude { get; set; }
            public double Lattitude{ get; set; }
            public int ChargeSlots{ get; set; }
        }
        
        public struct DroneCharge
        {
            public int Droneld { get; set; }
            public int Stationld { get; set; }
        }
    }
}
