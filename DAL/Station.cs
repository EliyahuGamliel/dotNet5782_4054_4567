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
        }  
    }
}
