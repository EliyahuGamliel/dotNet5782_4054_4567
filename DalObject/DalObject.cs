using System;
using System.Collections.Generic;
using DO;
using DalApi;

namespace Dal
{
    /// <summary>
    /// Class partial DalObject - All functions running on the list of customers
    /// </summary>
    sealed partial class DalObject : IDal
    {
        //class Nested
        //{
        //    static Nested(){ }
        //    internal static readonly DalObject instance = new DalObject();
        //}
        static readonly DalObject instance = new DalObject();

        public static DalObject Instance { get => instance; }

        /// <summary>
        /// Ctor of DalObject - Uses the function Initialize() in order to initialise the data
        /// </summary>
        static DalObject() { DataSource.Initialize(); }

        /// <summary>
        /// Checks if the ״id״ already exists, if there is an error return
        /// </summary>
        /// <param name="list">List of T-objects</param>
        /// <param name="id">The id for check</param>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <returns>Nothing</returns>
        public void CheckExistId <T>(List<T> list, int id) {
            foreach (var item in list) {
                int idobject = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                if (idobject == id)
                    throw new IdExistException(id);   
            }
        }

        /// <summary>
        /// Checks if ״id״ does not exist, if not returns error
        /// </summary>
        /// <param name="list">List of T-objects</param>
        /// <param name="id">The id for check</param>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <returns>Nothing</returns>
        public void CheckNotExistId <T>(List<T> list, int id) {
            foreach (var item in list) {
                int idobject = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                if (idobject == id)
                    return;          
            }
            throw new IdNotExistException(id);   
        }

        /// <summary>
        /// Checks if the ״phone״ already exists, if there is an error return
        /// </summary>
        /// <param name="list">List of T-objects</param>
        /// <param name="id">The phone for check</param>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <returns>Nothing</returns>
        public void CheckExistPhone <T>(List<T> list, string phone) {
            foreach (var item in list) {
                string phoneobject = (string)(typeof(T).GetProperty("Phone").GetValue(item, null));
                if (phoneobject == phone) {
                    throw new PhoneExistException(phone);
                }         
            }
        }
    }
}
