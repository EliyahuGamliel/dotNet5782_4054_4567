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
        public void AddParcel(Parcel p) {
            List<Parcel> l = Read<Parcel>();
            ////////////////////////////////////////check for same id error
            l.Add(p);
            Write<Parcel>(l);
        }

        public void UpdateParcel(Parcel p) {
            List<Parcel> l = Read<Parcel>();
            l[Update<Parcel>(l, p)] = p;
            Write<Parcel>(l);
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
