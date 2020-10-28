using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace caffeKambodzaApplication
{
    public class StorehouseItem  : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        #region members

        private string _itemCode;
        private string _itemName;
        private string _itemGroup;
        private int _itemforOnePrice; // amount for one number of store item in din
        private double _itemforOneAmount;// amount for one number of store item in kg/l 
        private double _itemPrice;//_itemRealAmount/_itemforOneAmount * _itemforOnePrice
        private double _itemRealAmount;

        private double _threshold = 0.0;


        #endregion

        #region constructors

        public StorehouseItem() { }


        public StorehouseItem(string itemCode, string itemName, string itemGroup, int itemforOnePrice, double itemforOneAmount, double itemRealAmount) 
        {
            _itemCode = itemCode;
            _itemName = itemName;
            _itemGroup = itemGroup;
            _itemforOnePrice = itemforOnePrice;
            _itemforOneAmount = itemforOneAmount;
            _itemRealAmount = itemRealAmount;
            _itemPrice = _itemRealAmount / _itemforOneAmount * _itemforOnePrice;
        }

        #endregion


        #region properties


        public string ItemCode
        {
            get { return _itemCode; }
            set { _itemCode = value; }
        }

        public string ItemName 
        {
            get { return _itemName; }
            set { _itemName = value; }
        }

        public string ItemGroup
        {
            get { return _itemGroup; }
            set { _itemGroup = value; }
        }

        public int ItemforOnePrice
        {
            get { return _itemforOnePrice; }
            set 
            { 
                _itemforOnePrice = value;
                OnPropertyChanged("ItemforOnePrice");
            }
        }


        public double ItemforOneAmount
        {
            get { return _itemforOneAmount; }
            set 
            { 
                _itemforOneAmount = value;
                OnPropertyChanged("ItemforOneAmount");
            }
        }

        public double ItemRealAmount
        {
            get { return _itemRealAmount; }
            set 
            { 
                _itemRealAmount = value;
                OnPropertyChanged("ItemRealAmount");
            }
        }

        public double ItemPrice
        {
            get { return _itemPrice; }
            set 
            { 
                _itemPrice = value;
                OnPropertyChanged("ItemPrice");
            }
        }

        public double Threshold
        {
            get { return _threshold; }
            set 
            {
                _threshold = value;
                OnPropertyChanged("Threshold");
            }
        }

        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        #region methods

        public override string ToString()
        {
            return _itemCode + "&" + _itemName + "&" + _itemGroup + "&" + _itemRealAmount + "&" +  _itemPrice;
        }

        #endregion

    }
}
