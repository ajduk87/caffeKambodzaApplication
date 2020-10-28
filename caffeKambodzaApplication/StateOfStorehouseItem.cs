using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace caffeKambodzaApplication
{
    public class StateOfStorehouseItem
    {

        #region members

        private string _storeItemCode;
        private string _storeItemName;
        private string _storeItemGroup;
        private string _realAmount;
        private string _stateOfEndDateTime;


        #endregion

        #region constructors

        public StateOfStorehouseItem(string storeItemCode, string storeItemName, string storeItemGroup, string realAmount, DateTime stateOfEndDateTime)
        {
            _storeItemCode = storeItemCode;
            _storeItemName = storeItemName;
            _storeItemGroup = storeItemGroup;
            _realAmount = realAmount;
            _stateOfEndDateTime = stateOfEndDateTime.ToShortDateString();
        }


        #endregion

        #region properties

        public string StoreItemCode
        {
            get { return _storeItemCode; }
            set { _storeItemCode = value; }
        }

        public string StoreItemName
        {
            get { return _storeItemName; }
            set { _storeItemName = value; }
        }

        public string StoreItemGroup
        {
            get { return _storeItemGroup; }
            set { _storeItemGroup = value; }
        }

        public string RealAmount
        {
            get { return _realAmount; }
            set { _realAmount = value; }
        }

        public string StateOfEndDateTime
        {
            get { return _stateOfEndDateTime; }
            set { _stateOfEndDateTime = value; }
        }



        #endregion

    }
}
