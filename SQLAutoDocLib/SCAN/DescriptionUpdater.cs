using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace SQLAutoDocLib.SCAN
{
    public class DescriptionUpdater
    {
        Guid m_DBID = Guid.Empty;

        private WORKER.DatabaseDescriptionUpdater m_Updater = null;

        public delegate void DescriptionUpdateBeginEvent(DateTime e);
        public delegate void DescriptionUpdateEndEvent(DateTime e);

        public delegate void TypeDescriptionUpdateEvent(string type, DateTime e);
        public delegate void ObjectDescriptionUpdateEvent(string type, string name, DateTime e);

        public event DescriptionUpdateBeginEvent DescriptionUpdateBegin;
        public event DescriptionUpdateEndEvent DescriptionUpdateEnd;

        public event TypeDescriptionUpdateEvent TypeDescriptionUpdate;
        public event ObjectDescriptionUpdateEvent ObjectDescriptionUpdate;

        private Control m_HostForm = null;

        public DescriptionUpdater(Control HostForm, Guid DBID)
        {
            m_DBID = DBID;
            m_HostForm = HostForm;

            m_Updater = new WORKER.DatabaseDescriptionUpdater(DBID:m_DBID);
            init_events();
        }

        private void init_events()
        {
            m_Updater.DescriptionUpdateBegin += new SCAN.WORKER.DatabaseDescriptionUpdater.DescriptionUpdateBeginEvent(DescriptionUpdateBeginEvent_Handler);
            m_Updater.DescriptionUpdateEnd += new SCAN.WORKER.DatabaseDescriptionUpdater.DescriptionUpdateEndEvent(DescriptionUpdateEndEvent_Handler);

            m_Updater.TypeDescriptionUpdate += new SCAN.WORKER.DatabaseDescriptionUpdater.TypeDescriptionUpdateEvent(TypeDescriptionUpdateEvent_Handler);
            m_Updater.ObjectDescriptionUpdate += new SCAN.WORKER.DatabaseDescriptionUpdater.ObjectDescriptionUpdateEvent(ObjectDescriptionUpdateEvent_Handler);
        }

        public void Update()
        {
            Task.Factory.StartNew(() => this.ExecuteUpdate());
        }

        private void ExecuteUpdate()
        {
            m_Updater.Update();
        }

        #region Events
        public void DescriptionUpdateBeginEvent_Handler(DateTime e)
        {
            m_HostForm.Invoke(DescriptionUpdateBegin, e);
        }

        public void DescriptionUpdateEndEvent_Handler(DateTime e)
        {
            m_HostForm.Invoke(DescriptionUpdateEnd, e);
        }

        public void TypeDescriptionUpdateEvent_Handler(string type, DateTime e)
        {
            m_HostForm.Invoke(TypeDescriptionUpdate, type,e);
        }

        public void ObjectDescriptionUpdateEvent_Handler(string type, string name, DateTime e)
        {
            m_HostForm.Invoke(ObjectDescriptionUpdate,type, name,e);
        }
        #endregion
    }
}
