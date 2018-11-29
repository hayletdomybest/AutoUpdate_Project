using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using AutoUpdate;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Xml;
namespace TestApp
{
    public partial class TestForm : Form
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
        
        private AutoUpdate.AutoUpdate UpdateInterface;
        
        public TestForm()
        {
            InitializeComponent();
            Uri uri = new Uri(Server);
            UpdateInterface = new AutoUpdate.AutoUpdate(uri, local);
            UpdateInterface.UpdateComplete += new EventHandler(UpdateComplete);
        }

        private void btn_check_Click(object sender, EventArgs e)
        {            
            UpdateInterface.DoUpdate();
        }
        
        private  void UpdateComplete(object sneder,EventArgs args)
        {
            //download application path
            UpdateInterface.StartDownLoad(DownLoadFormFullPath);  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "當前版本號碼" + "\n     " + ProductVersion; 
        }
    }
}
