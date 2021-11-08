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
                IDAL.DO.Drone d = data.GetDroneById(id);
                d.Model = model;
                data.UpdateDrone(d);
                return "The update was successful\n";
            }
            catch (IDAL.DO.IdNotExistException)
            {
                throw new IdNotExistException(id);
            }
        }

        public void SendDrone(int idDrone) {
            DroneList d = dronesList.Find(dr => idDrone == dr.Id);
            return;
        }

        public void ReleasDrone(int id, double time){
            

            return;
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

                        CustomerInParcel cp = new CustomerInParcel();
                        cp.Id = itemParcels.TargetId;
                        IDAL.DO.Customer c_help = data.GetCustomerById(cp.Id); 
                        cp.Name = c_help.Name;
                        pt.Recipient = cp;
                        pt.Destination_Location.Lattitude = c_help.Lattitude;
                        pt.Destination_Location.Longitude = c_help.Longitude;

                        cp.Id = itemParcels.SenderId;
                        c_help = data.GetCustomerById(cp.Id); 
                        cp.Name = c_help.Name;
                        pt.Sender = cp;
                        pt.Collection_Location.Lattitude = c_help.Lattitude;
                        pt.Collection_Location.Longitude = c_help.Longitude;

                        pt.Transport_Distance = DistanceTo(pt.Collection_Location, pt.Destination_Location);
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
