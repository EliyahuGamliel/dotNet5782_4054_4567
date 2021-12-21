﻿using System;
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
            string dalType = DalConfig.DalName;
            string dalPkg = DalConfig.DalPackages[dalType];
            string dalNamespace = DalConfig.DalNamespaces[dalType];
            string dalClass = DalConfig.DalClasses[dalType];
            if (dalPkg == null) throw new DalConfigException($"Package {dalType} is not found in packages list in dal-config.xml");
            
            try { Assembly.Load(dalPkg); }
            catch (Exception) { throw new DalConfigException("Failed to load the dal-config.xml"); }

            Type type = Type.GetType($"{dalNamespace}.{dalClass}, {dalPkg}");
            if (type == null) { throw new DalConfigException($"Class {dalClass} was not found in {dalPkg}.dll"); }

            IDal dal = (IDal)type.GetProperty("Instance",
                        BindingFlags.Public | BindingFlags.Static).GetValue(null);
            if (dal == null) { throw new DalConfigException($"Class {dalClass} is not singleton or wrong property name for Instance"); }

            return dal;
        }
    }
}
