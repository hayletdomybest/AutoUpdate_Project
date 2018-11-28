using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;
using System.Windows.Forms;
namespace DownLoadForm
{
    public class UnzipFile : UpdateProcess
    {
        private ZipRouter UnzipInfo;

        public UnzipFile(ZipRouter info,DownLoadForm father)
        {
            UnzipInfo = info;
            ParentForm = father;
        
        }


        /// <summary>
        /// Unzip File about unzip information
        /// </summary>
        public override bool Start()
        {
            ParentForm.DelegateLable(ParentForm.lab_Title, "更新檔解壓縮");
            ZipFile unzip;
            try
            {
                //MessageBox.Show("壓縮來源: " + UnzipInfo.GetFullPath());
                //MessageBox.Show("壓縮地址: " + UnzipInfo.GetFullPath_Unzip());
                unzip = ZipFile.Read(UnzipInfo.GetFullPath());

                foreach (ZipEntry e in unzip)
                {
                    e.Extract(UnzipInfo.GetFullPath_Unzip(),
                        ExtractExistingFileAction.OverwriteSilently);

                }
                unzip.Dispose();

            }
            catch (Exception e)
            {
                ParentForm.DelegateShowError("解壓縮失敗");
                //ParentForm.DelegateShowError(e.ToString());
                return false;
            }
            finally
            {
                GC.Collect();
            }

            return base.Start();
        }
    }
}
