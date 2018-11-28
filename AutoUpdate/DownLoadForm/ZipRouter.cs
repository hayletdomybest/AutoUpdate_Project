using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace DownLoadForm
{
    public class ZipRouter : Router
    {
        public string Unzip_app_name { set { _Unzip_app_name = value; } get { return _Unzip_app_name; } }

        private string _Unzip_app_name;


        public ZipRouter(string name, string rounte) : base(name,rounte)
        {
            _Unzip_app_name = _app_name.Replace(".zip", "");
        }
        public string GetFullPath_Unzip()
        {
            return Path.Combine(_position, _Unzip_app_name);
        }
    }
}
