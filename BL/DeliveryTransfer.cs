using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "Customer" class
        /// </summary>
        public class DeliveryTransfer
        {
            public int Id { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DeliveryStatuses Status { get; set; }
            public Location Collection_Location { get; set; }
            public Location Destination_Location { get; set; }
            public double Transport_Distance { get; set; }
            
            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString()  {
                string str = $"Id: {Id}\nWeight: {Weight}\nPriority: {Priority}\nStatus: {Status}\n";
                str += $"Collection Location: {Id}\nWeight: {Collection_Location.ToString()}\nDestination Location: {Destination_Location.ToString()}\n";
                str += $"Transport_Distance: {DistanceTo(Collection_Location.Lattitude, Collection_Location.Longitude, Destination_Location.Lattitude, Destination_Location.Longitude)}\n";
                return str;
            }
        
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
