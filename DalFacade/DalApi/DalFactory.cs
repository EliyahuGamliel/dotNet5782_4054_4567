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
        static public IDal GetDal()
        {
            /*
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Assembly assembly = Assembly.LoadFrom(path + "DalObject.dll");
            Type type = assembly.GetType("Dal.DalObject");
            switch(typeDL)
            {
                case "Object":
                    return (IDal)type.GetProperty("Instance").GetValue(null, null);
                case "Xml":
                    return null;
                default:
                    return null;
            }*/
            string dalType = DalConfig.DalName;
            string dalPkg = DalConfig.DalPackages[dalType];
            if (dalPkg == null) throw new DalConfigException($"Package {dalType} is not found in packages list in dal-config.xml");
            
            try { Assembly.Load(dalPkg); }
            catch (Exception) { throw new DalConfigException("Failed to load the dal-config.xml"); }

            Type type = Type.GetType($"Dal.{dalPkg}, {dalPkg}");
            if (type == null) { throw new DalConfigException($"Class {dalPkg} was not found in {dalPkg}.dll"); }

            IDal dal = (IDal)type.GetProperty("Instance",
                        BindingFlags.Public | BindingFlags.Static).GetValue(null);
            if (dal == null) { throw new DalConfigException($"Class {dalPkg} is not singleton or wrong property name for Instance"); }

            return dal;
        }
    }
}
