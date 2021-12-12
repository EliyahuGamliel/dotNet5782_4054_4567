using System;
using System.Linq;
using System.Collections.Generic;
using BO;
using BlApi;
using DalApi;
using DO;

namespace BL
{
    public partial class BL : IBL
    {       
        /// <summary>
        /// BL constructor
        /// </summary>
        public BL()
        {
            data = DalApi.DalFactory.GetDal();
            Avaliable = data.DroneElectricityUse()[0];
            WeightLight = data.DroneElectricityUse()[1];
            WeightMedium = data.DroneElectricityUse()[2];
            WeightHeavy = data.DroneElectricityUse()[3];
            ChargingRate = data.DroneElectricityUse()[4];

            IEnumerable<DO.Drone> droneslist = data.GetDroneByFilter(d => true);
            
            foreach (var item in droneslist)
            {
                bool NotDel = true;
                double minbattery = 0;

                DroneList dl = new DroneList();
                dl.CLocation = new Location();
                dl.Id = item.Id;
                dl.MaxWeight = (BO.WeightCategories)(int)item.MaxWeight;
                dl.Model = item.Model;

                IEnumerable<DO.Parcel> parcelslist = data.GetParcelByFilter(p => true);
                foreach (var itemParcel in parcelslist)
                {
                    //If the parcel is associated with the drone and also the parcrel in the middle of the shipment
                    if (itemParcel.DroneId == dl.Id && itemParcel.Delivered == null)
                    {
                        dl.Status = DroneStatuses.Delivery;
                        dl.ParcelId = itemParcel.Id;
                        //If the parcel was only associated
                        if (itemParcel.PickedUp == null)
                        {
                            BO.Customer customer = GetCustomerById(itemParcel.SenderId);
                            dl.CLocation = ReturnCloseStation(data.GetStationByFilter(s => true), customer.Location).Location;
                        }
                        //If the parcel was also collected
                        else
                        {
                            BO.Customer customer = GetCustomerById(itemParcel.SenderId);
                            dl.CLocation = customer.Location;
                        }

                        //The customer target of the parcel
                        BO.Customer customertar = GetCustomerById(itemParcel.TargetId);
                        Location tarloc = new Location();
                        tarloc = customertar.Location;

                        Location staloc = new Location();
                        staloc = ReturnCloseStation(data.GetStationByFilter(s => true), tarloc).Location;

                        dl.Battery = new double();
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
                        IEnumerable<DO.Station> stationslist = data.GetStationByFilter(sta => sta.ChargeSlots > 0);
                        int counter = stationslist.Count();
                        //The drone is at a random station
                        int stIndex = rand.Next(0, counter);
                        DO.Station st = stationslist.ElementAt(stIndex);
                        dl.CLocation = GetStationById(st.Id).Location;

                        DO.DroneCharge droneCharge = new DO.DroneCharge();
                        droneCharge.DroneId = dl.Id;
                        droneCharge.StationId = st.Id;
                        data.AddDroneCharge(droneCharge);

                        UpdateStation(st.Id, "", st.ChargeSlots - 1 + ChargeSlotsCatched(st.Id));
                        dl.Battery = rand.NextDouble() + rand.Next(0, 20);
                    }
                    //If the situation that came out is: available
                    else
                    {
                        IEnumerable<CustomerList> customerslist = GetCustomers().Where(cus => cus.ParcelsGet > 0);
                        int counter = customerslist.Count();
                        //The drone is at a random customer Location
                        int cuIndex = rand.Next(0, counter);
                        CustomerList cu = customerslist.ElementAt(cuIndex);
                        BO.Customer c = GetCustomerById(cu.Id);
                        dl.CLocation = c.Location;

                        Location lst = new Location();
                        lst = ReturnCloseStation(data.GetStationByFilter(s => true), dl.CLocation).Location;
                        minbattery = ReturnBattery(3, dl.CLocation, lst);
                        dl.Battery = rand.NextDouble() + rand.Next((int)minbattery + 1, 100);
                    }
                }
                dronesList.Add(dl);
            }
        }
    }
}