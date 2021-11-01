using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Set up all the enums relevant to the program
        /// </summary>
        public enum WeightCategories {Light, Medium, Heavy}
        
        public enum DroneStatuses {Available, Maintenance, Delivery}
        
        public enum Priorities {Normal, Fast, Emergency}

        public enum DeliveryStatuses {Waiting_For_Collection, On_The_Way}

        public enum Statuses {Created, Associated, Collected, Provided}
    }
}