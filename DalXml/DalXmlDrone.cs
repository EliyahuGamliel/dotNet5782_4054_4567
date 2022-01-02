using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Xml.Linq;

namespace Dal
{
    partial class DalXml : IDal
    {
        public void AddDrone(Drone d) {
            throw new NotImplementedException();
        }

        public void UpdateDrone(Drone d) {
            throw new NotImplementedException();
        }

        public void DeleteDrone(Drone drone) {
            throw new NotImplementedException();
        }

        public Drone GetDroneById(int Id) {
            try {
                XElement droneRoot = XElement.Load(@"Drones.xml");
                return
                    (from d in droneRoot.Elements()
                     where Int32.Parse(d.Element("Id").Value) == Id
                     select new Drone() {
                         Id = Int32.Parse(d.Element("Id").Value),
                         Model = d.Element("Model").Value,
                         MaxWeight = (WeightCategories)Enum.Parse(typeof(WeightCategories), d.Element("MaxWeight").Value),
                         Active = bool.Parse(d.Element("Active").Value)
                     }).FirstOrDefault();
            }
            catch { throw new DO.IdNotExistException(Id); }
        }

        public IEnumerable<Drone> GetDroneByFilter(Predicate<Drone> droneList) {
            try {
                XElement droneRoot = XElement.Load(@"Data\Drones.xml");
                return
                    (from d in droneRoot.Elements()
                     select new Drone() {
                         Id = Int32.Parse(d.Element("Id").Value),
                         Model = d.Element("Model").Value,
                         MaxWeight = (WeightCategories)Enum.Parse(typeof(WeightCategories), d.Element("MaxWeight").Value),
                         Active = bool.Parse(d.Element("Active").Value)
                     }).ToList().FindAll(droneList);
            }
            catch { return null; }
        }

        public double[] DroneElectricityUse() {
            throw new NotImplementedException();
        }
    }
}
