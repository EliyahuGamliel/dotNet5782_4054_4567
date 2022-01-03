using System;
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
        public void AddDroneCharge(DroneCharge dc) {
            List<DroneCharge> ldc = Read<DroneCharge>();
            ldc.Add(dc);
            Write<DroneCharge>(ldc);
        }

        public void DeleteDroneCharge(DroneCharge dc) {
            List<DroneCharge> l = Read<DroneCharge>();
            var DroneId = typeof(DroneCharge).GetProperty("DroneId");

            CheckNotExistId(dc.DroneId);
            int index = l.FindIndex(x => (int)DroneId.GetValue(x, null) == (int)DroneId.GetValue(dc, null) && x.Active);
            if (index != -1)
                l[index] = dc;

            l[index] = dc;
            Write<DroneCharge>(l);
        }

        public DroneCharge GetDroneChargeById(int Id) {
            List<DroneCharge> ldc = Read<DroneCharge>();
            CheckNotExistId(Id);
            return ldc.Find(ldc => ldc.DroneId == Id && ldc.Active);
        }

        public IEnumerable<DroneCharge> GetDroneChargeByFilter(Predicate<DroneCharge> droneChargeList) {
            List<DroneCharge> ldc = Read<DroneCharge>();
            return ldc.FindAll(droneChargeList);
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
