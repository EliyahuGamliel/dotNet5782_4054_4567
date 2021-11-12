using System;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        public void AddCustomer(Customer c) {
            CheckExistId(DataSource.customers, c.Id);
            DataSource.customers.Add(c);
        }

        public void UpdateCustomer(Customer c, string phone)
        {
            CheckNotExistId(DataSource.customers, c.Id);
            if (phone != "")
                CheckNotExistPhone(DataSource.customers, phone);
            Customer cu = DataSource.customers.Find(cus => c.Id == cus.Id);
            int index = DataSource.customers.IndexOf(cu);
            DataSource.customers[index] = c;
        }
        
        public Customer GetCustomerById(int Id) {
            CheckNotExistId(DataSource.customers, Id);
            Customer c = DataSource.customers.Find(cu => Id == cu.Id);
            return c;
        }

        public IEnumerable<Customer> GetCustomers() {
            return DataSource.customers;
        }
    }
}