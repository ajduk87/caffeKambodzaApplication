using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace caffeKambodzaApplication
{
    public abstract class HistoryChange
    {

        #region members

        private string _productCode = String.Empty;
        private string _storeItemCode = String.Empty;
        private string _kindOfProduct = String.Empty;
        private string _storeItemName = String.Empty;
        private string _storeItemGroup = String.Empty;
        protected string _type = String.Empty;

        #endregion

        #region constructors

        public HistoryChange() { }

        public HistoryChange(string productCode, string storeItemCode, string kindOfProduct, string storeItemName, string storeItemGroup)
        {
            _productCode = productCode;
            _storeItemCode = storeItemCode;
            _kindOfProduct = kindOfProduct;
            _storeItemName = storeItemName;
            _storeItemGroup = storeItemGroup;
        }

      

        #endregion


        #region properties 
      

        public string ProductCode 
        {
            get { return _productCode; }
            set { _productCode = value; }
        }

        public string StoreItemCode
        {
            get { return _storeItemCode; }
            set { _storeItemCode = value; }
        }

        public string KindOfProduct
        {
            get { return _kindOfProduct; }
            set { _kindOfProduct = value; }
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

        public string Type
        {
            get { return _type; }
        }

        #endregion

        #region methods

        public abstract  void setType(string type);
        
        #endregion

    }
}
