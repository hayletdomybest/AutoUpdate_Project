using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
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
        //private string StartPath = System.Windows.Forms.Application.StartupPath;

        public EventWaitHandle waitHandle = new AutoResetEvent(false);

        private const string TempDirName = "Temp";


        /// <summary>
        /// Download file Uri
        /// </summary>
        private string Download_Uri;

        /// <summary>
        /// Download file path info
        /// </summary>
        private ZipRouter Download_Path;


        /// <summary>
        /// Install path info
        /// </summary>
        private Router Install_Path;


        private bool IsUpdate = false;
        /// <summary>
        /// Prepare parameter for download file
        /// </summary>
        internal DownLoadForm(string[] sender)
        {
            InitializeComponent();

            string RestartFullPath, Download_FileName;

            RestartFullPath = sender[0];
            Download_Uri = sender[1];
            Download_FileName = sender[2];



            Install_Path = new Router(Path.GetFileName(RestartFullPath),
                Path.GetDirectoryName(RestartFullPath));

            Download_Path = new ZipRouter(Download_FileName,
                Path.Combine(Environment.CurrentDirectory, TempDirName));

        }
        private void TestVariable()
        {
            MessageBox.Show(string.Format("下載檔案名稱 : {0}",Download_Path.app_name));
            MessageBox.Show(string.Format("下載檔案路徑 : {0}", Download_Path.position));
            MessageBox.Show(string.Format("下載檔案完整路徑 : {0}", Download_Path.GetFullPath()));
            MessageBox.Show(string.Format("下載檔案上一個路徑 : {0}", Download_Path.GetUpPath()));

            MessageBox.Show(string.Format("下載解壓縮檔案名稱 : {0}", Download_Path.Unzip_app_name));
            MessageBox.Show(string.Format("下載解壓縮檔案路徑 : {0}", Download_Path.position));
            MessageBox.Show(string.Format("下載解壓縮檔案完整路徑 : {0}", Download_Path.GetFullPath_Unzip()));
            MessageBox.Show(string.Format("下載解壓縮檔案上一個路徑 : {0}", Download_Path.GetUpPath()));

            MessageBox.Show(string.Format("安裝檔案名稱 : {0}", Install_Path.app_name));
            MessageBox.Show(string.Format("安裝檔案路徑 : {0}", Install_Path.position));
            MessageBox.Show(string.Format("安裝檔案完整路徑 : {0}", Install_Path.GetFullPath()));
            MessageBox.Show(string.Format("安裝檔案上一個路徑 : {0}", Install_Path.GetUpPath()));

        }
        private void DownLoadForm_Load(object sender, EventArgs e)
        {
            
            List<UpdateProcess> tasks = new List<UpdateProcess>();
            DownloadFile download_Process = new DownloadFile(Download_Uri,Download_Path,this);
            UnzipFile unzip_Process = new UnzipFile(Download_Path,this);

            InstallFile install_Process = new InstallFile(Download_Path, Install_Path, this);

            tasks.Add(download_Process);
            tasks.Add(unzip_Process);
            tasks.Add(install_Process);
            Thread DownLoadThread = new Thread(new ParameterizedThreadStart(DonwLoadProcess));
            DownLoadThread.Start(tasks.ToArray());
        }

        private void DonwLoadProcess(object tasks)
        {
            foreach (UpdateProcess task in (UpdateProcess[])tasks)
            {
                if (!task.Start())
                    break;
                waitHandle.WaitOne();
            }
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
                        p.StartInfo.FileName = Install_Path.GetFullPath();
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
        public void DelegateBtn(Button btn,string str)
        {
            InvokFunc display;
            display = new InvokFunc(delegate()
            {
                btn.Text = str;
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
