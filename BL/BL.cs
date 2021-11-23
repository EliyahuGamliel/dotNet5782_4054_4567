using System;
using System.Linq;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        static Random rand = new Random();
        IDAL.IDal data;
        List<DroneList> dronesList = new List<DroneList>();
        double Avaliable;
        double WeightLight;
        double WeightMedium;
        double WeightHeavy;
        double ChargingRate;

        /// <summary>
        /// /// If all is fine, the drone assign to a parcel, else throw exception
        /// </summary>
        /// <param name="DroneId">ID of the drone to assign a parcel</param>
        /// <returns>Notice if the addition was successful</returns>
        public string AssignDroneParcel(int DroneId)
        {
            CheckNotExistId(dronesList, DroneId);
            DroneList d = dronesList.Find(dr => dr.Id == DroneId);
            int index = dronesList.IndexOf(d);
            
            //Removed all the parcels that cann't assign to the Drone
            IEnumerable<IDAL.DO.Parcel> parcelslist = data.GetParcelDrone().Where(p => (WeightCategories)p.Weight <= d.MaxWeight);
            parcelslist = parcelslist.Where(p => 
                ReturnBattery(3, d.CLocation, GetCustomerById(p.SenderId).Location) +
                ReturnBattery((int)p.Weight, GetCustomerById(p.SenderId).Location, GetCustomerById(p.TargetId).Location) +
                ReturnBattery(3, GetCustomerById(p.TargetId).Location, ReturnCloseStation(data.GetStations(), GetCustomerById(p.TargetId).Location).Location)
                <= d.Battery);

            //There are no matching parcels
            if (parcelslist.Count() == 0 || d.ParcelId != 0)
                throw new DroneCannotAssigan();

            //By Priority
            parcelslist =  parcelslist.OrderByDescending(p => p.Priority);
            IDAL.DO.Parcel parcelchoose = parcelslist.First();
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
            return "The update was successful\n";
        }

        /// <summary>
        /// If all is fine, the drone pick up the parcel, else throw exception
        /// </summary>
        /// <param name="id">ID of the drone to pickup a parcel</param>
        /// <returns>Notice if the addition was successful</returns>        
        public string PickUpDroneParcel(int id)
        {
            CheckNotExistId(dronesList, id);
            DroneList d = dronesList.Find(dr => dr.Id == id);
            Drone chosendrone = GetDroneById(id);
            int index = dronesList.IndexOf(d);

            IDAL.DO.Parcel chosenp = data.GetParcelById(d.ParcelId);
            if (ReturnStatus(chosenp) != 1)
                throw new DroneCannotPickUp();

            chosenp.PickedUp = DateTime.Now;
            double battery = ReturnBattery(3, d.CLocation, chosendrone.PTransfer.CollectionLocation);
            d.Battery -= battery;
            d.CLocation = chosendrone.PTransfer.CollectionLocation;
            dronesList[index] = d;
            data.UpdateParcel(chosenp);
            return "The update was successful\n";
        }

        /// <summary>
        /// If all is fine, the drone deliver the parcel, else throw exception
        /// </summary>
        /// <param name="id">ID of the drone to deliver a parcel</param>
        /// <returns>Notice if the addition was successful</returns>   
        public string DeliverParcelCustomer(int id)
        {
            CheckNotExistId(dronesList, id);
            DroneList d = dronesList.Find(dr => dr.Id == id);
            Drone chosendrone = GetDroneById(id);
            int index = dronesList.IndexOf(d);

            IDAL.DO.Parcel chosenp = data.GetParcelById(d.ParcelId);
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
            return "The update was successful\n";
        }
    }
}
