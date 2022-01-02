﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;

namespace Dal
{
    partial class DalXml : IDal
    {
        public void AddDroneCharge(DroneCharge d) {
            List<DroneCharge> l = Read<DroneCharge>();
            ////////////////////////////////////////check for same id error
            l.Add(d);
            Write<DroneCharge>(l);
        }

        public void DeleteDroneCharge(DroneCharge dc) {
            List<DroneCharge> l = Read<DroneCharge>();
            l[Update<DroneCharge>(l, dc)] = dc;
            Write<DroneCharge>(l);
        }

        public DroneCharge GetDroneChargeById(int Id) {
            List<DroneCharge> l = Read<DroneCharge>();
            return l.Find(l => l.DroneId == Id);
        }

        public IEnumerable<DroneCharge> GetDroneChargeByFilter(Predicate<DroneCharge> droneChargeList) {
            List<DroneCharge> l = Read<DroneCharge>();
            return l.FindAll(droneChargeList);
        }
    }
}
