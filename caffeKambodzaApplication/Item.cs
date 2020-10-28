using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace caffeKambodzaApplication
{
    public class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region members

        private string _codeProduct;
        private string _kindOfProduct;
        private string _wayDisplayProduct;
        private int _price;
        private int _amount;
        private int _costItem;
        private string _shift;
        private ObservableCollection<StoreItemProduct> usedStoreItem ;
        int numOfUsedRecipes;
        private Product product;

        private long _numOfCount;

        #endregion

        #region constructors

        public Item() { usedStoreItem = new ObservableCollection<StoreItemProduct>(); numOfUsedRecipes = 0; }

        public Item(string kindOfProduct, int price, int amount, int costItem)
        {
            _kindOfProduct = kindOfProduct;
            _price = price;
            _amount = amount;
            _costItem = costItem;
            _codeProduct = "Code product is not entered.";
            usedStoreItem = new ObservableCollection<StoreItemProduct>(); 
        }

        public Item(string codeProduct, string kindOfProduct, int price, int amount, int costItem)
        {
            _codeProduct = codeProduct;
            _kindOfProduct = kindOfProduct;
            _price = price;
            _amount = amount;
            _costItem = costItem;
            usedStoreItem = new ObservableCollection<StoreItemProduct>();
            numOfUsedRecipes = 0;
        }

        public Item(string codeProduct, string kindOfProduct, int price, int amount, int costItem, string shift)
        {
            
            _codeProduct = codeProduct;
            _kindOfProduct = kindOfProduct;
            _price = price;
            _amount = amount;
            _costItem = costItem;
            _shift = shift;
            usedStoreItem = new ObservableCollection<StoreItemProduct>();
            numOfUsedRecipes = 0;
        }

        public Item(Product p,string codeProduct, string kindOfProduct,string wayDisplayProduct, int price, int amount, int costItem, string shift, long numOfCount)
        {
            product = p;
            _codeProduct = codeProduct;
            _kindOfProduct = kindOfProduct;
            _wayDisplayProduct = wayDisplayProduct;
            _price = price;
            _amount = amount;
            _costItem = costItem;
            _shift = shift;
            _numOfCount = numOfCount;
            usedStoreItem = new ObservableCollection<StoreItemProduct>();
            numOfUsedRecipes = 0;
        }

        public Item(string codeProduct, string kindOfProduct, string wayDisplayProduct, int price, int amount, int costItem, string shift, long numOfCount)
        {
        
            _codeProduct = codeProduct;
            _kindOfProduct = kindOfProduct;
            _wayDisplayProduct = wayDisplayProduct;
            _price = price;
            _amount = amount;
            _costItem = costItem;
            _shift = shift;
            _numOfCount = numOfCount;
            usedStoreItem = new ObservableCollection<StoreItemProduct>();
            numOfUsedRecipes = 0;
        }

        #endregion

        #region properties

        public Product Product 
        {
            get { return product; }
            set { product = value; }
        }

        public long NumOfCount
        {
            get { return _numOfCount; }
            set { _numOfCount = value; }
        }

        public string CodeProduct
        {
            get { return _codeProduct; }
            set 
            { 
                _codeProduct = value;
                OnPropertyChanged("CodeProduct");
            }
        }

        public string KindOfProduct
        {
            get { return _kindOfProduct; }
            set { _kindOfProduct = value; }
        }

        public string WayDisplayProduct
        {
            get { return _wayDisplayProduct; }
            set { _wayDisplayProduct = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set 
            { 
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public int CostItem
        {
            get { return _costItem; }
            set 
            { 
                _costItem = value;
                OnPropertyChanged("CostItem");
            }
        }

        public string Shift 
        {
            get { return _shift; }
            set { _shift = value; }
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


        public ObservableCollection<StoreItemProduct> UsedStoreItem
        {
            get { return usedStoreItem; }
            set { usedStoreItem = value; }
        }



        public int NumOfUsedRecipes 
        {
            get { return numOfUsedRecipes; }
            set { numOfUsedRecipes = value; }
        }

        #endregion

        #region methods

        public void addUsedStoreItem(StoreItemProduct sItem)
        {
            usedStoreItem.Add(sItem);
        }


        #endregion
    }
}
