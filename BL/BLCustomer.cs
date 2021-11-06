using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        public string AddCustomer(Customer c){
            try
            {
                IDAL.DO.Customer cu = new IDAL.DO.Customer();
                cu.Id = c.Id;
                cu.Name = c.Name;
                cu.Phone = c.Phone;
                cu.Lattitude = c.Location.Lattitude; //
                cu.Longitude = c.Location.Longitude; //
                data.AddCustomer(cu); 
                return "The addition was successful";
            }
            catch (IDAL.DO.IdExistException exp)
            {
                throw exp;
            }     
        }
        public string UpdateCustomer(int id, string nameCustomer, string phoneCustomer) {
            try
            {
                data.UpdateCustomer(id, nameCustomer, phoneCustomer); 
                return "The update was successful";
            }
            catch (IDAL.DO.IdExistException exp)
            {
                throw exp;
            }
            catch (IDAL.DO.PhoneExistException exp)
            {
                throw exp;
            }
        }

        public string GetCustomerById(int Id) {
            return data.GetCustomerById(Id);
        }

        public IEnumerable<Customer> GetCustomers(){
            return (IEnumerable<Customer>)data.GetCustomers();
        }
    }
}