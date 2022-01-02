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

        public void DeleteDroneCharge(DroneCharge droneCharge) {
            throw new NotImplementedException();
        }

        public DroneCharge GetDroneChargeById(int Id) {
            throw new NotImplementedException();
        }

        public IEnumerable<DroneCharge> GetDroneChargeByFilter(Predicate<DroneCharge> droneChargeList) {
            throw new NotImplementedException();
        }
    }
}
