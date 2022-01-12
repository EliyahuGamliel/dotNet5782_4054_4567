using System;
using System.Linq;
using System.Collections.Generic;
using BO;
using BlApi;
using DO;
using DalApi;
using System.Device.Location;

namespace BL
{
    public partial class BL
    {
        /// <summary>
        /// According to calculations, returns the status of the parcel
        /// </summary>
        /// <param name="p">Object of parcel</param>
        /// <returns>int of Status of parcel</returns>
        private int ReturnStatus(DO.Parcel chosenp) {
            if (chosenp.Scheduled == null)
                return (int)Statuses.Created;
            else if (chosenp.PickedUp == null)
                return (int)Statuses.Associated;
            else if (chosenp.Delivered == null)
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
        private double ReturnBattery(int weight, Location l1, Location l2) {
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
        private BO.Station ReturnCloseStation(IEnumerable<DO.Station> s, Location drone) {
            if (s.Count() == 0)
                throw new DroneCannotAssigan();
            DO.Station st = s.OrderBy(sta => DistanceTo(ReturnLocation(sta), drone)).First();
            return GetStationById(st.Id);
        }

        /// <summary>
        /// returns the location of the station
        /// </summary>
        /// <param name="s">the station that we want its location</param>
        /// <returns></returns>
        private Location ReturnLocation(DO.Station s) {
            Location location = new Location();
            location.Lattitude = s.Lattitude;
            location.Longitude = s.Longitude;
            return location;
        }

        /// <summary>
        /// Calculates the distance between two locations and returns it
        /// </summary>
        /// <param name="l1">Object Location 1</param>
        /// <param name="l2">Object Location 2</param>
        /// <returns>Returns the distance between two locations</returns>
        private double DistanceTo(Location l1, Location l2) {
            var coord1 = new GeoCoordinate(l1.Lattitude.Value, l1.Longitude.Value);
            var coord2 = new GeoCoordinate(l2.Lattitude.Value, l2.Longitude.Value);
            return Math.Round((coord1.GetDistanceTo(coord2) / 1000), 3);
            /*
            if (l1.Lattitude == l2.Lattitude && l1.Longitude == l2.Longitude) {
                return 0;
            }
            double rlat1 = Math.PI * l1.Lattitude.Value / 180;
            double rlat2 = Math.PI * l2.Lattitude.Value / 180;
            double theta = l1.Longitude.Value - l2.Longitude.Value;
            double rtheta = Math.PI * theta / 180;
            double dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            return Math.Round(dist * 1.609344, 2);
            */
        }

        /// <summary>
        /// Checks how much slots are taken
        /// </summary>
        /// <param name="idStation">id of station</param>
        /// <returns>the number of taken slots</returns>
        private int ChargeSlotsCatched(int idStation) {
            return data.GetDroneChargeByFilter(dc => dc.StationId == idStation && dc.Active).Count();
        }

        /// <summary>
        /// Checks if ״id״ does not exist, if not returns error
        /// </summary>
        /// <param name="list">List of T-objects</param>
        /// <param name="id">The id for check</param>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <returns>Nothing</returns>
        private void CheckNotExistId<T>(IEnumerable<T> list, int id) {
            foreach (var item in list) {
                int idobject = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                if (idobject == id)
                    return;
            }
            throw new BO.IdNotExistException(id);
        }

        /// <summary>
        /// Checks if the id is valid, if not it throws an error
        /// </summary>
        /// <param name="id">The id that we want to check</param>
        private void CheckValidId(int id) {
            if (id <= 0)
                throw new IdNotValid(id);
        }

        /// <summary>
        /// All the choice need to be between 0-2
        /// </summary>
        /// <param name="choice">the choice</param>
        private void CheckLegelChoice(int choice) {
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
        private double CheckDroneCannotSend(IEnumerable<DO.Station> list, DroneList dl) {
            if (list.Count() == 0)
                throw new DroneCannotSend();

            Location lst = new Location();
            Location locationStation = new Location();
            bool cntsn = false;
            foreach (var item in list) {
                locationStation.Lattitude = item.Lattitude;
                locationStation.Longitude = item.Longitude;
                if ((!cntsn || DistanceTo(locationStation, dl.CLocation) < DistanceTo(lst, dl.CLocation))) {
                    cntsn = true;
                    lst.Lattitude = locationStation.Lattitude;
                    lst.Longitude = locationStation.Longitude;
                }
            }
            double battery = ReturnBattery(3, dl.CLocation, lst);

            //If the drone is not available or the drone will not be able to reach the station
            if (dl.Status != DroneStatuses.Available || dl.Battery < battery)
                throw new DroneCannotSend();

            return battery;
        }

        /// <summary>
        /// Checks if the location is legal
        /// </summary>
        /// <param name="Longitude">The longitude of the location</param>
        /// <param name="Lattitude">The lattitude of the location</param>
        private void CheckLegelLocation(double Longitude, double Lattitude) {
            if (90 < Lattitude || -90 > Lattitude || 180 < Longitude || -180 > Longitude)
                throw new LocationNotLegal(Longitude, Lattitude);
        }

        private void CheckDeleteDrone(BO.DroneList drone) {
            if (drone.Status != DroneStatuses.Available)
                throw new CanntDeleteDrone(drone.Id);
        }

        private void CheckDeleteCustomer(int customerID) {
            BO.Customer customer = GetCustomerById(customerID);
            foreach (var item in customer.ForCustomer) {
                if (item.Status != Statuses.Provided)
                    throw new CanntDeleteCustomer(customerID);
            }
        }
    }
}