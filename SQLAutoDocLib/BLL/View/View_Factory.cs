using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SQLAutoDocLib.BLL
{
    public class View_Factory:shared.BLLFactoryBase
    {
        private DBL.DBView_Factory m_DBFactory = new DBL.DBView_Factory();

        public List<SQLAutoDocLib.BLL.View> ListAllViewsInDatabase(Guid DBID)
        {
            List<SQLAutoDocLib.BLL.View> oList = new List<SQLAutoDocLib.BLL.View>();

            DataTable oIDs = m_DBFactory.AllViews(DBID: DBID);
            foreach (DataRow oID in oIDs.Rows)
            {
                BLL.View oView = new BLL.View(DBID: DBID, ViewID: (Guid)oID["ViewID"]);
                oView.Load();

                oList.Add(oView);
            }
            return oList;
        }

        public List<SQLAutoDocLib.BLL.View> ListAllViewsInDatabase(Guid DBID, string VersionID, bool ChangedOnly)
        {
            List<SQLAutoDocLib.BLL.View> oList = new List<SQLAutoDocLib.BLL.View>();

            DataTable oIDs = m_DBFactory.AllViews(DBID: DBID, VersionID: VersionID, ChangedOnly: ChangedOnly);
            foreach (DataRow oID in oIDs.Rows)
            {
                BLL.View oView = new BLL.View(DBID: DBID, ViewID: (Guid)oID["ViewID"]);
                oView.Load();

                oList.Add(oView);
            }
            return oList;
        }

        public View GetViewByName(Guid DBID, string ViewName)
        {
            View oReturn = null;

            DataTable oViewID = m_DBFactory.GetViewByName(DBID, ViewName);
            oReturn = new View(DBID: DBID, ViewID: (Guid)oViewID.Rows[0]["ViewID"]);
            oReturn.Load();

            return oReturn;
        }

        public View GetViewByName(Guid DBID, string ViewName, string VersionID)
        {
            View oReturn = null;

            DataTable oViewID = m_DBFactory.GetViewByName(DBID, ViewName,VersionID:VersionID);
            oReturn = new View(DBID: DBID, ViewID: (Guid)oViewID.Rows[0]["ViewID"]);
            oReturn.Load();

            return oReturn;
        }

        public string Reconstitute(BLL.View oView)
        {
            SCAN.AGENT.DBViewAgent_Factory oFactory = new SCAN.AGENT.DBViewAgent_Factory();
            return oFactory.Reconstitute(oView);
        }
    }
}
