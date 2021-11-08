using System;
using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public partial class BL : IBL
    {
        public string AddCustomer(Customer c){
            try
            {
                IDAL.DO.Customer cu = new IDAL.DO.Customer();
                cu.Id = c.Id;
                cu.Name = c.Name;
                cu.Phone = c.Phone;
                cu.Lattitude = c.Location.Lattitude;
                cu.Longitude = c.Location.Longitude;
                data.AddCustomer(cu); 
                return "The addition was successful\n";
            }
            catch (IDAL.DO.IdExistException)
            {
                throw new IdExistException(c.Id);
            }     
        }
        public string UpdateCustomer(int id, string nameCustomer, string phoneCustomer) {
            try
            {
                IDAL.DO.Customer c = data.GetCustomerById(id);
                if (nameCustomer != "")
                    c.Name = nameCustomer;
                if (phoneCustomer != "")
                    c.Phone = phoneCustomer;
                data.UpdateCustomer(c, phoneCustomer);
                return "The update was successful\n"; 
            }
            catch (IDAL.DO.IdNotExistException)
            {
                throw new IdNotExistException(id);
            }
            catch (IDAL.DO.PhoneExistException)
            {
                throw new PhoneExistException(phoneCustomer);
            }
        }

        public Customer GetCustomerById(int Id) {
            try
            {
                IDAL.DO.Customer c = data.GetCustomerById(Id); 
                Customer cu = new Customer();
                cu.Id = c.Id;
                cu.Name = c.Name;
                cu.Phone = c.Phone;
                cu.Location.Longitude = c.Longitude;
                cu.Location.Lattitude = c.Lattitude;
                foreach (var item in data.GetParcels())
                {
                    if (item.TargetId == cu.Id)
                    {
                        ParcelInCustomer pc = new ParcelInCustomer();
                        pc.Id = item.Id;
                        pc.Priority = (Priorities)(int)item.Priority;
                        pc.Weight = (WeightCategories)(int)item.Weight;
                        pc.Status = (Statuses)ReturnStatus(item);

                        CustomerInParcel cp = new CustomerInParcel();
                        cp.Id = item.SenderId;
                        IDAL.DO.Customer c_help = data.GetCustomerById(Id); 
                        cp.Name = c_help.Name;
                        pc.CParcel = cp;

                        cu.ForCustomer.Add(pc);
                    }
                    else if (item.SenderId == cu.Id)
                    {
                        ParcelInCustomer pc = new ParcelInCustomer();
                        pc.Id = item.Id;
                        pc.Priority = (Priorities)(int)item.Priority;
                        pc.Weight = (WeightCategories)(int)item.Weight;
                        pc.Status = (Statuses)ReturnStatus(item);

                        CustomerInParcel cp = new CustomerInParcel();
                        cp.Id = item.TargetId;
                        IDAL.DO.Customer c_help = data.GetCustomerById(Id); 
                        cp.Name = c_help.Name;
                        pc.CParcel = cp;

                        cu.FromCustomer.Add(pc);
                    }
                } 
                return cu;
            }
            catch (IDAL.DO.IdNotExistException)
            {
                throw new IdNotExistException(Id);
            }
        }

        public IEnumerable<CustomerList> GetCustomers(){
            IEnumerable<IDAL.DO.Customer> list_s = data.GetCustomers();
            List<CustomerList> customer = new List<CustomerList>();
            foreach (var item in list_s)
            {
                CustomerList cu = new CustomerList();
                cu.Id = item.Id;
                cu.Name = item.Name;
                cu.Phone = item.Phone;
                cu.ParcelsGet = 0;
                cu.ParcelsInTheWay = 0;  
                cu.ParcelsOnlySend = 0;
                cu.ParcelsSent = 0;
                IEnumerable<IDAL.DO.Parcel> list_p = data.GetParcels();
                foreach (var itemParcel in list_p)
                {
                    if (itemParcel.SenderId == cu.Id) {
                        if (DateTime.Compare(itemParcel.Requested, itemParcel.Scheduled) > 0)
                            cu.ParcelsOnlySend += 1;
                        else if (DateTime.Compare(itemParcel.Scheduled, itemParcel.PickedUp) > 0 || DateTime.Compare(itemParcel.PickedUp, itemParcel.Delivered) > 0)
                            cu.ParcelsInTheWay += 1;
                        else if (DateTime.Compare(itemParcel.Delivered, itemParcel.PickedUp) > 0)
                            cu.ParcelsSent += 1;
                    }
                    if (itemParcel.TargetId == cu.Id && DateTime.Compare(itemParcel.Delivered, itemParcel.PickedUp) > 0)
                        cu.ParcelsGet += 1;
                }
                customer.Add(cu);
            }
            return customer;
        }
    }
}