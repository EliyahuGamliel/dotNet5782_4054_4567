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
    sealed partial class DalXmlCustomer
    {
        public void AddCustomer(Customer c) {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Customer));
            TextWriter textWriter = new StreamWriter("CustomerData");

            try {
                xmlSerializer.Serialize(textWriter, c);
            }
            catch (Exception err) {
                throw err;
            }
            finally {
                textWriter.Close();
            }
        }

        public void UpdateCustomer(Customer c) {
            ;
        }

        public void DeleteCustomer(int customerID) {
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
