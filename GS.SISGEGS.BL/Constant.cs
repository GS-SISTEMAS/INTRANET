using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace GS.SISGEGS.BL
{
    public class Constant
    {
        public static string sistema = ConfigurationManager.AppSettings["sistema"];
        public static string key = ConfigurationManager.AppSettings["key"];
    }
}
