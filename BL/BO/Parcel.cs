using System;
using DO;
using DalApi;

namespace BO
{
    /// <summary>
    /// Defining the "Parcel" class
    /// </summary>
    public class Parcel
    {
        public int? Id { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public DroneInParcel Drone { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DateTime? Requested { get; set; }
        public DateTime? Scheduled { get; set; }
        public DateTime? PickedUp { get; set; }
        public DateTime? Delivered { get; set; }

        /// <summary><returns>
        /// The function returns a string to print on all entity data
        /// </returns></summary>
        public override string ToString()
        {
            string str =  $"Id: {Id}\nWeight: {Weight}\nPriority: {Priority}\nRequested: {Requested}\nScheduled: ";
            if (Scheduled != null)
                str += Scheduled;
            str += $"\nPickedUp: ";
            if (PickedUp != null)
                str += PickedUp;
            str += $"\nDelivered: ";
            if (Delivered != null)
                str += Delivered;
            str += $"\nDrone: \n{Drone}Target: {Target}Sender: {Sender}\n"; 
            return str;    
        }
    }
}