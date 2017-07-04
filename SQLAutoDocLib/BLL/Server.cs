using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.BLL
{
    class Server : shared.BLLbase
    {
        public Server()
        {
            this.ServerID = Guid.Empty;
            this.Name = "";
            this.Description = "";
        }

        public Server(Guid ServerID)
        {
            this.ServerID = ServerID;
            this.Name = "";
            this.Description = "";
        }

        public Guid ServerID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public void Load()
        {
            using (DBL.DBServer_Factory oFactory = new DBL.DBServer_Factory())
            {
                oFactory.LoadSingleServer(this);
            }

        }

        public void Save()
        {
            using (DBL.DBServer_Factory oFactory = new DBL.DBServer_Factory())
            {
                if (this.ServerID == Guid.Empty)
                {
                    this.ServerID = System.Guid.NewGuid();
                    oFactory.InsertSingleServer(this);
                }
                else
                    oFactory.UpdateSingleServer(this);
            }
            
        }
    }
}
