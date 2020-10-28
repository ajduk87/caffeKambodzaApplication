using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace caffeKambodzaApplication
{
    public class StorehouseItemRecordDel : StorehouseItemRecord
    {
        #region members

        private string _deleteReason = String.Empty;

        #endregion


        #region constructors

        public StorehouseItemRecordDel(StorehouseItemRecord sR, string deleteReason)
            : base(sR) 
        {
            _deleteReason = deleteReason;
        }

        #endregion


        #region properties

        public string DeleteReason
        {
            get { return _deleteReason; }
            set { _deleteReason = value; }
        }

        #endregion
    }
}
