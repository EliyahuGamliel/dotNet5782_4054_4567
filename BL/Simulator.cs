using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BO;
using static BL.BL;

namespace BL
{
    class Simulator
    {
        int DELAY = 500;
        double SPEED = 100;
        Simulator(BL bl, int Id, Action<int> updateDrone, Func<bool> stop) {
            while (!stop()) {
                BO.Drone drone = bl.GetDroneById(Id);
                if (drone.Status == DroneStatuses.Delivery) {
                    BO.Parcel parcel = bl.GetParcelById(drone.PTransfer.Id);
                    if (parcel.PickedUp == null) {
                        bl.PickUpDroneParcel(Id);
                        updateDrone(Id);
                        Thread.Sleep(DELAY);
                    }
                    else {
                        bl.DeliverParcelCustomer(Id);
                        updateDrone(Id);
                        Thread.Sleep(DELAY);
                    }
                }
                else if (drone.Status == DroneStatuses.Available) {
                    try {
                        bl.AssignDroneParcel(Id);
                        updateDrone(Id);
                        Thread.Sleep(DELAY);
                    }
                    catch {
                        try {
                            bl.SendDrone(Id);
                            updateDrone(Id);
                            Thread.Sleep(DELAY);
                        }
                        catch {
                            Thread.Sleep(DELAY);
                        }
                    }
                }
                else {
                    if (drone.Battery == 100) {
                        bl.ReleasDrone(Id);
                        updateDrone(Id);
                        Thread.Sleep(DELAY);
                    }
                    else {
                        Thread.Sleep(DELAY);
                    }
                }
            }
        }
    }
}
