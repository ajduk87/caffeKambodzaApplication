using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.ComponentModel;

namespace caffeKambodzaApplication
{
    /// <summary>
    /// storehouse item representation in table EverEnterInStorehouse
    /// </summary>
    public class StorehouseItemRecord : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        #region members

        private string _storeItemCode = String.Empty;
        private string _storeItemName = String.Empty;
        private string _realAmount = String.Empty;
        private string _realPrice = String.Empty;
        private string _valuta = String.Empty;
        private string _createdDateTimeInApp;
        private string _lastDateTimeUpdatedInApp;
        private string _userCanControlDateTime;
        private string _userLastUpdateDateTime;
        private string _numberOfUpdates;
        private string _threshold;

        #endregion



        #region constructors

        public StorehouseItemRecord() { }

        public StorehouseItemRecord(string storeItemCode, string storeItemName, string realAmount, string realPrice, string valuta, DateTime createdUserDateTime, DateTime lastDateTimeUpdatedInApp, DateTime userCanControlDateTime, DateTime userLastUpdateDateTime, string numberOfUpdates, string threshold) 
        {
            _storeItemCode = storeItemCode;
            _storeItemName = storeItemName;
            _realAmount = realAmount;
            _realPrice = realPrice;
            _valuta = valuta;
            _createdDateTimeInApp = createdUserDateTime.ToShortDateString();
            _lastDateTimeUpdatedInApp = lastDateTimeUpdatedInApp.ToString();
           
            _userCanControlDateTime = userCanControlDateTime.ToShortDateString();
            _userLastUpdateDateTime = userLastUpdateDateTime.ToShortDateString();

            _numberOfUpdates = numberOfUpdates;
            _threshold = threshold;
        }

        public StorehouseItemRecord(string storeItemCode, string storeItemName, string realAmount, string realPrice, string valuta, string numberOfUpdates)
        {
            _storeItemCode = storeItemCode;
            _storeItemName = storeItemName;
            _realAmount = realAmount;
            _realPrice = realPrice;
            _valuta = valuta;
          /*  _createdDateTimeInApp = createdDateTimeInApp.ToString();
            _lastDateTimeUpdatedInApp = lastDateTimeUpdatedInApp.ToString();

            _userCanControlDateTime = userCanControlDateTime;
            _userLastUpdateDateTime = userLastUpdateDateTime;*/

            _numberOfUpdates = numberOfUpdates;
          //  _threshold = threshold;
        }



        public StorehouseItemRecord(StorehouseItemRecord sR)
        {
            _storeItemCode = sR.StoreItemCode;
            _storeItemName = sR.StoreItemName;
            _realAmount = sR.RealAmount;
            _realPrice = sR.RealPrice;
            _valuta = sR.Valuta;
            _createdDateTimeInApp = sR.CreatedDateTimeInApp.ToString();
            _lastDateTimeUpdatedInApp = sR.LastDateTimeUpdatedInApp.ToString();

            _userCanControlDateTime = sR.UserCanControlDateTime;
            _userLastUpdateDateTime = sR.UserLastUpdateDateTime;

            _numberOfUpdates = sR.NumberOfUpdates;
            _threshold = sR.Threshold;
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

        public string RealAmount
        {
            get { return _realAmount; }
            set { _realAmount = value; }
        }


        public string RealPrice 
        {
            get { return _realPrice; }
            set { _realPrice = value; }
        }

        public string Valuta
        {
            get { return _realPrice; }
            set { _realPrice = value; }
        }

        public string CreatedDateTimeInApp
        {
            get { return _createdDateTimeInApp; }
            set { _createdDateTimeInApp = value; }
        }


        public string LastDateTimeUpdatedInApp
        {
            get { return _lastDateTimeUpdatedInApp; }
            set { _lastDateTimeUpdatedInApp = value; }
        }

        public string UserCanControlDateTime
        {
            get { return _userCanControlDateTime; }
            set { _userCanControlDateTime = value; }
        }


        public string UserLastUpdateDateTime
        {
            get { return _userLastUpdateDateTime; }
            set { _userLastUpdateDateTime = value; }
        }


        public string NumberOfUpdates
        {
            get { return _numberOfUpdates; }
            set 
            { 
                _numberOfUpdates = value;
                OnPropertyChanged("NumberOfUpdates");
            }
        }


        public string Threshold
        {
            get { return _threshold; }
            set { _threshold = value; }
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


    }
}
