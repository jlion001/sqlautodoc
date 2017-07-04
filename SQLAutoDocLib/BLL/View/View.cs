using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.BLL
{
    public class View : shared.BLLbase
    {
        private string m_Configuration = null;

     public View(Guid DBID)
        {
            this.ViewID = Guid.Empty;
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

        public View(Guid DBID, Guid ViewID)
        {
            this.ViewID = ViewID;
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

        public Guid ViewID { get; set; }
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
                if (value.Length > 0)
                    this.ConfigurationUpdated = true;
            }
        }
        internal bool ConfigurationUpdated { get; set; }

        public void Load()
        {
            using (DBL.DBView_Factory oFactory = new DBL.DBView_Factory())
            {
                oFactory.LoadSingleView(this);
            }

        }

        public void Save()
        {
            using (DBL.DBView_Factory oFactory = new DBL.DBView_Factory())
            {
                if (this.ViewID == Guid.Empty)
                {
                    this.ViewID = System.Guid.NewGuid();
                    oFactory.InsertSingleView(this);
                }
                else
                    oFactory.UpdateSingleView(this);
            }
        }
    }
}
