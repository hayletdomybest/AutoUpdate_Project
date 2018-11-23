using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using System.Diagnostics;
using System.IO;

using AutoUpdate;
namespace DownLoadForm
{
    public delegate void InvokFunc();
    public partial class DownLoadForm : Form
    {
        private string StartPath = System.Windows.Forms.Application.StartupPath;

        private const string DownLoadFilePath = "Temp";

        private string DownLoadFileFullPath = Path.Combine(Environment.CurrentDirectory,
                                                            DownLoadFilePath);

        private string UnZipFilePath;

        /// <summary>
        /// Where update file which is zip file
        /// </summary>
        private string UnZipFileFullPath;

        /// <summary>
        /// Restart Exe file Path 
        /// </summary>
        private string RestartPath;

        /// <summary>
        /// Restart Exe file full Path
        /// </summary>
        private string RestartFullPath;

        /// <summary>
        /// Download data uri
        /// </summary>
        private string Download_Uri;


        /// <summary>
        /// Download data file name
        /// </summary>
        private string Download_FileName;
        
        private bool IsUpdate = false;


        /// <summary>
        /// Prepare parameter for download file
        /// </summary>
        internal DownLoadForm(string[] sender)
        {
            InitializeComponent();
            
            RestartFullPath = sender[0];
            Download_Uri = sender[1];
            Download_FileName = sender[2];

            RestartPath = Path.GetDirectoryName(RestartFullPath);
            
            //DownLoadFilePath = Path.Combine(StartPath, DownLoadFilePath);
            
            DownLoadFileFullPath = Path.Combine(DownLoadFilePath, Download_FileName);
            
            UnZipFilePath = DownLoadFilePath;
            UnZipFileFullPath = DownLoadFileFullPath.Replace(".zip", "");
        }
        private void TestVariable()
        {
            MessageBox.Show(string.Format("RestartFullPath = {0}", RestartFullPath));
            MessageBox.Show(string.Format("Download_Uri = {0}", Download_Uri));
            MessageBox.Show(string.Format("Download_FileName = {0}", Download_FileName));
            MessageBox.Show(string.Format("DownLoadFilePath = {0}", DownLoadFilePath));
            MessageBox.Show(string.Format("DownLoadFileFullPath = {0}", DownLoadFileFullPath));
            MessageBox.Show(string.Format("UnZipFilePath = {0}", UnZipFilePath));
            MessageBox.Show(string.Format("UnZipFileFullPath = {0}", UnZipFileFullPath));
        }
        private void DownLoadForm_Load(object sender, EventArgs e)
        {
            //TestVariable();
            List<UpdateProcess> tasks = new List<UpdateProcess>();
            DownloadFile download_Process = new DownloadFile(Download_Uri,Download_FileName,
                                                             DownLoadFilePath,this);
            UnzipFile unzip_Process = new UnzipFile(DownLoadFileFullPath, UnZipFileFullPath,this);
            
            tasks.Add(download_Process);
            tasks.Add(unzip_Process);
            Thread DownLoadThread = new Thread(new ParameterizedThreadStart(DonwLoadProcess));
            DownLoadThread.Start(tasks.ToArray());
        }

        private void DonwLoadProcess(object tasks)
        {
            foreach (UpdateProcess task in (UpdateProcess[])tasks)
            {
                task.Start();
                Thread.Sleep(1);
            }
        }
               
        private void mvFile(object sender, DoWorkEventArgs e)
        {
            lab_Title.Text = "安裝更新檔";
            DirectoryInfo source =new DirectoryInfo(UnZipFileFullPath);
            DirectoryInfo dir =new DirectoryInfo(RestartPath);
            try
            {
                CopyFilesRecursively(source, dir);
            }
            catch (Exception ex) {

                AutoUpdate.AutoUpdate.Debug_Error(ex.ToString());
            }
        }
      
        private void mvFile_compelete(object sender, RunWorkerCompletedEventArgs e)
        {
            bar_rate.Value = 100;
            lab_Title.Text = "更新完成";
            btn_Cancel.Text = "完成";
            File.Delete(DownLoadFileFullPath);
            Directory.Delete(UnZipFileFullPath, true);
        }

        private static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name),true);
        }






        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (!this.IsUpdate)
                Application.Exit();
            else
            {
                try
                {
                    using (Process p = new Process())
                    {
                        p.StartInfo.FileName = RestartFullPath;
                        p.Start();
                    }
                    Application.Exit();
                }
                catch
                {
                    MessageBox.Show("Setting Tool 開啟失敗");
                }
            }
        }



        /*------------------In this section implement delegate 
         *------------------which help another thread to chang UI item
         *-----------------------------------------------------------------*/

        //------------------------Chang TextBox-------------
        public void DelegateTextBox(TextBox txtbox,string str)
        {
            InvokFunc display;

            display = new InvokFunc(delegate()
                                    {
                                        txtbox.Text = str;
                                    });
            display.Invoke();  
        }
        //---------------------------------------------------

        //------------------------Chang Lable-------------
        public void DelegateLable(Label lab, string str)
        {
            InvokFunc display;
            display = new InvokFunc(delegate()
                                    {
                                        lab.Text = str;
                                    });
            display.Invoke();
        }
        //-------------------------------------------------

        //------------------------Chang ProgressBar---------
        public void DelegateBar(ProgressBar bar, int val)
        {
            InvokFunc display;
            display = new InvokFunc(delegate()
                                    {
                                        bar.Value = val;
                                    });
            display.Invoke();
        }

        public void DelegateShowError(string str)
        {
            InvokFunc display = new InvokFunc(delegate()
                                              {
                                                MessageBox.Show(str);
                                              });
            display.Invoke();
        }

    }
}
