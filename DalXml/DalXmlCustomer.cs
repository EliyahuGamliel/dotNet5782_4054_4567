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
            List<Customer> lc = Read<Customer>();
            CheckExistId<Customer>(lc, c.Id);
            lc.Add(c);
            Write<Customer>(lc);
        }

        public void UpdateCustomer(Customer c) {
            List<Customer> lc = Read<Customer>();
            CheckNotExistId<Customer>(lc, c.Id);
            lc[Update<Customer>(lc, c)] = c;
            Write<Customer>(lc);

        }
        public void DeleteCustomer(Customer c) {
            List<Customer> customerList = Read<Customer>();
            CheckNotExistId<Customer>(customerList, c.Id);
            customerList[Update<Customer>(customerList, c)] = c;
            Write<Customer>(customerList);
        }

        public Customer GetCustomerById(int Id) {
            List<Customer> customerList = Read<Customer> ();
            CheckNotExistId<Customer>(customerList, Id);
            return customerList.Find(c => c.Id == Id);
            
        }

        public IEnumerable<Customer> GetCustomerByFilter(Predicate<Customer> cutomerList) {
            List<Customer> l = Read<Customer>();
            return l.FindAll(cutomerList);
        }
    }
}
