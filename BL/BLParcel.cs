using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {   
        public int AddParcel(Parcel p){
            IDAL.DO.Parcel pa = new IDAL.DO.Parcel();
            return data.AddParcel(pa);;
        }
        public IEnumerable<IDAL.DO.Parcel> GetParcels(){
            return data.GetParcels();
        }
    }
} 