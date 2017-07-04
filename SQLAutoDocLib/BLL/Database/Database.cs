using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.BLL
{
    public class Database : shared.BLLbase
    {
        public Database(Guid ServerID)
        {
            this.ServerID = ServerID;
            this.DBID = Guid.Empty;
            this.Name = "";
            this.Description = "";
        }

        public Database(Guid ServerID,Guid DBID)
        {
            this.ServerID = ServerID;
            this.DBID = DBID;
            this.Name = "";
            this.Description = "";
        }

        public Guid ServerID { get; set; }
        public Guid DBID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public void Load()
        {
            using (DBL.DBDatabase_Factory oFactory= new DBL.DBDatabase_Factory())
            {
                oFactory.LoadSingleDatabase(this);
            }

        }

        public void Save()
        { 
            using (DBL.DBDatabase_Factory oFactory = new DBL.DBDatabase_Factory())
            {
                if (this.DBID == Guid.Empty)
                {
                    this.DBID = System.Guid.NewGuid();
                    oFactory.InsertSingleDatabase(this);
                }
                else
                    oFactory.UpdateSingleDatabase(this);
            }
        }
    }
}
