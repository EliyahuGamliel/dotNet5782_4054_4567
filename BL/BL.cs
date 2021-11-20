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
        /// Returns the most preferred parcel for pairing
        /// </summary>
        /// <param name="p1">Object of parcel 1 for comparison</param>
        /// <param name="p2">Object of parcel 2 for comparison</param>
        /// <param name="d">Object of drone</param>
        /// <returns>Returns the most preferred parcel</returns>
        private IDAL.DO.Parcel CompressParcels(IDAL.DO.Parcel p1, IDAL.DO.Parcel p2, DroneList d)
        {
            Customer c1 = GetCustomerById(p1.SenderId);
            Customer c2 = GetCustomerById(p2.SenderId);
            if (p1.Priority > p2.Priority)
                return p1;
            if (p2.Priority > p1.Priority)
                return p2;
            if (p1.Weight > p2.Weight && (int)p1.Weight <= (int)d.MaxWeight)
                return p1;
            if (p2.Weight > p1.Weight && (int)p2.Weight <= (int)d.MaxWeight)
                return p2;
            if (DistanceTo(d.CLocation, c1.Location) > DistanceTo(d.CLocation, c2.Location))
                return p1;
            if (DistanceTo(d.CLocation, c2.Location) > DistanceTo(d.CLocation, c1.Location))
                return p2;
            return p1;
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

        /// <summary>
        /// According to calculations, returns the status of the parcel
        /// </summary>
        /// <param name="p">Object of parcel</param>
        /// <returns>int of Status of parcel</returns>
        private int ReturnStatus(IDAL.DO.Parcel chosenp)
        {
            if (DateTime.Compare(chosenp.Requested, chosenp.Scheduled) > 0)
                return (int)Statuses.Created;
            else if (DateTime.Compare(chosenp.Scheduled, chosenp.PickedUp) > 0)
                return (int)Statuses.Associated;
            else if (DateTime.Compare(chosenp.PickedUp, chosenp.Delivered) > 0)
                return (int)Statuses.Collected;
            return (int)Statuses.Provided;
        }

        /// <summary>
        /// Calculates the amount of battery consumed according to weight and distance and returns the amount
        /// </summary>
        /// <param name="w">The weight the drone carries in int</param>
        /// <param name="l1">Object Location 1</param>
        /// <param name="l2">Object Location 2</param>
        /// <returns>Returns the amount of battery consumed</returns>
        private double ReturnBattery(int weight, Location l1, Location l2)
        {
            if (weight == 0)
                return DistanceTo(l1, l2) * WeightLight;
            else if (weight == 1)
                return DistanceTo(l1, l2) * WeightMedium;
            else if (weight == 2)
                return DistanceTo(l1, l2) * WeightHeavy;
            //w==3: the drone carries nothing
            return DistanceTo(l1, l2) * Avaliable;
        }

        /// <summary>
        /// Calculate what station is closest to the drone's location and returns it
        /// </summary>
        /// <param name="s">list of Station</param>
        /// <param name="drone">Object of Drone's Location</param>
        /// <returns>Returns a station object</returns>
        private IDAL.DO.Station ReturnCloseStation(IEnumerable<IDAL.DO.Station> s, Location drone)
        {
            Location l1 = new Location();
            Location l2 = new Location();
            IDAL.DO.Station st = new IDAL.DO.Station();
            bool first = false;
            foreach (var item in s)
            {
                l1.Longitude = item.Longitude;
                l1.Lattitude = item.Lattitude;
                //First stop or closer stop
                if (!first || DistanceTo(l1, drone) < DistanceTo(l2, drone))
                {
                    first = true;
                    st = item;
                    l2.Longitude = st.Longitude;
                    l2.Lattitude = st.Lattitude;
                }
            }
            return st;
        }

        /// <summary>
        /// Calculates the distance between two locations and returns it
        /// </summary>
        /// <param name="l1">Object Location 1</param>
        /// <param name="l2">Object Location 2</param>
        /// <returns>Returns the distance between two locations</returns>
        private double DistanceTo(Location l1, Location l2)
        {
            double rlat1 = Math.PI * l1.Lattitude / 180;
            double rlat2 = Math.PI * l2.Lattitude / 180;
            double theta = l1.Longitude - l2.Longitude;
            double rtheta = Math.PI * theta / 180;
            double dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            return Math.Round(dist * 1.609344, 2);
        }


        /// <summary>
        /// check how much slots are taken
        /// </summary>
        /// <param name="idStation">id of station</param>
        /// <returns>the number of taken slots</returns>
        private int ChargeSlotsCatched(int idStation)
        {
            int catched = 0;
            IEnumerable<IDAL.DO.DroneCharge> listDCharge = data.GetDroneCharge();
            foreach (var item in listDCharge)
                if (item.StationId == idStation)
                    catched += 1;
            return catched;
        }


    }
}
