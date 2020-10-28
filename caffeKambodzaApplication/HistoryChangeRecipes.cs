using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace caffeKambodzaApplication
{
    public class HistoryChangeRecipes : HistoryChange
    {


        #region members

        private string _oldProductAmount;
        private string _newProductAmount;
        private string _oldStoreItemAmount;
        private string _newStoreItemAmount;
        private string _dateChanged;

        #endregion


        #region constructors



        public HistoryChangeRecipes(string productCode, string storeItemCode, string kindOfProduct, string storeItemName, string storeItemGroup, string oldProductAmount, string newProductAmount, string oldStoreItemAmount, string newStoreItemAmount, DateTime date)
            : base(productCode, storeItemCode, kindOfProduct, storeItemName, storeItemGroup)
        {
            _oldProductAmount = oldProductAmount;
            _newProductAmount = newProductAmount;
            _oldStoreItemAmount = oldStoreItemAmount;
            _newStoreItemAmount = newStoreItemAmount;
            _dateChanged = date.ToShortDateString();
        }

        #endregion

        #region properties


        public string OldProductAmount
        {
            get { return _oldProductAmount; }
            set { _oldProductAmount = value; }
        }

        public string NewProductAmount
        {
            get { return _newProductAmount; }
            set { _newProductAmount = value; }
        }

        public string OldStoreItemAmount
        {
            get { return _oldStoreItemAmount; }
            set { _oldStoreItemAmount = value; }
        }

        public string NewStoreItemAmount
        {
            get { return _newStoreItemAmount; }
            set { _newStoreItemAmount = value; }
        }

        public string DateChanged
        {
            get { return _dateChanged; }
            set { _dateChanged = value; }
        }

        #endregion


        #region methods

        public override void setType(string type)
        {
            _type = type;
        }

        #endregion

    }//end of class
}
