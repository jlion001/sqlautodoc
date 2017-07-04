using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.BLL
{
    public class Version : shared.BLLbase
    {
        public Version(Guid DBID)
        {
            this.DBID = DBID; 
        }

        public Version(Guid DBID, string VersionID)
        {
            this.DBID = DBID;
            this.VersionID = VersionID;

        }

        public string VersionID { get; set; }
        public string ScanStart { get; set; }
        public string ScanComplete { get; set; }
        public bool ChangesFound { get; set; }

        public Guid DBID { get; set; }

        private string MakeVersion()
        {
            return DateTime.Now.ToString("yyyyMMdd-HHmmss");
        }

        /// <summary>
        /// Save this version record
        /// </summary>
        public void Save()
        {
            DBL.DBVersion_Factory oDB = new DBL.DBVersion_Factory();

            if (this.VersionID == null)
            {
                this.VersionID = MakeVersion();
                oDB.AddSingleVersion(this);
            }
            else
            {
                oDB.UpdateSingleVersion(this);
            }
        }

        /// <summary>
        /// Load this version record
        /// </summary>
        public void Load()
        {
            using (DBL.DBVersion_Factory oFactory = new DBL.DBVersion_Factory())
            {
                oFactory.LoadSingleVersion(this);
            }
        }
    }
}
