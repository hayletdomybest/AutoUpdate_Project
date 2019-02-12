using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
namespace AutoUpdate
{
    
    public class Update
    {
        //public delegate void _UpdateComplete();
        public EventHandler UpdateComplete;

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
        /// For Check update informate 
        /// </summary>
        private System.Threading.Timer checkInfo;

        
        private bool IsSetTimmer = false;

        /// <summary>
        /// Timmer wait ms time to start
        /// </summary>
        public int dueTime { get; set; }

        /// <summary>
        /// Timmer period(ms)
        /// </summary>
        public int period { get; set; }

        private bool _lock = false;
        /// <summary>
        /// Creates a new AutoUpdate object
        /// </summary>
        public Update(Uri server, Version location,int durTime,int period)
        {
            UpdateXmlServer = server;
            LocalVersion = location;
            this.dueTime = dueTime;
            this.period = period;
            IsSetTimmer = true;
        }
        /// <summary>
        /// Creates a new AutoUpdate object
        /// </summary>
        public Update(Uri server, Version location) 
        {
            UpdateXmlServer = server;
            LocalVersion = location;
            this.dueTime = 0;
            this.period = 3000;
        }

        /// <summary>
        /// Update data
        /// </summary>
        public void DoUpdate()
        {
            if (UpdateComplete == null)
            {
                MessageBox.Show("請實作檢查更新完成事件");
                return;
            }

            TimerCallback checkInfoCallBack = new TimerCallback(CheckUpdateInfo);
            checkInfo = new System.Threading.Timer(checkInfoCallBack, null, dueTime, period);
        
        }


        /// <summary>
        /// Checks for/parses update.xml on server
        /// </summary>
        private void CheckUpdateInfo(object sender)
        {
            if (_lock)
                return;
            _lock = true;
            // Check for update on server
            //AutoResetEvent autoReset = (AutoResetEvent)sender;
            if (!AutoUpdateXml.IsExistServer(UpdateXmlServer))
            {
                MessageBox.Show("下載路徑失效");
            }
            else
            {
                ServerUpdateInfo = AutoUpdateXml.XmlParse(UpdateXmlServer);
                CheckUpdate_State result = Bg_checkUpdateInfo_completed();

                switch (result)
                {
                    case CheckUpdate_State.Update:
                        UpdateComplete(this, null);
                        break;
                    case CheckUpdate_State.Cancel:
                        checkInfo.Dispose();
                        break;
                    default:
                        if (!IsSetTimmer)
                            checkInfo.Dispose();
                        break;
                }
            }
            _lock = false;
       }


        /// <summary>
        /// After the background worker is done, prompt to update if there is one
        /// </summary>
        private CheckUpdate_State Bg_checkUpdateInfo_completed()
        {
            
            // If there is a file on the server
            if (!IsNeedUpdate(ServerUpdateInfo._Version,LocalVersion))
                return CheckUpdate_State.lastest;
   
            AcceptForm acceptform = new AcceptForm(ServerUpdateInfo);
            acceptform.ShowDialog();
            return (acceptform.DialogResult == DialogResult.Yes) ? CheckUpdate_State.Update :
                                                                   CheckUpdate_State.Cancel ;        
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


public enum CheckUpdate_State
{ 
    lastest,
    Update,
    Cancel
}
