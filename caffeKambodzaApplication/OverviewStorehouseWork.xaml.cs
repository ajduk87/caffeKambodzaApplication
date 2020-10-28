using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;
using System.Data.OleDb;


namespace caffeKambodzaApplication
{
    /// <summary>
    /// Interaction logic for OverviewStorehouseWork.xaml
    /// </summary>
    public partial class OverviewStorehouseWork : UserControl
    {


        private OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
        //private OleDbConnection conCorrection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
        private OleDbCommand com;
        private OleDbDataReader dr;




        private bool filteredDgridUsedTab1;
        private bool filteredDgridUsedTab2;
        private bool filteredDgridUsedTab3;


        public ObservableCollection<StorehouseItemRecord> sRecord;
        public ObservableCollection<StorehouseItemRecordDel> sRecordDel;
        public ObservableCollection<StorehouseItemRecordCorr> sRecordCor;


        public ICollectionView cvsRecord;
        public ICollectionView cvsRecordDel;
        public ICollectionView cvsRecordCor;

        private DateTime _dateCreatedReportStart, _dateCreatedReportEnd;
        private DateTime _dateCreatedReportStartTab2, _dateCreatedReportEndTab2;
        private DateTime _dateCreatedReportStartTab3, _dateCreatedReportEndTab3;


        public DateTime DateCreatedReportStart
        {
            get { return _dateCreatedReportStart; }
            set { _dateCreatedReportStart = value; }
        }

        public DateTime DateCreatedReportEnd
        {
            get { return _dateCreatedReportEnd; }
            set { _dateCreatedReportEnd = value; }
        }

        public DateTime DateCreatedReportStartTab2
        {
            get { return _dateCreatedReportStartTab2; }
            set { _dateCreatedReportStartTab2 = value; }
        }

        public DateTime DateCreatedReportEndTab2
        {
            get { return _dateCreatedReportEndTab2; }
            set { _dateCreatedReportEndTab2 = value; }
        }

        public DateTime DateCreatedReportStartTab3
        {
            get { return _dateCreatedReportStartTab3; }
            set { _dateCreatedReportStartTab3 = value; }
        }


        public DateTime DateCreatedReportEndTab3
        {
            get { return _dateCreatedReportEndTab3; }
            set { _dateCreatedReportEndTab3 = value; }
        }


        public OverviewStorehouseWork()
        {
            InitializeComponent();

            cmbFilterColumnTab1.SelectedIndex = 0;
            filteredDgridUsedTab1 = false;
            cmbFilterColumnTab2.SelectedIndex = 0;
            filteredDgridUsedTab2 = false;
            cmbFilterColumnTab3.SelectedIndex = 0;
            filteredDgridUsedTab3 = false;

            sRecord = new ObservableCollection<StorehouseItemRecord>();
            sRecordDel = new ObservableCollection<StorehouseItemRecordDel>();
            sRecordCor = new ObservableCollection<StorehouseItemRecordCorr>();

            cvsRecord = CollectionViewSource.GetDefaultView(sRecord);
            if (cvsRecord != null)
            {
                dataGridReadStore.ItemsSource = cvsRecord;
            }

            cvsRecordDel = CollectionViewSource.GetDefaultView(sRecordDel);
            if (cvsRecordDel != null)
            {
                dataGridReadStoreTab2.ItemsSource = cvsRecordDel;
            }

            cvsRecordCor = CollectionViewSource.GetDefaultView(sRecordCor);
            if (cvsRecordCor != null)
            {
                dataGridReadStoreTab3.ItemsSource = cvsRecordCor;
            }
        }

       


        #region Tab1

        private void datepickerStartTab1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {


                // ... Get DatePicker reference.
                var picker = sender as DatePicker;

                // ... Get nullable DateTime from SelectedDate.
                DateTime? date = picker.SelectedDate;
                if (date == null)
                {
                    // ... A null object.

                }
                else
                {
                    // ... No need to display the time.

                    _dateCreatedReportStart = date.Value;
                    Logger.writeNode(Constants.INFORMATION, "Tab5 PodTab1 Postavljanje pocetnog datuma. Postavljeni datum je :" + _dateCreatedReportStart.ToString());

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("You did not enter date in tab1!!!");
                Logger.writeNode(Constants.EXCEPTION, "You did not enter date in tab1!!!");
            }
        }

        private void datepickerEndTab1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {


                // ... Get DatePicker reference.
                var picker = sender as DatePicker;

                // ... Get nullable DateTime from SelectedDate.
                DateTime? date = picker.SelectedDate;
                if (date == null)
                {
                    // ... A null object.

                }
                else
                {
                    // ... No need to display the time.

                    _dateCreatedReportEnd = date.Value;
                    Logger.writeNode(Constants.INFORMATION, "Tab5 PodTab1 Postavljanje krajnjeg datuma. Postavljeni datum je :" + _dateCreatedReportEnd.ToString());

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("You did not enter date in tab1!!!");
                Logger.writeNode(Constants.EXCEPTION, "You did not enter date in tab1!!!");
            }
        }


        private void btnloadReport_Click(object sender, RoutedEventArgs e)
        {
            if (datepickerStartTab1.Text.Equals(String.Empty) || datepickerEndTab1.Text.Equals(String.Empty))
            {
                System.Windows.Forms.MessageBox.Show("Morate uneti vremenski interval koji želite učitati!");
                Logger.writeNode(Constants.MESSAGEBOX, "Morate uneti vremenski interval koji želite učitati!");

                return;
            }
            else
            {
                try
                {
                    if (sRecord.Count > 0) sRecord.Clear();

                    Logger.writeNode(Constants.INFORMATION, "Tab5 PodTab1 Ucitavanje izvestaja ikada ucitanih stavki šanka");

                    string id = "40";//Queries.xml ID
                    XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                      where xml2.Element("ID").Value == id
                                      select xml2).FirstOrDefault();
                    Console.WriteLine(Query.ToString());
                    string query = Query.Attribute(Constants.TEXT).Value;

                    con.Open();
                    com = new OleDbCommand(query, con);
                    com.Parameters.AddWithValue("@StartDate", _dateCreatedReportStart);
                    com.Parameters.AddWithValue("@EndDate", _dateCreatedReportEnd);
                    dr = com.ExecuteReader();


                    string StoreItemCode = String.Empty;
                    string StoreItemName = String.Empty;
                    string RealAmount = String.Empty;
                    string RealPrice = String.Empty;
                    string Valuta = String.Empty;
                    string CreatedDateTimeInApp = String.Empty;
                    string LastDateTimeUpdatedInApp = String.Empty;
                    string UserCanControlDateTime = String.Empty;
                    string UserLastUpdateDateTime = String.Empty;
                    string NumberOfUpdates = String.Empty;
                    string Threshold = String.Empty;

                    string DateCreatedReport = String.Empty;


                    while (dr.Read())
                    {

                        StoreItemCode = dr["StoreItemCode"].ToString();
                        StoreItemName = dr["StoreItemName"].ToString();
                        RealAmount = dr["RealAmount"].ToString();
                        RealPrice = dr["RealPrice"].ToString();
                        Valuta = dr["Valuta"].ToString();
                        CreatedDateTimeInApp = dr["CreatedDateTimeInApp"].ToString();
                        DateTime createDateTime = DateTime.Parse(CreatedDateTimeInApp);
                        LastDateTimeUpdatedInApp = dr["LastDateTimeUpdatedInApp"].ToString();
                        DateTime lastDateTimeApp = DateTime.Parse(LastDateTimeUpdatedInApp);
                        UserCanControlDateTime = dr["UserCanControlDateTime"].ToString();
                        UserCanControlDateTime = UserCanControlDateTime.Replace("0:00:00", "");
                        DateTime userCanConDateTime = DateTime.Parse(UserCanControlDateTime);
                        UserLastUpdateDateTime = dr["UserLastUpdateDateTime"].ToString();
                        UserLastUpdateDateTime = UserLastUpdateDateTime.Replace("0:00:00", "");
                        DateTime userLastUpdateDateTime = DateTime.Parse(UserLastUpdateDateTime);
                        NumberOfUpdates = dr["NumberOfUpdates"].ToString();
                        Threshold = dr["Threshold"].ToString();
                       
                       
                        StorehouseItemRecord sR = new StorehouseItemRecord(StoreItemCode, StoreItemName, RealAmount, RealPrice, Valuta, createDateTime, lastDateTimeApp, userCanConDateTime, userLastUpdateDateTime, NumberOfUpdates, Threshold);
                        sRecord.Add(sR);
                    }

                    cvsRecord = CollectionViewSource.GetDefaultView(sRecord);
                    if (cvsRecord != null)
                    {
                        dataGridReadStore.ItemsSource = cvsRecord;
                    }


                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    Logger.writeNode(Constants.EXCEPTION, ex.Message);
                }
                finally
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                    if (dr != null)
                    {
                        dr.Close();
                    }
                }
            }
        }


        #endregion



        #region Tab2

        private void datepickerStartTab2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {


                // ... Get DatePicker reference.
                var picker = sender as DatePicker;

                // ... Get nullable DateTime from SelectedDate.
                DateTime? date = picker.SelectedDate;
                if (date == null)
                {
                    // ... A null object.

                }
                else
                {
                    // ... No need to display the time.

                    _dateCreatedReportStartTab2 = date.Value;
                    Logger.writeNode(Constants.INFORMATION, "Tab5 PodTab2 Postavljanje pocetnog datuma. Postavljeni datum je :" + _dateCreatedReportStartTab2.ToString());

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("You did not enter date in tab1!!!");
                Logger.writeNode(Constants.EXCEPTION, "You did not enter date in tab1!!!");
            }

        }

        private void datepickerEndTab2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {


                // ... Get DatePicker reference.
                var picker = sender as DatePicker;

                // ... Get nullable DateTime from SelectedDate.
                DateTime? date = picker.SelectedDate;
                if (date == null)
                {
                    // ... A null object.

                }
                else
                {
                    // ... No need to display the time.

                    _dateCreatedReportEndTab2 = date.Value;
                    Logger.writeNode(Constants.INFORMATION, "Tab5 PodTab2 Postavljanje krajnjeg datuma. Postavljeni datum je :" + _dateCreatedReportEndTab2.ToString());

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("You did not enter date in tab1!!!");
                Logger.writeNode(Constants.EXCEPTION, "You did not enter date in tab1!!!");
            }
        }

        private void btnloadReportTab2_Click(object sender, RoutedEventArgs e)
        {
            if (datepickerStartTab2.Text.Equals(String.Empty) || datepickerEndTab2.Text.Equals(String.Empty))
            {
                System.Windows.Forms.MessageBox.Show("Morate uneti vremenski interval koji želite učitati!");
                Logger.writeNode(Constants.MESSAGEBOX, "Morate uneti vremenski interval koji želite učitati!");
                return;
            }
            else
            {
                try
                {
                    if (sRecordDel.Count > 0) sRecordDel.Clear();

                    Logger.writeNode(Constants.INFORMATION, "Tab5 PodTab2 Ucitavanje izvestaja ikada obrisanih stavki šanka");

                    string id = "41";//Queries.xml ID
                    XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                      where xml2.Element("ID").Value == id
                                      select xml2).FirstOrDefault();
                    Console.WriteLine(Query.ToString());
                    string query = Query.Attribute(Constants.TEXT).Value;

                    con.Open();
                    com = new OleDbCommand(query, con);
                    com.Parameters.AddWithValue("@StartDate", _dateCreatedReportStartTab2);
                    com.Parameters.AddWithValue("@EndDate", _dateCreatedReportEndTab2);
                    dr = com.ExecuteReader();


                    string StoreItemCode = String.Empty;
                    string StoreItemName = String.Empty;
                    string RealAmount = String.Empty;
                    string RealPrice = String.Empty;
                    string Valuta = String.Empty;
                    string CreatedDateTimeInApp = String.Empty;
                    string LastDateTimeUpdatedInApp = String.Empty;
                    string UserCanControlDateTime = String.Empty;
                    string UserLastUpdateDateTime = String.Empty;
                    string NumberOfUpdates = String.Empty;
                    string Threshold = String.Empty;
                    string delReas = String.Empty;

                    string DateCreatedReport = String.Empty;


                    while (dr.Read())
                    {

                        StoreItemCode = dr["StoreItemCode"].ToString();
                        StoreItemName = dr["StoreItemName"].ToString();
                        RealAmount = dr["RealAmount"].ToString();
                        RealPrice = dr["RealPrice"].ToString();
                        Valuta = dr["Valuta"].ToString();
                        CreatedDateTimeInApp = dr["DeletionUserDateTime"].ToString();
                        DateTime createDateTime = DateTime.Parse(CreatedDateTimeInApp);
                        LastDateTimeUpdatedInApp = dr["LastDateTimeUpdatedInApp"].ToString();
                        DateTime lastDateTimeApp = DateTime.Parse(LastDateTimeUpdatedInApp);
                        UserCanControlDateTime = dr["UserCanControlDateTime"].ToString();
                        
                        DateTime userCanConDateTime = DateTime.Parse(UserCanControlDateTime);
                        UserLastUpdateDateTime = dr["UserLastUpdateDateTime"].ToString();
                        
                        DateTime userLastUpdateDateTime = DateTime.Parse(UserLastUpdateDateTime);
                        NumberOfUpdates = dr["NumberOfUpdates"].ToString();
                        Threshold = dr["Threshold"].ToString();
                        delReas = dr["DeletionReason"].ToString();

                        StorehouseItemRecord sR = new StorehouseItemRecord(StoreItemCode, StoreItemName, RealAmount, RealPrice, Valuta, createDateTime, lastDateTimeApp, userCanConDateTime, userLastUpdateDateTime, NumberOfUpdates, Threshold);
                        StorehouseItemRecordDel sRDel = new StorehouseItemRecordDel(sR, delReas);
                        sRecordDel.Add(sRDel);
                    }

                    cvsRecordDel = CollectionViewSource.GetDefaultView(sRecordDel);
                    if (cvsRecordDel != null)
                    {
                        dataGridReadStoreTab2.ItemsSource = cvsRecordDel;
                    }


                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    Logger.writeNode(Constants.EXCEPTION, ex.Message);
                }
                finally
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                    if (dr != null)
                    {
                        dr.Close();
                    }
                }
            }

        }

        #endregion

       


        #region Tab3


        private void datepickerStartTab3_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {


                // ... Get DatePicker reference.
                var picker = sender as DatePicker;

                // ... Get nullable DateTime from SelectedDate.
                DateTime? date = picker.SelectedDate;
                if (date == null)
                {
                    // ... A null object.

                }
                else
                {
                    // ... No need to display the time.

                    _dateCreatedReportStartTab3 = date.Value;
                    Logger.writeNode(Constants.INFORMATION, "Tab5 PodTab3 Postavljanje pocetnog datuma. Postavljeni datum je :" + _dateCreatedReportStartTab3.ToString());

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("You did not enter date in tab1!!!");
                Logger.writeNode(Constants.EXCEPTION, "You did not enter date in tab1!!!");
            }

        }

        private void datepickerEndTab3_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {


                // ... Get DatePicker reference.
                var picker = sender as DatePicker;

                // ... Get nullable DateTime from SelectedDate.
                DateTime? date = picker.SelectedDate;
                if (date == null)
                {
                    // ... A null object.

                }
                else
                {
                    // ... No need to display the time.

                    _dateCreatedReportEndTab3 = date.Value;
                    Logger.writeNode(Constants.INFORMATION, "Tab5 PodTab3 Postavljanje krajnjeg datuma. Postavljeni datum je :" + _dateCreatedReportEndTab3.ToString());

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("You did not enter date in tab1!!!");
                Logger.writeNode(Constants.EXCEPTION, "You did not enter date in tab1!!!");
            }
        }


        private void btnloadReportTab3_Click(object sender, RoutedEventArgs e)
        {
            if (datepickerStartTab3.Text.Equals(String.Empty) || datepickerEndTab3.Text.Equals(String.Empty))
            {
                System.Windows.Forms.MessageBox.Show("Morate uneti vremenski interval koji želite učitati!");
                Logger.writeNode(Constants.MESSAGEBOX, "Morate uneti vremenski interval koji želite učitati!");
                return;
            }
            else
            {
                try
                {
                    if (sRecordCor.Count > 0) sRecordCor.Clear();

                    Logger.writeNode(Constants.INFORMATION, "Tab5 PodTab2 Ucitavanje izvestaja ikada korigovanih stavki šanka");

                    string id = "42";//Queries.xml ID
                    XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                      where xml2.Element("ID").Value == id
                                      select xml2).FirstOrDefault();
                    Console.WriteLine(Query.ToString());
                    string query = Query.Attribute(Constants.TEXT).Value;

                    con.Open();
                    com = new OleDbCommand(query, con);
                    com.Parameters.AddWithValue("@StartDate", _dateCreatedReportStartTab3);
                    com.Parameters.AddWithValue("@EndDate", _dateCreatedReportEndTab3);
                    dr = com.ExecuteReader();


                    string StoreItemCode = String.Empty;
                    string StoreItemName = String.Empty;
                    string OLDRealAmount = String.Empty;
                    string NEWRealAmount = String.Empty;
                    string DiffRealAmount = String.Empty;
                    string OLDRealPrice = String.Empty;
                    string NEWRealPrice = String.Empty;
                    string DiffRealPrice = String.Empty;
                    string Valuta = String.Empty;
                    string CreatedDateTimeInApp = String.Empty;
                    string LastDateTimeUpdatedInApp = String.Empty;
                    string UserCanControlDateTime = String.Empty;
                    string UserLastUpdateDateTime = String.Empty;
                    string NumberOfUpdates = String.Empty;
                    string CorrectionDateTimeInApp = String.Empty;
                    string corReas = String.Empty;

                    string DateCreatedReport = String.Empty;


                    while (dr.Read())
                    {

                        StoreItemCode = dr["StoreItemCode"].ToString();
                        StoreItemName = dr["StoreItemName"].ToString();
                        OLDRealAmount = dr["OLDRealAmount"].ToString();
                        NEWRealAmount = dr["NEWRealAmount"].ToString();
                        DiffRealAmount = dr["DifferenceRealAmount"].ToString();
                        OLDRealPrice = dr["OLDRealPrice"].ToString();
                        NEWRealPrice = dr["NEWRealPrice"].ToString();
                        DiffRealPrice = dr["DifferenceRealPrice"].ToString();
                        Valuta = dr["Valuta"].ToString();

                        CorrectionDateTimeInApp = dr["CorrectionUserDateTime"].ToString();
                        DateTime dateUserCorrection = DateTime.Parse(CorrectionDateTimeInApp);
                        
                        corReas = dr["CorrectionReason"].ToString();


                        StorehouseItemRecordCorr sRCor = new StorehouseItemRecordCorr(StoreItemCode, StoreItemName, OLDRealAmount, NEWRealAmount, DiffRealAmount, OLDRealPrice, NEWRealPrice, DiffRealPrice, Valuta, dateUserCorrection.ToShortDateString(), corReas);
                        sRecordCor.Add(sRCor);
                    }

                    cvsRecordCor = CollectionViewSource.GetDefaultView(sRecordCor);
                    if (cvsRecordCor != null)
                    {
                        dataGridReadStoreTab3.ItemsSource = cvsRecordCor;
                    }


                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    Logger.writeNode(Constants.EXCEPTION, ex.Message);
                }
                finally
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                    if (dr != null)
                    {
                        dr.Close();
                    }
                }
            }
        }

        #endregion

        #region filteringTab1

        private void filterDataTab1()
        {

            tblFilterStatusTab1.Text = Constants.FILTERON;
            tblFilterStatusTab1.Background = Brushes.Orange;

            this.cvsRecord.Filter = item =>
            {
                var vitem = item as StorehouseItemRecord;
                if (vitem == null) return false;
                string searchText = tfFilterTab1.Text.ToUpper();

                if (cmbFilterColumnTab1.SelectedIndex == 1)
                {

                    string codeOfProduct = vitem.StoreItemCode.ToUpper();
                    if (codeOfProduct.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab1.SelectedIndex == 2)
                {
                    string storeName = vitem.StoreItemName.ToUpper();
                    if (storeName.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab1.SelectedIndex == 3)
                {

                    string realAmount = vitem.RealAmount.ToUpper();
                    if (realAmount.Equals(searchText) == true) { return true; }
                    else { return false; }
                }
                else 
                {

                    string priceReal = vitem.RealPrice.ToString().ToUpper();
                    if (priceReal.Equals(searchText) == true) { return true; }
                    else { return false; }
                }
               

            };

        }



        private void unFilteredDataTab1()
        {
            tblFilterStatusTab1.Text = String.Empty;
            tblFilterStatusTab1.Background = Brushes.White;

            this.cvsRecord.Filter = item =>
            {

                var vitem = item as StorehouseItemRecord;
                if (vitem == null) return false;
                else return true;

            };
        }

        private void tfFilterTab1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfFilterTab1.Text.Equals(String.Empty) == false)
            {
                Logger.writeNode(Constants.INFORMATION, "Tab5 PodTab1 Filtriranje reci. Broj kolone koja se filtrira " + cmbFilterColumnTab1.SelectedIndex.ToString() + ". Filtrirana rec :" + tfFilterTab1.Text);
                filterDataTab1();
                filteredDgridUsedTab1 = true;
            }
            else
            {
                unFilteredDataTab1();
                filteredDgridUsedTab1 = false;
                tblFilterStatusTab1.Text = String.Empty;
                MainWindow win = (MainWindow)Window.GetWindow(this);
                if (win.options.chkbMask.IsChecked == true)
                {
                    Object obj1 = this.Resources["Gradient4"];
                    tblFilterStatusTab1.Background = (Brush)obj1;
                }
                else
                {
                    tblFilterStatusTab1.Background = Brushes.White;
                }
            }

        }


        private void tfFilterTab1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (cmbFilterColumnTab1.SelectedIndex == 0)
            {
                tfFilterTab1.IsReadOnly = true;

                tblFilterStatusTab1.Text = Constants.FILTER_COLUMN;
                tblFilterStatusTab1.Foreground = Brushes.White;
                tblFilterStatusTab1.Background = Brushes.Red;
            }
            else
            {
                tfFilterTab1.IsReadOnly = false;
            }
        }

        private void tfFilterTab1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (filteredDgridUsedTab1 == false)
            {
                tblFilterStatusTab1.Text = String.Empty;
                MainWindow win = (MainWindow)Window.GetWindow(this);
                if (win.options.chkbMask.IsChecked == true)
                {
                    Object obj1 = this.Resources["Gradient4"];
                    tblFilterStatusTab1.Background = (Brush)obj1;
                }
                else
                {
                    tblFilterStatusTab1.Background = Brushes.White;
                }
            }
        }



        #endregion

          

        #region filteringTab2

        private void filterDataTab2()
        {

            tblFilterStatusTab2.Text = Constants.FILTERON;
            tblFilterStatusTab2.Background = Brushes.Orange;

            this.cvsRecordDel.Filter = item =>
            {
                var vitem = item as StorehouseItemRecord;
                if (vitem == null) return false;
                string searchText = tfFilterTab2.Text.ToUpper();

                if (cmbFilterColumnTab2.SelectedIndex == 1)
                {

                    string codeOfProduct = vitem.StoreItemCode.ToUpper();
                    if (codeOfProduct.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab2.SelectedIndex == 2)
                {
                    string storeName = vitem.StoreItemName.ToUpper();
                    if (storeName.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab2.SelectedIndex == 3)
                {

                    string realAmount = vitem.RealAmount.ToUpper();
                    if (realAmount.Equals(searchText) == true) { return true; }
                    else { return false; }
                }
                else
                {

                    string priceReal = vitem.RealPrice.ToString().ToUpper();
                    if (priceReal.Equals(searchText) == true) { return true; }
                    else { return false; }
                }


            };

        }



        private void unFilteredDataTab2()
        {
            tblFilterStatusTab2.Text = String.Empty;
            tblFilterStatusTab2.Background = Brushes.White;

            this.cvsRecordDel.Filter = item =>
            {

                var vitem = item as StorehouseItemRecord;
                if (vitem == null) return false;
                else return true;

            };
        }

        private void tfFilterTab2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfFilterTab2.Text.Equals(String.Empty) == false)
            {
                Logger.writeNode(Constants.INFORMATION, "Tab5 PodTab2 Filtriranje reci. Broj kolone koja se filtrira " + cmbFilterColumnTab2.SelectedIndex.ToString() + ". Filtrirana rec :" + tfFilterTab2.Text);
                filterDataTab2();
                filteredDgridUsedTab2 = true;
            }
            else
            {
                unFilteredDataTab2();
                filteredDgridUsedTab2 = false;
                tblFilterStatusTab2.Text = String.Empty;
                MainWindow win = (MainWindow)Window.GetWindow(this);
                if (win.options.chkbMask.IsChecked == true)
                {
                    Object obj1 = this.Resources["Gradient4"];
                    tblFilterStatusTab2.Background = (Brush)obj1;
                }
                else
                {
                    tblFilterStatusTab2.Background = Brushes.White;
                }
            }

        }


        private void tfFilterTab2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (cmbFilterColumnTab2.SelectedIndex == 0)
            {
                tfFilterTab2.IsReadOnly = true;

                tblFilterStatusTab2.Text = Constants.FILTER_COLUMN;
                tblFilterStatusTab2.Foreground = Brushes.White;
                tblFilterStatusTab2.Background = Brushes.Red;
            }
            else
            {
                tfFilterTab2.IsReadOnly = false;
            }
        }

        private void tfFilterTab2_MouseLeave(object sender, MouseEventArgs e)
        {
            if (filteredDgridUsedTab2 == false)
            {
                tblFilterStatusTab2.Text = String.Empty;
                MainWindow win = (MainWindow)Window.GetWindow(this);
                if (win.options.chkbMask.IsChecked == true)
                {
                    Object obj1 = this.Resources["Gradient4"];
                    tblFilterStatusTab2.Background = (Brush)obj1;
                }
                else
                {
                    tblFilterStatusTab2.Background = Brushes.White;
                }
            }
        }

        #endregion


        #region filteringTab3





        private void filterDataTab3()
        {

            tblFilterStatusTab3.Text = Constants.FILTERON;
            tblFilterStatusTab3.Background = Brushes.Orange;

            this.cvsRecordCor.Filter = item =>
            {
                var vitem = item as StorehouseItemRecordCorr;
                if (vitem == null) return false;
                string searchText = tfFilterTab3.Text.ToUpper();

                if (cmbFilterColumnTab3.SelectedIndex == 1)
                {

                    string codeOfProduct = vitem.StoreItemCode.ToUpper();
                    if (codeOfProduct.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab3.SelectedIndex == 2)
                {
                    string storeName = vitem.StoreItemName.ToUpper();
                    if (storeName.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab3.SelectedIndex == 3)
                {

                    string oldrealAmount = vitem.OldAmount.ToUpper();
                    if (oldrealAmount.Equals(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab3.SelectedIndex == 4)
                {

                    string newrealAmount = vitem.NewRealAmount.ToUpper();
                    if (newrealAmount.Equals(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab3.SelectedIndex == 5)
                {

                    string diffrealAmount = vitem.DifferenceRealAmount.ToUpper();
                    if (diffrealAmount.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab3.SelectedIndex == 6)
                {

                    string oldPrice = vitem.OldRealPrice.ToUpper();
                    if (oldPrice.Equals(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab3.SelectedIndex == 7)
                {

                    string newPrice = vitem.NewRealPrice.ToUpper();
                    if (newPrice.Equals(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab3.SelectedIndex == 8)
                {

                    string diffPrice = vitem.DiffRealPrice.ToUpper();
                    if (diffPrice.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else
                {

                    string corrReason = vitem.CorrectionReason.ToString().ToUpper();
                    if (corrReason.Contains(searchText) == true) { return true; }
                    else { return false; }
                }


            };

        }



        private void unFilteredDataTab3()
        {
            tblFilterStatusTab3.Text = String.Empty;
            tblFilterStatusTab3.Background = Brushes.White;

            this.cvsRecordCor.Filter = item =>
            {

                var vitem = item as StorehouseItemRecordCorr;
                if (vitem == null) return false;
                else return true;

            };
        }

        private void tfFilterTab3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfFilterTab3.Text.Equals(String.Empty) == false)
            {
                Logger.writeNode(Constants.INFORMATION, "Tab5 PodTab3 Filtriranje reci. Broj kolone koja se filtrira " + cmbFilterColumnTab3.SelectedIndex.ToString() + ". Filtrirana rec :" + tfFilterTab3.Text);
                filterDataTab3();
                filteredDgridUsedTab3 = true;
            }
            else
            {
                unFilteredDataTab3();
                filteredDgridUsedTab3 = false;
                tblFilterStatusTab3.Text = String.Empty;
                MainWindow win = (MainWindow)Window.GetWindow(this);
                if (win.options.chkbMask.IsChecked == true)
                {
                    Object obj1 = this.Resources["Gradient4"];
                    tblFilterStatusTab3.Background = (Brush)obj1;
                }
                else
                {
                    tblFilterStatusTab3.Background = Brushes.White;
                }
            }

        }


        private void tfFilterTab3_MouseEnter(object sender, MouseEventArgs e)
        {
            if (cmbFilterColumnTab3.SelectedIndex == 0)
            {
                tfFilterTab3.IsReadOnly = true;

                tblFilterStatusTab3.Text = Constants.FILTER_COLUMN;
                tblFilterStatusTab3.Foreground = Brushes.White;
                tblFilterStatusTab3.Background = Brushes.Red;
            }
            else
            {
                tfFilterTab3.IsReadOnly = false;
            }
        }

        private void tfFilterTab3_MouseLeave(object sender, MouseEventArgs e)
        {
            if (filteredDgridUsedTab3 == false)
            {
                tblFilterStatusTab3.Text = String.Empty;
                MainWindow win = (MainWindow)Window.GetWindow(this);
                if (win.options.chkbMask.IsChecked == true)
                {
                    Object obj1 = this.Resources["Gradient4"];
                    tblFilterStatusTab3.Background = (Brush)obj1;
                }
                else
                {
                    tblFilterStatusTab3.Background = Brushes.White;
                }
            }
        }






        #endregion


    }
}
