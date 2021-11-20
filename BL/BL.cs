using System;
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

            IDAL.DO.Parcel parcelchoose = new IDAL.DO.Parcel();
            IEnumerable<IDAL.DO.Parcel> parcelslist = data.GetParcels();
            parcelchoose.Id = 0;
            bool first = true;
            foreach (var item in parcelslist)
            {
                Customer customersender = GetCustomerById(item.SenderId);
                Customer customertarget = GetCustomerById(item.TargetId);
                IDAL.DO.Station stationclose = ReturnCloseStation(data.GetStations(), customertarget.Location);
                Location stationlocation = new Location();
                stationlocation.Lattitude = stationclose.Lattitude;
                stationlocation.Longitude = stationclose.Longitude;
                //drone go to the coustomer sender location
                double minbattery = ReturnBattery(3, d.CLocation, customersender.Location);
                //and fron there drone go to the coustomer target location
                minbattery += ReturnBattery((int)item.Weight, customersender.Location, customertarget.Location);
                //and fron there drone go to the close station
                minbattery += ReturnBattery(3, customertarget.Location, stationlocation);
                if (d.Battery >= minbattery)
                    //First parcel that not associated
                    if (first && ReturnStatus(item) == 0)
                        parcelchoose = item;
                    else if (ReturnStatus(item) == 0)
                        //The most preferred parcel for associated
                        parcelchoose = CompressParcels(parcelchoose, item, d);
            }

            //There are no matching parcels
            if (parcelchoose.Id == 0 || d.ParcelId != 0)
                throw new DroneCannotAssigan();

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
