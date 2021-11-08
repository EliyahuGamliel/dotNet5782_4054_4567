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
                return "The addition was successful\n";
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

        public Parcel GetParcelById(int Id) {
            try
            {
                IDAL.DO.Parcel p = data.GetParcelById(Id);
                Parcel pa = new Parcel();
                pa.Id = p.Id;
                pa.PickedUp = p.PickedUp;
                pa.Priority = (Priorities)(int)p.Priority;
                pa.Requested = p.Requested;
                pa.Scheduled = p.Scheduled;
                pa.Weight = (WeightCategories)(int)p.Weight;
                pa.Delivered = p.Delivered;
                pa.Drone = GetDroneById(p.DroneId);

                CustomerInParcel cp = new CustomerInParcel();
                cp.Id = p.TargetId;
                IDAL.DO.Customer c_help = data.GetCustomerById(cp.Id); 
                cp.Name = c_help.Name;
                pa.Target = cp;
                
                cp.Id = p.SenderId;
                c_help = data.GetCustomerById(cp.Id); 
                cp.Name = c_help.Name;
                pa.Sender = cp;
            
                return pa;
            }
            catch (IDAL.DO.IdNotExistException)
            {
                throw new IdNotExistException(Id);
            }
        }

        public IEnumerable<ParcelList> GetParcels(){
            IEnumerable<IDAL.DO.Parcel> list_p = data.GetParcels();
            List<ParcelList> parcel = new List<ParcelList>();
            foreach (var item in list_p)
            {
                ParcelList pa = new ParcelList();
                pa.Id = item.Id;
                pa.Priority = (Priorities)(int)item.Priority;
                pa.SenderId = item.SenderId;
                pa.TargetId = item.TargetId;
                pa.Weight = (WeightCategories)(int)item.Weight;
                pa.Status = (Statuses)ReturnStatus(item);
                parcel.Add(pa);
            }
            return parcel;
        }

        public IEnumerable<ParcelList> GetParcelDrone(){
            IEnumerable<IDAL.DO.Parcel> list_pD = data.GetParcelDrone();
            List<ParcelList> parcel = new List<ParcelList>();
            foreach (var item in list_pD)
            {
                ParcelList pa = new ParcelList();
                pa.Id = item.Id;
                pa.Priority = (Priorities)(int)item.Priority;
                pa.SenderId = item.SenderId;
                pa.TargetId = item.TargetId;
                pa.Weight = (WeightCategories)(int)item.Weight;
                pa.Status = (Statuses)ReturnStatus(item);
                parcel.Add(pa);
            }
            return parcel;
        }
    }
} 