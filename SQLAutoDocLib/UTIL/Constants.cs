using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.UTIL
{
    public enum ObjectType
    {
        Not_Specified=0,
        Database=1,
        Table=2,
        Function=3,
        Trigger=4,
        Procedure=5,
        View=6
    }

    public static class Constants
    {
        public static string DATABASENODE = "Database";
        public static string TABLENODE = "Tables";
        public static string FUNCTIONNODE = "Functions";
        public static string TRIGGERNODE = "Triggers";
        public static string PROCEDURENODE = "Procedures";
        public static string VIEWNODE = "Views";
    }
}
