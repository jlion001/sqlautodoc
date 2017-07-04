using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.BLL
{
    class TableHistory:shared.BLLbase
    {
        public TableHistory(Guid TableHistoryID)
        {
            this.TableHistoryID = TableHistoryID;
            this.TableID = Guid.Empty;
            this.VersionID = "";
            this.Description = "";
            this.Configuration = "";
            this.CurrentlyExists = true;
        }

        public TableHistory(Guid TableID, string VersionID)
        {
            this.TableHistoryID = Guid.Empty;
            this.TableID = TableID;
            this.VersionID = VersionID;
            this.Description = "";
            this.Configuration = "";
        }

        public Guid TableHistoryID { get; set; }
        public Guid TableID { get; set; }
        public string VersionID { get; set; }
        public string Description { get; set; }
        public string Configuration { get; set; }
        public bool CurrentlyExists { get; set; }

        public void Load()
        {
            using (DBL.DBTableHistory_Factory oFactory = new DBL.DBTableHistory_Factory())
            {
                oFactory.LoadSingleTableHistory(this);
            }

        }

        public void Save()
        {
            using (DBL.DBTableHistory_Factory oFactory = new DBL.DBTableHistory_Factory())
            {
                if (this.TableHistoryID == Guid.Empty)
                {
                    this.TableHistoryID = System.Guid.NewGuid();
                    oFactory.InsertSingleTableHistory(this);
                }
                else
                    oFactory.UpdateSingleTableHistory(this);
            }
        }
    }
}
