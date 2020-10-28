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
using System.Windows.Forms;
using System.Data.OleDb;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace caffeKambodzaApplication
{
    /// <summary>
    /// Interaction logic for WorkingWithCreatedReports.xaml
    /// </summary>
    public partial class WorkingWithCreatedReports : System.Windows.Controls.UserControl
    {

        private OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
        //private OleDbConnection conCorrection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
        private OleDbCommand com;
        private OleDbDataReader dr;
        private DateTime _dateCreatedReportStart, _dateCreatedReportEnd;
        private DateTime _dateCreatedReportStartTab2, _dateCreatedReportEndTab2;
        private DateTime _dateCreatedReportStartTab3, _dateCreatedReportEndTab3;
        
        private int _currOldAmount, _currOldCostItem; 

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

        private DateTime _dateCreatedReportStartTab4, _dateCreatedReportEndTab4;

        private ObservableCollection<ItemWithDate> _itemsLoad;
        private ObservableCollection<ItemWithDate> _itemsLoad2;
        private ObservableCollection<ItemWithDateDeletion> _itemsDeleted;
        private ObservableCollection<ItemWithDateCorrection> _itemsCorrected;

        public ObservableCollection<ItemWithDateDeletion> ItemsDeleted
        {
            get { return _itemsDeleted; }
            set { _itemsDeleted = value; }
        }

        public ICollectionView cvItemsLoad;
        public ICollectionView cvItemsLoad2;
        public ICollectionView cvItemsDeleted;
        public ICollectionView cvItemsCorrected;
        private int _selectedIndex = -1;
        int newamount;
        int priceforOne;
        private string correctionReason = String.Empty;
        private string deletionReason = String.Empty;

        public WorkingWithCreatedReports()
        {
            InitializeComponent();
            _itemsLoad = new ObservableCollection<ItemWithDate>();
            cvItemsLoad = CollectionViewSource.GetDefaultView(_itemsLoad);
            if (cvItemsLoad != null)
            {
                dataGridRead.ItemsSource = cvItemsLoad;
            }



            _itemsLoad2 = new ObservableCollection<ItemWithDate>();
            cvItemsLoad2 = CollectionViewSource.GetDefaultView(_itemsLoad2);
            if (cvItemsLoad2 != null)
            {
                dataGridReadByProduct.ItemsSource = cvItemsLoad2;
            }


            _itemsDeleted = new ObservableCollection<ItemWithDateDeletion>();
            cvItemsDeleted = CollectionViewSource.GetDefaultView(_itemsDeleted);
            if (cvItemsDeleted != null)
            {
                dataGridReadDeletion.ItemsSource = cvItemsDeleted;
            }

            _itemsCorrected = new ObservableCollection<ItemWithDateCorrection>();
            cvItemsCorrected = CollectionViewSource.GetDefaultView(_itemsCorrected);
            if (cvItemsCorrected != null)
            {
                dataGridReadCorrection.ItemsSource = cvItemsCorrected;
            }
        }

        #region Tab1

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
                    Logger.writeNode(Constants.INFORMATION, "Tab4 PodTab1 Ucitavanje izvestaja");
                    if (_itemsLoad.Count > 0) _itemsLoad.Clear();

                    string id = "31";//Queries.xml ID
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


                    string NumberOfItemCreated = String.Empty;
                    string CodeProduct = String.Empty;
                    string Product = String.Empty;
                    string PriceofProduct = String.Empty;
                    string NumberOfSoldItemPieces = String.Empty;
                    string WholeItemCost = String.Empty;
                    string Shift = String.Empty;
                    string DateCreatedReport = String.Empty;
                    DateTime dateCreatedReport;


                    while (dr.Read())
                    {
                        NumberOfItemCreated = dr["NumberOfItemCreated"].ToString();
                        CodeProduct = dr["CodeProduct"].ToString();
                        Product = dr["Product"].ToString();
                        PriceofProduct = dr["PriceofProduct"].ToString();
                        NumberOfSoldItemPieces = dr["NumberOfSoldItemPieces"].ToString();
                        WholeItemCost = dr["WholeItemCost"].ToString();
                        Shift = dr["Shift"].ToString();
                        DateCreatedReport = dr["DateCreatedReport"].ToString();
                        dateCreatedReport = DateTime.Parse(DateCreatedReport);

                        long numOfCount;
                        bool isN = long.TryParse(NumberOfItemCreated, out numOfCount);
                        int costItem;
                        bool isNN = int.TryParse(WholeItemCost, out costItem);
                        int amount;
                        bool isNNN = int.TryParse(NumberOfSoldItemPieces, out amount);
                        int price;
                        bool isNNNN = int.TryParse(PriceofProduct, out price);

                        ItemWithDate item = new ItemWithDate(CodeProduct, Product, price, amount, costItem, Shift, numOfCount, dateCreatedReport.ToShortDateString());
                        _itemsLoad.Add(item);
                    }

                    cvItemsLoad = CollectionViewSource.GetDefaultView(_itemsLoad);
                    if (cvItemsLoad != null)
                    {
                        dataGridRead.ItemsSource = cvItemsLoad;
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
                    Logger.writeNode(Constants.INFORMATION, "Tab4 PodTab1 Postavljanje pocetnog datuma. Postavljeni datum je :" + _dateCreatedReportStart.ToString());

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
                    Logger.writeNode(Constants.INFORMATION, "Tab4 PodTab1 Postavljanje krajnjeg datuma. Postavljeni datum je :" + _dateCreatedReportEnd.ToString());

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("You did not enter date in tab1!!!");
                Logger.writeNode(Constants.EXCEPTION, "You did not enter date in tab1!!!");
            }
        }


        private void dataGridRead_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataGridRead.SelectedItem != null) 
            {
                _selectedIndex = dataGridRead.SelectedIndex;
                string selectedItem = dataGridRead.SelectedItem.ToString();
                string[] arr = selectedItem.Split('&');

                tf1.Text = arr[0];
                tf2.Text = arr[1];
                tf3.Text = arr[2];
                tf4.Text = arr[3];
                tf5.Text = arr[4];
                tf6.Text = arr[5];
                Logger.writeNode(Constants.INFORMATION, "Tab4 PodTab1 Selektovanje stavke racuna. Sifra proizvoda na racunu :" + tf1.Text + ". Naziv proizvoda na racunu :" + tf2.Text + ". Jedinicna cena proizvoda na racunu :" + tf3.Text + ". Broj komada proizvoda na racunu :" + tf4.Text + ". Ukupna vrednost stavke racuna :" + tf5.Text + ". Datum kreiranja racuna :" + tf6.Text);

                bool isN = int.TryParse(tf4.Text, out _currOldAmount);
                bool isNN = int.TryParse(tf5.Text, out _currOldCostItem);
 
            }
        }


        private void tf4_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            if (tf4.Text.Equals(String.Empty) == true)
            {
                System.Windows.Forms.MessageBox.Show("Polje za količinu je prazno! Morate uneti neki broj!");
                Logger.writeNode(Constants.MESSAGEBOX, "Polje za količinu je prazno! Morate uneti neki broj!");
            }
            else 
            {
                
                bool isN = int.TryParse(tf4.Text, out newamount);
                if (isN == false)
                {
                    System.Windows.Forms.MessageBox.Show("Količinu morate uneti u obliku broja!");
                    Logger.writeNode(Constants.MESSAGEBOX, "Količinu morate uneti u obliku broja!");
                }
                else 
                {

                    bool isNum = int.TryParse(tf3.Text, out priceforOne);
                    int newCostItem = newamount * priceforOne;
                    tf5.Text = newCostItem.ToString();
                }
            }
        }


        private void tfDeletionOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfDeletionOutput.Text.Equals(String.Empty) == false) 
            {
                deletionReason = tfDeletionOutput.Text;
            }

        }

        private void tfDeletionOutput_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (tfDeletionOutput.Text.Equals(Constants.tfDeletionOutput_INITIALTEXT)) 
            {
                tfDeletionOutput.Text = String.Empty;
            }
        }

        private void tfDeletionOutput_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (tfDeletionOutput.Text.Equals(String.Empty))
            {
                tfDeletionOutput.Text = Constants.tfDeletionOutput_INITIALTEXT;
            }
        }


        private void deleteItemInAllItemsSoldEver(Item itemForRemove)
        {
            try
            {

                string id = "28";//Queries.xml ID

                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;
                query = query + "'" + itemForRemove.NumOfCount + "'" + ";";

                con.Open();
                com = new OleDbCommand(query, con);
                com.ExecuteNonQuery();
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
            }
        }

        private void insertItemInAllItemsDeletedEver(ItemWithDate item) 
        {
            try
            {

                string id = "30";//Queries.xml ID

                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;
                query = query + "(" + "'" + item.NumOfCount + "'" + "," + "'" + item.CodeProduct + "'" + "," + "'" + item.KindOfProduct + "'" + "," + "'" + item.Price + "'" + "," + "'" + item.Amount + "'" + "," + "'" + item.CostItem + "'" + "," + "'" + item.Shift + "'" + "," + "'" + item.Date + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + deletionReason + "'" + ");";

                con.Open();
                com = new OleDbCommand(query, con);
                com.ExecuteNonQuery();
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
            }
        }



       


        private void btnDeletion_Click(object sender, RoutedEventArgs e)
        {
            if (tfDeletionOutput.Text.Equals(Constants.tfDeletionOutput_INITIALTEXT))
            {
                System.Windows.Forms.MessageBox.Show("Morate uneti razlog brisanja stavke!");
                Logger.writeNode(Constants.MESSAGEBOX, "Morate uneti razlog brisanja stavke!");
                return;
            }
            else 
            {
               
                tfDeletionOutput.Text = String.Empty;
                Logger.writeNode(Constants.INFORMATION, "Tab4 PodTab1 Rucno brisanje stavke racuna.Sifra proizvoda na racunu :" + tf1.Text + ". Naziv proizvoda na racunu :" + tf2.Text + ". Jedinicna cena proizvoda na racunu :" + tf3.Text + ". Broj komada proizvoda na racunu :" + tf4.Text + ". Ukupna vrednost stavke racuna :" + tf5.Text + ". Datum kreiranja racuna :" + tf6.Text);
                // first load selected item from collection
                ItemWithDate it = _itemsLoad.ElementAt(_selectedIndex);

                //then remove from table allItemsSoldEver 
                deleteItemInAllItemsSoldEver(it);

                //then insert record in table allItemsDeletedEver
                insertItemInAllItemsDeletedEver(it);

                // finally remove item from collection
                _itemsLoad.RemoveAt(_selectedIndex);


                //update _itemsDeleted (deletion collection) 
                ItemWithDateDeletion itDel = new ItemWithDateDeletion(it, deletionReason);

                for (DateTime x = _dateCreatedReportStartTab3; x <= _dateCreatedReportEndTab3; x = x.AddDays(1))
                {
                    string dateCurrStr = x.ToString().Replace("0:00:00", "");
                    if (dateCurrStr.Equals(it.Date.ToString()) == true)
                    {
                        _itemsDeleted.Add(itDel);

                        dataGridReadDeletion.ItemsSource = _itemsDeleted;
                       
                        break;
                    }
                }
            }

        }

        private void tfCorrectionOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfCorrectionOutput.Text.Equals(String.Empty) == false)
            {
                correctionReason = tfCorrectionOutput.Text;
            }

        }



        private void tfCorrected_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (tfCorrectionOutput.Text.Equals(Constants.tfCorrectionOutput_INITIALTEXT))
            {
                tfCorrectionOutput.Text = String.Empty;
            }
        }

        private void tfCorrected_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

            if (tfCorrectionOutput.Text.Equals(String.Empty))
            {
                tfCorrectionOutput.Text = Constants.tfCorrectionOutput_INITIALTEXT;
            }
        }


        private void insertRecordinAllItemsCorrectedEver(ItemWithDate item, int newamount, int newcostitem) 
        {
            try
            {

                int diff = item.Amount - newamount;
                int diffCostItem = item.CostItem - newcostitem;

                Logger.writeNode(Constants.INFORMATION, "Tab4 PodTab1 Korekcija racuna. Sifra proizvoda na racunu :" + item.CodeProduct + ". Naziv proizvoda na racunu :" + item.KindOfProduct + ". Jedinicna cena proizvoda na racunu :" + item.Price + ". Broj komada proizvoda na racunu [stara vrednost] :" + item.Amount + ". Broj komada proizvoda na racunu [nova vrednost] :" + newamount.ToString() + ". Ukupna vrednost stavke racuna [stara vrednost](din):" + item.Price + ". Ukupna vrednost stavke racuna [nova vrednost](din):" + newcostitem + ". Datum kreiranja racuna :" + tf6.Text);

                string id = "32";//Queries.xml ID

                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;
                query = query + "(" + "'" + item.NumOfCount + "'" + "," + "'" + item.CodeProduct + "'" + "," + "'" + item.KindOfProduct + "'" + "," + "'" + item.Price + "'" + "," + "'" + item.Amount + "'" + "," + "'" + newamount + "'" + "," + "'" + diff + "'" + "," + "'" + item.CostItem + "'" + "," + "'" + newcostitem + "'" + "," + "'" + diffCostItem + "'" + "," + "'" + item.Shift + "'" + "," + "'" + item.Date + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + correctionReason + "'" + ");";

                con.Open();
                com = new OleDbCommand(query, con);
                com.ExecuteNonQuery();
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
            }
        }

        private void btnCorrection_Click(object sender, RoutedEventArgs e)
        {
            if (tfCorrectionOutput.Text.Equals(Constants.tfCorrectionOutput_INITIALTEXT))
            {
                System.Windows.Forms.MessageBox.Show("Morate uneti razlog korekcije stavke!");
                Logger.writeNode(Constants.MESSAGEBOX, "Morate uneti razlog korekcije stavke!");
                return;
            }
            else 
            {
                tfCorrectionOutput.Text = String.Empty;

                // first load selected item from collection
                ItemWithDate it = _itemsLoad.ElementAt(_selectedIndex);

                //then update record in table allItemsSoldEver
                try
                {

                    
                    string query = "UPDATE allItemsSoldEver SET NumberOfSoldItemPieces = " + "'" + tf4.Text + "'" + " WHERE NumberOfItemCreated =" + "'" + it.NumOfCount + "'" + ";";
                    con.Open();
                    com = new OleDbCommand(query, con);
                    com.ExecuteNonQuery();

                    query = "UPDATE allItemsSoldEver SET WholeItemCost = " + "'" + tf5.Text + "'" + " WHERE NumberOfItemCreated =" + "'" + it.NumOfCount + "'" + ";";
                    com = new OleDbCommand(query, con);
                    com.ExecuteNonQuery();

                    int newAmount;
                    bool isN = int.TryParse(tf4.Text, out newAmount);
                    int newCostItem;
                    bool isNN = int.TryParse(tf5.Text, out newCostItem);
                    it.Amount = newAmount;
                    it.CostItem = newCostItem;

                    //update _itemsDeleted (correction collection) 
                    ItemWithDateCorrection itCor = new ItemWithDateCorrection(it, _currOldAmount, _currOldCostItem, correctionReason);

                    for (DateTime x = _dateCreatedReportStartTab4; x <= _dateCreatedReportEndTab4; x = x.AddDays(1))
                    {
                        string dateCurrStr = x.ToString().Replace("0:00:00", "");
                        if (dateCurrStr.Equals(it.Date.ToString()) == true)
                        {
                            _itemsCorrected.Add(itCor);


                            cvItemsCorrected = CollectionViewSource.GetDefaultView(_itemsCorrected);
                            if (cvItemsCorrected != null)
                            {
                                dataGridReadCorrection.ItemsSource = cvItemsCorrected;
                            }

                            break;
                        }
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
                   
                }

                //then insert record in table allItemsCorrectedEver
                int newcostitem = newamount * priceforOne;
                insertRecordinAllItemsCorrectedEver(it, newamount, newcostitem);

                // finally update item in collection
                _itemsLoad.ElementAt(_selectedIndex).Amount = newamount;
                _itemsLoad.ElementAt(_selectedIndex).CostItem = newamount * priceforOne;
                cvItemsLoad = CollectionViewSource.GetDefaultView(_itemsLoad);
                if (cvItemsLoad != null)
                {
                    dataGridRead.ItemsSource = cvItemsLoad;
                }
            }
        }


        #endregion

        


        #region Tab2


        private string findCodeProduct (string kindOfProduct) 
        {
            string code = String.Empty;

            MainWindow window = (MainWindow)Window.GetWindow(this);
            ObservableCollection<Product> products = window.ProductsWholeInformation;

            for (int i = 0; i < window.ProductsWholeInformation.Count; i++)
            {
                if (window.ProductsWholeInformation.ElementAt(i).KindOfProduct.Equals(kindOfProduct) == true)
                {
                    code = window.ProductsWholeInformation.ElementAt(i).CodeProduct;
                    return code;
                }
            }


            return code;
        }


        private void btnloadReportByProduct_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProductsTab2.SelectedIndex == 0)
            {
                System.Windows.Forms.MessageBox.Show("Morate izabrati proizvod!");
                Logger.writeNode(Constants.MESSAGEBOX, "Morate izabrati proizvod!");
                return;
            }
            else 
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

                        string codeProduct = findCodeProduct(cmbProductsTab2.SelectedItem.ToString());
                        Logger.writeNode(Constants.INFORMATION, "Tab4 PodTab2 Ucitavanje izvestaja za proizvod kafica :" + cmbProductsTab2.SelectedItem.ToString());

                        if (_itemsLoad2.Count > 0) _itemsLoad2.Clear();

                        string id = "37";//Queries.xml ID
                        XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                        XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                          where xml2.Element("ID").Value == id
                                          select xml2).FirstOrDefault();
                        Console.WriteLine(Query.ToString());
                        string query = Query.Attribute(Constants.TEXT).Value;

                        con.Open();
                        com = new OleDbCommand(query, con);
                        com.Parameters.AddWithValue("@CodeProduct", codeProduct);
                        com.Parameters.AddWithValue("@StartDate", _dateCreatedReportStartTab2);
                        com.Parameters.AddWithValue("@EndDate", _dateCreatedReportEndTab2);
                        dr = com.ExecuteReader();


                        string NumberOfItemCreated = String.Empty;
                        string CodeProduct = String.Empty;
                        string Product = String.Empty;
                        string PriceofProduct = String.Empty;
                        string NumberOfSoldItemPieces = String.Empty;
                        string WholeItemCost = String.Empty;
                        string Shift = String.Empty;
                        string DateCreatedReport = String.Empty;
                        DateTime dateCreatedReport;


                        while (dr.Read())
                        {
                            NumberOfItemCreated = dr["NumberOfItemCreated"].ToString();
                            CodeProduct = dr["CodeProduct"].ToString();
                            Product = dr["Product"].ToString();
                            PriceofProduct = dr["PriceofProduct"].ToString();
                            NumberOfSoldItemPieces = dr["NumberOfSoldItemPieces"].ToString();
                            WholeItemCost = dr["WholeItemCost"].ToString();
                            Shift = dr["Shift"].ToString();
                            DateCreatedReport = dr["DateCreatedReport"].ToString();
                            dateCreatedReport = DateTime.Parse(DateCreatedReport);

                            long numOfCount;
                            bool isN = long.TryParse(NumberOfItemCreated, out numOfCount);
                            int costItem;
                            bool isNN = int.TryParse(WholeItemCost, out costItem);
                            int amount;
                            bool isNNN = int.TryParse(NumberOfSoldItemPieces, out amount);
                            int price;
                            bool isNNNN = int.TryParse(PriceofProduct, out price);

                            ItemWithDate item = new ItemWithDate(CodeProduct, Product, price, amount, costItem, Shift, numOfCount, dateCreatedReport.ToShortDateString());
                            _itemsLoad2.Add(item);
                        }

                        cvItemsLoad2 = CollectionViewSource.GetDefaultView(_itemsLoad2);
                        if (cvItemsLoad2 != null)
                        {
                            dataGridReadByProduct.ItemsSource = cvItemsLoad2;
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

        }



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
                    Logger.writeNode(Constants.INFORMATION, "Tab4 PodTab2 Postavljanje pocetnog datuma. Postavljeni datum je :" + _dateCreatedReportStartTab2.ToString());

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
                    Logger.writeNode(Constants.INFORMATION, "Tab4 PodTab2 Postavljanje krajnjeg datuma. Postavljeni datum je :" + _dateCreatedReportEndTab2.ToString());

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("You did not enter date in tab1!!!");
                Logger.writeNode(Constants.EXCEPTION, "You did not enter date in tab1!!!");
            }
        }

        #endregion




        #region Tab3


        private void btnloadReportDeletion_Click(object sender, RoutedEventArgs e)
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
                    if (_itemsDeleted.Count > 0) _itemsDeleted.Clear();

                    Logger.writeNode(Constants.INFORMATION, "Tab4 PodTab3 Ucitavanje izvestaja obrisanih proizvoda kafica");

                    string id = "38";//Queries.xml ID
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


                    string NumberOfItemCreated = String.Empty;
                    string CodeProduct = String.Empty;
                    string Product = String.Empty;
                    string PriceofProduct = String.Empty;
                    string NumberOfSoldItemPieces = String.Empty;
                    string WholeItemCost = String.Empty;
                    string Shift = String.Empty;
                    string DateCreatedReport = String.Empty;
                    DateTime dateCreatedReport;
                    string deleteR = String.Empty;


                    while (dr.Read())
                    {
                        NumberOfItemCreated = dr["NumberOfItemCreated"].ToString();
                        CodeProduct = dr["CodeProduct"].ToString();
                        Product = dr["Product"].ToString();
                        PriceofProduct = dr["PriceofProduct"].ToString();
                        NumberOfSoldItemPieces = dr["NumberOfSoldItemPieces"].ToString();
                        WholeItemCost = dr["WholeItemCost"].ToString();
                        Shift = dr["Shift"].ToString();
                        DateCreatedReport = dr["DateCreatedReport"].ToString();
                        dateCreatedReport = DateTime.Parse(DateCreatedReport);
                        deleteR = dr["DeletionReason"].ToString();

                        long numOfCount;
                        bool isN = long.TryParse(NumberOfItemCreated, out numOfCount);
                        int costItem;
                        bool isNN = int.TryParse(WholeItemCost, out costItem);
                        int amount;
                        bool isNNN = int.TryParse(NumberOfSoldItemPieces, out amount);
                        int price;
                        bool isNNNN = int.TryParse(PriceofProduct, out price);

                        ItemWithDateDeletion item = new ItemWithDateDeletion(CodeProduct, Product, price, amount, costItem, Shift, numOfCount, dateCreatedReport.ToShortDateString(), deleteR);
                        _itemsDeleted.Add(item);
                    }

                   
                    cvItemsDeleted = CollectionViewSource.GetDefaultView(_itemsDeleted);
                    if (cvItemsDeleted != null)
                    {
                        dataGridReadDeletion.ItemsSource = cvItemsDeleted;
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
                    Logger.writeNode(Constants.INFORMATION, "Tab4 PodTab3 Postavljanje pocetnog datuma. Postavljeni datum je :" + _dateCreatedReportStartTab3.ToString());

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
                    Logger.writeNode(Constants.INFORMATION, "Tab4 PodTab3 Postavljanje krajnjeg datuma. Postavljeni datum je :" + _dateCreatedReportEndTab3.ToString());

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("You did not enter date in tab1!!!");
                Logger.writeNode(Constants.EXCEPTION, "You did not enter date in tab1!!!");
            }
        }


        #endregion

       


        #region Tab4

        private void datepickerStartTab4_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
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

                    _dateCreatedReportStartTab4 = date.Value;
                    Logger.writeNode(Constants.INFORMATION, "Tab4 PodTab4 Postavljanje pocetnog datuma. Postavljeni datum je :" + _dateCreatedReportStartTab4.ToString());

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("You did not enter date in tab1!!!");
                Logger.writeNode(Constants.EXCEPTION, "You did not enter date in tab1!!!");
            }
        }

        private void datepickerEndTab4_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
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

                    _dateCreatedReportEndTab4 = date.Value;
                    Logger.writeNode(Constants.INFORMATION, "Tab4 PodTab4 Postavljanje krajnjeg datuma. Postavljeni datum je :" + _dateCreatedReportEndTab4.ToString());

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("You did not enter date in tab1!!!");
                Logger.writeNode(Constants.EXCEPTION, "You did not enter date in tab1!!!");
            }
        }



        private void btnloadReportCorrection_Click(object sender, RoutedEventArgs e)
        {
            if (datepickerStartTab4.Text.Equals(String.Empty) || datepickerEndTab4.Text.Equals(String.Empty))
            {
                System.Windows.Forms.MessageBox.Show("Morate uneti vremenski interval koji želite učitati!");
                Logger.writeNode(Constants.MESSAGEBOX, "Morate uneti vremenski interval koji želite učitati!");
                return;
            }
            else
            {
                try
                {
                    if (_itemsCorrected.Count > 0) _itemsCorrected.Clear();

                    Logger.writeNode(Constants.INFORMATION, "Tab4 PodTab4 Ucitavanje izvestaja korigovanih proizvoda kafica");

                    string id = "39";//Queries.xml ID
                    XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                      where xml2.Element("ID").Value == id
                                      select xml2).FirstOrDefault();
                    Console.WriteLine(Query.ToString());
                    string query = Query.Attribute(Constants.TEXT).Value;

                    con.Open();
                    com = new OleDbCommand(query, con);
                    com.Parameters.AddWithValue("@StartDate", _dateCreatedReportStartTab4);
                    com.Parameters.AddWithValue("@EndDate", _dateCreatedReportEndTab4);
                    dr = com.ExecuteReader();


                    string NumberOfItemCreated = String.Empty;
                    string CodeProduct = String.Empty;
                    string Product = String.Empty;
                    string PriceofProduct = String.Empty;
                    string oldNumberOfSoldItemPieces = String.Empty;
                    string newNumberOfSoldItemPieces = String.Empty;
                    string diffNumberOfSoldItemPieces = String.Empty;
                    string oldWholeItemCost = String.Empty;
                    string newWholeItemCost = String.Empty;
                    string diffWholeItemCost = String.Empty;
                    string Shift = String.Empty;
                    string DateCreatedReport = String.Empty;
                    DateTime dateCreatedReport;
                    string correctionR = String.Empty;


                    while (dr.Read())
                    {
                        NumberOfItemCreated = dr["NumberOfItemCreated"].ToString();
                        CodeProduct = dr["CodeProduct"].ToString();
                        Product = dr["Product"].ToString();
                        PriceofProduct = dr["PriceofProduct"].ToString();
                        oldNumberOfSoldItemPieces = dr["OLDNumberOfSoldItemPieces"].ToString();
                        newNumberOfSoldItemPieces = dr["NEWNumberOfSoldItemPieces"].ToString();
                        diffNumberOfSoldItemPieces = dr["Difference"].ToString();
                        oldWholeItemCost = dr["OLDWholeItemCost"].ToString();
                        newWholeItemCost = dr["NEWWholeItemCost"].ToString();
                        diffWholeItemCost = dr["DifferenceCostItem"].ToString();
                        Shift = dr["Shift"].ToString();
                        DateCreatedReport = dr["DateCreatedReport"].ToString();
                        dateCreatedReport = DateTime.Parse(DateCreatedReport);
                        correctionR = dr["CorrectionReason"].ToString();

                        int price;
                        bool isNum = int.TryParse(PriceofProduct, out price);
                        long numOfCount;
                        bool isN = long.TryParse(NumberOfItemCreated, out numOfCount);
                        int oldamount;
                        bool isNN = int.TryParse(oldNumberOfSoldItemPieces, out oldamount);
                        int newamount;
                        bool isNNN = int.TryParse(newNumberOfSoldItemPieces, out newamount);
                        int oldcostItem;
                        bool isNNNN = int.TryParse(oldWholeItemCost, out oldcostItem);
                        int newcostItem;
                        bool isNNNNN = int.TryParse(newWholeItemCost, out newcostItem);


                        ItemWithDate it = new ItemWithDate(CodeProduct, Product, price, newamount, newcostItem, Shift, numOfCount, dateCreatedReport.ToShortDateString());
                        ItemWithDateCorrection item = new ItemWithDateCorrection(it,oldamount,oldcostItem, correctionR);
                        _itemsCorrected.Add(item);
                    }


                    
                    cvItemsCorrected = CollectionViewSource.GetDefaultView(_itemsCorrected);
                    if (cvItemsCorrected != null)
                    {
                        dataGridReadCorrection.ItemsSource = cvItemsCorrected;
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

















    }
}
