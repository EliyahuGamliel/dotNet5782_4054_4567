using System;
using System.Collections.Generic;

namespace IDAL
{
    namespace DO
    {
        public class MyException
        {
            public void Check_Add_ID <T>(List<T> list, int ID)
            {
                foreach (var item in list) {
                   int id_object = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                   if (id_object == ID)
                        throw new Exception("The ID number already exists");
                }
            }

            public void Check_Update_ID <T>(List<T> list, int ID)
            {
                foreach (var item in list) {
                   int id_object = (int)(typeof(T).GetProperty("Id").GetValue(item, null));
                   if (id_object == ID)
                        return;
                   throw new Exception("The ID number already exists");
                }
            }

            
        }
    }
}