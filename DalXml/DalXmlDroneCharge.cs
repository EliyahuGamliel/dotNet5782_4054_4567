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
            throw new NotImplementedException();
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
