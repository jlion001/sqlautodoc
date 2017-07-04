using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SQLAutoDocLib.BLL
{
    public class Version_Factory
    {
        private DBL.DBVersion_Factory m_DBFactory = new DBL.DBVersion_Factory();

        public List<SQLAutoDocLib.BLL.Version> ListAllVersionsInDatabase(Guid DBID)
        {
            List<SQLAutoDocLib.BLL.Version> oList = new List<SQLAutoDocLib.BLL.Version>();

            DataTable oIDs = m_DBFactory.AllVersions(DBID: DBID);
            foreach (DataRow oID in oIDs.Rows)
            {
                BLL.Version oVersion = new BLL.Version(DBID: DBID, VersionID: oID[0].ToString());
                oVersion.Load();

                oList.Add(oVersion);
            }
            return oList;
        }
    }
}
