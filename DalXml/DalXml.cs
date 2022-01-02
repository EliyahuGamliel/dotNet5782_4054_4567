using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using DO;
using DalApi;
using System.Xml.Linq;

namespace Dal
{
    sealed partial class DalXml : IDal
    {
        /// <summary>
        /// Ctor for the compiler
        /// </summary>
        static DalXml() { }

        class Nested
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
        Dictionary<Type, string> fileNames = new Dictionary<Type, string>() {
                {typeof(Customer) , "Customers.xml"},
                {typeof(Drone) , "Drones.xml"},
                {typeof(DroneCharge) , "DronesCharges.xml"},
                {typeof(Parcel) , "Parcels.xml"},
                {typeof(Station) , "Stations.xml"},
            };

        List<T> Read<T>() {
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
        void Write<T>(List<T> data) {
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
            catch (Exception err) { throw err; }
            finally {
                Writer.Close();
            }
        }

        static int Update<T>(List<T> listy, T updater) {
            var Id = typeof(T).GetProperty("Id");

            int index = listy.FindIndex(x => (int)Id.GetValue(x, null) == (int)Id.GetValue(updater, null));

            if (index != -1)
                listy[index] = updater;

            return index;
        }
    }
}
