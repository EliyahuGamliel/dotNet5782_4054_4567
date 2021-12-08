using System;
using DO;
using DalApi;

namespace BO
{
    /// <summary>
    /// Set up all the enums relevant to the program
    /// </summary>
    public enum WeightCategories { Light, Medium, Heavy }

    public enum DroneStatuses { Available, Maintenance, Delivery }

    public enum Priorities { Normal, Fast, Emergency }

    public enum Statuses { Created, Associated, Collected, Provided }
}
