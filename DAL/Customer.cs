using System;

namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// Defining the "Customer" class
        /// </summary>
        public struct Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Lattitude{ get; set; }
            
            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"Id: {Id}\nName: {Name}\nPhone: {Phone}\nLongitude: {Longitude_Bonus(Longitude)}\nLattitude: {Lattitude_Bonus(Lattitude)}\n"; }

            /// <summary>
            /// The function gets a longitube and turns it into a longitube at base 60
            /// </summary>
            /// <param name="num">The number to change</param>
            /// <returns>The function returns a string of a number representing base 60</returns>
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
            
            /// <summary>
            /// The function gets a lattitude and turns it into a lattitude at base 60
            /// </summary>
            /// <param name="num">The number to change</param>
            /// <returns>The function returns a string of a number representing base 60</returns>
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
            
            /// <summary>
            /// The function calculates the distance between two points by their longitude and latitude lines
            /// </summary>
            /// <param name="lat1">latitude number 1</param>
            /// <param name="lon1">longitude number 1</param>
            /// <param name="lat2">latitude number 2</param>
            /// <param name="lon2">longitude number 2</param>
            /// <returns>Distance between two points on the globe</returns>
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
