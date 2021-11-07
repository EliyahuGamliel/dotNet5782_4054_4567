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
                List<IDAL.DO.Customer> list_c = (List<IDAL.DO.Customer>)data.GetCustomers();
                CheckNotExistId(list_c, id);
                IDAL.DO.Customer c = list_c.Find(cu => id == cu.Id);
                int index = list_c.IndexOf(c);
                c.Name = nameCustomer;
                c.Phone = phoneCustomer;
                data.UpdateCustomer(c,index);
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
            return data.GetCustomerById(Id);
        }

        public IEnumerable<IDAL.DO.Customer> GetCustomers(){
            return data.GetCustomers();
        }
    }
}