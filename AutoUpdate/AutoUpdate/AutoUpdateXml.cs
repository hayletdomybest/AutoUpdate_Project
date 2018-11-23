using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace AutoUpdate
{


    /// <summary>
    /// This class is implement for handle XML file
    /// </summary>
    public class AutoUpdateXml
    {
        /// <summary>
        /// Check uri whether or not Exist
        /// </summary>
        /// <param name="local">Uniform resource identifier</param>
        /// <returns>True: Exist False: dosen't Exist</returns>
        public static bool IsExistServer(Uri local)
        {
            try
            {
                
                ServicePointManager.ServerCertificateValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true; //ignore ssl Certificate
                HttpWebRequest request =  (HttpWebRequest)WebRequest.Create(local);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //Stream ftpStream = response.GetResponseStream();
                response.Close();
                //ftpStream.Close();
                return (response.StatusCode == HttpStatusCode.OK);
                

            }
            catch
            {
                return false;
            }
        }

    
        /// <summary>
        /// Analy Server Xml information
        /// </summary>
        /// <param name="server">Server uri</param>
        /// <returns>Xml information which type is UpdateInfo</returns>
        public static UpdateInfo XmlParse(Uri server)
        {
            ServicePointManager.ServerCertificateValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => true;//ignore ssl Certificate

            UpdateInfo info = new UpdateInfo();
            XmlDocument xmldoc = new XmlDocument();
            try{
                xmldoc.Load(server.AbsoluteUri);
                XmlNode node = xmldoc.FirstChild;
                XmlNodeList list = node.ChildNodes;
                foreach (XmlNode n in list)
                {
                    switch (n.Name)
                    { 
                        case "Version":
                            info._Version = new Version(n.InnerText);
                            break;
                        case "Uri":    
                            info._Uri = new Uri(n.InnerText);
                            break;
                        case "FileName":
                            info._FileName = n.InnerText;
                            break;
                    } 
                } 
            }
            catch{
                AutoUpdate.Debug_Error("下載資料有誤");
            }
            return info;
        }
    }
}
