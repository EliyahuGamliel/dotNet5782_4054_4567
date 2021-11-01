using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {   
        public int AddParcel(Parcel p){
            IDAL.DO.Parcel pa = new IDAL.DO.Parcel{}
            data.AddParcel(pa);
        }
        public IEnumerable<Parcel> PrintListParcel(){

        }
    }
}