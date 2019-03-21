using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutobazarApp
{
    class AutobazarException : Exception
    {
        public AutobazarException(string message): base(message)
        {

        }
    }
}
