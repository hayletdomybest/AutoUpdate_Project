using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DownLoadForm
{
    public class UpdateProcess
    {
        protected DownLoadForm ParentForm;

        public virtual bool Start() 
        {
            Finish();
            return true;
        }
        protected virtual void Finish()
        {
            ParentForm.waitHandle.Set(); 
        }
    }
}
