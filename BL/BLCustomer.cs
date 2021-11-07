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
                cu.Lattitude = c.Location.Lattitude;
                cu.Longitude = c.Location.Longitude;
                data.AddCustomer(cu); 
                return "The addition was successful";
            }
            catch (IDAL.DO.IdExistException)
            {
                throw new IdExistException(c.Id);
            }     
        }
        public string UpdateCustomer(int id, string nameCustomer, string phoneCustomer) {
            try
            {
                IDAL.DO.Customer c = data.GetCustomerById(id);
                c.Name = nameCustomer;
                c.Phone = phoneCustomer;
                data.UpdateCustomer(c);
                return "The update was successful"; 

            }
            catch (IDAL.DO.IdNotExistException)
            {
                throw new IdNotExistException(id);
            }
            catch (IDAL.DO.PhoneExistException)
            {
                throw new PhoneExistException(phoneCustomer);
            }
        }

        public string GetCustomerById(int Id) {
            return data.GetCustomerById(Id).ToString();
        }

        public IEnumerable<IDAL.DO.Customer> GetCustomers(){
            return data.GetCustomers();
        }
    }
}