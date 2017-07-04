using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.BLL
{
    class ViewHistory : shared.BLLbase
    {
        public ViewHistory(Guid ViewHistoryID)
        {
            this.ViewHistoryID = ViewHistoryID;
            this.ViewID = Guid.Empty;
            this.VersionID = "";
            this.Description = "";
            this.Configuration = "";
        }

        public ViewHistory(Guid ViewID, string VersionID)
        {
            this.ViewHistoryID = Guid.Empty;
            this.ViewID = ViewID;
            this.VersionID = VersionID;
            this.Description = "";
            this.Configuration = "";
        }

        public Guid ViewHistoryID { get; set; }
        public Guid ViewID { get; set; }
        public string VersionID { get; set; }
        public string Description { get; set; }
        public string Configuration { get; set; }
        public bool CurrentlyExists { get; set; }

        public void Load()
        {
            using (DBL.DBViewHistory_Factory oFactory = new DBL.DBViewHistory_Factory())
            {
                oFactory.LoadSingleViewHistory(this);
            }

        }

        public void Save()
        {
            using (DBL.DBViewHistory_Factory oFactory = new DBL.DBViewHistory_Factory())
            {
                if (this.ViewHistoryID == Guid.Empty)
                {
                    this.ViewHistoryID = System.Guid.NewGuid();
                    oFactory.InsertSingleViewHistory(this);
                }
                else
                    oFactory.UpdateSingleViewHistory(this);
            }
        }
    }
}
