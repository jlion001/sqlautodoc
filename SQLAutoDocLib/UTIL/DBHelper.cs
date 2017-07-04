using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.UTIL
{
    static internal class DBHelper
    {
        #region HelperFunctions
        static public string PadApostrophes(string sValue)
        {
            return "'" + sValue.Replace("'", "''") + "'";
        }

        static public string EmptyStringToNull(string sValue)
        {
            string sRetValue = "";

            if (sValue == "" || sValue.Length == 0)
                sRetValue = "NULL";
            else
                sRetValue = "'" + sValue.Replace("'", "''") + "'";

            return sRetValue;
        }

        static public string NullToString(object oValue)
        {
            string sRetValue = "";

            if (oValue != null && oValue != System.DBNull.Value)
                sRetValue = oValue.ToString();

            return sRetValue;
        }

        static public byte BoolToBit(object oValue)
        {
            byte iRetValue = 0;

            if (oValue != null && oValue != System.DBNull.Value)
                if ((bool)oValue == true)
                    iRetValue = 1;

            return iRetValue;
        }

        static public bool BitToBool(object oValue)
        {
            bool bRetValue = false;

            if (oValue != null && oValue != System.DBNull.Value)
                if ((int)oValue != 0)
                    bRetValue = true;

            return bRetValue;
        }
        #endregion
    }
}
