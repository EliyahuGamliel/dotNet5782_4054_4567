using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Xml.Serialization;
using System.IO;

namespace Dal
{
    partial class DalXml : IDal
    {
        public void AddCustomer(Customer c) {
            List<Customer> l = Read<Customer>();
            ////////////////////////////////////////check for same id error
            l.Add(c);
            Write<Customer>(l);
        }

        public void UpdateCustomer(Customer c) {
            List<Customer> l = Read<Customer>();
            l[Update<Customer>(l, c)] = c;
            Write<Customer>(l);

        }
        public void DeleteCustomer(Customer customer) {
            throw new NotImplementedException();
        }

        public Customer GetCustomerById(int Id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomerByFilter(Predicate<Customer> cutomerList) {
            throw new NotImplementedException();
        }
    }
}
