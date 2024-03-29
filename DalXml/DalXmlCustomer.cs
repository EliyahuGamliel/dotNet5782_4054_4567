﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DalApi;
using DO;

namespace Dal
{
    internal partial class DalXml : IDal
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer c) {
            List<Customer> customers = Read<Customer>();
            CheckExistId<Customer>(customers, c.Id);
            customers.Add(c);
            Write<Customer>(customers);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(Customer c) {
            List<Customer> customers = Read<Customer>();
            CheckNotExistId<Customer>(customers, c.Id);
            customers[Update<Customer>(customers, c)] = c;
            Write<Customer>(customers);

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(Customer c) {
            List<Customer> customers = Read<Customer>();
            CheckNotExistId<Customer>(customers, c.Id);
            customers[Update<Customer>(customers, c)] = c;
            Write<Customer>(customers);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomerById(int Id) {
            List<Customer> customers = Read<Customer>();
            CheckNotExistId<Customer>(customers, Id);
            return customers.Find(c => c.Id == Id);

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomerByFilter(Predicate<Customer> cutomerList) {
            List<Customer> customers = Read<Customer>();
            return customers.FindAll(cutomerList);
        }
    }
}
