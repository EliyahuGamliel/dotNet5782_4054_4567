using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL
    {
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
        /// Checks if the drone can to relese from cahrge, if not returns error
        /// </summary>
        /// <param name="dl">Object of Drone</param>
        /// <returns>Nothing</returns>
        private void CheckDroneCannotRelese(DroneList dl)
        {
            if (dl.Status != DroneStatuses.Maintenance)
                throw new DroneCannotRelese();
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