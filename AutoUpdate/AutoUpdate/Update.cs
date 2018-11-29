using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AutoUpdate
{
    
    public class Update
    {
        //public delegate void _UpdateComplete();
        public EventHandler UpdateComplete;

        /// <summary>
        /// Thread to find update
        /// </summary>
        private BackgroundWorker Bg_checkUpdateInfo;

        private UpdateInfo ServerUpdateInfo;

        /// <summary>
        /// Uri of the update xml on the server
        /// </summary>
        private Uri UpdateXmlServer;


        /// <summary>
        /// Version of Local
        /// </summary>
        private Version LocalVersion;

        /// <summary>
        /// Creates a new AutoUpdate object
        /// </summary>
        /// <param name="a">Parent ssembly to be attached</param>
        /// <param name="owner">Parent form to be attached</param>
        /// <param name="XMLOnServer">Uri of the update xml on the server</param>
        public Update(Uri server, Version location)
        {
            UpdateXmlServer = server;
            LocalVersion = location;

            // Set up backgroundworker
            Bg_checkUpdateInfo = new BackgroundWorker();
            Bg_checkUpdateInfo.DoWork += new DoWorkEventHandler(CheckUpdateInfo);
            Bg_checkUpdateInfo.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Bg_checkUpdateInfo_completed);
        }

        /// <summary>
        /// Update data
        /// </summary>
        public void DoUpdate()
        {
            if (!Bg_checkUpdateInfo.IsBusy)
                Bg_checkUpdateInfo.RunWorkerAsync();
        }


        /// <summary>
        /// Checks for/parses update.xml on server
        /// </summary>
        private void CheckUpdateInfo(object sender, DoWorkEventArgs e)
        {
            // Check for update on server
            e.Cancel = (!AutoUpdateXml.IsExistServer(UpdateXmlServer));

            if (e.Cancel){
                MessageBox.Show("下載路徑失效");
                return;
            }
            
            ServerUpdateInfo = AutoUpdateXml.XmlParse(UpdateXmlServer);

        }


        /// <summary>
        /// After the background worker is done, prompt to update if there is one
        /// </summary>
        private void Bg_checkUpdateInfo_completed(object sender, RunWorkerCompletedEventArgs e)
        {
            bool IsUpdate = false;
            // If there is a file on the server
            if (e.Cancelled)
                return;

            
            if (IsNeedUpdate(ServerUpdateInfo._Version,LocalVersion))
            {
                AcceptForm acceptform = new AcceptForm(ServerUpdateInfo);
                acceptform.ShowDialog();
                IsUpdate = (acceptform.DialogResult == DialogResult.Yes);
            }
            if (UpdateComplete != null)
                if(IsUpdate)
                    UpdateComplete(this,null);
            
        }
        /// <summary>
        /// Check version if need update return true 
        /// </summary>
        /// <returns>True: Need update False :Not need</returns>
        public  bool IsNeedUpdate(Version Server, Version local)
        {
            return (Server > local);
        }


        /// <summary>
        /// Start Download application
        /// </summary>
        /// <param name="path">Download application path</param>
        /// <param name="openPath">Restart Application path</param>
        public void StartDownLoad(string start)
        {
            /*
            --------Past to Download application Parameter description:---------
             
            1. RestartFullPath    ------Download finish then where application start
            2. Download_Uri       ------Download Uri
            3. Download_FileName  ------Download file name
            */
            string RestartFullPath = System.Windows.Forms.Application.ExecutablePath;
            string Download_Uri = ServerUpdateInfo._Uri.ToString();
            string Download_FileName = ServerUpdateInfo._FileName;


            //Parameter for passing to DownloadForm 
            ArgsBuilder args = new ArgsBuilder();

            //For start process application information 
            ProcessStartInfo pInfo = new ProcessStartInfo(start);

            //Parameter[1] = current process name
            args.Add(RestartFullPath);

            args.Add(Download_Uri);

            args.Add(Download_FileName);

            /*
            RestartFullPath = sender[0];
            Download_Uri = sender[1];
            Download_FileName = sender[2];
            */
            pInfo.Arguments = args.ToString();
            try
            {
                using (Process p = new Process())
                {
                    p.StartInfo = pInfo;
                    p.Start();
                }
            }
            catch(Exception e) {
                Debug_Error("下載程式路徑錯誤");
                //Debug_Error(e.ToString());
            }
            Application.Exit();
        }


        public static void Debug_Error(string err)
        {
            MessageBox.Show(err);
            Application.Exit();
        }
    }
}
