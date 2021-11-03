using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        public void AddCustomer(Customer c){
            IDAL.DO.Customer cu = new IDAL.DO.Customer();
            cu.Id = c.Id;
            cu.Name = c.Name;
            cu.Phone = c.Phone;
            data.AddCustomer(cu);
        }
        public void UpdateCustomer(int id, string nameCustomer, string phoneCustomer) {
            
        }

        public string GetCustomerById(int Id) {
            return data.GetCustomerById(Id);
        }

        public IEnumerable<Customer> GetCustomers(){
            return (IEnumerable<Customer>)data.GetCustomers();
        }
    }
}