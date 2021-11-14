using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Defining the "CustomerInParcel" class
        /// </summary>
        public class CustomerInParcel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            
            /// <summary><returns>
            /// The function returns a string to print on all entity data
            /// </returns></summary>
            public override string ToString() 
            { return $"\n        Id: {Id}\n        Name: {Name}\n"; }
        }
    }
}
