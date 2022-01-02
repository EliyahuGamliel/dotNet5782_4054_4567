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
        XElement droneRoot;
        string dronesPath = @"Data\Drones.xml";
        public void AddDrone(Drone d) {
            try {
                XElement droneRoot = XElement.Load(dronesPath);
                droneRoot.Add(new XElement("drone",
                    new XElement("Active", d.Active),
                    new XElement("Id", d.Id),
                    new XElement("MaxWeight", d.MaxWeight),
                    new XElement("Model", d.Model)
                    ));
                droneRoot.Save(dronesPath);
            }
            catch { throw new IdExistException(d.Id); }
        }

        public void UpdateDrone(Drone d) {
            XElement droneRoot = XElement.Load(dronesPath);
            XElement e = (from drone in droneRoot.Elements()
                          where Int32.Parse(drone.Element("Id").Value) == d.Id
                          select drone).FirstOrDefault();
            e.Element("Model").Value = d.Model;
            droneRoot.Save(dronesPath);
        }

        public void DeleteDrone(Drone drone) {
            XElement droneRoot = XElement.Load(dronesPath);
            XElement e = (from d in droneRoot.Elements()
                          where Int32.Parse(d.Element("Id").Value) == drone.Id
                          select d).FirstOrDefault();
            e.Element("Active").Value = drone.Active.ToString();
            droneRoot.Save(dronesPath);
        }

        public Drone GetDroneById(int Id) {
            try {
                XElement droneRoot = XElement.Load(dronesPath);
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
            catch { throw new IdNotExistException(Id); }
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
            try {
                XElement configRoot = XElement.Load(@"xml\config.xml");
                return
                    (from c in configRoot.Elements()
                     select new double[5] {
                         double.Parse(c.Element("Avaliable").Value),
                         double.Parse(c.Element("WeightLight").Value),
                         double.Parse(c.Element("WeightMedium").Value),
                         double.Parse(c.Element("WeightHeavy").Value),
                         double.Parse(c.Element("ChargingRate").Value),
                     }).First().ToArray();
            }
            catch { return null; }
        }
    }
}
