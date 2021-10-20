using System;

namespace IDAL
{
    namespace DO
    {
        public struct DroneCharge
        {
            public int Droneld { get; set; }
            public int Stationld { get; set; }
            
            public override string ToString() 
            { return $"Droneld: {Droneld}\nStationld: {Stationld}\n"; }
        } 
    }
}
