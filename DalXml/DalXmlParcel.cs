using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal partial class DalXml : IDal
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int AddParcel(Parcel p) {
            List<Parcel> parcels = Read<Parcel>();
            CheckExistId<Parcel>(parcels, p.Id);
            XElement configRoot = XElement.Load(@"xml\config.xml");
            int numberID = Int32.Parse(configRoot.Element("NumberID").Value);
            p.Id = numberID;
            parcels.Add(p);
            Write<Parcel>(parcels);
            configRoot.Element("NumberID").SetValue(numberID + 1);
            configRoot.Save(@"xml\config.xml");
            return numberID;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(Parcel p) {
            List<Parcel> parcels = Read<Parcel>();
            CheckNotExistId<Parcel>(parcels, p.Id);
            parcels[Update<Parcel>(parcels, p)] = p;
            Write<Parcel>(parcels);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(Parcel p) {
            List<Parcel> parcels = Read<Parcel>();
            CheckNotExistId<Parcel>(parcels, p.Id);
            parcels[Update<Parcel>(parcels, p)] = p;
            Write<Parcel>(parcels);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcelById(int Id) {
            List<Parcel> parcels = Read<Parcel>();
            CheckNotExistId<Parcel>(parcels, Id);
            return parcels.Find(pa => pa.Id == Id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcelByFilter(Predicate<Parcel> parcelList) {
            List<Parcel> parcels = Read<Parcel>();
            return parcels.FindAll(parcelList);
        }
    }
}
