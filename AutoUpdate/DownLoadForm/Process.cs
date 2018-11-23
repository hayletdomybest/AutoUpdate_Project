using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace DownLoadForm
{
    public class UpdateProcess
    {
        public DownLoadForm ParentForm { set; get; }

        public virtual bool Start() { return true; }
        //public virtual bool Finish(object sender, EventArgs e) { return true; }
    }
}
