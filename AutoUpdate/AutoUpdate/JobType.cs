using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoUpdate
{
    public enum JobType
    {
        UPDATE,
        REMOVE
    }

    public class UpdateInfo
    {
        //Private innounce
        public Version _Version;

        public Uri _Uri;

        public string _FileName;
    }
}
