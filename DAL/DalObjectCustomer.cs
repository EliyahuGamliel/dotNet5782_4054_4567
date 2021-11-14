using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    /// <summary>
    /// Class partial DalObject - All functions running on the list of customers
    /// </summary>
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// If everything is fine, add a customer to the list of customers
        /// </summary>
        /// <param name="c">Object of customer to add</param>
        public void AddCustomer(Customer c) {
            CheckExistId(DataSource.customers, c.Id);
            DataSource.customers.Add(c);
        }

        /// <summary>
        /// If all is fine, update the customer in a list of customers
        /// </summary>
        /// <param name="c">Object of customer to update</param>
        /// <param name="phone">Object.phone of customer to update</param>
        public void UpdateCustomer(Customer c, string phone) {
            CheckNotExistId(DataSource.customers, c.Id);
            if (phone != "")
                CheckExistPhone(DataSource.customers, phone);
            Customer cu = DataSource.customers.Find(cus => c.Id == cus.Id);
            int index = DataSource.customers.IndexOf(cu);
            DataSource.customers[index] = c;
        }
        
        /// <summary>
        /// If all is fine, return a customer object by id
        /// </summary>
        /// <param name="Id">The id of the requested customer</param>
        /// <returns>The object of the requested customer</returns>
        public Customer GetCustomerById(int Id) {
            CheckNotExistId(DataSource.customers, Id);
            Customer c = DataSource.customers.Find(cu => Id == cu.Id);
            return c;
        }

        /// <summary>
        /// Returns the list of customers
        /// </summary>
        /// <returns>Returns the list of customers</returns>
        public IEnumerable<Customer> GetCustomers() {
            return DataSource.customers;
        }
    }
}