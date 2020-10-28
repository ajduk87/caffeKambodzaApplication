using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace caffeKambodzaApplication
{
    public class Product
    {
        #region members

        private string _codeProduct;
        private string _kindOfProduct;
        private string _nameProduct;
        private string _measureProduct;
        private int _price;
        private double _amount=0.0;//in litar or kg
        private ObservableCollection<StoreItemProduct> _storeItemProducts ;

        private string _wayDisplayBookBar;
        #endregion

        #region constructors

        public Product() { }

        public Product(string kindOfProduct, int price)
        {
            _kindOfProduct = kindOfProduct;
            _price = price;
            _codeProduct = Constants.CODENOTENTERED;
            _storeItemProducts = new ObservableCollection<StoreItemProduct>();
        }

        public Product(string codeProduct, string kindOfProduct, int price)
        {
            _codeProduct = codeProduct;
            _kindOfProduct = kindOfProduct;
            _price = price;
            _storeItemProducts = new ObservableCollection<StoreItemProduct>();
        }

        public Product(string codeProduct, string kindOfProduct, string nameProduct, string measureProduct, int price)
        {
            _codeProduct = codeProduct;
            _kindOfProduct = kindOfProduct;
            _nameProduct = nameProduct;
            _measureProduct = measureProduct;
            _price = price;
            _storeItemProducts = new ObservableCollection<StoreItemProduct>();
        }

        public Product(string codeProduct, string kindOfProduct, string nameProduct, string measureProduct, int price, double amount, string wayDisplayBookBar)
        {
            _codeProduct = codeProduct;
            _kindOfProduct = kindOfProduct;
            _nameProduct = nameProduct;
            _measureProduct = measureProduct;
            _price = price;
            _amount = amount;
            _storeItemProducts = new ObservableCollection<StoreItemProduct>();
            _wayDisplayBookBar = wayDisplayBookBar;
        }


        public Product(Product product)
        {
            _codeProduct = product.CodeProduct;
            _kindOfProduct = product.KindOfProduct;
            _nameProduct = product.NameProduct;
            _measureProduct = product.MeasureProduct;
            _price = product.Price;
            _amount = product.Amount;

            if (product.StoreItemProducts != null)
            {
                _storeItemProducts = product.StoreItemProducts;
            }
            else 
            {
                _storeItemProducts = new ObservableCollection<StoreItemProduct>();
            }

            _wayDisplayBookBar = product.WayDisplayBookBar;
        }

        #endregion


        #region properties


        public string WayDisplayBookBar
        {
            get { return _wayDisplayBookBar; }
            set { _wayDisplayBookBar = value; }
        }

        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
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

        public string NameProduct
        {
            get { return _nameProduct; }
            set { _nameProduct = value; }
        }

        public string MeasureProduct
        {
            get { return _measureProduct; }
            set { _measureProduct = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public ObservableCollection<StoreItemProduct> StoreItemProducts
        {
            get { return _storeItemProducts; }
            set { _storeItemProducts = value; }
        }

        #endregion

        #region methods

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
            return _nameProduct + " " + _measureProduct;
        }

        #endregion
    }
}
