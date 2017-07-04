using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.BLL
{
    public class Function : shared.BLLbase
    {
        private string m_Configuration=null;

      public Function(Guid DBID)
        {
            this.FunctionID = Guid.Empty;
            this.DBID = DBID;
            this.Name = "";
            this.Description = "";

            this.Configuration = "";
            this.FindFirstVersion = "";
            this.FindLastVersion = "";
            this.CurrentlyExists = true;
            this.ScanInProgress = false;
            this.ConfigurationUpdated = false;
        }

      public Function(Guid DBID, Guid FunctionID)
        {
            this.FunctionID = FunctionID;
            this.DBID = DBID;
            this.Name = "";
            this.Description = "";

            this.Configuration = "";
            this.FindFirstVersion = "";
            this.FindLastVersion = "";
            this.CurrentlyExists = true;
            this.ScanInProgress = false;
            this.ConfigurationUpdated = false;
        }

      public Guid FunctionID { get; set; }
        public Guid DBID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FindFirstVersion { get; set; }
        public string FindLastVersion { get; set; }
        public bool CurrentlyExists { get; set; }
        public bool ScanInProgress { get; set; }

        public string Configuration
        {
            get { return m_Configuration; }
            set
            {
                m_Configuration = value;
                if (value.Length>0)
                    this.ConfigurationUpdated = true;
            }
        }
        internal bool ConfigurationUpdated { get; set; }

        public void Load()
        {
            using (DBL.DBFunction_Factory oFactory = new DBL.DBFunction_Factory())
            {
                oFactory.LoadSingleFunction(this);
            }

        }

        public void Save()
        {
            using (DBL.DBFunction_Factory oFactory = new DBL.DBFunction_Factory())
            {
                if (this.FunctionID == Guid.Empty)
                {
                    this.FunctionID = System.Guid.NewGuid();
                    oFactory.InsertSingleFunction(this);
                }
                else
                    oFactory.UpdateSingleFunction(this);
            }
        }
    }
}
