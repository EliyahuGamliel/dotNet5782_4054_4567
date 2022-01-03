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
        public void AddDroneCharge(DroneCharge d) {
            List<DroneCharge> l = Read<DroneCharge>();
            ////////////////////////////////////////check for same id error
            l.Add(d);
            Write<DroneCharge>(l);
        }

        public void DeleteDroneCharge(DroneCharge dc) {
            List<DroneCharge> l = Read<DroneCharge>();
            var DroneId = typeof(DroneCharge).GetProperty("DroneId");

            //int index = l.FindIndex(x => (int)Id.GetValue(x, null) == (int)Id.GetValue(dc, null));
            int index = l.FindIndex(x => (int)DroneId.GetValue(x, null) == (int)DroneId.GetValue(dc, null) && x.Active);
            if (index != -1)
                l[index] = dc;

            l[index] = dc;
            Write<DroneCharge>(l);
        }

        public DroneCharge GetDroneChargeById(int Id) {
            List<DroneCharge> l = Read<DroneCharge>();
            return l.Find(l => l.DroneId == Id && l.Active);
        }

        public IEnumerable<DroneCharge> GetDroneChargeByFilter(Predicate<DroneCharge> droneChargeList) {
            List<DroneCharge> l = Read<DroneCharge>();
            return l.FindAll(droneChargeList);
        }
    }
}
