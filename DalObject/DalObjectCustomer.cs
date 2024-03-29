using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DalApi;
using DO;

namespace Dal
{
    /// <summary>
    /// Class partial DalObject - All functions running on the list of customers
    /// </summary>
    public partial class DalObject : IDal
    {
        /// <summary>
        /// If everything is fine, add a customer to the list of customers
        /// </summary>
        /// <param name="c">Object of customer to add</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer c) {
            CheckExistId(DataSource.Customers, c.Id);
            c.Active = true;
            DataSource.Customers.Add(c);
        }

        /// <summary>
        /// If all is fine, update the customer in a list of customers
        /// </summary>
        /// <param name="c">Object of customer to update</param>
        /// <param name="phone">Object.phone of customer to update</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(Customer c) {
            CheckNotExistId(DataSource.Customers, c.Id);
            Customer cu = DataSource.Customers.Find(cus => c.Id == cus.Id);
            int index = DataSource.Customers.IndexOf(cu);
            DataSource.Customers[index] = c;
        }

        /// <summary>
        /// If all is fine, return a customer object by id
        /// </summary>
        /// <param name="Id">The id of the requested customer</param>
        /// <returns>The object of the requested customer</returns> 
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomerById(int Id) {
            CheckNotExistId(DataSource.Customers, Id);
            Customer c = DataSource.Customers.Find(cu => Id == cu.Id);
            return c;
        }

        /// <summary>
        /// Returns all the customers that fit the filter
        /// </summary>
        /// <param name="cutomerList">The paradicte to filter</param>
        /// <returns>The Ienumerable to the customers</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomerByFilter(Predicate<Customer> cutomerList) {
            return DataSource.Customers.FindAll(cutomerList);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(Customer customer) {
            int index = DataSource.Customers.FindIndex(c => c.Id == customer.Id && c.Active);
            DataSource.Customers[index] = customer;
        }
    }
}
