using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.IO;
namespace DownLoadForm
{
    public class DownloadFile :UpdateProcess
    {
        /// <summary>
        /// Download data uri
        /// </summary>
        private string Download_Uri;

        private string DownLoadFilePath;

        private string DownLoadFileFullPath;

        private string DownloadFileName;

        private long DowloadFileSize;
        public DownloadFile(string uri,string FileName,string TempPath,DownLoadForm father)
        {
            Download_Uri = uri;
            DownLoadFilePath = TempPath;
            DownLoadFileFullPath = Path.Combine(DownLoadFilePath, FileName);
            DownloadFileName = FileName;
            ParentForm = father;
            MessageBox.Show(DownLoadFilePath);
            MessageBox.Show(DownLoadFileFullPath);
        }


        public override bool Start()
        {
            WebClient DownloadClient;
            HttpWebRequest Httpreques;
            HttpWebResponse Httpresponse;
            ServicePointManager.ServerCertificateValidationCallback =
                        (sender, cert, chain, sslPolicyErrors) => true;//ignore ssl Certificate
            DownloadClient = new WebClient();
            DownloadClient.DownloadProgressChanged += DownloadProgressChanged;
            DownloadClient.DownloadFileCompleted += DownLoadFinish;
            try
            {
                Httpreques = (HttpWebRequest)HttpWebRequest.Create(Download_Uri);
                Httpresponse = (HttpWebResponse)Httpreques.GetResponse();
                DowloadFileSize = Httpresponse.ContentLength;
                ParentForm.DelegateLable(ParentForm.lab_FileName, DownloadFileName);
                if (!Directory.Exists(DownLoadFilePath))
                    Directory.CreateDirectory(DownLoadFilePath);
                DownloadClient.DownloadFileAsync(new Uri(Download_Uri),
                    DownLoadFileFullPath);
            }
            catch (Exception ex)
            {
                AutoUpdate.AutoUpdate.Debug_Error("下載失敗");
                //AutoUpdate.AutoUpdate.Debug_Error(ex.ToString());
                return false;
            }
            return true;
        }

        private void DownLoadFinish(object sender, EventArgs e)
        {
            ParentForm.DelegateLable(ParentForm.lab_Title, "更新檔下載完成");
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            string DisplayBytes = FormatBytes(e.BytesReceived, 1, false) + "/" + FormatBytes(DowloadFileSize, 1, true);
            ParentForm.DelegateLable(ParentForm.lab_FileSize, DisplayBytes);
            int BarValue = (int)((e.ProgressPercentage) * 0.8);
            ParentForm.DelegateBar(ParentForm.bar_rate, BarValue);
        }

        private string FormatBytes(long bytes, int decimalPlaces, bool showByteType)
        {
            double newBytes = bytes;
            string formatString = "{0";
            string byteType;

            // Check if best size in KB
            if (newBytes > 1024 && newBytes < 1048576)
            {
                newBytes /= 1024;
                byteType = "KB";
            }
            else if (newBytes > 1048576 && newBytes < 1073741824)
            {
                // Check if best size in MB
                newBytes /= 1048576;
                byteType = "MB";
            }
            else if (newBytes > 1073741824)
            {
                // Best size in GB
                newBytes /= 1073741824;
                byteType = "GB";
            }
            else
                byteType = "B";

            // Show decimals
            if (decimalPlaces > 0)
                formatString += ":0.";

            // Add decimals
            for (int i = 0; i < decimalPlaces; i++)
                formatString += "0";

            // Close placeholder
            formatString += "}";

            // Add byte type
            if (showByteType)
                formatString += byteType;

            return string.Format(formatString, newBytes);
        }

    }
}
