using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace caffeKambodzaApplication
{
    public class ItemWithDateCorrection : ItemWithDate
    {

        #region members

        private double _oldAmount;
        private double _diffAmount;
        private double _oldCostItem;
        private double _diffCostItem;

        private string _correctionReason;

        #endregion


        #region constructors

        public ItemWithDateCorrection() { }

        public ItemWithDateCorrection(ItemWithDate it, double oldAmount, double oldCostItem, string correctionReason)
            : base(it.CodeProduct, it.KindOfProduct, it.Price, it.Amount, it.CostItem, it.Shift, it.NumOfCount, it.Date)
        {
            _oldAmount = oldAmount;
            _oldCostItem = oldCostItem;
            _diffAmount = it.Amount - _oldAmount ;
            _diffCostItem = it.CostItem - _oldCostItem;
            _correctionReason = correctionReason;
 
        }

        #endregion


        #region properties


        public double OldAmount 
        {
            get { return _oldAmount; }
            set { _oldAmount = value; }
        }

        public double OldCostItem
        {
            get { return _oldCostItem; }
            set { _oldCostItem = value; }
        }

        public double DiffAmount
        {
            get { return _diffAmount; }
            set { _diffAmount = value; }
        }

        public double DiffCostItem
        {
            get { return _diffCostItem; }
            set { _diffCostItem = value; }
        }

        public string CorrectionReason 
        {
            get { return _correctionReason; }
            set { _correctionReason = value; }
        }

        #endregion

    }
}
