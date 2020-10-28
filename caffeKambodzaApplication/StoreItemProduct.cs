using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace caffeKambodzaApplication
{
    public class StoreItemProduct : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        #region members

        private string _codeProduct;
        private string _kindOfProduct;
        private string _measure;
        private int _price;
       
        private int _ratio;
        private string _groupStoreItem;
        private bool _isUsed;
        private double _amount=0.0;//in litar or kg


        private double _realAmount = 0.0;
        private double _threshold = 0.0;

        #endregion

         #region constructors

        public StoreItemProduct()
        { }

        public StoreItemProduct(string kindOfProduct)
        {
            _kindOfProduct = kindOfProduct;
        }

       /* public StoreItemProduct(string codeProduct, string kindOfProduct, int price,string groupStoreItem, bool isUsed)
        {
            _codeProduct = codeProduct;
            _kindOfProduct = kindOfProduct;
            _price = price;
            _groupStoreItem = groupStoreItem;
            _isUsed = isUsed;
    
        }*/

        public StoreItemProduct(string codeProduct, string kindOfProduct,string measure, int price, string groupStoreItem, bool isUsed,double amount)
        {
            _codeProduct = codeProduct;
            _kindOfProduct = kindOfProduct;
            _measure = measure;
            _price = price;
            _groupStoreItem = groupStoreItem;
            _isUsed = isUsed;
            _amount = amount;

        }

        public StoreItemProduct(string codeProduct, string kindOfProduct,string measure, int price, string groupStoreItem, bool isUsed, double amount, double threshold)
        {
            _codeProduct = codeProduct;
            _kindOfProduct = kindOfProduct;
            _measure = measure;
            _price = price;
            _groupStoreItem = groupStoreItem;
            _isUsed = isUsed;
            _amount = amount;
            _threshold = threshold;

        }

        public StoreItemProduct(string codeProduct, string kindOfProduct, string measure, int price)
        {
            _codeProduct = codeProduct;
            _kindOfProduct = kindOfProduct;
            _measure = measure;
            _price = price;
          
        }

        #endregion

        #region properties

        public string Measure 
        {
            get { return _measure; }
            set { _measure = value; }
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

        public double RealAmount
        {
            get { return _realAmount; }
            set { _realAmount = value; }
        }

        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public bool isUsed
        {
            get { return _isUsed; }
            set { _isUsed = value; }
        }

        public string Group 
        {
            get { return _groupStoreItem; }
            set { _groupStoreItem = value; }
        }

        public int Ratio
        {
            get { return _ratio; }
            set { _ratio = value; }
        }

        public string CodeProduct
        {
            get { return _codeProduct; }
            set { _codeProduct = value; }
        }

        public string KindOfProduct
        {
            get { return _kindOfProduct; }
            set { _kindOfProduct = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
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

        public string IsUsedInformation()
        {
            if (_isUsed) return Constants.YES;
            else return Constants.NO;
        }

        public string ComboBoxForm()
        {
            return this._kindOfProduct;
        }

        public string Code()
        {
            return this._codeProduct;
        }


        public override string ToString()
        {
            return _kindOfProduct;
        }

        #endregion
    }
}
