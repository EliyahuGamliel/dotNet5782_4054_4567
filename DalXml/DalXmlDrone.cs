using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Xml.Linq;
using System.Runtime.CompilerServices;

namespace Dal
{
    partial class DalXml : IDal
    {
        XElement droneRoot;
        string dronesPath = @"Data\Drones.xml";

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone d) {
            try {
                droneRoot = XElement.Load(dronesPath); }
            catch { droneRoot = new XElement("drone"); }

            try {
                GetDroneById(d.Id);
                throw new IdExistException(d.Id);
            }
            catch {
                droneRoot.Add(new XElement("drone",
                new XElement("Active", d.Active),
                new XElement("Id", d.Id),
                new XElement("MaxWeight", d.MaxWeight),
                new XElement("Model", d.Model)
                ));
                droneRoot.Save(dronesPath);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(Drone d) {
            XElement droneRoot = XElement.Load(dronesPath);
            XElement e = (from drone in droneRoot.Elements()
                          where Int32.Parse(drone.Element("Id").Value) == d.Id
                          select drone).FirstOrDefault();
            e.Element("Model").Value = d.Model;
            droneRoot.Save(dronesPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(Drone drone) {
            XElement droneRoot = XElement.Load(dronesPath);
            XElement e = (from d in droneRoot.Elements()
                          where Int32.Parse(d.Element("Id").Value) == drone.Id
                          select d).FirstOrDefault();
            e.Element("Active").Value = drone.Active.ToString();
            droneRoot.Save(dronesPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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
            catch { return new List<Drone>(); }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] DroneElectricityUse() {
            try {
                XElement configRoot = XElement.Load(@"xml/config.xml");
                return new double[5] {
                         double.Parse(configRoot.Element("Avaliable").Value),
                         double.Parse(configRoot.Element("WeightLight").Value),
                         double.Parse(configRoot.Element("WeightMedium").Value),
                         double.Parse(configRoot.Element("WeightHeavy").Value),
                         double.Parse(configRoot.Element("ChargingRate").Value),
                     };
            }
            catch { return null; }
        }
    }
}
