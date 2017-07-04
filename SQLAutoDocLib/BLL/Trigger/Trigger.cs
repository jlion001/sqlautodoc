using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.BLL
{
    public class Trigger : shared.BLLbase
    {
        private string m_Configuration = null;

       public Trigger(Guid DBID)
        {
            this.TriggerID = Guid.Empty;
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

        public Trigger(Guid DBID, Guid TriggerID)
        {
            this.TriggerID = TriggerID;
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

        public Guid TriggerID { get; set; }
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
            using (DBL.DBTrigger_Factory oFactory = new DBL.DBTrigger_Factory())
            {
                oFactory.LoadSingleTrigger(this);
            }

        }

        public void Save()
        {
            using (DBL.DBTrigger_Factory oFactory = new DBL.DBTrigger_Factory())
            {
                if (this.TriggerID == Guid.Empty)
                {
                    this.TriggerID = System.Guid.NewGuid();
                    oFactory.InsertSingleTrigger(this);
                }
                else
                    oFactory.UpdateSingleTrigger(this);
            }
        }

        public void UpdateExtendedDescription()
        {
            using (DBL.DBFunction_Factory oFactory = new DBL.DBFunction_Factory())
            {
            //TODO: Can't directly assign MS_DESCRIPTION to triggers
            }
        }
    }
}
