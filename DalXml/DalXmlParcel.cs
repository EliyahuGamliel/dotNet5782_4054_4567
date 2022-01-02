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
        public int AddParcel(Parcel p) {
            List<Parcel> l = Read<Parcel>();
            ////////////////////////////////////////check for same id error
            l.Add(p);
            Write<Parcel>(l);
            XElement configRoot = XElement.Load(@"xml\config.xml");
            int numberID = Int32.Parse(configRoot.Element("NumberID").Value);
            configRoot.Element("NumberID").SetValue(numberID + 1);
            configRoot.Save(@"xml\config.xml");
            return numberID;
        }

        public void UpdateParcel(Parcel p) {
            List<Parcel> l = Read<Parcel>();
            l[Update<Parcel>(l, p)] = p;
            Write<Parcel>(l);
        }

        public void DeleteParcel(Parcel p) {
            List<Parcel> l = Read<Parcel>();
            l[Update<Parcel>(l, p)] = p;
            Write<Parcel>(l);
        }

        public Parcel GetParcelById(int Id) {
            List<Parcel> l = Read<Parcel>();
            return l.Find(l => l.Id == Id);
        }

        public IEnumerable<Parcel> GetParcelByFilter(Predicate<Parcel> parcelList) {
            List<Parcel> l = Read<Parcel>();
            return l.FindAll(parcelList);
        }
    }
}
