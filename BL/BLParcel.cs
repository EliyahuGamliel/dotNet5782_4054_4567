using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    { 
        /// <summary>
        /// If everything is fine, add a parcel to the list of parcels, else throw exception
        /// </summary>
        /// <param name="p">Object of parcel to add</param>
        /// <param name="SenderId">The ID of the sender of the parcel</param>
        /// <param name="TargetId">The ID of the target of the parcel</param>
        /// <returns>Notice if the addition was successful</returns>
        public string AddParcel(Parcel p, int SenderId, int TargetId){
            try {
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
            catch (IDAL.DO.IdExistException) {
                throw new IdExistException(p.Id);
            }
            catch (IDAL.DO.IdNotExistException exp) {
                throw new IdNotExistException(exp.id);
            }   
        }
        
        /// <summary>
        /// If all is fine, return a parcel object by id, else throw exception
        /// </summary>
        /// <param name="Id">The id of the requested parcel</param>
        /// <returns>The object of the requested parcel</returns>
            public Parcel GetParcelById(int Id) {
            try {
                IDAL.DO.Parcel p = data.GetParcelById(Id);
                Parcel pa = new Parcel();
                pa.Id = p.Id;
                pa.PickedUp = p.PickedUp;
                pa.Priority = (Priorities)(int)p.Priority;
                pa.Requested = p.Requested;
                pa.Scheduled = p.Scheduled;
                pa.Weight = (WeightCategories)(int)p.Weight;
                pa.Delivered = p.Delivered;
                //If a drone is associated with a parcel
                if (p.DroneId != 0)
                {
                    pa.Drone = new DroneInParcel();
                    Drone d = GetDroneById(p.DroneId);
                    pa.Drone.Battery = d.Battery;
                    pa.Drone.Id = d.Id;
                    pa.Drone.CLocation = d.CLocation;
                }
                
                //CustomerInParcel - The Target Customer of Parcel 
                CustomerInParcel cp1 = new CustomerInParcel();
                cp1.Id = p.TargetId;
                IDAL.DO.Customer customerhelp = data.GetCustomerById(cp1.Id); 
                cp1.Name = customerhelp.Name;
                pa.Target = cp1;

                //CustomerInParcel - The Sender Customer of Parcel 
                CustomerInParcel cp2 = new CustomerInParcel();
                cp2.Id = p.SenderId;
                customerhelp = data.GetCustomerById(cp2.Id); 
                cp2.Name = customerhelp.Name;
                pa.Sender = cp2;
            
                return pa;
            }
            catch (IDAL.DO.IdNotExistException) {
                throw new IdNotExistException(Id);
            }
        }

        /// <summary>
        /// Returns the list of parcels
        /// </summary>
        /// <returns>Returns the list of parcels</returns>
        public IEnumerable<ParcelList> GetParcels(){
            IEnumerable<IDAL.DO.Parcel> listparcels = data.GetParcels();
            List<ParcelList> parcel = new List<ParcelList>();
            foreach (var item in listparcels) {
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
        /// Returns a list of all unassigned parcels
        /// </summary>
        /// <returns>Returns a list of all unassigned parcels</returns>
        public IEnumerable<ParcelList> GetParcelDrone(){
            IEnumerable<IDAL.DO.Parcel> listparcelsD = data.GetParcelDrone();
            List<ParcelList> parcel = new List<ParcelList>();
            foreach (var item in listparcelsD) {
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