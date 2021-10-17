using System;

namespace IDAL
{
    namespace DO
    {
       public struct Station
       {
           public int Id { get; set; }
           public int Name { get; set; }
           public double Longitude { get; set; }
           public double Lattitude{ get; set; } 
           public int ChargeSlots{ get; set; }
           
           public string override ToString() 
           { return $"Id: {Id}\nName: {Name}\nLongitude: {Longitude}\nLattitude: {Lattitude}\nChargeSlots: {ChargeSlots}\n"; }      
        }  
    }
}
