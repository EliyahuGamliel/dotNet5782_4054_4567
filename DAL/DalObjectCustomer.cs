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
        public string AddCustomer(Customer c) {
            exp.Check_Add_ID<Customer>(DataSource.customers, c.Id);
            DataSource.customers.Add(c);
            return "The addition was successful";
        }

        public string GetCustomerById(int Id) {
            exp.Check_Update_or_Get_By_ID<Customer>(DataSource.customers, Id);
            Customer c = DataSource.customers.Find(cu => Id == cu.Id);
            return c.ToString();
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