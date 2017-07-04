using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace SQLAutoDoc.classes
{
    static class UIManager
    {
        public static void ShowTableDetail(
                                System.Windows.Forms.Form oParent,
                                Guid DBID,
                                string VersionID,
                                string TableName)
        {
            SQLAutoDocLib.BLL.Table_Factory oFactory = new SQLAutoDocLib.BLL.Table_Factory();
            SQLAutoDocLib.BLL.Table oTable = oFactory.GetTableByName(DBID: DBID, TableName: TableName, VersionID: VersionID);

            StringReader oReader = new StringReader(oTable.Configuration);
            XPathDocument oMyDoc = new XPathDocument(oReader);

            XsltArgumentList oArgsList = new XsltArgumentList();
            oArgsList.AddParam("CurrentlyExists", "", oTable.CurrentlyExists ? (byte)1 : (byte)0);

            XslCompiledTransform oProcessor = new XslCompiledTransform();
            string sXSL = GetResourceTextFile(oParent,"SQLAutoDoc.xsl.table.xsl");
            oProcessor.Load(XmlReader.Create(new StringReader(sXSL)));

            StringBuilder oBuffer = new StringBuilder();
            StringWriter oSWriter = new StringWriter(oBuffer);

            oProcessor.Transform(
                    XmlReader.Create(new StringReader(oTable.Configuration)),
                    oArgsList,
                    oSWriter);

            SetFormFields(
                oParent: oParent,
                VersionID: VersionID,
                Name: TableName,
                Desc: oTable.Description,
                FirstDate: oTable.FindFirstVersion,
                LastDate: oTable.FindLastVersion,
                HTML: oSWriter.ToString());

            oSWriter.Flush();
            oSWriter.Dispose();

            Application.DoEvents();
        }

        public static void ShowViewDetail(
                                System.Windows.Forms.Form oParent,
                                Guid DBID,
                                string VersionID,
                                string ViewName)
        {
            SQLAutoDocLib.BLL.View_Factory oFactory = new SQLAutoDocLib.BLL.View_Factory();
            SQLAutoDocLib.BLL.View oView = oFactory.GetViewByName(DBID: DBID, ViewName: ViewName, VersionID: VersionID);

            StringReader oReader = new StringReader(oView.Configuration);
            XPathDocument oMyDoc = new XPathDocument(oReader);

            XslCompiledTransform oProcessor = new XslCompiledTransform();
            string sXSL = GetResourceTextFile(oParent, "SQLAutoDoc.xsl.view.xsl");
            oProcessor.Load(XmlReader.Create(new StringReader(sXSL)));

            StringBuilder oBuffer = new StringBuilder();
            StringWriter oSWriter = new StringWriter(oBuffer);

            oProcessor.Transform(
                    XmlReader.Create(new StringReader(oView.Configuration)),
                    null,
                    oSWriter);

            SetFormFields(
                oParent: oParent,
                VersionID: VersionID,
                Name: ViewName,
                Desc: oView.Description,
                FirstDate: oView.FindFirstVersion,
                LastDate: oView.FindLastVersion,
                HTML: oSWriter.ToString());

            oSWriter.Flush();
            oSWriter.Dispose();

            Application.DoEvents();
        }

        public static void ShowTriggerDetail(
                                System.Windows.Forms.Form oParent,
                                Guid DBID,
                                string VersionID,
                                string TriggerName)
        {
            SQLAutoDocLib.BLL.Trigger_Factory oFactory = new SQLAutoDocLib.BLL.Trigger_Factory();
            SQLAutoDocLib.BLL.Trigger oTrigger = oFactory.GetTriggerByName(DBID: DBID, TriggerName: TriggerName,VersionID:VersionID);

            StringReader oReader = new StringReader(oTrigger.Configuration);
            XPathDocument oMyDoc = new XPathDocument(oReader);

            XslCompiledTransform oProcessor = new XslCompiledTransform();
            string sXSL = GetResourceTextFile(oParent, "SQLAutoDoc.xsl.trigger.xsl");
            oProcessor.Load(XmlReader.Create(new StringReader(sXSL)));

            StringBuilder oBuffer = new StringBuilder();
            StringWriter oSWriter = new StringWriter(oBuffer);

            oProcessor.Transform(
                    XmlReader.Create(new StringReader(oTrigger.Configuration)),
                    null,
                    oSWriter);

            SetFormFields(
                oParent: oParent,
                VersionID: VersionID,
                Name: TriggerName,
                Desc: oTrigger.Description,
                FirstDate: oTrigger.FindFirstVersion,
                LastDate: oTrigger.FindLastVersion,
                HTML: oSWriter.ToString());

            oSWriter.Flush();
            oSWriter.Dispose();

            Application.DoEvents();
        }

        public static void ShowProcDetail(
                                System.Windows.Forms.Form oParent,
                                Guid DBID,
                                string VersionID,
                                string ProcName)
        {
            SQLAutoDocLib.BLL.Procedure_Factory oFactory = new SQLAutoDocLib.BLL.Procedure_Factory();
            SQLAutoDocLib.BLL.Procedure oProcedure = oFactory.GetProcedureByName(DBID: DBID, ProcedureName: ProcName,VersionID:VersionID);

            StringReader oReader = new StringReader(oProcedure.Configuration);
            XPathDocument oMyDoc = new XPathDocument(oReader);

            XsltArgumentList oArgsList = new XsltArgumentList();
            oArgsList.AddParam("CurrentlyExists", "", oProcedure.CurrentlyExists ? (byte)1 : (byte)0);

            XslCompiledTransform oProcessor = new XslCompiledTransform();
            string sXSL = GetResourceTextFile(oParent, "SQLAutoDoc.xsl.proc.xsl");
            oProcessor.Load(XmlReader.Create(new StringReader(sXSL)));

            StringBuilder oBuffer = new StringBuilder();
            StringWriter oSWriter = new StringWriter(oBuffer);

            oProcessor.Transform(
                    XmlReader.Create(new StringReader(oProcedure.Configuration)),
                    null,
                    oSWriter);

            SetFormFields(
                oParent: oParent,
                VersionID: VersionID,
                Name: ProcName,
                Desc: oProcedure.Description,
                FirstDate: oProcedure.FindFirstVersion,
                LastDate: oProcedure.FindLastVersion,
                HTML: oSWriter.ToString());

            oSWriter.Flush();
            oSWriter.Dispose();

            Application.DoEvents();
        }

        public static void ShowFunctionDetail(
                                System.Windows.Forms.Form oParent,
                                Guid DBID,
                                string VersionID,
                                string FunctionName)
        {
            SQLAutoDocLib.BLL.Function_Factory oFactory = new SQLAutoDocLib.BLL.Function_Factory();
            SQLAutoDocLib.BLL.Function oFunction = oFactory.GetFunctionByName(DBID: DBID, FunctionName: FunctionName, VersionID: VersionID);

            StringReader oReader = new StringReader(oFunction.Configuration);
            XPathDocument oMyDoc = new XPathDocument(oReader);

            XslCompiledTransform oProcessor = new XslCompiledTransform();
            string sXSL = GetResourceTextFile(oParent, "SQLAutoDoc.xsl.function.xsl");
            oProcessor.Load(XmlReader.Create(new StringReader(sXSL)));

            StringBuilder oBuffer = new StringBuilder();
            StringWriter oSWriter = new StringWriter(oBuffer);

            oProcessor.Transform(
                    XmlReader.Create(new StringReader(oFunction.Configuration)),
                    null,
                    oSWriter);

            SetFormFields(
                oParent: oParent,
                VersionID: VersionID,
                Name: FunctionName,
                Desc:oFunction.Description,
                FirstDate: oFunction.FindFirstVersion,
                LastDate: oFunction.FindLastVersion,
                HTML: oSWriter.ToString());

            oSWriter.Flush();
            oSWriter.Dispose();

            Application.DoEvents();
        }

        //http://stackoverflow.com/questions/2820384/reading-embedded-xml-file-c-sharp#comment15356584_2820439
        //filename == namespace.folder.file.ext
        public static string GetResourceTextFile(
                                    System.Windows.Forms.Form oParent,
                                    string filename)
        {
            string sResult = "";

            using (Stream stream = oParent.GetType().Assembly.GetManifestResourceStream(filename))
            {
                //Read the stream 
                using (StreamReader sr = new StreamReader(stream))
                {
                    sResult = sr.ReadToEnd();
                }

            }

            return sResult;
        }

        private static void SetFormFields(
                        System.Windows.Forms.Form oParent,
                        string VersionID,
                        string Name,
                        string Desc,
                        string FirstDate,
                        string LastDate,
                        string HTML)
        {
            Label lblName = (Label)oParent.Controls.Find("lblName", true)[0];
            TextBox txtDesc = (TextBox)oParent.Controls.Find("txtDesc", true)[0];
            Label lblFirstDate = (Label)oParent.Controls.Find("lblFirstDate", true)[0];
            Label lblLastDate = (Label)oParent.Controls.Find("lblLastDate", true)[0];
            WebBrowser htmView = (WebBrowser)oParent.Controls.Find("html", true)[0];

            lblName.Text = Name;
            txtDesc.Text = Desc;
            lblFirstDate.Text = FirstDate;
            lblLastDate.Text = LastDate;
            htmView.DocumentText = HTML;

            //which view is this? If this is a detail form view, then show the version id as well.
            Control[] lblVersion = oParent.Controls.Find("lblVersion", true);
            if (lblVersion.Length ==1)
            {
                lblVersion[0].Text = VersionID;
            }
        }
    }
}
