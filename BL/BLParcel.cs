using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {   
        /// <summary>
        /// the function tries to add a parcel to the list
        /// </summary>
        /// <param name="p"></param>
        /// <param name="SenderId"></param>
        /// <param name="TargetId"></param>
        /// <returns>the number of the parcel</returns>
        public string AddParcel(Parcel p, int SenderId, int TargetId){
            try
            {
                if (SenderId == TargetId)
                    throw new SameCustomerException(TargetId);
                IDAL.DO.Parcel pa = new IDAL.DO.Parcel();
                pa.SenderId = SenderId;
                pa.TargetId = TargetId;
                pa.Weight = (IDAL.DO.WeightCategories)(int)p.Weight;
                pa.Priority = (IDAL.DO.Priorities)(int)p.Priority;
                pa.Requested = DateTime.Now;
                int Id = data.AddParcel(pa);
                return $"The next number parcel: {Id}\n";
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
        
        /// <summary>
        /// the function returns a parcel by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>the parcel with the matching id</returns>
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
                if (p.DroneId != 0)
                    pa.Drone = GetDroneById(p.DroneId);
                

                CustomerInParcel cp1 = new CustomerInParcel();
                cp1.Id = p.TargetId;
                IDAL.DO.Customer c_help = data.GetCustomerById(cp1.Id); 
                cp1.Name = c_help.Name;
                pa.Target = cp1;

                CustomerInParcel cp2 = new CustomerInParcel();
                cp2.Id = p.SenderId;
                c_help = data.GetCustomerById(cp2.Id); 
                cp2.Name = c_help.Name;
                pa.Sender = cp2;
            
                return pa;
            }
            catch (IDAL.DO.IdNotExistException)
            {
                throw new IdNotExistException(Id);
            }
        }
        /// <summary>
        /// returns the list of parcels as an UEnumerable
        /// </summary>
        /// <returns>the list of parcels as an IEnumerable</returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>///////////////
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