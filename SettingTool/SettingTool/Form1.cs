using System;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using AutoUpdate;
namespace SettingTool
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Download information Server address
        /// </summary>
        private const string Server = @"https://raw.githubusercontent.com/t628x7600/AutoUpdate_Project/master/updateInfo.xml";


        /// <summary>
        /// Previous update information address
        /// </summary>
        private readonly Version local = Assembly.GetEntryAssembly().GetName().Version;


        /// <summary>
        /// Download application address
        /// </summary>
        private const string DownLoadFormName = "DownLoadForm.exe";

        private string DownLoadFormFullPath = Path.Combine(Environment.CurrentDirectory,
                                                           DownLoadFormName);

        private Update UpdateInterface;
       


        public Form1()
        {
            InitializeComponent();
            Uri uri = new Uri(Server);
            UpdateInterface = new AutoUpdate.Update(uri, local,1000,3000);
            UpdateInterface.UpdateComplete += new EventHandler(UpdateComplete);
            UpdateInterface.DoUpdate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lab_version.Text = "當前版本號碼" + "\n     " + ProductVersion;
        }
        private void UpdateComplete(object sneder, EventArgs args)
        {
            //download application path
            UpdateInterface.StartDownLoad(DownLoadFormFullPath);
        }

    }
}
