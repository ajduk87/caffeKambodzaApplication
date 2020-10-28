using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace caffeKambodzaApplication
{
    public class ProductWithOrderNumber : Product
    {

        #region members

        private int _orderNumber = -1;//first order number is 1 not zero

        #endregion


        #region constructors

        public ProductWithOrderNumber() { }

        public ProductWithOrderNumber(Product product, int orderNumber)
            : base(product) 
        {
            _orderNumber = orderNumber;
        }

        #endregion

        #region properties

        public int OrderNumber
        {
            get { return _orderNumber; }
            set { _orderNumber = value; }
        }

        #endregion

    }
}
