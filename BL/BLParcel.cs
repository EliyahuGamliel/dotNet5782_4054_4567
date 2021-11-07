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
                if (SenderId == TargetId)
                    throw new SameCustomerException(TargetId);
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
            catch (IDAL.DO.IdExistException)
            {
                throw new IdExistException(p.Id);
            }
            catch (IDAL.DO.IdNotExistException)
            {
                throw new IdNotExistException(p.Id); //
            }
            
        }
        
        public void UpdateParcel(int id, int name) {
    
        }

        public string GetParcelById(int Id) {
            return data.GetParcelById(Id);
        }

        public IEnumerable<IDAL.DO.Parcel> GetParcels(){
            return data.GetParcels();
        }

        public IEnumerable<IDAL.DO.Parcel> GetParcelDrone(){
            return data.GetParcelDrone();
        }
    }
} 