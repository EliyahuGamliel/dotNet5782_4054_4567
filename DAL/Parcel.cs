using System;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public int Id { get; set; }
            public int Senderld { get; set; }
            public int Targetld { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities priority{ get; set; }
            public DateTime Requested{ get; set; }
            public int Droneld{ get; set; }
            public DateTime Scheduled{ get; set; }
            public DateTime PickedUp{ get; set; }
            public DateTime Delivered{ get; set; }
            
            public override string ToString() 
            { return $"Id: {Id}\nSenderld: {Senderld}\nTargetld: {Targetld}\nWeight: {Weight}\nRequested: {Requested}\nDroneld: {Droneld}\nScheduled: {Scheduled}\nPickedUp: {PickedUp}\n Delivered: {Delivered}\n"; }
        }
    }
}
