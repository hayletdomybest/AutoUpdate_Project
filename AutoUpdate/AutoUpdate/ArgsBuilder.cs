using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoUpdate
{
    public class ArgsBuilder
    {
        StringBuilder _arg = new StringBuilder();
        public ArgsBuilder()
        {
            _arg.Append("");
        }

        /// <summary>
        /// add parameter to application
        /// </summary>
        /// <param name="str"></param>
        public void Add(string str)
        {
            /*------if last string is "\\" then will be define '\'  
             *------so prevention this case--------------------*/

            if (str.EndsWith("\\")) 
            {
                str += "\\";
            }
            _arg.AppendFormat("\"{0}\"", str);
            _arg.Append(" "); //divid parameter
        }
        public override string ToString()
        {
            return _arg.ToString();
        }
    }
}
