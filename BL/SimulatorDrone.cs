﻿using System;
using System.Threading;
using BO;
using static System.Math;

namespace BL
{
    internal class SimulatorDrone
    {
        private int DELAY = 1000;
        private double SPEED = 1000;
        private double[] electric;
        public SimulatorDrone(BL bl, int Id, Action updateDrone, Func<bool> stop) {
            electric = bl.data.DroneElectricityUse();
            while (!stop()) {
                BO.Drone drone = bl.GetDroneById(Id);
                switch (drone.Status) {
                    case DroneStatuses.Delivery:
                        BO.Parcel parcel = bl.GetParcelById(drone.PTransfer.Id);
                        if (parcel.PickedUp == null) {
                            if (drone.PTransfer.TransportDistance > SPEED) {
                                Location l = CalculterLocation(drone.CLocation, drone.PTransfer.CollectionLocation, SPEED);
                                bl.UpdateDrone(Id, drone.Model, drone.Battery - electric[0] * SPEED, l);
                            }
                            else
                                bl.PickUpDroneParcel(Id);
                        }
                        else {
                            if (drone.PTransfer.TransportDistance > SPEED) {
                                Location l = CalculterLocation(drone.CLocation, drone.PTransfer.DestinationLocation, SPEED);
                                switch (drone.PTransfer.Weight) {
                                    case WeightCategories.Light:
                                        bl.UpdateDrone(Id, drone.Model, drone.Battery - electric[1] * SPEED, l);
                                        break;

                                    case WeightCategories.Medium:
                                        bl.UpdateDrone(Id, drone.Model, drone.Battery - electric[2] * SPEED, l);
                                        break;

                                    case WeightCategories.Heavy:
                                        bl.UpdateDrone(Id, drone.Model, drone.Battery - electric[3] * SPEED, l);
                                        break;
                                }
                            }
                            else
                                bl.DeliverParcelCustomer(Id);
                        }
                        break;

                    case DroneStatuses.Available:
                        try {
                            bl.AssignDroneParcel(Id);
                        }
                        catch {
                            try {
                                bl.SendDrone(Id);
                            }
                            catch {
                                Thread.Sleep(DELAY);
                            }
                        }
                        break;

                    case DroneStatuses.Maintenance:
                        if (drone.Battery >= 100) {
                            bl.ReleasDrone(Id);
                        }
                        else
                            bl.UpdateDrone(Id, drone.Model, drone.Battery + electric[4]);
                        break;
                }
                updateDrone();
                Thread.Sleep(DELAY);
            }
        }

        private Location CalculterLocation(Location from, Location to, double dis) {
            double radian = PI / 180;
            double degree = 180 / PI;
            double la1 = from.Lattitude.Value * radian;
            double lo1 = from.Longitude.Value * radian;
            double Ad = dis / 6376.5;
            Location l = new Location();
            var teth = Bearing(from, to);
            l.Lattitude = Asin(Sin(la1) * Cos(Ad) + Cos(la1) * Sin(Ad) * Cos(teth));
            l.Longitude = lo1 + Atan2(Sin(teth) * Sin(Ad) * Cos(la1), Cos(Ad) - Sin(la1) * Sin(l.Lattitude.Value));
            l.Lattitude *= degree;
            l.Longitude *= degree;
            l.Longitude = (l.Longitude + 540) % 360 - 180;
            return l;
        }

        private double Bearing(Location from, Location to) {
            double radian = PI / 180;
            double la1 = from.Lattitude.Value * radian;
            double lo1 = from.Longitude.Value * radian;
            double la2 = to.Lattitude.Value * radian;
            double lo2 = to.Longitude.Value * radian;
            double deltaLo = lo2 - lo1;
            double X = Cos(la2) * Sin(deltaLo);
            double Y = Cos(la1) * Sin(la2) - Sin(la1) * Cos(la2) * Cos(deltaLo);
            return Atan2(X, Y);
        }
    }
}

