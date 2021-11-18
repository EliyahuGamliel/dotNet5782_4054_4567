using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {

        /// <summary>
        /// If everything is fine, add a customer to the list of customers, else throw exception
        /// </summary>
        /// <param name="c">Object of customer to add</param>
        /// <returns>Notice if the addition was successful</returns>
        public string AddCustomer(Customer c){
            try {
                IDAL.DO.Customer cu = new IDAL.DO.Customer();
                cu.Id = c.Id;
                cu.Name = c.Name;
                cu.Phone = c.Phone;
                cu.Lattitude = c.Location.Lattitude;
                cu.Longitude = c.Location.Longitude;
                data.AddCustomer(cu); 
                return "The addition was successful\n";
            }
            catch (IDAL.DO.IdExistException) {
                throw new IdExistException(c.Id);
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
                IDAL.DO.Customer chosenc = data.GetCustomerById(id);
                //If input is received
                if (nameCustomer != "") 
                    chosenc.Name = nameCustomer;
                //If input is received
                if (phoneCustomer != "")
                    chosenc.Phone = phoneCustomer;
                data.UpdateCustomer(chosenc, phoneCustomer);
                return "The update was successful\n"; 
            }
            catch (IDAL.DO.IdNotExistException) {
                throw new IdNotExistException(id);
            }
            catch (IDAL.DO.PhoneExistException) {
                throw new PhoneExistException(phoneCustomer);
            }
        }

        /// <summary>
        /// If all is fine, return a customer object by id
        /// </summary>
        /// <param name="Id">The id of the requested customer</param>
        /// <returns>The object of the requested customer</returns>
        public Customer GetCustomerById(int Id) {
            try {
                IDAL.DO.Customer chosenc = data.GetCustomerById(Id); 
                Customer cu = new Customer();
                cu.ForCustomer = new List<ParcelInCustomer>();
                cu.FromCustomer = new List<ParcelInCustomer>();
                cu.Location = new Location();
                cu.Id = chosenc.Id;
                cu.Name = chosenc.Name;
                cu.Phone = chosenc.Phone;
                cu.Location.Longitude = chosenc.Longitude;
                cu.Location.Lattitude = chosenc.Lattitude;

                foreach (var item in data.GetParcels()) {
                    //If the customer is the target of the parcel
                    if (item.TargetId == cu.Id) {
                        ParcelInCustomer pc = new ParcelInCustomer();
                        pc.CParcel = new CustomerInParcel();
                        pc.Id = item.Id;
                        pc.Priority = (Priorities)(int)item.Priority;
                        pc.Weight = (WeightCategories)(int)item.Weight;
                        pc.Status = (Statuses)ReturnStatus(item);

                        CustomerInParcel cp = new CustomerInParcel();
                        cp.Id = item.SenderId;
                        IDAL.DO.Customer customerhelp = data.GetCustomerById(cp.Id); 
                        cp.Name = customerhelp.Name;
                        pc.CParcel = cp;

                        cu.ForCustomer.Add(pc);
                    }
                    //If the customer is the sender of the parcel
                    else if (item.SenderId == cu.Id) {
                        ParcelInCustomer pc = new ParcelInCustomer();
                        pc.CParcel = new CustomerInParcel();
                        pc.Id = item.Id;
                        pc.Priority = (Priorities)(int)item.Priority;
                        pc.Weight = (WeightCategories)(int)item.Weight;
                        pc.Status = (Statuses)ReturnStatus(item);

                        CustomerInParcel cp = new CustomerInParcel();
                        cp.Id = item.TargetId;
                        IDAL.DO.Customer customerhelp = data.GetCustomerById(cp.Id); 
                        cp.Name = customerhelp.Name;
                        pc.CParcel = cp;

                        cu.FromCustomer.Add(pc);
                    }
                } 
                return cu;
            }
            catch (IDAL.DO.IdNotExistException) {
                throw new IdNotExistException(Id);
            }
        }

        /// <summary>
        /// Returns the list of customers
        /// </summary>
        /// <returns>Returns the list of customers</returns>
        public IEnumerable<CustomerList> GetCustomers(){
            IEnumerable<IDAL.DO.Customer> listcustomers = data.GetCustomers();
            List<CustomerList> customer = new List<CustomerList>();
            foreach (var item in listcustomers) {
                CustomerList cu = new CustomerList();
                cu.Id = item.Id;
                cu.Name = item.Name;
                cu.Phone = item.Phone;
                cu.ParcelsGet = 0;
                cu.ParcelsInTheWay = 0;  
                cu.ParcelsOnlySend = 0;
                cu.ParcelsSent = 0;

                IEnumerable<IDAL.DO.Parcel> listparcels = data.GetParcels();
                foreach (var itemParcel in listparcels) {
                    //If the customer is the sender
                    if (itemParcel.SenderId == cu.Id) {
                        if (ReturnStatus(itemParcel) == 0)
                            cu.ParcelsOnlySend += 1;
                        else if (ReturnStatus(itemParcel) == 1 || ReturnStatus(itemParcel) == 2)
                            cu.ParcelsInTheWay += 1;
                        else if (ReturnStatus(itemParcel) == 3)
                            cu.ParcelsSent += 1;
                    }
                    //If the customer is the target and the parcel is arrived
                    if (itemParcel.TargetId == cu.Id && ReturnStatus(itemParcel) == 3)
                        cu.ParcelsGet += 1;
                }
                customer.Add(cu);
            }
            return customer;
        }
    }
}