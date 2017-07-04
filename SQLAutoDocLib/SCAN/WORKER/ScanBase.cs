using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.SCAN.WORKER
{
    abstract public class ScanBase
    {
        public delegate void ScanCountOfObjectsToScan(string type, int count);
        public delegate void ScanObjectBegin(string type, string name);
        public delegate void ScanObjectNotFound(string type,string name);
        public delegate void ScanObjectChanged(string type,string name);
        public delegate void ScanObjectDeleted(string type, string name);
    }
}
