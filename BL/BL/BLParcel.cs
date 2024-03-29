using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BlApi;
using BO;

namespace BL
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string AddParcel(BO.Parcel p, int SenderId, int TargetId) {
            try {
                if (SenderId == TargetId)
                    throw new SameCustomerException(TargetId);
                lock (data) {

                    CheckLegelChoice((int)p.Weight);
                    CheckLegelChoice((int)p.Priority);
                    DO.Parcel pa = new DO.Parcel();
                    pa.Active = true;
                    pa.SenderId = SenderId;
                    pa.TargetId = TargetId;
                    pa.Weight = (DO.WeightCategories)(int)p.Weight;
                    pa.Priority = (DO.Priorities)(int)p.Priority;
                    pa.Requested = DateTime.Now;
                    int Id = data.AddParcel(pa);
                    return $"The number of parcel: {Id}\n";
                }
            }
            catch (DO.IdExistException exp) {
                throw new BO.IdExistException(exp.id);
            }
            catch (DO.IdNotExistException exp) {
                throw new BO.IdNotExistException(exp.id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string UpdateParcel(int id, BO.Priorities priorty) {
            lock (data) {
                DO.Parcel pa = data.GetParcelById(id);
                pa.Priority = (DO.Priorities)(int)priorty;
                data.UpdateParcel(pa);
            }
            return "The update was successful\n";
        }

        /// <summary>
        /// If all is fine, return a parcel object by id, else throw exception
        /// </summary>
        /// <param name="Id">The id of the requested parcel</param>
        /// <returns>The object of the requested parcel</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Parcel GetParcelById(int Id) {
            try {
                lock (data) {

                    DO.Parcel chosenp = data.GetParcelById(Id);
                    BO.Parcel pa = new BO.Parcel();
                    pa.Id = chosenp.Id;
                    pa.PickedUp = chosenp.PickedUp;
                    pa.Priority = (BO.Priorities)(int)chosenp.Priority;
                    pa.Requested = chosenp.Requested;
                    pa.Scheduled = chosenp.Scheduled;
                    pa.Weight = (BO.WeightCategories)(int)chosenp.Weight;
                    pa.Delivered = chosenp.Delivered;
                    //If a drone is associated with a parcel
                    if (chosenp.DroneId != 0) {
                        pa.Drone = new DroneInParcel();
                        BO.Drone d = GetDroneById(chosenp.DroneId);
                        pa.Drone.Battery = d.Battery.Value;
                        pa.Drone.Id = d.Id.Value;
                        pa.Drone.CLocation = d.CLocation;
                    }

                    //CustomerInParcel - The Target Customer of Parcel 
                    CustomerInParcel cp1 = new CustomerInParcel();
                    cp1.Id = chosenp.TargetId;
                    DO.Customer customerhelp = data.GetCustomerById(cp1.Id);
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
            }
            catch (DO.IdNotExistException exp) {
                throw new BO.IdNotExistException(exp.id);
            }
        }

        /// <summary>
        /// Returns the list of parcels
        /// </summary>
        /// <returns>Returns the list of parcels</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelList> GetParcels() {
            lock (data) {
                return ConvertToBL(data.GetParcelByFilter(pa => pa.Active));
            }
        }

        /// <summary>
        /// Returns a list of all unassigned parcels
        /// </summary>
        /// <returns>Returns a list of all unassigned parcels</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelList> GetParcelDrone() {
            lock (data) {
                return ConvertToBL(data.GetParcelByFilter(pa => pa.Scheduled == null));
            }
        }

        /// <summary>
        /// Convert the list from type of DAL(Parcel) to type of BL(ParcelList)
        /// </summary>
        /// <param name="listCustomers">The list we want to convert</param>
        /// <returns>The same list converted to BL(ParcelList)</returns>
        private IEnumerable<ParcelList> ConvertToBL(IEnumerable<DO.Parcel> listParcels) {
            List<ParcelList> parcel = new List<ParcelList>();
            foreach (var item in listParcels) {
                ParcelList pa = new ParcelList();
                pa.Id = item.Id;
                pa.Priority = (BO.Priorities)(int)item.Priority;
                pa.SenderId = item.SenderId;
                pa.TargetId = item.TargetId;
                pa.Weight = (BO.WeightCategories)(int)item.Weight;
                pa.Status = (Statuses)ReturnStatus(item);
                parcel.Add(pa);
            }
            return parcel;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelList> GetParcelByFilter(object weight, object status, object priorty, object fromDate, object toDate) {
            lock (data) {
                IEnumerable<ParcelList> parcelLists;
                if (fromDate is not null && toDate is not null)
                    parcelLists = ConvertToBL(data.GetParcelByFilter(p => p.Requested <= (DateTime)toDate && p.Requested >= (DateTime)fromDate && p.Active));
                else if (fromDate is not null)
                    parcelLists = ConvertToBL(data.GetParcelByFilter(p => p.Requested >= (DateTime)fromDate && p.Active));
                else if (toDate is not null)
                    parcelLists = ConvertToBL(data.GetParcelByFilter(p => p.Requested <= (DateTime)toDate && p.Active));
                else
                    parcelLists = GetParcels();
                if (status is not null and not "All")
                    parcelLists = parcelLists.Where(p => p.Status == (Statuses)status);
                if (weight is not null and not "All")
                    parcelLists = parcelLists.Where(p => p.Weight == (BO.WeightCategories)weight);
                if (priorty is not null and not "All")
                    parcelLists = parcelLists.Where(p => p.Priority == (BO.Priorities)priorty);
                return parcelLists;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string DeleteParcel(int id) {
            lock (data) {
                DO.Parcel p = data.GetParcelById(id);
                if (p.Scheduled != null)
                    throw new CanntDeleteParcel(p.Id);
                p.Active = false;
                data.DeleteParcel(p);
            }
            return "The delete was successful\n";
        }
    }
}