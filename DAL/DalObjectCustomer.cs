using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// Add a customer at the request of the user to the list
        /// </summary>
        /// <param name="Id">id of customer</param>
        /// <param name="Name">name of customer</param>
        /// <param name="Phone"> phone of customer</param>
        /// <param name="Longitude">Longitude of customer</param>
        /// <param name="Lattitude">Lattitude of customer</param>
        public void AddCustomer(Customer c) {
            CheckExistId(DataSource.customers, c.Id);
            DataSource.customers.Add(c);
        }

        public void UpdateCustomer(Customer c)
        {
            CheckNotExistId(DataSource.customers, c.Id);
            CheckNotExistPhone(DataSource.customers, c.Phone);
            Customer cu = DataSource.customers.Find(cus => c.Id == cus.Id);
            int index = DataSource.customers.IndexOf(cu);
            DataSource.customers[index] = c;
        }
        
        public Customer GetCustomerById(int Id) {
            CheckNotExistId(DataSource.customers, Id);
            Customer c = DataSource.customers.Find(cu => Id == cu.Id);
            return c;
        }

        /// <summary>
        /// prints the customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomers() {
            return DataSource.customers;
        }
    }
}