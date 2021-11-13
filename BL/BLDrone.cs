using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        Random rand = new Random();
        
        public string AddDrone(DroneList d, int idStation) {
            try
            {
                IDAL.DO.Station s = data.GetStationById(idStation);
                d.Battery = rand.Next(20,41);
                d.Status = DroneStatuses.Maintenance;
                d.CLocation.Lattitude = s.Lattitude;
                d.CLocation.Longitude = s.Longitude;
                d.ParcelId = 0;
                IDAL.DO.Drone dr = new IDAL.DO.Drone();
                dr.Id = d.Id;
                dr.Model = d.Model;
                dr.MaxWeight = (IDAL.DO.WeightCategories)((int)d.MaxWeight);
                data.AddDrone(dr);
                dronesList.Add(d);
                return "The addition was successful\n";
            }
            catch (IDAL.DO.IdExistException)
            {
                throw new IdExistException(d.Id);
            }
        }

        public string UpdateDrone(int id, string model) {
            try
            {
                IDAL.DO.Drone dr = data.GetDroneById(id);
                dr.Model = model;
                data.UpdateDrone(dr);
                DroneList d = dronesList.Find(dro => dro.Id == id);
                int index = dronesList.IndexOf(d);
                d.Model = model;
                dronesList[index] = d;
                return "The update was successful\n";
            }
            catch (IDAL.DO.IdNotExistException)
            {
                throw new IdNotExistException(id);
            }
        }

        public String SendDrone(int idDrone) {
            try
            {
                CheckExistId(dronesList, idDrone);
                DroneList d = dronesList.Find(dr => idDrone == dr.Id);
                int index = dronesList.IndexOf(d);
                double battery = CheckDroneCannotSend(data.GetStations(), d);
                d.Status = DroneStatuses.Maintenance;
                IDAL.DO.Station st = new IDAL.DO.Station();
                st = ReturnCloseStation(data.GetStations(), d.CLocation);
                d.CLocation.Lattitude = st.Lattitude;
                d.CLocation.Longitude = st.Longitude;
                d.Battery = d.Battery - battery;
                dronesList[index] = d;
                UpdateStation(st.Id, -1, st.ChargeSlots - 1);
                IDAL.DO.DroneCharge dc = new IDAL.DO.DroneCharge();
                dc.DroneId = d.Id;
                dc.StationId = st.Id;
                data.AddDroneCharge(dc);
                return "The update was successful\n";
            }
            catch (IDAL.DO.IdExistException)
            {
                throw new IdNotExistException(idDrone);
            }
        }

        public String ReleasDrone(int idDrone, double time){
            try
            {
                CheckNotExistId(dronesList, idDrone);
                DroneList d = dronesList.Find(dr => idDrone == dr.Id);
                int index = dronesList.IndexOf(d);
                //CheckDroneCannotRelese(data.GetStations(), d);
                d.Status = DroneStatuses.Available;
                d.Battery = d.Battery + time * ChargingRate;
                if (d.Battery > 100)
                    d.Battery = 100;
                dronesList[index] = d;

                IDAL.DO.Station st = new IDAL.DO.Station();
                st = ReturnCloseStation(data.GetStations(), d.CLocation);
                UpdateStation(st.Id, -1, st.ChargeSlots + 1);

                IDAL.DO.DroneCharge dc = new IDAL.DO.DroneCharge();
                dc.DroneId = d.Id;
                dc.StationId = st.Id;
                data.DeleteDroneCharge(dc);
                return "The update was successful\n";
            }
            catch (IDAL.DO.IdNotExistException)
            {
                throw new IdNotExistException(idDrone);
            }
        }

        public Drone GetDroneById(int Id) {
            try
            {
                IDAL.DO.Drone d = data.GetDroneById(Id);
                Drone dr = new Drone();
                dr.Id = d.Id;
                dr.MaxWeight = (WeightCategories)(int)d.MaxWeight;
                dr.Model = d.Model;
                DroneList dl = dronesList.Find(drl => drl.Id == dr.Id);
                dr.Status = dl.Status;
                dr.Battery = dl.Battery;
                dr.CLocation = dl.CLocation;
                ParcelTransfer pt = new ParcelTransfer();
                foreach (var itemParcels in data.GetParcels()) {
                    if (itemParcels.Id == dl.ParcelId) {
                        pt.Id = itemParcels.Id;
                        pt.Priority = (Priorities)(int)itemParcels.Priority;
                            pt.Weight = (WeightCategories)(int)itemParcels.Weight;
                        pt.Status = false;
                        if (DateTime.Compare(itemParcels.PickedUp, itemParcels.Scheduled) > 0)
                            pt.Status = true;

                        CustomerInParcel cp1 = new CustomerInParcel();
                        cp1.Id = itemParcels.TargetId;
                        IDAL.DO.Customer c_help = data.GetCustomerById(cp1.Id); 
                        cp1.Name = c_help.Name;
                        pt.Recipient = cp1;
                        pt.Destination_Location.Lattitude = c_help.Lattitude;
                        pt.Destination_Location.Longitude = c_help.Longitude;

                        CustomerInParcel cp2 = new CustomerInParcel();
                        cp2.Id = itemParcels.SenderId;
                        c_help = data.GetCustomerById(cp2.Id); 
                        cp2.Name = c_help.Name;
                        pt.Sender = cp2;
                        pt.Collection_Location.Lattitude = c_help.Lattitude;
                        pt.Collection_Location.Longitude = c_help.Longitude;

                        pt.Transport_Distance = DistanceTo(pt.Collection_Location, pt.Destination_Location);

                        dr.PTransfer = pt;
                    }
                }
                return dr;
            }
            catch (IDAL.DO.IdNotExistException)
            {
                throw new IdNotExistException(Id);
            }
            
        }
        
        public IEnumerable<DroneList> GetDrones(){
            return dronesList;
        }
        public double[] DroneElectricityUse(){
            return data.DroneElectricityUse();
        }
    }
}
