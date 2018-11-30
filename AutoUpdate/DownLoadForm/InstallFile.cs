using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace DownLoadForm
{
    public class InstallFile : UpdateProcess
    {
        private Router InstallInfo;

        private ZipRouter SourceInfo;
        public InstallFile(ZipRouter source,Router dir,DownLoadForm father)
        {
            SourceInfo  = source;
            InstallInfo = dir;
            ParentForm = father;
        }

                    
        public override bool Start()
        {
            ParentForm.DelegateLable(ParentForm.lab_Title,"安裝更新檔");

            //ParentForm.DelegateShowError("安裝來源: " + SourceInfo.GetFullPath_Unzip());
            //ParentForm.DelegateShowError("安裝位置: " + InstallInfo.position);
            DirectoryInfo source =new DirectoryInfo(SourceInfo.GetFullPath_Unzip());
            DirectoryInfo dir =new DirectoryInfo(InstallInfo.position);
            try
            {
                CopyFilesRecursively(source, dir);
            }
            catch (Exception ex) {

                ParentForm.DelegateShowError("安裝失敗");
            }
            return base.Start();
        }

        protected override void Finish()
        {
            ParentForm.DelegateLable(ParentForm.lab_Title, "更新完成");
            ParentForm.DelegateBar(ParentForm.bar_rate, 100);
            ParentForm.DelegateBtn(ParentForm.btn_Cancel, "完成");
            File.Delete(SourceInfo.GetFullPath());
            Directory.Delete(SourceInfo.GetFullPath_Unzip(), true);
            base.Finish();
        }

        private static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
        }
    }
}
