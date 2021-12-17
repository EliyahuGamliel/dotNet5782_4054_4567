using System;
using System.Collections.Generic;
using DO;
using DalApi;

namespace BO
{
    /// <summary>
    /// Defining the "CustomerList" class
    /// </summary>
    public class CustomerList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int ParcelsSent { get; set; }
        public int ParcelsOnlySend { get; set; }
        public int ParcelsGet { get; set; }
        public int ParcelsInTheWay { get; set; }

        /// <summary><returns>
        /// The function returns a string to print on all entity data
        /// </returns></summary>
        public override string ToString()
        { return $"Id: {Id}\nName: {Name}\nPhone: {Phone}\nParcels shipped and arrived: {ParcelsSent}\nParcels shipped and on the way: {ParcelsInTheWay}\nParcels sent and not yet delivered: {ParcelsOnlySend}\nParcels received: {ParcelsGet}\n"; }
    }
}