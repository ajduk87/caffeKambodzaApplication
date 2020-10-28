using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace caffeKambodzaApplication
{
    public class HistoryChangePrices
    {

        #region members

        private string _code;
        private string _name;
        private string _type;
        private string _oldPrice;
        private string _newPrice;
        private string _dateChanged;

        #endregion


        #region constructors

        public HistoryChangePrices() { }

        public HistoryChangePrices(string code, string name, string type, string oldPrice, string newPrice, DateTime date) 
        {
            _code = code;
            _name = name;
            _type = type;
            _oldPrice = oldPrice;
            _newPrice = newPrice;
            _dateChanged = date.ToShortDateString();
        }

        #endregion


        #region properties


        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }


        public string OldPrice
        {
            get { return _oldPrice; }
            set { _oldPrice = value; }
        }

        public string NewPrice
        {
            get { return _newPrice; }
            set { _newPrice = value; }
        }

        public string DateChanged
        {
            get { return _dateChanged; }
            set { _dateChanged = value; }
        }

        #endregion

    }
}
