using System.Collections.Generic;
using DalApi;
using DO;

namespace Dal
{
    /// <summary>
    /// Class partial DalObject - All functions running on the list of customers
    /// </summary>
    public sealed partial class DalObject : IDal
    {
        /// <summary>
        /// Ctor for the compiler
        /// </summary>
        static DalObject() { }

        private class Nested
        {
            internal static volatile DalObject _instance = null;
            internal static readonly object _lock = new object();
            static Nested() { }
        }

        public static DalObject Instance
        {
            get
            {
                if (Nested._instance == null) {
                    lock (Nested._lock) {
                        if (Nested._instance == null) {
                            Nested._instance = new DalObject();
                        }
                    }
                }
                return Nested._instance;
            }
        }

        /// <summary>
        /// Ctor of DalObject - Uses the function Initialize() in order to initialise the data
        /// </summary>
        public DalObject() { DataSource.Initialize(); }

        /// <summary>
        /// Checks if the ״id״ already exists, if there is an error return
        /// </summary>
        /// <param name="list">List of T-objects</param>
        /// <param name="id">The id for check</param>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <returns>Nothing</returns>
        private void CheckExistId<T>(List<T> list, int id) {
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
        private void CheckNotExistId<T>(List<T> list, int id) {
            foreach (var item in list) {
                int idobject = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                bool active = (bool)(typeof(T).GetProperty("Active").GetValue(item, null));
                if (idobject == id && active == true)
                    return;
            }
            throw new IdNotExistException(id);
        }
    }
}
