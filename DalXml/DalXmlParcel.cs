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
            List<Parcel> lp = Read<Parcel>();
            CheckExistId<Parcel>(lp, p.Id);
            XElement configRoot = XElement.Load(@"xml\config.xml");
            int numberID = Int32.Parse(configRoot.Element("NumberID").Value);
            p.Id = numberID;
            lp.Add(p);
            Write<Parcel>(lp);
            configRoot.Element("NumberID").SetValue(numberID + 1);
            configRoot.Save(@"xml\config.xml");
            return numberID;
        }

        public void UpdateParcel(Parcel p) {
            List<Parcel> lp = Read<Parcel>();
            CheckNotExistId<Parcel>(lp, p.Id);

            lp[Update<Parcel>(lp, p)] = p;
            Write<Parcel>(lp);
        }

        public void DeleteParcel(Parcel p) {
            List<Parcel> lp = Read<Parcel>();
            CheckNotExistId<Parcel>(lp, p.Id);
            lp[Update<Parcel>(lp, p)] = p;
            Write<Parcel>(lp);
        }

        public Parcel GetParcelById(int Id) {
            List<Parcel> lp = Read<Parcel>();
            CheckNotExistId<Parcel>(lp, Id);
            return lp.Find(lp => lp.Id == Id);
        }

        public IEnumerable<Parcel> GetParcelByFilter(Predicate<Parcel> parcelList) {
            List<Parcel> l = Read<Parcel>();
            return l.FindAll(parcelList);
        }
    }
}
