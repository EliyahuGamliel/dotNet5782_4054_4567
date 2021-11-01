using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    public partial class DalObject : IDAL.IDAL
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
            DataSource.customers.Add(c);
        }

        /// <summary>
        /// prints the customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> PrintListCustomer() {
            return DataSource.customers;
        }
    }
}