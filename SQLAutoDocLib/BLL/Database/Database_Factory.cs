using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SQLAutoDocLib.BLL
{
    public class Database_Factory
    {
        private DBL.DBDatabase_Factory m_DBFactory = new DBL.DBDatabase_Factory();

        public Database GetDatabaseByID(Guid DBID)
        {
            Database oDB = new Database(ServerID:Guid.Empty);
            oDB.DBID = DBID;
            oDB.Load();

            return oDB;
        }
    }
}
