using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;
namespace DownLoadForm
{
    public class UnzipFile : UpdateProcess
    {
        private string UnZipFilePath; //Compress file path

        private string StoragePath;     //Unzip file storage path
        public UnzipFile(string unzipPath,string storagePath,DownLoadForm father)
        {
            UnZipFilePath = unzipPath;
            StoragePath = storagePath;
            ParentForm = father;
        
        }
        public override bool Start()
        {
            ParentForm.DelegateLable(ParentForm.lab_Title, "更新檔解壓縮");

            return UnZipFiles(UnZipFilePath, StoragePath, null);
        }

        
        /// <summary>
        /// Unzip-Files
        /// </summary>
        /// <param name="soruce">Unzip File path</param>
        /// <param name="dir">storage Path</param>
        private bool UnZipFiles(string soruce, string dir, string password)
        {
            ZipFile unzip;
            try
            {
                string testRoot = Environment.CurrentDirectory + @"\Temp\SettingTool.zip";
                ParentForm.DelegateShowError(testRoot);
                unzip = ZipFile.Read(testRoot);
                if (password != null && password != string.Empty) unzip.Password = password;

                foreach (ZipEntry e in unzip)
                {
                    e.Extract(dir, ExtractExistingFileAction.OverwriteSilently);
                }
                unzip.Dispose();

            }
            catch (Exception e)
            {
                //ParentForm.DelegateShowError("解壓縮失敗");
                ParentForm.DelegateShowError(e.ToString());
                return false;
            }
            finally
            {
                
                GC.Collect();
            }
            return true;
        }
    }
}
