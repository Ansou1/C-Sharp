using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSW_10_UWA.Common
{
    class Globals
    {
        private static Globals _Instance = null;
        public static Globals getInstance()
        {
            if (_Instance == null)
                _Instance = new Globals();
            return _Instance;
        }

        private Globals() {}

        private string userConnected;
        public string UserConnected
        {
            get { return userConnected; }
            set { userConnected = value; }
        }

        private string ipAPI = "http://localhost/trunk/web/app_dev.php/";
        public string IpAPI
        {
            get { return ipAPI; }
        }

        //https://musicsheetwriter.tk/
        //public string SomeProperty { get; set; }
    }
}
