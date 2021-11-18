using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    /// <summary>
    /// Class partial DalObject - All functions running on the list of parcels
    /// </summary>
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// If everything is fine, add a parcel to the list of parcels
        /// </summary>
        /// <param name="p">Object of parcel to add</param>
        public int AddParcel(Parcel p) {
            CheckExistId(DataSource.Parcels, p.Id);
            CheckNotExistId(DataSource.Customers, p.SenderId);
            CheckNotExistId(DataSource.Customers, p.TargetId);
            p.Id = DataSource.Config.NumberID;
            DataSource.Parcels.Add(p);
            DataSource.Config.NumberID += 1;
            return DataSource.Config.NumberID;
        }

        /// <summary>
        /// If all is fine, update the parcel in a list of parcels
        /// </summary>
        /// <param name="p">Object of parcel to update</param>
        public void UpdateParcel(Parcel p){
            CheckNotExistId(DataSource.Parcels, p.Id);
            Parcel pa = DataSource.Parcels.Find(par => par.Id == p.Id);
            int index = DataSource.Parcels.IndexOf(pa);
            DataSource.Parcels[index] = p;
        }

        /// <summary>
        /// If all is fine, return a parcel object by id
        /// </summary>
        /// <param name="Id">The id of the requested parcel</param>
        /// <returns>The object of the requested parcel</returns>
        public Parcel GetParcelById(int Id) {
            CheckNotExistId(DataSource.Parcels, Id);
            Parcel p = DataSource.Parcels.Find(pa => Id == pa.Id);
            return p;
        }

        /// <summary>
        /// Returns the list of parcela
        /// </summary>
        /// <returns>Returns the list of parcels</returns>
        public IEnumerable<Parcel> GetParcels() {
            return DataSource.Parcels;
        }

        /// <summary>
        /// Returns a list of all unassigned parcels
        /// </summary>
        /// <returns>Returns a list of all unassigned parcels</returns>
        public IEnumerable<Parcel> GetParcelDrone() {
            return DataSource.Parcels.FindAll(pa => 0 == pa.DroneId);
        }   
    }
}