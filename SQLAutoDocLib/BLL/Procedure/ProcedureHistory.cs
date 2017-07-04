using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.BLL
{
    class ProcedureHistory : shared.BLLbase
    {
       public ProcedureHistory(Guid ProcedureHistoryID)
        {
            this.ProcedureHistoryID = ProcedureHistoryID;
            this.ProcedureID = Guid.Empty;
            this.VersionID = "";
            this.Description = "";
            this.Configuration = "";
        }

       public ProcedureHistory(Guid ProcedureID, string VersionID)
        {
            this.ProcedureHistoryID = Guid.Empty;
            this.ProcedureID = ProcedureID;
            this.VersionID = VersionID;
            this.Description = "";
            this.Configuration = "";
        }

        public Guid ProcedureHistoryID { get; set; }
        public Guid ProcedureID { get; set; }
        public string VersionID { get; set; }
        public string Description { get; set; }
        public string Configuration { get; set; }
        public bool CurrentlyExists { get; set; }

        public void Load()
        {
            using (DBL.DBProcedureHistory_Factory oFactory = new DBL.DBProcedureHistory_Factory())
            {
                oFactory.LoadSingleProcedureHistory(this);
            }

        }

        public void Save()
        {
            using (DBL.DBProcedureHistory_Factory oFactory = new DBL.DBProcedureHistory_Factory())
            {
                if (this.ProcedureHistoryID == Guid.Empty)
                {
                    this.ProcedureHistoryID = System.Guid.NewGuid();
                    oFactory.InsertSingleProcedureHistory(this);
                }
                else
                    oFactory.UpdateSingleProcedureHistory(this);
            }
        }
    }
}
