using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DalApi;
using DO;

namespace Dal
{
    internal partial class DalXml : IDal
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(DroneCharge dc) {
            List<DroneCharge> droneCharges = Read<DroneCharge>();
            droneCharges.Add(dc);
            Write<DroneCharge>(droneCharges);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDroneCharge(DroneCharge dc) {
            List<DroneCharge> droneCharges = Read<DroneCharge>();
            var DroneId = typeof(DroneCharge).GetProperty("DroneId");
            CheckNotExistId(dc.DroneId);
            int index = droneCharges.FindIndex(x => (int)DroneId.GetValue(x, null) == (int)DroneId.GetValue(dc, null) && x.Active);
            if (index != -1)
                droneCharges[index] = dc;
            //droneCharges[index] = dc;
            Write<DroneCharge>(droneCharges);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge GetDroneChargeById(int Id) {
            List<DroneCharge> droneCharges = Read<DroneCharge>();
            CheckNotExistId(Id);
            return droneCharges.Find(dc => dc.DroneId == Id && dc.Active);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDroneChargeByFilter(Predicate<DroneCharge> droneChargeList) {
            List<DroneCharge> droneCharges = Read<DroneCharge>();
            return droneCharges.FindAll(droneChargeList);
        }

        private void CheckNotExistId(int dcid) {
            List<DroneCharge> ldc = Read<DroneCharge>();
            foreach (var item in ldc) {
                int idobject = (int)(typeof(DroneCharge).GetProperty("DroneId").GetValue(item, null));
                bool active = (bool)(typeof(DroneCharge).GetProperty("Active").GetValue(item, null));
                if (idobject == dcid && active == true)
                    return;
            }
            throw new IdNotExistException(dcid);
        }

    }
}
