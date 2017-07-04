using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDoc.classes
{
    public class AutoDocBase : DBBase
	{
        public enum ScriptType
        {
            UNKNOWN = 0,
            TABLE = 1,
            PROC = 2,
            VIEW = 3,
            FUNCTION = 4
        }

        protected string m_UserID = "";

        public AutoDocBase(string sUserID, string sConnectionString)
            : base(sConnectionString)
        {
            m_UserID = sUserID;
        }

        public string UserID
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }

        #region "District"
        public string MakeDistrictHeader()
        {
            StringBuilder sSQL = new StringBuilder();

            sSQL.AppendLine("DECLARE @intDistrict int ")
                .AppendLine(" DECLARE curDistrict cursor for")
                .AppendLine("      select district from reg_district where validation_only = 'N'")
                .AppendLine(" OPEN curDistrict")
                .AppendLine(" FETCH curDistrict into @intDistrict")
                .AppendLine(" WHILE @@FETCH_STATUS = 0")
                .AppendLine(" BEGIN");

            return sSQL.ToString();
        }

        public string MakeDistrictFooter()
        {
            StringBuilder sSQL = new StringBuilder();

            sSQL.AppendLine("FETCH curDistrict INTO @intDistrict")
                .AppendLine(" End")
                .AppendLine(" Close curDistrict")
                .AppendLine(" DEALLOCATE curDistrict")
                .AppendLine(" GO");

            return sSQL.ToString();
        }
        #endregion

        public string DropIfExisting(string sName, ScriptType iType)
        {
            string sType="";
            string sCMD="";

            switch (iType)
            {
                case ScriptType.FUNCTION:
                    sType="FN";
                    sCMD="FUNCTION";

                    break;

                case ScriptType.PROC:
                    sType="P";
                    sCMD="PROC";

                    break;

                case ScriptType.TABLE:
                    sType="U";
                    sCMD="TABLE";

                    break;

                case ScriptType.VIEW:
                    sType="V";
                    sCMD="VIEW";

                    break;

            }

            StringBuilder sSQL=new StringBuilder();
            sSQL.AppendLine("if exists(select 1 from sysobjects where type='" + sType + "'")
                .AppendLine("   and name='" + sName + "')")
                .AppendLine(" drop " + sCMD + " s" + sName)
                .AppendLine("GO");

            return sSQL.ToString();
        }
    }
}
