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
            try
            {
                Assembly.Load("DalObject.dll");
            }
            catch (Exception)
            {

                throw;
            }
            Type type = Type.GetType("Dal.DalObject, DalOblect.dll");
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
