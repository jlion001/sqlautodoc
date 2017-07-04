using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.BLL.shared
{
    abstract public class BLLbase
    {
        private string m_ChangeDateTime = DateTime.Now.ToString();
        private string m_ChangeUID = Environment.UserName;
        private bool m_ChangedInLastScan = false;

        public string ChangeDateTime
        {
            get{return m_ChangeDateTime;}
            set { m_ChangeDateTime = value; }
        }

        public string ChangeUID
        {
            get { return m_ChangeUID; }
            set { m_ChangeUID = value; }
        }

        public bool ChangedInLastScan
        {
            get { return m_ChangedInLastScan; }
            set { m_ChangedInLastScan = value; }
        }
    }
}
