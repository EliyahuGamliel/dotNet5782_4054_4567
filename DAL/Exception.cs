using System;
using System.Collections.Generic;

namespace IDAL
{
    namespace DO
    {
        public class MyException
        {
            public void Check_ID <T>(List<T> list, int ID)
            {
                foreach (var item in list)
                {
                   int id_object = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                   if (id_object == ID)
                   {
                        throw new Exception("Id is exist");
                   }
                }
            }

            
        }
    }
}