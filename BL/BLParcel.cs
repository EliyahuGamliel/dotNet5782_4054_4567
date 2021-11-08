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

        public string GetParcelById(int Id) {
            try
            {
                return data.GetParcelById(Id).ToString();
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
                if (DateTime.Compare(item.Requested, item.Scheduled) > 0)
                        pa.Status = Statuses.Created;
                else if (DateTime.Compare(item.Scheduled, item.PickedUp) > 0)
                        pa.Status = Statuses.Associated;
                else if (DateTime.Compare(item.PickedUp, item.Delivered) > 0)
                        pa.Status = Statuses.Collected;
                else if (DateTime.Compare(item.Delivered, item.PickedUp) > 0)
                        pa.Status = Statuses.Provided;
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
                if (DateTime.Compare(item.Requested, item.Scheduled) > 0)
                        pa.Status = Statuses.Created;
                else if (DateTime.Compare(item.Scheduled, item.PickedUp) > 0)
                        pa.Status = Statuses.Associated;
                else if (DateTime.Compare(item.PickedUp, item.Delivered) > 0)
                        pa.Status = Statuses.Collected;
                else if (DateTime.Compare(item.Delivered, item.PickedUp) > 0)
                        pa.Status = Statuses.Provided;
                parcel.Add(pa);
            }
            return parcel;
        }
    }
} 