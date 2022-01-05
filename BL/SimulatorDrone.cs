using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BO;
using static BL.BL;

namespace BL
{
    class SimulatorDrone
    {
        int DELAY = 1000;
        double SPEED = 100;

        public SimulatorDrone(BL bl, int Id, Action updateDrone, Func<bool> stop) {
            double ChargingRate = bl.data.DroneElectricityUse()[4];
            while (!stop()) {
                BO.Drone drone = bl.GetDroneById(Id);
                switch (drone.Status) {
                    case DroneStatuses.Delivery:
                        BO.Parcel parcel = bl.GetParcelById(drone.PTransfer.Id);
                        if (parcel.PickedUp == null) {
                            bl.PickUpDroneParcel(Id);
                            updateDrone();
                            Thread.Sleep(DELAY);
                        }
                        else {
                            bl.DeliverParcelCustomer(Id);
                            updateDrone();
                            Thread.Sleep(DELAY);
                        }
                        break;

                    case DroneStatuses.Available:
                        try {
                            bl.AssignDroneParcel(Id);
                            updateDrone();
                            Thread.Sleep(DELAY);
                        }
                        catch {
                            try {
                                bl.SendDrone(Id);
                                updateDrone();
                                Thread.Sleep(DELAY);
                            }
                            catch {
                                Thread.Sleep(DELAY);
                            }
                        }
                        break;

                    case DroneStatuses.Maintenance:
                        Thread.Sleep(100000);
                        updateDrone();
                        bl.ReleasDrone(Id);
                        Thread.Sleep(DELAY);
                        break;
                }
            }
        }
    }
}

