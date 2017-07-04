using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.SCAN.WORKER
{
    public class DatabaseDescriptionUpdater
    {
        Guid m_DBID = Guid.Empty;

        public delegate void DescriptionUpdateBeginEvent(DateTime e);
        public delegate void DescriptionUpdateEndEvent(DateTime e);

        public delegate void TypeDescriptionUpdateEvent(string type, DateTime e);
        public delegate void ObjectDescriptionUpdateEvent(string type, string name, DateTime e);

        public event DescriptionUpdateBeginEvent DescriptionUpdateBegin;
        public event DescriptionUpdateEndEvent DescriptionUpdateEnd;

        public event TypeDescriptionUpdateEvent TypeDescriptionUpdate;
        public event ObjectDescriptionUpdateEvent ObjectDescriptionUpdate;

        public DatabaseDescriptionUpdater(Guid DBID)
        {
            m_DBID = DBID;
        }

        public void Update()
        {
            AGENT.DBDescriptionAgent_Factory oDescriptionFactory = new AGENT.DBDescriptionAgent_Factory(DBID: m_DBID);

            DescriptionUpdateBegin(DateTime.Now);

            TypeDescriptionUpdate(UTIL.Constants.FUNCTIONNODE.ToString(), DateTime.Now);
            using (SQLAutoDocLib.BLL.Function_Factory oFunction_Factory = new SQLAutoDocLib.BLL.Function_Factory())
                foreach (BLL.Function oFunction in oFunction_Factory.ListAllFunctionsInDatabase(DBID: m_DBID))
                    oDescriptionFactory.UpdateFunctionDescription(
                        Name: oFunction.Name,
                        Description: oFunction.Description);

            TypeDescriptionUpdate(UTIL.Constants.PROCEDURENODE.ToString(), DateTime.Now);
            using (SQLAutoDocLib.BLL.Procedure_Factory oProcedure_Factory = new SQLAutoDocLib.BLL.Procedure_Factory())
                foreach(BLL.Procedure oProcedure in oProcedure_Factory.ListAllProceduresInDatabase(DBID:m_DBID))
                    oDescriptionFactory.UpdateProcedureDescription(
                        Name: oProcedure.Name,
                        Description: oProcedure.Description);

            TypeDescriptionUpdate(UTIL.Constants.TABLENODE.ToString(), DateTime.Now);
            using (SQLAutoDocLib.BLL.Table_Factory oTable_Factory = new SQLAutoDocLib.BLL.Table_Factory())
                foreach(BLL.Table oTable in oTable_Factory.ListAllTablesInDatabase(DBID:m_DBID))
                    oDescriptionFactory.UpdateTableDescription(
                        Name: oTable.Name,
                        Description: oTable.Description);

            TypeDescriptionUpdate(UTIL.Constants.VIEWNODE.ToString(), DateTime.Now);
            using (SQLAutoDocLib.BLL.View_Factory oView_Factory = new SQLAutoDocLib.BLL.View_Factory())
                foreach(BLL.View oView in oView_Factory.ListAllViewsInDatabase(DBID:m_DBID))
                    oDescriptionFactory.UpdateViewDescription(
                        Name: oView.Name,
                        Description: oView.Description);

            /*
             * Can't update trigger descriptions at this time...
            SQLAutoDocLib.BLL.Trigger_Factory oTrigger_Factory = new SQLAutoDocLib.BLL.Trigger_Factory();
            oFunction_Factory.UpdateExtendedDescriptions(DBID: m_DBID);
            Application.DoEvents();
             * */

            DescriptionUpdateEnd(DateTime.Now);
        }
    }

}
