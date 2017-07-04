using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.BLL
{
    class FunctionHistory : shared.BLLbase
    {
      public FunctionHistory(Guid FunctionHistoryID)
        {
            this.FunctionHistoryID = FunctionHistoryID;
            this.FunctionID = Guid.Empty;
            this.VersionID = "";
            this.Description = "";
            this.Configuration = "";
            this.CurrentlyExists = true;
        }

      public FunctionHistory(Guid FunctionID, string VersionID)
        {
            this.FunctionHistoryID = Guid.Empty;
            this.FunctionID = FunctionID;
            this.VersionID = VersionID;
            this.Description = "";
            this.Configuration = "";
        }

        public Guid FunctionHistoryID { get; set; }
        public Guid FunctionID { get; set; }
        public string VersionID { get; set; }
        public string Description { get; set; }
        public string Configuration { get; set; }
        public bool CurrentlyExists { get; set; }

        public void Load()
        {
            using (DBL.DBFunctionHistory_Factory oFactory = new DBL.DBFunctionHistory_Factory())
            {
                oFactory.LoadSingleFunctionHistory(this);
            }

        }

        public void Save()
        {
            using (DBL.DBFunctionHistory_Factory oFactory = new DBL.DBFunctionHistory_Factory())
            {
                if (this.FunctionHistoryID == Guid.Empty)
                {
                    this.FunctionHistoryID = System.Guid.NewGuid();
                    oFactory.InsertSingleFunctionHistory(this);
                }
                else
                    oFactory.UpdateSingleFunctionHistory(this);
            }
        }
    }
}
