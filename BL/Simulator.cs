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
        double DELAY = 500;
        double SPEED = 100;
        Simulator(BL bl, int Id, Action<BO.ParcelTransfer> updateDrone, Func<bool> stop) {
            while (!stop()) {
                BO.Drone drone = bl.GetDroneById(Id);
                if (drone.Status == DroneStatuses.Delivery) {

                }
                else if (drone.Status == DroneStatuses.Available) {
                    try {
                        bl.AssignDroneParcel(Id);
                    }
                    catch {
                        try {
                            bl.SendDrone(Id);
                        }
                        catch {

                        }
                    }
                }
                else {

                }
            }
        }
    }
}
