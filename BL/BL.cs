using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        IDAL.IDal data;
        List<DroneList> dronesList = new List<DroneList>();
        double Avaliable;
        double WeightLight;
        double WeightMedium;
        double WeightHeavy;
        double ChargingRate;
        public BL() {
            data = new DalObject.DalObject();
            double[] arr = new double[5]; 
            arr = data.DroneElectricityUse();
            Avaliable = arr[0];
            WeightLight = arr[1];
            WeightMedium = arr[2];
            WeightHeavy = arr[3];
            ChargingRate = arr[4];
            IEnumerable<IDAL.DO.Drone> list_d = data.GetDrones();
            foreach (var item in list_d)
            {
                DroneList dl = new DroneList();
                dl.Id = item.Id;
                dl.MaxWeight = (WeightCategories)(int)item.MaxWeight;
                dl.Model = item.Model;
                if (true)
                {
                    
                }
                else {
                    dl.Status = (DroneStatuses)rand.Next(0,2);

                    if (dl.Status == DroneStatuses.Maintenance)
                    {
                        int counter = 0;
                        IEnumerable<IDAL.DO.Station> list_s = data.GetStations();
                        foreach (var itemStation in list_s)
                            counter += 1;
                        int s = rand.Next(0, counter);
                        foreach (var itemStation in list_s)
                        {
                            if (s == 0) {
                                dl.CLocation.Longitude = itemStation.Longitude;
                                dl.CLocation.Lattitude = itemStation.Lattitude;
                                UpdateStation(itemStation.Id, itemStation.Name, itemStation.ChargeSlots - 1);
                                break;
                            }
                            s -= 1;
                        }
                    }
                    else {

                    }
                }
            }
        }
        
        public void AssignDroneParcel(int DroneId){

        }
        public void PickUpDroneParcel(int id){

        }
        public void DeliverParcelCustomer(int id){

        }

        public int ReturnStatus(IDAL.DO.Parcel p){
            if (DateTime.Compare(p.Requested, p.Scheduled) > 0)
                return (int)Statuses.Created;
            else if (DateTime.Compare(p.Scheduled, p.PickedUp) > 0)
                return (int)Statuses.Associated;
            else if (DateTime.Compare(p.PickedUp, p.Delivered) > 0)
                return (int)Statuses.Collected;
            return (int)Statuses.Provided;
        }

        public double DistanceTo(Location l1, Location l2) {
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

        public void CheckExistId <T>(IEnumerable<T> list, int id)
        {
            foreach (var item in list) {
                int id_object = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                if (id_object == id)
                    throw new IdExistException(id);   
            }
        }

        public void CheckNotExistId <T>(IEnumerable<T> list, int id)
        {
            foreach (var item in list) {
                int id_object = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                if (id_object == id)
                    return;          
            }
            throw new IdNotExistException(id);   
        }

        public void CheckNotExistPhone <T>(IEnumerable<T> list, string phone)
        {
            foreach (var item in list) {
                string phone_object = (string)(typeof(T).GetProperty("Phone").GetValue(item, null));
                if (phone_object == phone)
                    throw new PhoneExistException(phone);         
            }
        }
    }
}
