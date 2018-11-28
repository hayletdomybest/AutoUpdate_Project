using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace DownLoadForm
{
    public class Router
    {
        public string app_name { set { _app_name = value; } get { return _app_name; } }

        public string position { set { _position = value; } get { return _position; } }


        protected string _app_name;
        protected string _position;

        public Router(string name, string rounte)
        {
            _app_name = name;
            _position = rounte;
        }
/*
        public Router(string full_path)
        {
            _app_name = Path.GetFileName(full_path);
            _position = Path.GetDirectoryName(full_path);
        }
*/

        /// <summary>
        /// Get uppper router  ex curr:home/temp  upper: home
        /// </summary>
        public string GetUpPath()
        {
            return Path.GetDirectoryName(_position);
        }

        public string GetFullPath()
        {
            return Path.Combine(_position, _app_name);
        }


    }
}
