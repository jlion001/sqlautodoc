using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.SCAN.AGENT
{
    internal class DBDescriptionAgent_Factory : SCAN.shared.DBLBase
    {
        Guid m_DBID = Guid.Empty;

        public DBDescriptionAgent_Factory(Guid DBID)
            : base(UTIL.DSNConnectionSchemaManager.ConnectionString)
        {
            m_DBID = DBID;
        }

        public void UpdateFunctionDescription(
                    string Name,
                    string Description)
        {
            base.UpdateProperty(
            Description: Description,
            Level1Type: "function",
            Level1Name: Name);
        }

        public void UpdateProcedureDescription(
                    string Name,
                    string Description)
        {
            base.UpdateProperty(
            Description: Description,
            Level1Type: "procedure",
            Level1Name: Name);
        }

        public void UpdateTableDescription(
                    string Name,
                    string Description)
        {
            base.UpdateProperty(
            Description: Description,
            Level1Type: "table",
            Level1Name: Name);
        }

        public void UpdateViewDescription(
                    string Name,
                    string Description)
        {
            base.UpdateProperty(
            Description: Description,
            Level1Type: "view",
            Level1Name: Name);
        }
    }
}
