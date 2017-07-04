using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SQLAutoDocLib.SCAN
{
    public class SCANNER
    {
        private WORKER.ScanDatabase m_Scanner = null;

        public delegate void ScanDatabaseBeginEvent(DateTime e);
        public delegate void ScanDatabaseEndEvent(DateTime e);

        public delegate void ScanObjectTypeBeginEvent(string type, DateTime e);
        public delegate void ScanObjectTypeEndEvent(string type, DateTime e);
        public delegate void ScanObjectBeginEvent(string type, string name, DateTime e);
        public delegate void ScanObjectNotFoundEvent(string type, string name, DateTime e);
        public delegate void ScanObjectChangedEvent(string type, string name, DateTime e);
        public delegate void ScanObjectDeletedEvent(string type, string name, DateTime e);
        public delegate void ScanCountOfObjectsEvent(string type, int count);

        public event ScanDatabaseBeginEvent ScanDatabaseBegin;
        public event ScanDatabaseEndEvent ScanDatabaseEnd;

        public event ScanObjectTypeBeginEvent ScanObjectTypeBegin;
        public event ScanObjectTypeEndEvent ScanObjectTypeEnd;

        public event ScanObjectBeginEvent ScanObjectBegin;
        public event ScanObjectNotFoundEvent ScanObjectNotFound;
        public event ScanObjectChangedEvent ScanObjectChanged;
        public event ScanObjectDeletedEvent ScanObjectDeleted;
        public event ScanCountOfObjectsEvent ScanObjectCount;

        private Control m_HostForm = null;

        public bool FoundTableChanges { get; set; }
        public bool FoundViewChanges { get; set; }
        public bool FoundFunctionChanges { get; set; }
        public bool FoundTriggerChanges { get; set; }
        public bool FoundProcChanges { get; set; }

        public SCANNER(Control HostForm,string sConnectionString)
        {
            m_HostForm = HostForm;

            m_Scanner = new WORKER.ScanDatabase(sConnectionString);
            init_events();

            this.FoundTableChanges = false;
            this.FoundViewChanges = false;
            this.FoundFunctionChanges = false;
            this.FoundTriggerChanges = false;
            this.FoundProcChanges = false;
        }

        public void Scan()
        {
            Task.Factory.StartNew(() => this.ExecuteScan());
        }

        private void ExecuteScan()
        {
            m_Scanner.Scan();
        }

        public bool FoundDatabaseChanges
        {
            get
            {
                return
                    this.FoundTableChanges
                    || this.FoundViewChanges
                    || this.FoundFunctionChanges
                    || this.FoundTriggerChanges
                    || this.FoundProcChanges;
            }
        }

        public Guid DBID
        {
            get { return m_Scanner.DBID; }
        }

        private void init_events()
        {
            m_Scanner.ScanDatabaseBegin += new SCAN.WORKER.ScanDatabase.ScanDatabaseBeginEvent(ScanDatabaseBeginEvent_Handler);
            m_Scanner.ScanDatabaseEnd += new SCAN.WORKER.ScanDatabase.ScanDatabaseEndEvent(ScanDatabaseEndEvent_Handler);

            m_Scanner.ScanObjectTypeBegin += new SCAN.WORKER.ScanDatabase.ScanObjectTypeBeginEvent(ScanObjectTypeBeginEvent_Handler);
            m_Scanner.ScanObjectTypeEnd += new SCAN.WORKER.ScanDatabase.ScanObjectTypeEndEvent(ScanObjectTypeEndEvent_Handler);

            m_Scanner.ScanObjectCount += new SCAN.WORKER.ScanDatabase.ScanObjectCountEvent(ScanCountOfObjectsToScan_Handler);
            m_Scanner.ScanObjectBegin += new SCAN.WORKER.ScanDatabase.ScanObjectBeginEvent(ScanObjectBegin_Handler);
            m_Scanner.ScanObjectChanged += new SCAN.WORKER.ScanDatabase.ScanObjectChangedEvent(ScanObjectChanged_Handler);
            m_Scanner.ScanObjectNotFound += new SCAN.WORKER.ScanDatabase.ScanObjectNotFoundEvent(ScanObjectNotFound_Handler);
            m_Scanner.ScanObjectDeleted += new SCAN.WORKER.ScanDatabase.ScanObjectDeletedEvent(ScanObjectDeleted_Handler);
        }

        #region Events

        public void ScanDatabaseBeginEvent_Handler(DateTime e)
        {
            m_HostForm.Invoke(ScanDatabaseBegin,e);
        }

        public void ScanDatabaseEndEvent_Handler(DateTime e)
        {
            m_HostForm.Invoke(ScanDatabaseEnd,e);
        }

        public void ScanObjectTypeBeginEvent_Handler(string type, DateTime e)
        {
            m_HostForm.Invoke(ScanObjectTypeBegin,type, e);
        }

        public void ScanObjectTypeEndEvent_Handler(string type, DateTime e)
        {
            m_HostForm.Invoke(ScanObjectTypeEnd,type, e);
        }

        public void ScanObjectBegin_Handler(string type, string name, DateTime e)
        {
            m_HostForm.Invoke(ScanObjectBegin, type, name, e);
        }

        public void ScanObjectChanged_Handler(string type, string name, DateTime e)
        {
            if (type== UTIL.Constants.TABLENODE)
                this.FoundTableChanges=true;
            else if (type==UTIL.Constants.VIEWNODE)
                this.FoundViewChanges=true;
            else if (type==UTIL.Constants.PROCEDURENODE)
                this.FoundProcChanges=true;
            else if (type==UTIL.Constants.FUNCTIONNODE)
                this.FoundFunctionChanges=true;
            else if (type==UTIL.Constants.TRIGGERNODE)
                this.FoundTriggerChanges=true;
            
            m_HostForm.Invoke(ScanObjectChanged,type, name, e);
        }

        public void ScanObjectNotFound_Handler(string type, string name, DateTime e)
        {
            if (type == UTIL.Constants.TABLENODE)
                this.FoundTableChanges = true;
            else if (type == UTIL.Constants.VIEWNODE)
                this.FoundViewChanges = true;
            else if (type == UTIL.Constants.PROCEDURENODE)
                this.FoundProcChanges = true;
            else if (type == UTIL.Constants.FUNCTIONNODE)
                this.FoundFunctionChanges = true;
            else if (type == UTIL.Constants.TRIGGERNODE)
                this.FoundTriggerChanges = true;

            m_HostForm.Invoke(ScanObjectNotFound,type, name, e);
        }

        public void ScanObjectDeleted_Handler(string type, string name, DateTime e)
        {
            if (type == UTIL.Constants.TABLENODE)
                this.FoundTableChanges = true;
            else if (type == UTIL.Constants.VIEWNODE)
                this.FoundViewChanges = true;
            else if (type == UTIL.Constants.PROCEDURENODE)
                this.FoundProcChanges = true;
            else if (type == UTIL.Constants.FUNCTIONNODE)
                this.FoundFunctionChanges = true;
            else if (type == UTIL.Constants.TRIGGERNODE)
                this.FoundTriggerChanges = true;

            m_HostForm.Invoke(ScanObjectDeleted, type, name, e);
        }

        public void ScanCountOfObjectsToScan_Handler(string type, int count)
        {
            m_HostForm.Invoke(ScanObjectCount, type, count);
        }

        #endregion
    }
}
