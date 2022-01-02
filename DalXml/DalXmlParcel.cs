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
        public int AddParcel(Parcel p) {
            var rootParcel = XElement.Load("Parcel.xml");
            rootParcel.Element("Parcels").Add(new XElement("Name", "Eliyahu"));
            return 0;
        }

        public void UpdateParcel(Parcel p) {
            throw new NotImplementedException();
        }

        public void DeleteParcel(Parcel parcel) {
            throw new NotImplementedException();
        }

        public Parcel GetParcelById(int Id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetParcelByFilter(Predicate<Parcel> parcelList) {
            throw new NotImplementedException();
        }
    }
}
