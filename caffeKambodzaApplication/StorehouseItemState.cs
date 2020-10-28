using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace caffeKambodzaApplication
{
    /// <summary>
    /// table statesStoreOnEndDay record
    /// </summary>
    public class StorehouseItemState : StorehouseItem
    {
        #region members

        private string _stateOfEndDateTime;

        #endregion

        #region constructors

        public StorehouseItemState() { }

        public StorehouseItemState(StorehouseItem sItem, string stateOfEndDateTime)
            : base(sItem.ItemCode,sItem.ItemName,sItem.ItemGroup,sItem.ItemforOnePrice, sItem.ItemforOneAmount, sItem.ItemRealAmount)
        {
            _stateOfEndDateTime = stateOfEndDateTime;
        }

        #endregion


        #region properties

        public string StateOfEndDateTime
        {
            get { return _stateOfEndDateTime; }
            set { _stateOfEndDateTime = value; }
        }

        #endregion

    }
}
