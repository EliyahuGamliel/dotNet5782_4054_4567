using System;
using System.Linq;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL
    {
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
            return data.GetDroneCharge().Where(dc => dc.StationId == idStation).Count();
        }

        /// <summary>
        /// Checks if ״id״ does not exist, if not returns error
        /// </summary>
        /// <param name="list">List of T-objects</param>
        /// <param name="id">The id for check</param>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <returns>Nothing</returns>
        private void CheckNotExistId<T>(IEnumerable<T> list, int id)
        {
            foreach (var item in list)
            {
                int idobject = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                if (idobject == id)
                    return;
            }
            throw new IdNotExistException(id);
        }

        /// <summary>
        /// all the choice need to be between 0-2
        /// </summary>
        /// <param name="choice">the choice</param>
        private void CheckLegelChoice(int choice)
        {
            if (choice > 2 || choice < 0)
                throw new ChoiceNotLegal(choice);
        }

        /// <summary>
        /// Checks if the drone can to send for cahrge, if not returns error
        /// </summary>
        /// <param name="list">List of T-objects</param>
        /// <param name="dl">Object of Drone</param>
        /// <typeparam name="T">he type of the list</typeparam>
        /// <returns>Nothing</returns>
        private double CheckDroneCannotSend(IEnumerable<IDAL.DO.Station> list, DroneList dl)
        {
            Location lst = new Location();
            Location locationStation = new Location();
            bool cntsn = false;
            foreach (var item in list)
            {
                locationStation.Lattitude = item.Lattitude;
                locationStation.Longitude = item.Longitude;
                if ((!cntsn || DistanceTo(locationStation, dl.CLocation) < DistanceTo(lst, dl.CLocation)) && item.ChargeSlots > 0)
                {
                    cntsn = true;
                    lst = locationStation;
                }
            }
            double battery = ReturnBattery(3, dl.CLocation, lst);

            //If the drone is not available or the drone will not be able to reach the station
            if (dl.Status != DroneStatuses.Available || dl.Battery < battery)
                throw new DroneCannotSend();

            return battery;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Longitude"></param>
        /// <param name="Lattitude"></param>
        private void CheckLegelLocation(double Longitude, double Lattitude)
        {
            if (90 < Lattitude || -90 > Lattitude || 180 < Longitude || -180 > Longitude)
                throw new LocationNotLegal(Longitude, Lattitude);
        }
    }
}