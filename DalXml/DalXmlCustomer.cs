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
        public void DeleteCustomer(Customer c) {
            List<Customer> l = Read<Customer>();
            l[Update<Customer>(l, c)] = c;
            Write<Customer>(l);
        }

        public Customer GetCustomerById(int Id) {
            List<Customer> l = Read<Customer> ();
            return l.Find(l => l.Id == Id);
            
        }

        public IEnumerable<Customer> GetCustomerByFilter(Predicate<Customer> cutomerList) {
            List<Customer> l = Read<Customer>();
            return l.FindAll(cutomerList);
        }
    }
}
