using System;

namespace DO
{
    /// <summary>
    /// Defining the "Station" struct
    /// </summary>
    public struct Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Lattitude { get; set; }
        public int ChargeSlots { get; set; }
        public bool Active { get; set; }

        /// <summary><returns>
        /// The function returns a string to print on all entity data
        /// </returns></summary>
        public override string ToString() { return $"Id: {Id}\nName: {Name}\nLongitude: {LongitudeBonus(Longitude)}\nLattitude: {LattitudeBonus(Lattitude)}\nChargeSlots: {ChargeSlots}\n"; }

        /// <summary>
        /// The function gets a longitube and turns it into a longitube at base 60
        /// </summary>
        /// <param name="num">The number to change</param>
        /// <returns>The function returns a string of a number representing base 60</returns>
        public static string LongitudeBonus(double num) {///returns the longitude
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
        public static string LattitudeBonus(double num) {///returns the lattitude
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
    }

}