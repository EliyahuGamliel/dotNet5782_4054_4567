using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace DalApi
{
    static public class DalFactory
    {
        static public IDal GetDal(string typeDL)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Assembly assembly;
            try
            {
                assembly = Assembly.LoadFrom(path + "DalObject.dll");
            }
            catch (Exception)
            {

                throw;
            }
            Type type = assembly.GetType("Dal.DalObject");
            switch(typeDL)
            {
                case "Object":
                    return (IDal)Activator.CreateInstance(type);
                case "Xml":
                    return null;
                default:
                    return null;
            }
        }
    }
}
