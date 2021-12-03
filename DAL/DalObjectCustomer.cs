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
            CheckExistId(DataSource.Customers, c.Id);
            DataSource.Customers.Add(c);
        }

        /// <summary>
        /// If all is fine, update the customer in a list of customers
        /// </summary>
        /// <param name="c">Object of customer to update</param>
        /// <param name="phone">Object.phone of customer to update</param>
        public void UpdateCustomer(Customer c, string phone) {
            CheckNotExistId(DataSource.Customers, c.Id);
            if (phone != "")
                CheckExistPhone(DataSource.Customers, phone);
            Customer cu = DataSource.Customers.Find(cus => c.Id == cus.Id);
            int index = DataSource.Customers.IndexOf(cu);
            DataSource.Customers[index] = c;
        }
        
        /// <summary>
        /// If all is fine, return a customer object by id
        /// </summary>
        /// <param name="Id">The id of the requested customer</param>
        /// <returns>The object of the requested customer</returns>
        public Customer GetCustomerById(int Id) {
            CheckNotExistId(DataSource.Customers, Id);
            Customer c = DataSource.Customers.Find(cu => Id == cu.Id);
            return c;
        }

        /// <summary>
        /// Returns all the customers that fit the filter
        /// </summary>
        /// <param name="cutomerList">the paradicte</param>
        /// <returns> the Ienumerable to the customers </returns>
        public IEnumerable<Customer> GetCustomerByFilter(Predicate<Customer> cutomerList) {
            return DataSource.Customers.FindAll(cutomerList);
        }
    }
}