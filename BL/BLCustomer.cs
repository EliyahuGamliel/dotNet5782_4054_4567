using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        public void AddCustomer(Customer c){

        }
        public IEnumerable<IDAL.DO.Customer> PrintListCustomer(){
            return data.GetCustomers();
        }
    }
}