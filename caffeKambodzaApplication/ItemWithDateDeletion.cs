using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace caffeKambodzaApplication
{
    public class ItemWithDateDeletion : ItemWithDate
    {

        #region members

        private string _deleteReason;

        #endregion



        #region constructors


        public ItemWithDateDeletion (string codeProduct, string kindOfProduct, int price, int amount, int costItem, string shift, long numOfCount,string date,string deleteReason)
        :base(codeProduct, kindOfProduct, price, amount, costItem, shift, numOfCount,date) 
        {
            _deleteReason = deleteReason;
        }

        public ItemWithDateDeletion(ItemWithDate it, string deleteReason)
            : base(it.CodeProduct, it.KindOfProduct, it.Price, it.Amount, it.CostItem, it.Shift, it.NumOfCount, it.Date)
        {
            _deleteReason = deleteReason;
        }

        #endregion


        #region properties

        public string DeleteReason
        {
            get { return _deleteReason; }
            set { _deleteReason = value; }
        }

        #endregion


    }
}
