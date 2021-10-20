using System;

namespace IDAL
{
    namespace DO
    {
       public struct Station
       {
           public int Id { get; set; }
           public int Name { get; set; }
           public double Longitude { get; set; }
           public double Lattitude{ get; set; } 
           public int ChargeSlots{ get; set; }
           
           public string override ToString() 
           { return $"Id: {Id}\nName: {Name}\nLongitude: {Longitube_Bonus(Longitude)}\nLattitude: {Lattitube_Bonus(Lattitude)}\nChargeSlots: {ChargeSlots}\n"; }     
           
           public static string Longitube_Bonus(double num)
           {
            char dir = 'S';
                if (num < 0)
                    num = -num;
                else
                    dir = 'N';
                int degrees = (int)num;
                num = num - degrees;
                int minutes = (int)(num * 60);
                double rest = num - ((double)minutes / 60);
                double seconds = rest * 3600;
                return $"{degrees}° {minutes}' {Math.Round(seconds, 3, MidpointRounding.ToZero)}\" {dir}\n";
            }
            
            public static string Lattitube_Bonus(double num)
            {
                char dir = 'W';
                if (num < 0)
                    num = -num;
                else
                    dir = 'E';
                int degrees = (int)num;
                num = num - degrees;
                int minutes = (int)(num * 60);
                double rest = num - ((double)minutes / 60);
                double seconds = rest * 3600;
                return $"{degrees}° {minutes}' {Math.Round(seconds, 3, MidpointRounding.ToZero)}\" {dir}\n";
            }
           
           public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
        {
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
