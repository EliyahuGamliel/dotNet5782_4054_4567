using System;
using System.Linq;
using System.Collections.Generic;
using BO;
using BlApi;
using DO;
using DalApi;

namespace BL
{
    public partial class BL : IBL
    {
        /// <summary>
        /// If everything is fine, add a customer to the list of customers, else throw exception
        /// </summary>
        /// <param name="c">Object of customer to add</param>
        /// <returns>Notice if the addition was successful</returns>
        public string AddCustomer(BO.Customer c){
            try {
                CheckValidId(c.Id);
                DO.Customer cu = new DO.Customer();
                cu.Id = c.Id;
                cu.Name = c.Name;
                cu.Phone = c.Phone;
                cu.Lattitude = c.Location.Lattitude;
                cu.Longitude = c.Location.Longitude;
                CheckLegelLocation(cu.Longitude, cu.Lattitude);
                data.AddCustomer(cu); 
                return "The addition was successful\n";
            }
            catch (DO.IdExistException) {
                throw new BO.IdExistException(c.Id);
            }     
        }

        /// <summary>
        /// If all is fine, update the customer in a list of customers, else throw exception
        /// </summary>
        /// <param name="id">The ID of the customer for updating</param>
        /// <param name="nameCustomer">If requested - the new name for the customer update</param>
        /// <param name="phoneCustomer">If requested - the new phone for the customer update</param>
        /// <returns>Notice if the addition was successful</returns>
        public string UpdateCustomer(int id, string nameCustomer, string phoneCustomer) {
            try {
                DO.Customer chosenc = data.GetCustomerById(id);
                //If input is received
                if (nameCustomer != "") 
                    chosenc.Name = nameCustomer;
                //If input is received
                if (phoneCustomer != "")
                    chosenc.Phone = phoneCustomer;
                data.UpdateCustomer(chosenc, phoneCustomer);
                return "The update was successful\n"; 
            }
            catch (DO.IdNotExistException) {
                throw new BO.IdNotExistException(id);
            }
            catch (DO.PhoneExistException) {
                throw new BO.PhoneExistException(phoneCustomer);
            }
        }

        /// <summary>
        /// If all is fine, return a customer object by id
        /// </summary>
        /// <param name="Id">The id of the requested customer</param>
        /// <returns>The object of the requested customer</returns>
        public BO.Customer GetCustomerById(int Id) {
            try {
                DO.Customer chosenc = data.GetCustomerById(Id); 
                BO.Customer cu = new BO.Customer();
                cu.ForCustomer = new List<ParcelInCustomer>();
                cu.FromCustomer = new List<ParcelInCustomer>();
                cu.Location = new Location();
                cu.Id = chosenc.Id;
                cu.Name = chosenc.Name;
                cu.Phone = chosenc.Phone;
                cu.Location.Longitude = chosenc.Longitude;
                cu.Location.Lattitude = chosenc.Lattitude;

                foreach (var item in data.GetParcelByFilter(p => p.TargetId == cu.Id)) {
                    //If the customer is the target of the parcel
                    ParcelInCustomer pc = new ParcelInCustomer();
                    pc.CParcel = new CustomerInParcel();
                    pc.Id = item.Id;
                    pc.Priority = (BO.Priorities)(int)item.Priority;
                    pc.Weight = (BO.WeightCategories)(int)item.Weight;
                    pc.Status = (Statuses)ReturnStatus(item);

                    CustomerInParcel cp = new CustomerInParcel();
                    cp.Id = item.SenderId;
                    DO.Customer customerhelp = data.GetCustomerById(cp.Id); 
                    cp.Name = customerhelp.Name;
                    pc.CParcel = cp;

                    cu.ForCustomer.Add(pc);
                }
                
                foreach (var item in data.GetParcelByFilter(p => p.SenderId == cu.Id)) {
                    //If the customer is the sender of the parcel
                    ParcelInCustomer pc = new ParcelInCustomer();
                    pc.CParcel = new CustomerInParcel();
                    pc.Id = item.Id;
                    pc.Priority = (BO.Priorities)(int)item.Priority;
                    pc.Weight = (BO.WeightCategories)(int)item.Weight;
                    pc.Status = (Statuses)ReturnStatus(item);

                    CustomerInParcel cp = new CustomerInParcel();
                    cp.Id = item.TargetId;
                    DO.Customer customerhelp = data.GetCustomerById(cp.Id); 
                    cp.Name = customerhelp.Name;
                    pc.CParcel = cp;

                    cu.FromCustomer.Add(pc);
                } 
                return cu;
            }
            catch (DO.IdNotExistException) {
                throw new BO.IdNotExistException(Id);
            }
        }

        /// <summary>
        /// Returns the list of customers
        /// </summary>
        /// <returns>Returns the list of customers</returns>
        public IEnumerable<CustomerList> GetCustomers(){
            return ConvertToBL(data.GetCustomerByFilter(c => true));
        }

        /// <summary>
        /// Convert the list from type of DAL(Customer) to type of BL(CustomerList)
        /// </summary>
        /// <param name="listCustomers">The list we want to convert</param>
        /// <returns>The same list converted to BL</returns>
        private IEnumerable<CustomerList> ConvertToBL(IEnumerable<DO.Customer> listCustomers)
        {
            List<CustomerList> customer = new List<CustomerList>();
            foreach (var item in listCustomers) {
                CustomerList cu = new CustomerList();
                cu.Id = item.Id;
                cu.Name = item.Name;
                cu.Phone = item.Phone;

                IEnumerable<DO.Parcel> listparcels = data.GetParcelByFilter(p => true);
                //If the customer is the target and the parcel is arrived
                cu.ParcelsGet = listparcels.Where(p => ReturnStatus(p) == 3 && p.TargetId == cu.Id).Count();

                //If the customer is the sender
                cu.ParcelsOnlySend = listparcels.Where(p => ReturnStatus(p) == 0 && p.SenderId == cu.Id).Count();
                cu.ParcelsInTheWay = listparcels.Where(p => (ReturnStatus(p) == 1 || ReturnStatus(p) == 2)  && p.SenderId == cu.Id).Count();
                cu.ParcelsSent = listparcels.Where(p => ReturnStatus(p) == 3 && p.SenderId == cu.Id).Count();
             
                customer.Add(cu);
            }
            return customer;
        }
    }
}