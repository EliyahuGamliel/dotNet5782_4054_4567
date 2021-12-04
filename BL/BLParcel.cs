using System;
using System.Linq;
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
                CheckLegelChoice((int)p.Weight);
                CheckLegelChoice((int)p.Priority);
                IDAL.DO.Parcel pa = new IDAL.DO.Parcel();
            
                pa.SenderId = SenderId;
                pa.TargetId = TargetId;
                pa.Weight = (IDAL.DO.WeightCategories)(int)p.Weight;
                pa.Priority = (IDAL.DO.Priorities)(int)p.Priority;
                pa.Requested = DateTime.Now;
                int Id = data.AddParcel(pa);
                return $"The number of parcel: {Id-1}\n";
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
                IDAL.DO.Parcel chosenp = data.GetParcelById(Id);
                Parcel pa = new Parcel();
                pa.Id = chosenp.Id;
                pa.PickedUp = chosenp.PickedUp;
                pa.Priority = (Priorities)(int)chosenp.Priority;
                pa.Requested = chosenp.Requested;
                pa.Scheduled = chosenp.Scheduled;
                pa.Weight = (WeightCategories)(int)chosenp.Weight;
                pa.Delivered = chosenp.Delivered;
                //If a drone is associated with a parcel
                if (chosenp.DroneId != 0)
                {
                    pa.Drone = new DroneInParcel();
                    Drone d = GetDroneById(chosenp.DroneId);
                    pa.Drone.Battery = d.Battery;
                    pa.Drone.Id = d.Id;
                    pa.Drone.CLocation = d.CLocation;
                }
                
                //CustomerInParcel - The Target Customer of Parcel 
                CustomerInParcel cp1 = new CustomerInParcel();
                cp1.Id = chosenp.TargetId;
                IDAL.DO.Customer customerhelp = data.GetCustomerById(cp1.Id); 
                cp1.Name = customerhelp.Name;
                pa.Target = cp1;

                //CustomerInParcel - The Sender Customer of Parcel 
                CustomerInParcel cp2 = new CustomerInParcel();
                cp2.Id = chosenp.SenderId;
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
            return ConvertToBL(data.GetParcelByFilter(pa => true));
        }

        /// <summary>
        /// Returns a list of all unassigned parcels
        /// </summary>
        /// <returns>Returns a list of all unassigned parcels</returns>
        public IEnumerable<ParcelList> GetParcelDrone(){
            return ConvertToBL(data.GetParcelByFilter(pa => pa.Scheduled == null));
        }

        /// <summary>
        /// Convert the list from type of DAL(Parcel) to type of BL(ParcelList)
        /// </summary>
        /// <param name="listCustomers">The list we want to convert</param>
        /// <returns>The same list converted to BL(ParcelList)</returns>
        private IEnumerable<ParcelList> ConvertToBL(IEnumerable<IDAL.DO.Parcel> listParcels)
        {
            List<ParcelList> parcel = new List<ParcelList>();
            foreach (var item in listParcels) {
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