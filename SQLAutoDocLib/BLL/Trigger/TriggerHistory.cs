using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.BLL
{
    class TriggerHistory : shared.BLLbase
    {
     public TriggerHistory(Guid TriggerHistoryID)
        {
            this.TriggerHistoryID = TriggerHistoryID;
            this.TriggerID = Guid.Empty;
            this.VersionID = "";
            this.Description = "";
            this.Configuration = "";
            this.CurrentlyExists = true;
        }

      public TriggerHistory(Guid TriggerID, string VersionID)
        {
            this.TriggerHistoryID = Guid.Empty;
            this.TriggerID = TriggerID;
            this.VersionID = VersionID;
            this.Description = "";
            this.Configuration = "";
        }

        public Guid TriggerHistoryID { get; set; }
        public Guid TriggerID { get; set; }
        public string VersionID { get; set; }
        public string Description { get; set; }
        public string Configuration { get; set; }
        public bool CurrentlyExists { get; set; }

        public void Load()
        {
            using (DBL.DBTriggerHistory_Factory oFactory = new DBL.DBTriggerHistory_Factory())
            {
                oFactory.LoadSingleTriggerHistory(this);
            }

        }

        public void Save()
        {
            using (DBL.DBTriggerHistory_Factory oFactory = new DBL.DBTriggerHistory_Factory())
            {
                if (this.TriggerHistoryID == Guid.Empty)
                {
                    this.TriggerHistoryID = System.Guid.NewGuid();
                    oFactory.InsertSingleTriggerHistory(this);
                }
                else
                    oFactory.UpdateSingleTriggerHistory(this);
            }
        }
    }
}
