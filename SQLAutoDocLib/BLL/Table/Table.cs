using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SQLAutoDocLib.UTIL;

namespace SQLAutoDocLib.BLL
{
    public class Table:shared.BLLbase
    {
        private string m_Configuration = null;

        public Table(Guid DBID)
        {
            this.TableID = Guid.Empty;
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

        public Table(Guid DBID,Guid TableID)
        {
            this.TableID = TableID;
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

        public Guid TableID { get; set; }
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
            using (DBL.DBTable_Factory oFactory = new DBL.DBTable_Factory())
            {
                oFactory.LoadSingleTable(this);
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
            this.ChangedInLastScan =(bool)(oDR["changedinlastscan"]);

            this.ChangeDateTime = oDR["change_date_time"].ToString();
            this.ChangeUID = oDR["change_uid"].ToString();

            this.ConfigurationUpdated = false;
        }

        public void Save()
        {
            using (DBL.DBTable_Factory oFactory = new DBL.DBTable_Factory())
            {
                if (this.TableID == Guid.Empty)
                {
                    this.TableID = System.Guid.NewGuid();
                    oFactory.InsertSingleTable(this);
                }
                else
                    oFactory.UpdateSingleTable(this);
            }
        }
    }
}
