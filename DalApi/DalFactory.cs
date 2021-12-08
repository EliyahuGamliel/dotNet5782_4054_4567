using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    static public class DalFactory
    {
        static public IDal GetDal(string typeDL)
        {
            switch(typeDL)
            {
                case "Object": return ;
                break;
                
            }
        }

    }
}
