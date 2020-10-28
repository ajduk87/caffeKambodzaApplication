using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace caffeKambodzaApplication
{
    public class ItemWithDate : Item
    {


        #region members

        private string _date;

        #endregion

        #region constructors

        public ItemWithDate() { }

        public ItemWithDate(string codeProduct, string kindOfProduct, int price, int amount, int costItem, string shift, long numOfCount,string date)
            : base(codeProduct,String.Empty, kindOfProduct, price, amount, costItem, shift, numOfCount) 
        {
            _date = date;
        }


        public ItemWithDate(Item item, string date)
            : base(item.CodeProduct, item.KindOfProduct,String.Empty, item.Price , item.Amount, item.CostItem, item.Shift, item.NumOfCount)
        {
            _date = date;
        }

        #endregion



        #region properties

        public string Date 
        {
            get { return _date; }
            set 
            {
                _date = value;
            }
        }

        #endregion



        #region methods

        public override string ToString()
        {
            return base.CodeProduct + '&' + base.KindOfProduct + '&' + base.Price + '&' + base.Amount + '&' + base.CostItem + '&' + _date;
        }

        #endregion
    }
}
