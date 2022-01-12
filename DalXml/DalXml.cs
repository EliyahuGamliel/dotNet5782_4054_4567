using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal
{
    internal sealed partial class DalXml : IDal
    {
        /// <summary>
        /// Ctor for the compiler
        /// </summary>
        static DalXml() { }

        private class Nested
        {
            internal static volatile DalXml _instance = null;
            internal static readonly object _lock = new object();
            static Nested() { }
        }

        public static DalXml Instance
        {
            get
            {
                if (Nested._instance == null) {
                    lock (Nested._lock) {
                        if (Nested._instance == null) {
                            Nested._instance = new DalXml();
                        }
                    }
                }
                return Nested._instance;
            }
        }

        public DalXml() { }

        private Dictionary<Type, string> fileNames = new Dictionary<Type, string>() {
                {typeof(Customer) , "Customers.xml"},
                {typeof(Drone) , "Drones.xml"},
                {typeof(DroneCharge) , "DronesCharges.xml"},
                {typeof(Parcel) , "Parcels.xml"},
                {typeof(Station) , "Stations.xml"},
            };

        [MethodImpl(MethodImplOptions.Synchronized)]
        private List<T> Read<T>() {
            XmlReader Reader;
            List<T> Data;
            XmlSerializer Ser = new XmlSerializer(typeof(List<T>));

            try {
                Reader = new XmlTextReader(Path.Combine("Data", fileNames[typeof(T)]));

                if (Ser.CanDeserialize(Reader)) {
                    Data = (List<T>)Ser.Deserialize(Reader);
                }
                else {
                    Data = new List<T>();
                }
                Reader.Close();

                return Data;
            }
            catch (DirectoryNotFoundException) {
                Directory.CreateDirectory("Data");
                return new List<T>();
            }
            catch (FileNotFoundException) {
                return new List<T>();
            }
            catch (XmlException) {
                return new List<T>();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Write<T>(List<T> data) {
            XmlSerializer ser = new XmlSerializer(typeof(List<T>));

            TextWriter Writer;
            try {
                Writer = new StreamWriter(Path.Combine("Data", fileNames[typeof(T)]));
            }
            catch (DirectoryNotFoundException) {
                Directory.CreateDirectory("Data");
                Writer = new StreamWriter(Path.Combine("Data", fileNames[typeof(T)]));
            }
            try {
                ser.Serialize(Writer, data);
            }
            catch (Exception) { throw; }
            finally {
                Writer.Close();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static int Update<T>(List<T> listy, T updater) {
            var Id = typeof(T).GetProperty("Id");

            int index = listy.FindIndex(x => (int)Id.GetValue(x, null) == (int)Id.GetValue(updater, null));

            if (index != -1)
                listy[index] = updater;

            return index;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void CheckExistId<T>(List<T> list, int id) {
            foreach (var item in list) {
                int idobject = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                if (idobject == id)
                    throw new IdExistException(id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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
