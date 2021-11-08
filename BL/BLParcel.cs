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
        
        public void UpdateParcel(int id, int name) {
    
        }

        public string GetParcelById(int Id) {
            return data.GetParcelById(Id).ToString();
        }

        public IEnumerable<ParcelList> GetParcels(){
            IEnumerable<IDAL.DO.Parcel> list_p = data.GetParcels();
            List<ParcelList> parcel = new List<ParcelList>();
            foreach (var item in list_p)
            {
                ParcelList pa = new ParcelList();
                /*
                cu.Id = item.Id;
                cu.Name = item.Name;
                cu.Phone = item.Phone;
                cu.ParcelsGet = 0;
                cu.ParcelsInTheWay = 0;  
                cu.ParcelsOnlySend = 0;
                cu.ParcelsSent = 0;
                IEnumerable<IDAL.DO.Parcel> list_p = data.GetParcels();
                foreach (var itemParcel in list_p)
                {
                    if (itemParcel.SenderId == cu.Id) {
                        if (DateTime.Compare(itemParcel.Requested, itemParcel.Scheduled) > 0)
                            cu.ParcelsOnlySend += 1;
                        else if (DateTime.Compare(itemParcel.Scheduled, itemParcel.PickedUp) > 0 || DateTime.Compare(itemParcel.PickedUp, itemParcel.Delivered) > 0)
                            cu.ParcelsInTheWay += 1;
                        else if (DateTime.Compare(itemParcel.Delivered, itemParcel.PickedUp) > 0)
                            cu.ParcelsSent += 1;
                    }
                    if (itemParcel.TargetId == cu.Id && DateTime.Compare(itemParcel.Delivered, itemParcel.PickedUp) > 0)
                        cu.ParcelsGet += 1;
                }
                customer.Add(cu);
                */
            }
            return parcel;
        }

        public IEnumerable<IDAL.DO.Parcel> GetParcelDrone(){
            return data.GetParcelDrone();
        }
    }
} 