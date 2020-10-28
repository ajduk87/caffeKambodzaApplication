using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace caffeKambodzaApplication
{
    public class StorehouseItemRecordCorr
    {
        #region members

        private string _storeItemCode = String.Empty;
        private string _storeItemName = String.Empty;
        private string _oldAmount = String.Empty;
        private string _newRealAmount = String.Empty;
        private string _differenceRealAmount = String.Empty;
        private string _oldRealPrice = String.Empty;
        private string _newRealPrice = String.Empty;
        private string _diffRealPrice = String.Empty;
        private string _valuta = String.Empty;
       
        private string _correctionUserDateTime = String.Empty;
        private string _correctionReason = String.Empty;
        

        #endregion


        #region constructors

        public StorehouseItemRecordCorr(string storeItemCode, string storeItemName, string oldAmount, string newRealAmount, string differenceRealAmount, string oldRealPrice, string newRealPrice, string diffRealPrice, string valuta, string correctionDateTimeInApp, string correctionReason)   
        {
            _storeItemCode = storeItemCode;
            _storeItemName = storeItemName;
            _oldAmount = oldAmount;
            _newRealAmount = newRealAmount;
            _differenceRealAmount = differenceRealAmount;
            _oldRealPrice = oldRealPrice;
            _newRealPrice = newRealPrice;
            _diffRealPrice = diffRealPrice;
            _valuta = valuta;
            _correctionUserDateTime = correctionDateTimeInApp;
            _correctionReason = correctionReason;
        }

        #endregion


        #region properties


        public string StoreItemCode
        {
            get { return _storeItemCode; }
            set { _storeItemCode = value; }
        }


        public string StoreItemName
        {
            get { return _storeItemName; }
            set { _storeItemName = value; }
        }

        public string OldAmount
        {
            get { return _oldAmount; }
            set { _oldAmount = value; }
        }

        public string NewRealAmount
        {
            get { return _newRealAmount; }
            set { _newRealAmount = value; }
        }

        public string DifferenceRealAmount
        {
            get { return _differenceRealAmount; }
            set {_differenceRealAmount = value;}
        }


        public string OldRealPrice
        {
            get { return _oldRealPrice; }
            set { _oldRealPrice = value; }
        }


        public string NewRealPrice
        {
            get { return _newRealPrice; }
            set { _newRealPrice = value; }
        }

        public string DiffRealPrice
        {
            get { return _diffRealPrice; }
            set { _diffRealPrice = value; }
        }

        public string Valuta
        {
            get { return _valuta; }
            set { _valuta = value; }
        }

        public string CorrectionUserDateTime
        {
            get { return _correctionUserDateTime; }
            set { _correctionUserDateTime = value; }
        }

        public string CorrectionReason
        {
            get { return _correctionReason; }
            set { _correctionReason = value; }
        }

       

       

        #endregion

    }
}
