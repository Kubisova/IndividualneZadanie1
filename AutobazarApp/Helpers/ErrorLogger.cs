using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutobazarApp.Helpers
{
    static class ErrorLogger
    {
        public static void ErrorLogging(Exception ex)
        {
            string path = "errorLog.txt";
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine("-----------------------------------------");
                sw.WriteLine("Date: " + DateTime.Now.ToString());
                sw.WriteLine();
                while (ex != null)
                {
                    sw.WriteLine(ex.GetType().FullName);
                    sw.WriteLine("Message: " + ex.Message);
                    sw.WriteLine("Stacktrace: " + ex.StackTrace);

                    ex = ex.InnerException;
                }
            }
        }
    }
}
