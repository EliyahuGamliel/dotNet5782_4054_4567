using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "Customer" class
        /// </summary>
        public class ParcelTransfer
        {
            public int Id { get; set; }
            public Priorities Priority { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Recipient { get; set; }
            public Location Collection_Location { get; set; }
            public Location Destination_Location { get; set; }
            public double Transport_Distance { get; set; }
            public WeightCategories Weight { get; set; }
            public bool Status { get; set; }
            
            
            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nPriority: {Priority}\nThe sender: {Sender.ToString()}The recipient: {Recipient.ToString()}"; }

            public double DistanceTo(double lat1, double lon1, double lat2, double lon2) {
                double rlat1 = Math.PI * lat1 / 180;
                double rlat2 = Math.PI * lat2 / 180;
                double theta = lon1 - lon2;
                double rtheta = Math.PI * theta / 180;
                double dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
                dist = Math.Acos(dist);
                dist = dist * 180 / Math.PI;
                dist = dist * 60 * 1.1515;
                return Math.Round(dist * 1.609344, 2);
            }  
        }
    }
}
