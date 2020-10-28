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
using System.Data.OleDb;
using System.Xml.Linq;



namespace caffeKambodzaApplication
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : UserControl
    {

        private OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
        private OleDbCommand com;
        private OleDbDataReader dr;

        private bool filteredDgridUsedTab1;
        private bool filteredDgridUsedTab2;

        private DateTime _dateCreatedReportStart, _dateCreatedReportEnd;
        private DateTime _dateCreatedReportStartTab2, _dateCreatedReportEndTab2;

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

        public ObservableCollection<HistoryChangeRecipes> hRecipes;
        public ICollectionView cvhRecipes;


        public ObservableCollection<HistoryChangePrices> hPrices;
        public ICollectionView cvhPrices;


        public History()
        {
            InitializeComponent();
            hRecipes = new ObservableCollection<HistoryChangeRecipes>();
            hPrices = new ObservableCollection<HistoryChangePrices>();


            cvhRecipes = CollectionViewSource.GetDefaultView(hRecipes);
            if (cvhRecipes != null)
            {
                dataGridReadHistoryRecipes.ItemsSource = cvhRecipes;
            }

            cvhPrices = CollectionViewSource.GetDefaultView(hPrices);
            if (cvhPrices != null)
            {
                dataGridReadHistoryPrices.ItemsSource = cvhPrices;
            }


            cmbFilterColumnTab1.SelectedIndex = 0;
            cmbFilterColumnTab2.SelectedIndex = 0;
            filteredDgridUsedTab1 = false;
            filteredDgridUsedTab2 = false;
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
                    Logger.writeNode(Constants.INFORMATION, "Tab6 PodTab1 Postavljanje pocetnog datuma. Postavljeni datum je :" + _dateCreatedReportStart.ToString());

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
                    Logger.writeNode(Constants.INFORMATION, "Tab6 PodTab1 Postavljanje krajnjeg datuma. Postavljeni datum je :" + _dateCreatedReportEnd.ToString());

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
                    if (hRecipes.Count > 0) hRecipes.Clear();

                    Logger.writeNode(Constants.INFORMATION, "Tab6 PodTab1 Ucitavanje izvestaja istorije promena recepata");

                    string id = "44";//Queries.xml ID
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


                    string productCode = String.Empty;
                    string storeItemCode = String.Empty;
                    string kindOfProduct = String.Empty;
                    string storeItemName = String.Empty;
                    string storeItemGroup = String.Empty;
                    string type = String.Empty;
                    string oldProductAmount = String.Empty;
                    string newProductAmount = String.Empty;
                    string oldStoreItemAmount = String.Empty;
                    string newStoreItemAmount = String.Empty;
                    string date = String.Empty;
                    DateTime dateChanged;




                    while (dr.Read())
                    {

                        productCode = dr["ProductCode"].ToString();
                        storeItemCode = dr["StoreItemCode"].ToString();
                        kindOfProduct = dr["KindOfProduct"].ToString();
                        storeItemName = dr["StoreItemName"].ToString();
                        storeItemGroup = dr["StoreItemGroup"].ToString();
                        type = dr["Type"].ToString();
                        oldProductAmount = dr["OLDProductAmount"].ToString();
                        newProductAmount = dr["NEWProductAmount"].ToString();
                        oldStoreItemAmount = dr["OLDStoreItemAmount"].ToString();
                        newStoreItemAmount = dr["NEWStoreItemAmount"].ToString();
                        date = dr["DateChangeEntered"].ToString();
                        dateChanged = DateTime.Parse(date);



                        HistoryChangeRecipes hRecipe = new HistoryChangeRecipes(productCode, storeItemCode, kindOfProduct, storeItemName, storeItemGroup, oldProductAmount, newProductAmount, oldStoreItemAmount, newStoreItemAmount, dateChanged);
                        hRecipe.setType(type);
                        hRecipes.Add(hRecipe);
                    }

                    cvhRecipes = CollectionViewSource.GetDefaultView(hRecipes);
                    if (cvhRecipes != null)
                    {
                        dataGridReadHistoryRecipes.ItemsSource = cvhRecipes;
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

            this.cvhRecipes.Filter = item =>
            {
                var vitem = item as HistoryChangeRecipes;
                if (vitem == null) return false;
                string searchText = tfFilterTab1.Text.ToUpper();

                if (cmbFilterColumnTab1.SelectedIndex == 1)
                {

                    string ProductCode = vitem.ProductCode.ToUpper();
                    if (ProductCode.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab1.SelectedIndex == 2)
                {
                    string StoreItemCode = vitem.StoreItemCode.ToUpper();
                    if (StoreItemCode.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab1.SelectedIndex == 3)
                {

                    string KindOfProduct = vitem.KindOfProduct.ToUpper();
                    if (KindOfProduct.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab1.SelectedIndex == 4)
                {

                    string StoreItemName = vitem.StoreItemName.ToUpper();
                    if (StoreItemName.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else
                {

                    string StoreItemGroup = vitem.StoreItemGroup.ToString().ToUpper();
                    if (StoreItemGroup.Contains(searchText) == true) { return true; }
                    else { return false; }
                }


            };

        }



        private void unFilteredDataTab1()
        {
            tblFilterStatusTab1.Text = String.Empty;
            tblFilterStatusTab1.Background = Brushes.White;

            this.cvhRecipes.Filter = item =>
            {

                var vitem = item as HistoryChangeRecipes;
                if (vitem == null) return false;
                else return true;

            };
        }

        private void tfFilterTab1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfFilterTab1.Text.Equals(String.Empty) == false)
            {
                Logger.writeNode(Constants.INFORMATION, "Tab6 PodTab1 Filtriranje reci. Broj kolone koja se filtrira " + cmbFilterColumnTab1.SelectedIndex.ToString() + ". Filtrirana rec :" + tfFilterTab1.Text);

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
                    Logger.writeNode(Constants.INFORMATION, "Tab6 PodTab2 Postavljanje pocetnog datuma. Postavljeni datum je :" + _dateCreatedReportStartTab2.ToString());

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
                    Logger.writeNode(Constants.INFORMATION, "Tab6 PodTab2 Postavljanje krajnjeg datuma. Postavljeni datum je :" + _dateCreatedReportEndTab2.ToString());

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
                    if (hPrices.Count > 0) hPrices.Clear();

                    Logger.writeNode(Constants.INFORMATION, "Tab6 PodTab2 Ucitavanje izvestaja istorije promenjenih cena proizvoda ili stavki šanka");

                    string id = "46";//Queries.xml ID
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


                    string code = String.Empty;
                    string name = String.Empty;
                    string type = String.Empty;
                    string oldprice = String.Empty;
                    string newprice = String.Empty;
                    string dateChanged = String.Empty;




                    while (dr.Read())
                    {

                        code = dr["Code"].ToString();
                        name = dr["Name"].ToString();
                        type = dr["Type"].ToString();
                        oldprice = dr["OLDPrice"].ToString();
                        newprice = dr["NEWPrice"].ToString();
                        dateChanged = dr["DateChangeEntered"].ToString();

                        DateTime date = DateTime.Parse(dateChanged);

                        HistoryChangePrices hPrice = new HistoryChangePrices(code, name, type, oldprice, newprice, date);
                        hPrices.Add(hPrice);
                    }

                    cvhPrices = CollectionViewSource.GetDefaultView(hPrices);
                    if (cvhPrices != null)
                    {
                        dataGridReadHistoryPrices.ItemsSource = cvhPrices;
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


        #region filteringTab2

        private void filterDataTab2()
        {

            tblFilterStatusTab2.Text = Constants.FILTERON;
            tblFilterStatusTab2.Background = Brushes.Orange;

            this.cvhPrices.Filter = item =>
            {
                var vitem = item as HistoryChangePrices;
                if (vitem == null) return false;
                string searchText = tfFilterTab2.Text.ToUpper();

                if (cmbFilterColumnTab2.SelectedIndex == 1)
                {

                    string code = vitem.Code.ToUpper();
                    if (code.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab2.SelectedIndex == 2)
                {
                    string Name = vitem.Name.ToUpper();
                    if (Name.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab2.SelectedIndex == 3)
                {

                    string type = vitem.Type.ToUpper();
                    if (type.Equals("PRODUCT") == true) type = "PROIZVOD KAFIĆA";
                    else type = "STAVKA ŠANKA";
                    if (type.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab2.SelectedIndex == 4)
                {

                    string oldPrice = vitem.OldPrice.ToString().ToUpper();
                    if (oldPrice.Equals(searchText) == true) { return true; }
                    else { return false; }
                }
                else
                {

                    string newPrice = vitem.NewPrice.ToString().ToUpper();
                    if (newPrice.Equals(searchText) == true) { return true; }
                    else { return false; }
                }


            };

        }



        private void unFilteredDataTab2()
        {
            tblFilterStatusTab2.Text = String.Empty;
            tblFilterStatusTab2.Background = Brushes.White;

            this.cvhPrices.Filter = item =>
            {

                var vitem = item as HistoryChangePrices;
                if (vitem == null) return false;
                else return true;

            };
        }

        private void tfFilterTab2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfFilterTab2.Text.Equals(String.Empty) == false)
            {
                Logger.writeNode(Constants.INFORMATION, "Tab6 PodTab2 Filtriranje reci. Broj kolone koja se filtrira " + cmbFilterColumnTab2.SelectedIndex.ToString() + ". Filtrirana rec :" + tfFilterTab2.Text);
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



       


    }//end of class
}
