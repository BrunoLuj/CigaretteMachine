using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Class
{
    public static class UtilityClass
    {
        public static bool IsCheckInternet()
        {
            try
            {
                using (var clint = new WebClient())
                {
                    using (clint.OpenRead("https://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
