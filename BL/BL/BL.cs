using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BlApi;
using BO;
using DalApi;

namespace BL
{
    public sealed partial class BL : IBL
    {
        /// <summary>
        /// Ctor for the compiler
        /// </summary>
        static BL() { }

        private class Nested
        {
            internal static volatile BL _instance = null;
            internal static readonly object _lock = new object();
            static Nested() { }
        }

        public static BL Instance
        {
            get
            {
                if (Nested._instance == null) {
                    lock (Nested._lock) {
                        if (Nested._instance == null) {
                            Nested._instance = new BL();
                        }
                    }
                }
                return Nested._instance;
            }
        }

        private Random rand = new Random();
        internal IDal data;
        private List<DroneList> dronesList = new List<DroneList>();
        private double Avaliable;
        private double WeightLight;
        private double WeightMedium;
        private double WeightHeavy;
        private double ChargingRate;

        /// <summary>
        /// /// If all is fine, the drone assign to a parcel, else throw exception
        /// </summary>
        /// <param name="DroneId">ID of the drone to assign a parcel</param>
        /// <returns>Notice if the addition was successful</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string AssignDroneParcel(int DroneId) {
            CheckNotExistId(dronesList, DroneId);

            lock (data) {
                DroneList d = dronesList.Find(dr => dr.Id == DroneId);
                int index = dronesList.IndexOf(d);

                //Removed all the parcels that cann't assign to the Drone
                IEnumerable<DO.Parcel> parcelslist = data.GetParcelByFilter(p => p.Active && ((BO.WeightCategories)p.Weight <= d.MaxWeight) &&
                    (ReturnBattery(3, d.CLocation, GetCustomerById(p.SenderId).Location) +
                    ReturnBattery((int)p.Weight, GetCustomerById(p.SenderId).Location, GetCustomerById(p.TargetId).Location) +
                    ReturnBattery(3, GetCustomerById(p.TargetId).Location, ReturnCloseStation(data.GetStationByFilter(s => s.Active), GetCustomerById(p.TargetId).Location).Location)
                    <= d.Battery) &&
                    p.Scheduled == null);

                //There are no matching parcels
                if (parcelslist.Count() == 0 || d.ParcelId != 0)
                    throw new DroneCannotAssigan();

                //By Priority
                parcelslist = parcelslist.OrderByDescending(p => p.Priority);
                DO.Parcel parcelchoose = parcelslist.First();
                parcelslist = parcelslist.Where(p => p.Priority == parcelchoose.Priority);

                //By Weight
                parcelslist = parcelslist.OrderByDescending(p => p.Weight);
                parcelchoose = parcelslist.First();
                parcelslist = parcelslist.Where(p => p.Weight == parcelchoose.Weight);

                //By Distance
                parcelslist = parcelslist.OrderByDescending(p => DistanceTo(d.CLocation, GetCustomerById(p.SenderId).Location));

                //The chosen parcel
                parcelchoose = parcelslist.First();

                d.Status = DroneStatuses.Delivery;
                d.ParcelId = parcelchoose.Id;
                dronesList[index] = d;

                parcelchoose.Scheduled = DateTime.Now;
                parcelchoose.DroneId = d.Id;

                data.UpdateParcel(parcelchoose);
            }
            return "The update was successful\n";
        }

        /// <summary>
        /// If all is fine, the drone pick up the parcel, else throw exception
        /// </summary>
        /// <param name="id">ID of the drone to pickup a parcel</param>
        /// <returns>Notice if the addition was successful</returns>        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string PickUpDroneParcel(int id) {
            lock (data) {
                CheckNotExistId(dronesList, id);
                DroneList d = dronesList.Find(dr => dr.Id == id);
                BO.Drone chosendrone = GetDroneById(id);
                int index = dronesList.IndexOf(d);

                DO.Parcel chosenp = data.GetParcelById(d.ParcelId);
                if (ReturnStatus(chosenp) != 1)
                    throw new DroneCannotPickUp();

                chosenp.PickedUp = DateTime.Now;
                double battery = ReturnBattery(3, d.CLocation, chosendrone.PTransfer.CollectionLocation);
                d.Battery -= battery;
                d.CLocation = chosendrone.PTransfer.CollectionLocation;
                dronesList[index] = d;
                data.UpdateParcel(chosenp);
            }
            return "The update was successful\n";
        }

        /// <summary>
        /// If all is fine, the drone deliver the parcel, else throw exception
        /// </summary>
        /// <param name="id">ID of the drone to deliver a parcel</param>
        /// <returns>Notice if the addition was successful</returns>   
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string DeliverParcelCustomer(int id) {
            lock (data) {
                CheckNotExistId(dronesList, id);
                DroneList d = dronesList.Find(dr => dr.Id == id);
                BO.Drone chosendrone = GetDroneById(id);
                int index = dronesList.IndexOf(d);

                DO.Parcel chosenp = data.GetParcelById(d.ParcelId);
                if (ReturnStatus(chosenp) != 2)
                    throw new DroneCannotDeliver();

                chosenp.Delivered = DateTime.Now;
                chosenp.DroneId = 0;
                double battery = ReturnBattery(3, d.CLocation, chosendrone.PTransfer.DestinationLocation);
                d.Status = DroneStatuses.Available;
                d.Battery -= battery;
                d.CLocation = chosendrone.PTransfer.DestinationLocation;
                d.ParcelId = 0;
                dronesList[index] = d;
                data.UpdateParcel(chosenp);
            }
            return "The update was successful\n";
        }

        public void PlaySimulator(int Id, Action updateDrone, Func<bool> stop) {
            new SimulatorDrone(Instance, Id, updateDrone, stop);
        }
    }
}
