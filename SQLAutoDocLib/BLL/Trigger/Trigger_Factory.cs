using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SQLAutoDocLib.BLL
{
    public class Trigger_Factory
    {
        private DBL.DBTrigger_Factory m_DBFactory = new DBL.DBTrigger_Factory();

        public List<SQLAutoDocLib.BLL.Trigger> ListAllTriggersInDatabase(Guid DBID)
        {
            List<SQLAutoDocLib.BLL.Trigger> oList = new List<SQLAutoDocLib.BLL.Trigger>();

            DataTable oIDs = m_DBFactory.AllTriggers(DBID: DBID);
            foreach (DataRow oID in oIDs.Rows)
            {
                BLL.Trigger oTrigger = new BLL.Trigger(DBID: DBID, TriggerID: (Guid)oID["TriggerID"]);
                oTrigger.Load();

                oList.Add(oTrigger);
            }
            return oList;
        }

        public List<SQLAutoDocLib.BLL.Trigger> ListAllTriggersInDatabase(Guid DBID, string VersionID, bool ChangedOnly)
        {
            List<SQLAutoDocLib.BLL.Trigger> oList = new List<SQLAutoDocLib.BLL.Trigger>();

            DataTable oIDs = m_DBFactory.AllTriggers(DBID: DBID, VersionID: VersionID, ChangedOnly: ChangedOnly);
            foreach (DataRow oID in oIDs.Rows)
            {
                BLL.Trigger oTrigger = new BLL.Trigger(DBID: DBID, TriggerID: (Guid)oID["TriggerID"]);
                oTrigger.Load();

                oList.Add(oTrigger);
            }
            return oList;
        }

        public Trigger GetTriggerByName(Guid DBID, string TriggerName)
        {
            Trigger oReturn = null;

            DataTable oTriggerID = m_DBFactory.GetTriggerByName(DBID, TriggerName);
            oReturn = new Trigger(DBID: DBID, TriggerID: (Guid)oTriggerID.Rows[0]["TriggerID"]);
            oReturn.Load();

            return oReturn;
        }

        public Trigger GetTriggerByName(Guid DBID, string TriggerName, string VersionID)
        {
            Trigger oReturn = null;

            DataTable oTriggerID = m_DBFactory.GetTriggerByName(DBID, TriggerName, VersionID: VersionID);
            oReturn = new Trigger(DBID: DBID, TriggerID: (Guid)oTriggerID.Rows[0]["TriggerID"]);
            oReturn.Load();

            return oReturn;
        }

        public string Reconstitute(BLL.Trigger oTrigger)
        {
            SCAN.AGENT.DBTriggerAgent_Factory oFactory = new SCAN.AGENT.DBTriggerAgent_Factory();
            return oFactory.Reconstitute(oTrigger);
        }
    }
}
