using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SQLAutoDocLib.UTIL;

namespace SQLAutoDocLib.BLL
{
    public class Procedure : shared.BLLbase
    {
        private string m_Configuration = null;

        public Procedure(Guid DBID)
        {
            this.ProcedureID = Guid.Empty;
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

        public Procedure(Guid DBID, Guid ProcedureID)
        {
            this.ProcedureID = ProcedureID;
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

        public Guid ProcedureID { get; set; }
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
            using (DBL.DBProcedure_Factory oFactory = new DBL.DBProcedure_Factory())
            {
                oFactory.LoadSingleProcedure(this);
            }

        }

        public void LoadFromRow(DataRow oDR)
        {
            this.DBID = (Guid)oDR["DBID"];
            this.Name = oDR["name"].ToString();
            this.Description = DBHelper.NullToString(oDR["description"]);
            this.Configuration = DBHelper.NullToString(oDR["configuration"]);
            this.FindFirstVersion = DBHelper.NullToString(oDR["firstfindversion"]);
            this.FindLastVersion = DBHelper.NullToString(oDR["lastfindversion"]);
            this.ScanInProgress = (bool)(oDR["scaninprogress"]);
            this.CurrentlyExists = (bool)(oDR["currentlyexists"]);
            this.ChangedInLastScan = (bool)(oDR["changedinlastscan"]);

            this.ChangeDateTime = oDR["change_date_time"].ToString();
            this.ChangeUID = oDR["change_uid"].ToString();

            this.ConfigurationUpdated = false;
        }

        public void Save()
        {
            using (DBL.DBProcedure_Factory oFactory = new DBL.DBProcedure_Factory())
            {
                if (this.ProcedureID == Guid.Empty)
                {
                    this.ProcedureID = System.Guid.NewGuid();
                    oFactory.InsertSingleProcedure(this);
                }
                else
                    oFactory.UpdateSingleProcedure(this);
            }
        }
    }
}
