using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutobazarApp.Helpers
{
    public static class Common
    {
        public static void ConsoleHorizontalLine(char c = '=')
        {
            Console.WriteLine(new string(c, Console.WindowWidth - 1));
        }
    }
}
