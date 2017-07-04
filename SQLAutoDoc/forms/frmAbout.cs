using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Text;

namespace SQLAutoDoc.forms
{
    partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void Form_Load(object sender, EventArgs e)
        {
            StringBuilder sFormattedVersion = new StringBuilder();
            

            //Is there a "" in the same directory as the application? If so, load it up and display it as the version history
            string sVersionHistoryXML = GetResourceTextFile("VersionHistory.xml");
            if (sVersionHistoryXML.Length>0)
            {
                try
                {
                    //Attempt to load up xml
                    XmlDocument oDoc = new XmlDocument();
                    oDoc.LoadXml(sVersionHistoryXML);

                    XmlNodeList oVersionNodes = oDoc.SelectNodes("versions/version");
                    foreach (XmlNode oNode in oVersionNodes)
                    {
                        sFormattedVersion.AppendLine("Version: " + oNode.Attributes["id"].Value);

                        XmlNodeList oChangeNodes = oNode.SelectNodes("change");
                        foreach (XmlNode oChange in oChangeNodes)
                        {
                            sFormattedVersion.AppendLine("   " + oChange.InnerText);
                        }
                    }         
                }
                catch (Exception ex)
                {
                    sFormattedVersion.AppendLine("Unable to load version history: " + ex.Message);
                }
            }
            else
                sFormattedVersion.AppendLine("No version history available.");

            txtVersion.Text = sFormattedVersion.ToString();
        }

        //http://stackoverflow.com/questions/2820384/reading-embedded-xml-file-c-sharp#comment15356584_2820439
        private string GetResourceTextFile(string filename)
        {
            string sResult = "";

            using (Stream stream = this.GetType().Assembly.GetManifestResourceStream("SQLAutoDoc.VersionHistory.xml"))
            {
                //Read the stream 
                using (StreamReader sr = new StreamReader(stream))
                {
                    sResult = sr.ReadToEnd();
                } 

            } 

            return sResult;
        } 

    }
}
