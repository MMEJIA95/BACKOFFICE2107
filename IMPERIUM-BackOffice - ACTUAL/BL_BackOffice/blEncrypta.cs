using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DA_BackOffice;

namespace BL_BackOffice
{
    public class blEncrypta
    {
        readonly daEncrypta _crypt;
        public blEncrypta(string key) { _crypt = new daEncrypta(key); }

        public string Encrypta(string valor) { return _crypt.Encrypta(valor); }
        public string Desencrypta(string valor) { return _crypt.Desencrypta(valor); }

        public void EncryptaFile() { string file_path = Application.StartupPath + "\\sql_reports.config"; _crypt.EncryptFile(file_path, "Jad-Soft"); }
        public void DesencryptaFile() { string file_path = Application.StartupPath + "\\sql_reports.config"; _crypt.DecryptFile(file_path, "Jad-Soft"); }

        #region Espacio para encriptado - appSettings
        public void EncryptaAppSettings()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes == null || node.Attributes.Count == 0) continue;

                        node.Attributes[0].Value = _crypt.Encrypta(node.Attributes[0].Value.ToString());
                        node.Attributes[1].Value = _crypt.Encrypta(node.Attributes[1].Value.ToString());
                    }
                }
                EncryptaConnectionString();
            }
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            ConfigurationManager.RefreshSection("appSettings");
        }

        public void EncryptaConnectionString()
        {
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            //foreach (XmlElement element in xmlDoc.DocumentElement)
            //{
            //    if (element.Name.Equals("connectionStrings"))
            //    {
            //        foreach (XmlNode node in element.ChildNodes)
            //        {
            //            if (node.Attributes == null || node.Attributes.Count == 0) continue;
            //            for (int i = 0; i < node.Attributes.Count; i++)
            //            {
            //                //MessageBox.Show(node.Attributes[i].Value, node.Attributes[i].Name);
            //                if (node.Attributes[i].Name.Equals("connectionString"))
            //                {
            //                    string value = node.Attributes[i].Value.ToString();
            //                    node.Attributes[i].Value = _crypt.Encrypta(value);
            //                }
            //            }
            //        }
            //    }
            //}
            //xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            //ConfigurationManager.RefreshSection("appSettings");
        }

        public void DesencryptaAppSettings()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes == null || node.Attributes.Count == 0) continue;

                        node.Attributes[0].Value = _crypt.Desencrypta(node.Attributes[0].Value.ToString());
                        node.Attributes[1].Value = _crypt.Desencrypta(node.Attributes[1].Value.ToString());
                    }
                }
                DesencryptaConnectionStrings();
            }
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            ConfigurationManager.RefreshSection("appSettings");
        }

        public void DesencryptaConnectionStrings()
        {
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            //foreach (XmlElement element in xmlDoc.DocumentElement)
            //{
            //    if (element.Name.Equals("connectionStrings"))
            //    {
            //        foreach (XmlNode node in element.ChildNodes)
            //        {
            //            if (node.Attributes == null || node.Attributes.Count == 0) continue;
            //            for (int i = 0; i < node.Attributes.Count; i++)
            //            {
            //                //MessageBox.Show(node.Attributes[i].Value, node.Attributes[i].Name);
            //                if (node.Attributes[i].Name.Equals("connectionString"))
            //                {
            //                    string value = node.Attributes[i].Value.ToString();
            //                    node.Attributes[i].Value = _crypt.Desencrypta(value);
            //                }
            //            }
            //        }
            //    }
            //}
            //xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            //ConfigurationManager.RefreshSection("appSettings");
        }

        #endregion
    }
}