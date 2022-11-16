using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EComm2022.Tienda.Helpers
{
    public static class HelperImagen
    {
        public static string ConvertirBase64(string archivo)
        {
            string base64 = string.Empty;
       
            try
            {
                byte[] bytes = File.ReadAllBytes(archivo);
                base64 = Convert.ToBase64String(bytes);
                return base64;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}