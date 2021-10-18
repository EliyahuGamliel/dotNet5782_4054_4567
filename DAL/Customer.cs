using System;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitube { get; set; }
            public double Lattitube{ get; set; }
            
            public string override ToString() 
            { return $"Id: {Id}\nName: {Name}\nPhone: {Phone}\nLongitube: {Longitube_Bonus(Longitube)}\nLattitube: {Lattitube_Bonus(Lattitube)}\n"; }
            
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
