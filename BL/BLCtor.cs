 using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {       
        /// <summary>
        /// BL constructor
        /// </summary>
        public BL()
        {
            data = new DalObject.DalObject();

            Avaliable = data.DroneElectricityUse()[0];
            WeightLight = data.DroneElectricityUse()[1];
            WeightMedium = data.DroneElectricityUse()[2];
            WeightHeavy = data.DroneElectricityUse()[3];
            ChargingRate = data.DroneElectricityUse()[4];

            IEnumerable<IDAL.DO.Drone> droneslist = data.GetDrones();
            
            foreach (var item in droneslist)
            {
                bool NotDel = true;
                double minbattery = 0;

                DroneList dl = new DroneList();
                dl.CLocation = new Location();
                dl.Id = item.Id;
                dl.MaxWeight = (WeightCategories)(int)item.MaxWeight;
                dl.Model = item.Model;

                IEnumerable<IDAL.DO.Parcel> parcelslist = data.GetParcels();
                foreach (var itemParcel in parcelslist)
                {
                    //If the parcel is associated with the drone and also the parcrel in the middle of the shipment
                    if (itemParcel.DroneId == dl.Id && (ReturnStatus(itemParcel) == 1 || ReturnStatus(itemParcel) == 2))
                    {
                        dl.Status = DroneStatuses.Delivery;
                        dl.ParcelId = itemParcel.Id;
                        //If the parcel was only associated
                        if (ReturnStatus(itemParcel) == 1)
                        {
                            Customer customer = GetCustomerById(itemParcel.SenderId);
                            dl.CLocation.Longitude = ReturnCloseStation(data.GetStations(), customer.Location).Longitude;
                            dl.CLocation.Lattitude = ReturnCloseStation(data.GetStations(), customer.Location).Lattitude;
                        }
                        //If the parcel was also collected
                        else
                        {
                            Customer customer = GetCustomerById(itemParcel.SenderId);
                            dl.CLocation = customer.Location;
                        }

                        //The customer target of the parcel
                        Customer customertar = GetCustomerById(itemParcel.TargetId);
                        Location tarloc = new Location();
                        tarloc = customertar.Location;

                        Location staloc = new Location();
                        staloc.Lattitude = ReturnCloseStation(data.GetStations(), tarloc).Lattitude;
                        staloc.Longitude = ReturnCloseStation(data.GetStations(), tarloc).Longitude;

                        //Minimum battery to finish the shipment
                        minbattery = ReturnBattery((int)itemParcel.Weight, dl.CLocation, tarloc) + ReturnBattery(3, dl.CLocation, staloc);
                        dl.Battery = rand.NextDouble() + rand.Next((int)minbattery + 1, 100);
                        if (dl.Battery > 100)
                            dl.Battery = 100;
                        //The drone is in delivery mode
                        NotDel = false;
                        break;
                    }
                }
                //If the drone is not in delivery mode (no parcel associated with it)
                if (NotDel)
                {
                    //The drone mode is randomized
                    dl.Status = (DroneStatuses)rand.Next(0, 2);
                    //If the situation that came out is: maintenance
                    if (dl.Status == DroneStatuses.Maintenance)
                    {
                        int counter = 0;
                        IEnumerable<IDAL.DO.Station> stationslist = data.GetStations();
                        foreach (var itemStation in stationslist)
                            counter += 1;
                        //The drone is at a random station
                        int st = rand.Next(0, counter);
                        foreach (var itemStation in stationslist)
                        {
                            if (st == 0)
                            {
                                dl.CLocation.Longitude = itemStation.Longitude;
                                dl.CLocation.Lattitude = itemStation.Lattitude;
                                UpdateStation(itemStation.Id, itemStation.Name, itemStation.ChargeSlots - 1);
                                break;
                            }
                            st -= 1;
                        }
                        dl.Battery = rand.NextDouble() + rand.Next(0, 20);
                    }
                    //If the situation that came out is: available
                    else
                    {
                        int counter = 0;
                        IEnumerable<CustomerList> customerslist = GetCustomers();
                        foreach (var itemCustomer in customerslist)
                            if (itemCustomer.ParcelsGet > 0)
                                counter += 1;
                        //The drone is at a random customer Location
                        int st = rand.Next(0, counter);
                        foreach (var itemCustomer in customerslist)
                        {
                            if (st == 0)
                            {
                                Customer c = GetCustomerById(itemCustomer.Id);
                                dl.CLocation = c.Location;
                                break;
                            }
                            //If the customer get least one parcel
                            if (itemCustomer.ParcelsGet > 0)
                                st -= 1;
                        }

                        Location lst = new Location();
                        lst.Lattitude = ReturnCloseStation(data.GetStations(), dl.CLocation).Lattitude;
                        lst.Longitude = ReturnCloseStation(data.GetStations(), dl.CLocation).Longitude;
                        minbattery = ReturnBattery(3, dl.CLocation, lst);
                        dl.Battery = rand.NextDouble() + rand.Next((int)minbattery + 1, 100);
                        if (dl.Battery > 100)
                            dl.Battery = 100;
                    }
                }
                dronesList.Add(dl);
            }
        }
    }
}