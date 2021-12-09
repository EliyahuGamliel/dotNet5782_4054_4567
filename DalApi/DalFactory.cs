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
            Type type = Type.GetType($"Dal.DalObject");
            switch(typeDL)
            {
                case "Object":
                    return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null) as IDal;
                case "Xml":
                    return null;
                default:
                    return null;
            }
        }
    }
}
