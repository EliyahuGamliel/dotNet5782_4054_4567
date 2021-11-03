using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {   
        public int AddParcel(Parcel p){
            p.Drone = null;
            p.Scheduled = new DateTime(0,0,0,0,0,0);
            p.PickedUp = new DateTime(0,0,0,0,0,0);
            p.Delivered = new DateTime(0,0,0,0,0,0);
            p.Requested = DateTime.Now;
            IDAL.DO.Parcel pa = new IDAL.DO.Parcel();
            pa.SenderId = p.SenderId;
            pa.TargetId = p.TargetId;
            pa.Weight = (IDAL.DO.WeightCategories)(int)p.Weight;
            pa.Priority = (IDAL.DO.Priorities)(int)p.Priority;
            return data.AddParcel(pa);
        }
        
        public void UpdateParcel(int id, int name) {
    
        }

        public string GetParcelById(int Id) {
            return data.GetParcelById(Id);
        }

        public IEnumerable<Parcel> GetParcels(){
            return (IEnumerable<Parcel>)data.GetParcels();
        }

        public IEnumerable<Parcel> GetParcelDrone(){
            return (IEnumerable<Parcel>)data.GetParcelDrone();
        }
    }
} 