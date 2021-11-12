using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        
        public int AddParcel(Parcel p) {
            CheckExistId(DataSource.parcels, p.Id);
            CheckNotExistId(DataSource.customers, p.SenderId);
            CheckNotExistId(DataSource.customers, p.TargetId);
            DataSource.Config.Number_ID += 1;
            DataSource.parcels.Add(p);
            return DataSource.Config.Number_ID;
        }

        public void UpdateParcel(Parcel p){
            Parcel pa = DataSource.parcels.Find(par => par.Id == p.Id);
            int index = DataSource.parcels.IndexOf(pa);
            DataSource.parcels[index] = p;
        }

        
        public Parcel GetParcelById(int Id) {
            CheckNotExistId(DataSource.parcels, Id);
            Parcel p = DataSource.parcels.Find(pa => Id == pa.Id);
            return p;
        }

        public IEnumerable<Parcel> GetParcels() {
            return DataSource.parcels;
        }

        public IEnumerable<Parcel> GetParcelDrone() {
            return DataSource.parcels.FindAll(pa => 0 == pa.DroneId);
        }
        
    }
}