using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace caffeKambodzaApplication
{
    public class ConnectionRecord : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        #region members

        private string _connCodeProduct;
        private string _connStoreItemCode;
        private string _connKindOfProduct;
        private string _connStoreItemName;
        private string _groupStoreItem;
        private string _amountProduct;
        private string _amountStoreItem;
        private string _price;
        private bool _isUsed = false;
       
        #endregion

        #region constructors

        public ConnectionRecord(string connCodeProduct, string connStoreItemCode, string connKindOfProduct, string connStoreItemName, string groupStoreItem, string amountProduct, string amountStoreItem, string price) 
        {
            _connCodeProduct = connCodeProduct;
            _connStoreItemCode = connStoreItemCode;
            _connKindOfProduct = connKindOfProduct;
            _connStoreItemName= connStoreItemName;
            _groupStoreItem = groupStoreItem;
            _amountProduct = amountProduct;
            _amountStoreItem = amountStoreItem;
            _price = price;
        }
        public ConnectionRecord()
        { }

        #endregion


        #region Properties


        public bool IsUsed
        {
            get { return _isUsed; }
            set 
            { 
                _isUsed = value;
                OnPropertyChanged("IsUsed");
            }
        }


        public string ConnCodeProduct
        {
            get { return _connCodeProduct; }
            set { _connCodeProduct = value; } 
        }
        public string ConnStoreItemCode
        {
            get { return _connStoreItemCode; }
            set { _connStoreItemCode = value;}
        }



        public string ConnKindOfProduct
        {
            get { return _connKindOfProduct; }
            set 
            { 
                _connKindOfProduct = value;
               
            }
        }
        public string ConnStoreItemName
        {
            get { return _connStoreItemName; }
            set 
            {
                _connStoreItemName = value;
               
            }
        }
        public string GroupStoreItem
        {
            get { return _groupStoreItem; }
            set 
            { 
                _groupStoreItem = value;
                
            }
        }
        public string AmountProduct
        {
            get { return _amountProduct; }
            set 
            { 
                _amountProduct = value;
                OnPropertyChanged("AmountProduct");
            }
        }
        public string AmountStoreItem
        {
            get { return _amountStoreItem; }
            set 
            { 
                _amountStoreItem = value;
                OnPropertyChanged("AmountStoreItem");
            }
        }

        public string Price
        {
            get { return _price; }
            set 
            { 
                _price = value;
                OnPropertyChanged("Price");
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

        #region method
        public override string ToString()
        {
           return _connKindOfProduct + "&" + _connStoreItemName;
        }

        #endregion
    }
}
