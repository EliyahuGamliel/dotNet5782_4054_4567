using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {   
        public string AddParcel(Parcel p, int SenderId, int TargetId){
            try
            {
                p.Drone = null;
                p.Scheduled = new DateTime(0,0,0,0,0,0);
                p.PickedUp = new DateTime(0,0,0,0,0,0);
                p.Delivered = new DateTime(0,0,0,0,0,0);
                p.Requested = DateTime.Now;
                IDAL.DO.Parcel pa = new IDAL.DO.Parcel();
                pa.SenderId = SenderId;
                pa.TargetId = TargetId;
                pa.Weight = (IDAL.DO.WeightCategories)(int)p.Weight;
                pa.Priority = (IDAL.DO.Priorities)(int)p.Priority;
                pa.PickedUp = new DateTime(0,0,0,0,0,0);
                pa.Scheduled = new DateTime(0,0,0,0,0,0);
                pa.Delivered = new DateTime(0,0,0,0,0,0);
                pa.Requested = DateTime.Now;
                pa.TargetId = TargetId;
                pa.SenderId = SenderId;
                pa.DroneId = -1;
                int Id = data.AddParcel(pa);
                return "The addition was successful";

            }
            catch (IDAL.DO.IdExistException exp)
            {
                throw exp;
            }
            
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