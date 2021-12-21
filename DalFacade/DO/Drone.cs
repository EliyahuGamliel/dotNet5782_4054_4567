using System;

namespace DO
{
    /// <summary>
    /// Defining the "Drone" struct
    /// </summary>
    public struct Drone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public bool Active { get; set; }

        /// <summary><returns>
        /// The function returns a string to print on all entity data
        /// </returns></summary>
        public override string ToString()
        { return $"Id: {Id}\nModel: {Model}\nMaxWeight: {MaxWeight}\n"; }
    }
}
