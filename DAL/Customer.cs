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
            public double Longitude { get; set; }
            public double Lattitude{ get; set; }
            
            public override string ToString() 
            { return $"Id: {Id}\nName: {Name}\nPhone: {Phone}\nLongitude: {Longitude_Bonus(Longitude)}\nLattitude: {Lattitude_Bonus(Lattitude)}\n"; }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="num"></param>
            /// <returns></returns>
            public static string Longitude_Bonus(double num) {
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
                return $"{degrees}° {minutes}' {Math.Round(seconds, 3)}\" {dir}";
            }
            
            public static string Lattitude_Bonus(double num) {
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
                return $"{degrees}° {minutes}' {Math.Round(seconds, 3)}\" {dir}";
            }
            
            public double DistanceTo(double lat1, double lon1, double lat2, double lon2) {///calculating the distance between them
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
