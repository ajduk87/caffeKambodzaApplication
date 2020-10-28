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
using System.Xml.Linq;
using System.Data.OleDb;
using System.Collections.ObjectModel;
using System.Globalization;
using System.ComponentModel;
using System.Reflection;
using System.Collections;
using System.Threading;

namespace caffeKambodzaApplication
{
    /// <summary>
    /// Interaction logic for Storehouse.xaml
    /// </summary>
    public partial class Storehouse : UserControl
    {

        private double oldRealAmount = 0.0;
        private double oldRealPrice = 0.0; //total sum price  

       
        public ObservableCollection<StoreItemProduct> DailyStoreItem = new ObservableCollection<StoreItemProduct>();
        public ObservableCollection<StoreItemProduct> StoreItemBought = new ObservableCollection<StoreItemProduct>();

        private string _currDate = String.Empty;
        private DateTime _dateCreatedReport;
        private DateTime _dateCreatedCorrOrDel;


        public bool isBarBookCreated = true;

        private string deletionCorrectionReasonStorehouse = String.Empty;
       
        private OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
        private OleDbConnection conStore = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
        private OleDbConnection conHelp = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
        private OleDbConnection conHistory = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_HISTORY);
        private OleDbCommand com;
        private OleDbDataReader dr;
        private OleDbDataReader dr2;
        private OleDbDataReader drInner;
        private OleDbDataReader drReal;
        //this is for left side
        private ObservableCollection<StoreItemProduct> _currStoreItemProducts = new ObservableCollection<StoreItemProduct>();
        private ObservableCollection<StoreItemProduct> _currStoreItemProductsInUse = new ObservableCollection<StoreItemProduct>();

        //this is for right side
        private ObservableCollection<ConnectionRecord> usedRecords = new ObservableCollection<ConnectionRecord>();
        public ObservableCollection<ConnectionRecord> UsedRecords
        {
            get { return usedRecords; }
            set { usedRecords = value; }
        }



        private Product workingProduct;//bitan za tab 2 gde se dodaju upotrebne stavke šanka
        public ObservableCollection<StoreItemProduct> ItemsThreshold = new ObservableCollection<StoreItemProduct>();
        public ObservableCollection<StorehouseItem> StorehouseItems = new ObservableCollection<StorehouseItem>();

        public ICollectionView cvUsedRecords;
        public ICollectionView cvItemsThreshold;
        public ICollectionView cvStorehouseItems;

        private bool filteredDgridUsed = false;
        private bool filteredDgridUsedTab3 = false;
        private bool filteredDgridUsedTab4 = false;

        public ObservableCollection<StorehouseItem> getStateOfStorehouse() 
        {
            ObservableCollection<StorehouseItem> result = new ObservableCollection<StorehouseItem>();
            try
            {
                string id = "20";//Queries.xml ID
                XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;

                conStore.Open();
                com = new OleDbCommand(query, conStore);
                dr = com.ExecuteReader();
                string codeProduct = String.Empty;
                string kindOfProduct = String.Empty;
                string groupItem = String.Empty;
                string amountforOne = String.Empty;
                double amountforOneDouble;
                string realAmount = String.Empty;
                double realAmountDouble;
                double threshold;

                int price = -1; // price for one item

                // get data from storehouse
                while (dr.Read())
                {
                    codeProduct = dr["StoreItemCode"].ToString();
                    //kindOfProduct = dr["StoreItemName"].ToString();
                    //bool isNumeric = int.TryParse(dr["storeItems.StoreItemPrice"].ToString(), out price);// price for one item
                    //if (isNumeric) { price = Convert.ToInt32(dr["storeItems.StoreItemPrice"].ToString()); }
                    //groupItem = dr["storeItems.StoreItemGroup"].ToString();
                    //amountforOne = dr["storeItems.Amount"].ToString();
                    //bool isNum = Double.TryParse(dr["storeItems.Amount"].ToString(), out amountforOneDouble);
                    string realAmountWithPoint = dr["RealAmount"].ToString().Replace(',', '.');
                    bool isNumm = Double.TryParse(realAmountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out realAmountDouble);

                   
                    StorehouseItem storehouseItem = new StorehouseItem(codeProduct, String.Empty, String.Empty, -1, 0.0, realAmountDouble);
                    result.Add(storehouseItem);
                   

                } // end of main while loop
               
                //for each items get name, get group, price for one, amount for one
                for (int i = 0; i < result.Count; i++)
                {
                    string id2 = "21";//Queries.xml ID
                    XDocument xdocStore2 = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement Query2 = (from xml2 in xdocStore2.Descendants("Query")
                                      where xml2.Element("ID").Value == id2
                                      select xml2).FirstOrDefault();
                    Console.WriteLine(Query2.ToString());
                    string query2 = Query2.Attribute(Constants.TEXT).Value;
                    query2 = query2 + "'" + result.ElementAt(i).ItemCode + "'" + ";";
                    com = new OleDbCommand(query2, conStore);
                    drInner = com.ExecuteReader();
                    while (drInner.Read())
                    {
                        //codeProduct = dr["StoreItemCode"].ToString();
                        kindOfProduct = drInner["StoreItemName"].ToString();
                        bool isNumeric = int.TryParse(drInner["StoreItemPrice"].ToString(), out price);// price for one item
                        if (isNumeric) { price = Convert.ToInt32(drInner["StoreItemPrice"].ToString()); }
                        groupItem = drInner["StoreItemGroup"].ToString();
                        amountforOne = drInner["Amount"].ToString();
                        string amountWithPoint = drInner["Amount"].ToString().Replace(',', '.');
                        bool isNum = Double.TryParse(amountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out amountforOneDouble);
                        //realAmount = dr["RealAmount"].ToString();
                        //bool isNumm = Double.TryParse(dr["RealAmount"].ToString(), out realAmountDouble);
                        string thresholdWithPoint = drInner["Threshold"].ToString().Replace(',', '.');
                        bool isNumN = Double.TryParse(thresholdWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out threshold);

                        result.ElementAt(i).ItemName = kindOfProduct;
                        result.ElementAt(i).ItemGroup = groupItem;
                        result.ElementAt(i).ItemforOnePrice = price;
                        result.ElementAt(i).ItemforOneAmount = amountforOneDouble;
                        result.ElementAt(i).Threshold = threshold;
                        //calculated value
                        result.ElementAt(i).ItemPrice = result.ElementAt(i).ItemRealAmount / result.ElementAt(i).ItemforOneAmount * result.ElementAt(i).ItemforOnePrice;

                    }
                }// end for loop

                    return result;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                MainWindow window = (MainWindow)MainWindow.GetWindow(this);
                window.savenumofitemsEVERCreated();
                return new ObservableCollection<StorehouseItem>();
            }
            finally
            {
                if (conStore != null)
                {
                    conStore.Close();
                }
                if (dr != null)
                {
                    dr.Close();

                }
                if (drInner != null)
                {
                    drInner.Close();
                }
            }
 
        }

        public Storehouse()
        {
            InitializeComponent();
            tfStoreRealAmount.IsEnabled = false;
            btnEnter.IsEnabled = false;
            usedRecords = getUsedData();

            cvUsedRecords = CollectionViewSource.GetDefaultView(usedRecords);
            if (cvUsedRecords != null)
            {
                dgridUsed.ItemsSource = cvUsedRecords;
            }

            cvItemsThreshold = CollectionViewSource.GetDefaultView(ItemsThreshold);
            if (cvItemsThreshold != null)
            {
                dgridThresholds.ItemsSource = cvItemsThreshold;
            }

          
            StorehouseItems = getStateOfStorehouse();

            cvStorehouseItems = CollectionViewSource.GetDefaultView(StorehouseItems);
            if (cvStorehouseItems != null)
            {
                dgridStateOfStorehouse.ItemsSource = cvStorehouseItems;
            }
            cmbFilterColumn.SelectedIndex = 0;
            cmbFilterColumnTab3.SelectedIndex = 0;
            cmbFilterColumnTab4.SelectedIndex = 0;

            cmbSGroup.IsEnabled = false;
            cmbSItem.IsEnabled = false;


            cvsRecordState = CollectionViewSource.GetDefaultView(sRecordState);
            if (cvsRecordState != null)
            {
                dataGridReadStateStorehouse.ItemsSource = cvsRecordState;
            }

            cmbFilterColumnTab5.SelectedIndex = 0;

            btnReturnOneDay.IsEnabled = true;
           
        }


        #region enterDataInStore_TabOne


        private void cmbSItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tfStoreRealAmount.IsEnabled = true;
        }

        private void cmbSGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            MainWindow window = (MainWindow)MainWindow.GetWindow(this);

            if (cmbSGroup.SelectedIndex == 0)
            {
                cmbSItem.SelectedIndex = 0;
                cmbSItem.IsEnabled = false;
                tfStoreRealAmount.IsEnabled = false;
            }
            else 
            {
                tfStoreRealAmount.IsEnabled = true;
                string group;
                cmbSItem.IsEnabled = true;

                if (cmbSGroup.SelectedItem != null)
                {
                    group = cmbSGroup.SelectedItem.ToString();
                    Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab1 Izabiranje grupe stavke koja se unosi. Izabrana grupa je :" + group);
                }
                else
                {
                    cmbSGroup.SelectedIndex = 0;
                    return;
                }

                for (int j = 1; j < window.enterStoreItemsTab2.GroupsItemsInStore.Count; j++)
                {
                    if (window.enterStoreItemsTab2.GroupsItemsInStore.ElementAt(j).Equals(group) == true)
                    {
                        cmbSItem.ItemsSource = window.enterStoreItemsTab2.StoreItemsByGroup.ElementAt(j);
                        cmbSItem.SelectedIndex = 0;
                    }
                }
            }
        }


      


        private void tfStoreRealAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfStoreRealAmount.Text.Equals(String.Empty) == false)
            {
                btnEnter.IsEnabled = true;
                double d;
                double amount;
                string tfStoreRealAmountWithPoint = tfStoreRealAmount.Text.Replace(',', '.');
                bool isNumeric = Double.TryParse(tfStoreRealAmountWithPoint,NumberStyles.Any, CultureInfo.InvariantCulture, out amount);
                if (isNumeric == false)
                {
                    MessageBox.Show("Količina nije uneta kao broj!!!");
                    Logger.writeNode(Constants.MESSAGEBOX, "Količina nije uneta kao broj!!!");
                    return;
                }
            }
            else 
            {
                btnEnter.IsEnabled = false;
            }
        }


        private void datepicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
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
                    _currDate = String.Empty;
                }
                else
                {
                    // ... No need to display the time.
                    _currDate = date.Value.ToShortDateString();
                    Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab1 Unos datuma za unos stavke u šank. Izabrani datum unosa stavke šanka je :" + _currDate);
                    _dateCreatedReport = date.Value;
                    dateRemark.Text = String.Empty;
                    cmbSGroup.IsEnabled = true;
                    cmbSItem.IsEnabled = true;

                    //return all
                   /* DailyStoreItem.Clear();
                    MainWindow win = (MainWindow)Window.GetWindow(this);
                    for (int i = 0; i < win.enterStoreItemsTab2.StoreItemProducts.Count; i++)
                    {
                        StoreItemProduct sip = new StoreItemProduct();
                        sip.CodeProduct = win.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).CodeProduct;
                        sip.Amount = win.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Amount;
                        sip.Group = win.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Group;
                        sip.isUsed = win.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).isUsed;
                        sip.KindOfProduct = win.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).KindOfProduct;
                        sip.Measure = win.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Measure;
                        sip.Price = win.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Price;
                        sip.RealAmount = win.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).RealAmount;
                        sip.Threshold = win.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Threshold;
                        DailyStoreItem.Add(sip);

                    }
                    dgridDailyEnterInStorehouse.ItemsSource = DailyStoreItem;
                    dgridDailyEnterInStorehouse.Foreground = Brushes.Black;*/
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You did not enter date in tab1!!!");
                Logger.writeNode(Constants.EXCEPTION, "You did not enter date in tab1!!!");
                MainWindow window = (MainWindow)MainWindow.GetWindow(this);
                window.savenumofitemsEVERCreated();
            }
        }


        private void insertRecordInEverEnterInStorehouse(StorehouseItem storehouseItem) 
        {
            try
            {
                MainWindow win = (MainWindow)MainWindow.GetWindow(this);

                string id = "33";//Queries.xml ID

                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;


                conHelp.Open();
                com = new OleDbCommand(query, conHelp);
                com.Parameters.AddWithValue("@StoreItemCode", storehouseItem.ItemCode);
                com.Parameters.AddWithValue("@StoreItemName", storehouseItem.ItemName);
                com.Parameters.AddWithValue("@RealAmount", storehouseItem.ItemRealAmount.ToString());
                com.Parameters.AddWithValue("@RealPrice", storehouseItem.ItemPrice.ToString());
                com.Parameters.AddWithValue("@Valuta", win.Currency);
                com.Parameters.AddWithValue("@CreatedDateTimeInApp", DateTime.Now.ToString());
                com.Parameters.AddWithValue("@LastDateTimeUpdatedInApp", DateTime.Now.ToString());
                com.Parameters.AddWithValue("@UserCanControlDateTime", _dateCreatedReport);
                com.Parameters.AddWithValue("@UserLastUpdateDateTime", _dateCreatedReport);
                com.Parameters.AddWithValue("@NumberOfUpdates", "0");
                com.Parameters.AddWithValue("@Threshold", 0.0);
               
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
            }
            finally
            {
                if (conHelp != null)
                {
                    conHelp.Close();
                }
            }
        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            window.reportNotYetCreated = true;


            dgridDailyEnterInStorehouse.Foreground = Brushes.Blue;

            datepicker1.Text = String.Empty;
            //cmbSGroup.SelectedIndex = 0;
            //cmbSItem.SelectedIndex = 0;
            cmbSGroup.IsEnabled = false;
            cmbSItem.IsEnabled = false;
            tfStoreRealAmount.IsEnabled = false;
            btnEnter.IsEnabled = false;

           
            window.IsEnteredMoreBuyedStoreItems = true;
            // now you can enter product in bar book
            window.cmbNameProductTab1.ItemsSource = window.ProductsWithOrderNames;
            window.cmbNameProductTab1.SelectedIndex = 1;

            DateTime OldDate = window.DateOfLastCreatedBarBook;
            datepicker1.SelectedDate = OldDate.AddDays(2);
            btnFinish.IsEnabled = false;


            window.cmbNameProductTab1.IsEnabled = true;
            window.datepicker1.SelectedDate = OldDate.AddDays(1);

            DailyStoreItem.Clear();
            btnReturnOneDay.IsEnabled = false;


            window.chbNotWorkingDay.IsEnabled = true;
        }


        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow window = (MainWindow)MainWindow.GetWindow(this);
                if (window.reportNotYetCreated == true)
                {
                    MessageBox.Show("Niste kreirali knjigu šanka! Ukoliko ste zaboravili da unesete u šank a kliknuli ste već na dugme [završi današnji unos u šank] uradite sledeće:" + System.Environment.NewLine + "1. Čekirajte opciju neradan dan, koja se nalazi na tabu 1" + System.Environment.NewLine + "2. Kliknuti na dugme kreiraj knjigu šanka" + System.Environment.NewLine + "3. Vratiti sistem dan unazad [tab 3]");
                    return;
                }


                if (isBarBookCreated == false) 
                {
                    MessageBox.Show("Niste kreirali knjigu šanaka!");
                    return;
                }
                else
                {
                    btnFinish.IsEnabled = true;
                }
                StorehouseItem storehouseItem2 = new StorehouseItem();
                string selectedSItem = String.Empty;
                string[] arr; 
                string selectedItemCode = String.Empty;
                bool isSelected = false;

                if (dgridStateOfStorehouse.SelectedItem != null)
                {
                    selectedSItem = dgridStateOfStorehouse.SelectedItem.ToString();
                    arr = selectedSItem.Split('&');
                    selectedItemCode = arr[0];
                }

               
                ObservableCollection<StoreItemProduct> storeItemProducts = window.enterStoreItemsTab2.StoreItemProducts;
                StoreItemProduct sItem = new StoreItemProduct();
               

                for (int i = 0; i < storeItemProducts.Count; i++)
                {
                    if (storeItemProducts.ElementAt(i).KindOfProduct.Equals(cmbSItem.SelectedItem.ToString()))
                    {
                        sItem = storeItemProducts.ElementAt(i);
                        break;
                    }
                }

                // remove from daily dont entered store items
                for (int i = 0; i < DailyStoreItem.Count; i++)
                {
                    if (DailyStoreItem.ElementAt(i).KindOfProduct.Equals(cmbSItem.SelectedItem.ToString()))
                    {
                        for (int j = 0; j < StoreItemBought.Count; j++ )
                        {
                            if (StoreItemBought.ElementAt(j).CodeProduct.Equals(DailyStoreItem.ElementAt(i).CodeProduct) == true)
                            {
                                string realNumberWithPoint = tfStoreRealAmount.Text.Replace(',','.');
                                double number;
                                bool isN = Double.TryParse(realNumberWithPoint, out number);
                                StoreItemBought.ElementAt(j).RealAmount = number * DailyStoreItem.ElementAt(i).Amount;
                                break;
                            }
                        }

                        DailyStoreItem.RemoveAt(i);
                        dgridDailyEnterInStorehouse.ItemsSource = DailyStoreItem;
                    }
                }

                double d;
                double numberOfItems;
                string tfStoreRealAmountWithPoint = tfStoreRealAmount.Text.Replace(',', '.');
                bool isNumeric = Double.TryParse(tfStoreRealAmountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out numberOfItems);
                if (isNumeric == false)
                {
                    MessageBox.Show("Količina nije uneta kao broj!!! Podatak nije unet u šank!!!");
                    Logger.writeNode(Constants.MESSAGEBOX, "Količina nije uneta kao broj!!! Podatak nije unet u šank!!!");
                    return;
                }

                double realAmount = numberOfItems * sItem.Amount;
                double realPrice = numberOfItems * sItem.Price;


                con.Open();

                string idCount = "17";//Queries.xml ID

                XDocument xdocCount = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement QueryCount = (from xml2 in xdocCount.Descendants("Query")
                                       where xml2.Element("ID").Value == idCount
                                       select xml2).FirstOrDefault();
               
                string queryCount = QueryCount.Attribute(Constants.TEXT).Value;
                queryCount = queryCount + "'" + sItem.CodeProduct + "'" + ";";


                com = new OleDbCommand(queryCount, con);
                Int32 count = (Int32)com.ExecuteScalar();

                if (count == 0)
                {
                    //insert in storehouse new store item
                    string id = "14";//Queries.xml ID

                    XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement Query = (from xml2 in xdoc.Descendants("Query")
                                      where xml2.Element("ID").Value == id
                                      select xml2).FirstOrDefault();
                    Console.WriteLine(Query.ToString());
                    string query = Query.Attribute(Constants.TEXT).Value;
                    query = query + "(" + "'" + sItem.CodeProduct + "'" + "," + "'" + realAmount.ToString() + "'" + "," + "'" + realPrice.ToString() + "'" + "," + "'" + window.Currency + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + _dateCreatedReport + "'" + "," + "'" + _dateCreatedReport + "'" + "," + "'" + "0" + "'" + "," + "'" + 0 + "'" + ");";


                    com = new OleDbCommand(query, con);
                    com.ExecuteNonQuery();


                    // refresh StorehouseItems collection

                    StorehouseItem storehouseItem = new StorehouseItem(sItem.CodeProduct, String.Empty, String.Empty, -1, 0.0, realAmount);

                    string kindOfProduct = String.Empty;
                    string groupItem = String.Empty;
                    string amountforOne = String.Empty;
                    double amountforOneDouble;
                    int price = -1;

                    double threshold;

                   
                        string id2 = "21";//Queries.xml ID
                        XDocument xdocStore2 = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                        XElement Query2 = (from xml2 in xdocStore2.Descendants("Query")
                                           where xml2.Element("ID").Value == id2
                                           select xml2).FirstOrDefault();
                        Console.WriteLine(Query2.ToString());
                        string query2 = Query2.Attribute(Constants.TEXT).Value;
                        query2 = query2 + "'" + storehouseItem.ItemCode + "'" + ";";
                        com = new OleDbCommand(query2, con);
                        drInner = com.ExecuteReader();
                        while (drInner.Read())
                        {
                            //codeProduct = dr["StoreItemCode"].ToString();
                            kindOfProduct = drInner["StoreItemName"].ToString();
                            bool isNumericNN = int.TryParse(drInner["StoreItemPrice"].ToString(), out price);// price for one item
  
                            groupItem = drInner["StoreItemGroup"].ToString();
                            amountforOne = drInner["Amount"].ToString();
                            string amountWithPoint = drInner["Amount"].ToString().Replace(',', '.');
                            bool isNum = Double.TryParse(amountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out amountforOneDouble);
                            //realAmount = dr["RealAmount"].ToString();
                            //bool isNumm = Double.TryParse(dr["RealAmount"].ToString(), out realAmountDouble);
                            string thresholdWithPoint = drInner["Threshold"].ToString().Replace(',', '.');
                            bool isNumN = Double.TryParse(thresholdWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out threshold);

                            storehouseItem.ItemName = kindOfProduct;
                            storehouseItem.ItemforOnePrice = price;
                            storehouseItem.ItemGroup = groupItem;
                            storehouseItem.ItemforOneAmount = amountforOneDouble;
                            storehouseItem.Threshold = threshold;
                            //calculated value
                            storehouseItem.ItemPrice = storehouseItem.ItemRealAmount / storehouseItem.ItemforOneAmount * storehouseItem.ItemforOnePrice;
                        }

                        StorehouseItems.Add(storehouseItem);
                        storehouseItem2 = storehouseItem;
                        cvStorehouseItems = CollectionViewSource.GetDefaultView(StorehouseItems);
                        if (cvStorehouseItems != null)
                        {
                            dgridStateOfStorehouse.ItemsSource = cvStorehouseItems;
                        }

                    // refresh StorehouseItems collection
                
 
                }
                else if (count == 1)
                {
                    //update amount in storehouse for existing item

                    //first find real amount of existing store item
                    double oldamount = 0.0;
                    double priceNew;
                    string id = "18";//Queries.xml ID
                    XDocument xdoc2 = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement Query2 = (from xml2 in xdoc2.Descendants("Query")
                                       where xml2.Element("ID").Value == id
                                       select xml2).FirstOrDefault();
                   
                    string query2 = Query2.Attribute(Constants.TEXT).Value;
                    query2 = query2 + "'" + sItem.CodeProduct + "'";

                   
                    com = new OleDbCommand(query2, con);
                    dr = com.ExecuteReader();
                   

                    while (dr.Read())
                    {
                        string realAmountWithPoint = dr["RealAmount"].ToString().Replace(',', '.');
                        bool isN = Double.TryParse(realAmountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out oldamount);
                    }

                    //now sum old amount with new and get new price
                    double newRealamount;
                    newRealamount = oldamount + realAmount;
                    priceNew = sItem.Price * (newRealamount/sItem.Amount); 


                    //now update database
                    string queryStorehouse = "UPDATE storehouse SET RealAmount = " + "'" + newRealamount.ToString() + "'" + " WHERE StoreItemCode =" + "'" + sItem.CodeProduct + "'" + ";";
                    com = new OleDbCommand(queryStorehouse, con);
                    com.ExecuteNonQuery();
                    queryStorehouse = "UPDATE storehouse SET RealPrice = " + "'" + priceNew.ToString() + "'" + " WHERE StoreItemCode =" + "'" + sItem.CodeProduct + "'" + ";";
                    com = new OleDbCommand(queryStorehouse, con);
                    com.ExecuteNonQuery();
                    queryStorehouse = "UPDATE storehouse SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + " WHERE StoreItemCode =" + "'" + sItem.CodeProduct + "'" + ";";
                    com = new OleDbCommand(queryStorehouse, con);
                    com.ExecuteNonQuery();
                    queryStorehouse = "UPDATE storehouse SET UserLastUpdateDateTime = " + "'" + _dateCreatedReport + "'" + " WHERE StoreItemCode =" + "'" + sItem.CodeProduct + "'" + ";";
                    com = new OleDbCommand(queryStorehouse, con);
                    com.ExecuteNonQuery();

                    string query = "SELECT NumberOfUpdates FROM storehouse WHERE StoreItemCode = " + "'" + sItem.CodeProduct + "'" + ";";
                    com = new OleDbCommand(query, con);
                    dr2 = com.ExecuteReader();
                    int oldUpNum = 0;
                    while (dr2.Read())
                    {
                        bool isNum = int.TryParse(dr2["NumberOfUpdates"].ToString(), out oldUpNum);
                    }


                    int upNum = oldUpNum + 1;
                    queryStorehouse = "UPDATE storehouse SET NumberOfUpdates = " + "'" + upNum.ToString() + "'" + "WHERE StoreItemCode =" + "'" + sItem.CodeProduct + "'" + ";";
                    com = new OleDbCommand(queryStorehouse, con);
                    com.ExecuteNonQuery();

                    // refresh StorehouseItems collection
                    //StorehouseItems = getStateOfStorehouse();
                    //dgridStateOfStorehouse.ItemsSource = StorehouseItems;

                    // refresh StorehouseItems collection

                    StorehouseItem storehouseItem = new StorehouseItem(sItem.CodeProduct, String.Empty, String.Empty, sItem.Price, 0.0, newRealamount);

                    string kindOfProduct = String.Empty;
                    string groupItem = String.Empty;
                    string amountforOne = String.Empty;
                    double amountforOneDouble;

                    double realAmountDouble;
                    double threshold;


                    string id3 = "21";//Queries.xml ID
                    XDocument xdocStore3 = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement Query3 = (from xml2 in xdocStore3.Descendants("Query")
                                       where xml2.Element("ID").Value == id3
                                       select xml2).FirstOrDefault();
                    Console.WriteLine(Query2.ToString());
                    string query3 = Query3.Attribute(Constants.TEXT).Value;
                    query3 = query3 + "'" + storehouseItem.ItemCode + "'" + ";";
                    com = new OleDbCommand(query3, con);
                    drInner = com.ExecuteReader();
                    while (drInner.Read())
                    {
                        //codeProduct = dr["StoreItemCode"].ToString();
                        kindOfProduct = drInner["StoreItemName"].ToString();
                      
                      
                        groupItem = drInner["StoreItemGroup"].ToString();
                        amountforOne = drInner["Amount"].ToString();
                        string amountWithPoint = drInner["Amount"].ToString().Replace(',', '.');
                        bool isNum = Double.TryParse(amountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out amountforOneDouble);
                        string thresholdWithPoint = drInner["Threshold"].ToString().Replace(',', '.');
                        bool isNumN = Double.TryParse(thresholdWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out threshold);

                        storehouseItem.ItemName = kindOfProduct;
                        storehouseItem.ItemGroup = groupItem;
                        storehouseItem.ItemforOneAmount = amountforOneDouble;
                        storehouseItem.Threshold = threshold;
                        storehouseItem.ItemPrice = priceNew;
                        storehouseItem.ItemRealAmount = newRealamount;
                    }
                       //instead this line because UPDATE not INSERT StorehouseItems.Add(storehouseItem);
                    for (int i = 0; i < StorehouseItems.Count; i++)
                    {
                        if (StorehouseItems.ElementAt(i).ItemCode.Equals(storehouseItem.ItemCode) == true)
                        {
                            StorehouseItems.ElementAt(i).ItemName = storehouseItem.ItemName;
                            StorehouseItems.ElementAt(i).ItemGroup = storehouseItem.ItemGroup;
                            StorehouseItems.ElementAt(i).ItemforOneAmount = storehouseItem.ItemforOneAmount;
                            StorehouseItems.ElementAt(i).Threshold = storehouseItem.Threshold;
                            StorehouseItems.ElementAt(i).ItemPrice = storehouseItem.ItemPrice;
                            StorehouseItems.ElementAt(i).ItemRealAmount = storehouseItem.ItemRealAmount;

                            if (StorehouseItems.ElementAt(i).ItemCode.Equals(selectedItemCode))
                             {
                                isSelected = true;
                             }
                             
                              if(isSelected)
                             {
                                tf4.Text = StorehouseItems.ElementAt(i).ItemRealAmount.ToString();
                                tf5.Text = StorehouseItems.ElementAt(i).ItemPrice.ToString();
                             }

                            break;
                        }
                       
                    }

                    // only update part save not sum only last enter if you enter 3 then 1 save in storehouseItem2 only 1 but storehouseItem save 4(3+1) for realAmount
                    storehouseItem2 = storehouseItem;
                    storehouseItem2.ItemRealAmount = realAmount;
                    storehouseItem2.ItemPrice = sItem.Price * (realAmount / sItem.Amount);
                    cvStorehouseItems = CollectionViewSource.GetDefaultView(StorehouseItems);
                    if (cvStorehouseItems != null)
                    {
                        dgridStateOfStorehouse.ItemsSource = cvStorehouseItems;
                    }
                    // refresh StorehouseItems collection

                   


                 
                   
                }
               

                tfStoreRealAmount.Text = String.Empty;
                cmbSItem.SelectedIndex = 0;

                //add real amount in temporary program structure
                if (workingProduct != null)
                {
                    StoreItemProduct sItemRealUpdate = new StoreItemProduct();

                    for (int i = 0; i < workingProduct.StoreItemProducts.Count; i++)
                    {
                        if (workingProduct.StoreItemProducts.ElementAt(i).KindOfProduct.Equals(sItem.KindOfProduct) == true)
                        {
                            sItemRealUpdate = workingProduct.StoreItemProducts.ElementAt(i);
                            break;
                        }
                    }


                    string idStore = "15";//Queries.xml ID
                    XDocument xdocStore2 = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement QueryStore = (from xml2 in xdocStore2.Descendants("Query")
                                           where xml2.Element("ID").Value == idStore
                                           select xml2).FirstOrDefault();

                    string queryStore = QueryStore.Attribute(Constants.TEXT).Value;
                    queryStore = queryStore + "'" + sItemRealUpdate.CodeProduct + "'" + ";";
                    com = new OleDbCommand(queryStore, con);
                    drReal = com.ExecuteReader();
                    while (drReal.Read())
                    {
                        double dd;
                        string realAmountWithPoint = drReal["RealAmount"].ToString().Replace(',', '.');
                        bool isN = Double.TryParse(realAmountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out dd);
                        sItemRealUpdate.RealAmount = dd;

                    }
                }//end of working product null check



                // insert record in EverEnterInStorehouse
               insertRecordInEverEnterInStorehouse(storehouseItem2);
               

                // refresh sRecord collection
               for (DateTime x = window.overviewStorehouse.DateCreatedReportStart; x <= window.overviewStorehouse.DateCreatedReportEnd; x = x.AddDays(1))
                    {
                        string dateCurrStr = x.ToString().Replace("0:00:00", "");
                        dateCurrStr = dateCurrStr.Substring(0,dateCurrStr.Length-1);
                        StorehouseItemRecord sr = new StorehouseItemRecord(storehouseItem2.ItemCode, storehouseItem2.ItemName, storehouseItem2.ItemRealAmount.ToString(), storehouseItem2.ItemPrice.ToString(), window.Currency, DateTime.Now, DateTime.Now, _dateCreatedReport, _dateCreatedReport, "0", storehouseItem2.Threshold.ToString());
                        Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab1 Unos stavke u šank. Sifra stavke šanka :" + sr.StoreItemCode + ". Naziv stavke šanka :" + sr.StoreItemName + ". Ukupna uneta kolicina(kg/l) :" + sr.RealAmount + ". Ukupna vrednost unete stavke(din) :" + sr.RealPrice + ". Datum korisnika" + sr.UserCanControlDateTime + ". Prag stavke :" + sr.Threshold  );
                        if (dateCurrStr.Equals(sr.UserCanControlDateTime.ToString()) == true)
                        {

                            int num = 0;
                           
                            window.overviewStorehouse.sRecord.Add(sr);

                            window.overviewStorehouse.cvsRecord = CollectionViewSource.GetDefaultView(window.overviewStorehouse.sRecord);
                            if (window.overviewStorehouse.cvsRecord != null)
                            {
                                window.overviewStorehouse.dataGridReadStore.ItemsSource = window.overviewStorehouse.cvsRecord;
                            }   

                         break;
                        }
                    }


               dgridDailyEnterInStorehouse.ItemsSource = DailyStoreItem;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                MainWindow window = (MainWindow)MainWindow.GetWindow(this);
                window.savenumofitemsEVERCreated();

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
                if (drInner != null)
                {
                    drInner.Close();
                }
                if (dr2 != null)
                {
                    dr2.Close();
                }
            }
        }

        #endregion

        #region leftside_TabTwo

        private void sayItemInUseForAllRecords(Product p, StoreItemProduct sItem) 
        {
            
            MainWindow window = (MainWindow)MainWindow.GetWindow(this);
            for (int i = 0; i < window.selectUpdateConnProdStore.Records.Count; i++)
            {
                if (window.selectUpdateConnProdStore.Records.ElementAt(i).ConnCodeProduct.Equals(p.CodeProduct) && window.selectUpdateConnProdStore.Records.ElementAt(i).ConnStoreItemCode.Equals(sItem.CodeProduct))
                {
                    window.selectUpdateConnProdStore.Records.ElementAt(i).IsUsed = true;
                  
                    break;
                }
            }

            for (int i = 0; i < window.selectUpdateConnProdStore.Records.Count; i++)
            {
                if (window.selectUpdateConnProdStore.Records.ElementAt(i).ConnCodeProduct.Equals(p.CodeProduct) && window.selectUpdateConnProdStore.Records.ElementAt(i).ConnStoreItemCode.Equals(sItem.CodeProduct))
                {
                    ConnectionRecord cr = new ConnectionRecord(p.CodeProduct, sItem.CodeProduct, p.KindOfProduct, sItem.KindOfProduct, sItem.Group, window.selectUpdateConnProdStore.Records.ElementAt(i).AmountProduct.ToString(), sItem.Amount.ToString(), sItem.Price.ToString());
                    usedRecords.Add(cr);
                    break;
                }
            }

            cvUsedRecords = CollectionViewSource.GetDefaultView(usedRecords);
            if (cvUsedRecords != null)
            {
                dgridUsed.ItemsSource = cvUsedRecords;
            }

    
        }


        private void sayItemInNOTUseForAllRecords(Product p, StoreItemProduct sItem)
        {
            MainWindow window = (MainWindow)MainWindow.GetWindow(this);
            for (int i = 0; i < window.selectUpdateConnProdStore.Records.Count; i++)
            {
                if (window.selectUpdateConnProdStore.Records.ElementAt(i).ConnCodeProduct.Equals(p.CodeProduct) && window.selectUpdateConnProdStore.Records.ElementAt(i).ConnStoreItemCode.Equals(sItem.CodeProduct))
                {
                    window.selectUpdateConnProdStore.Records.ElementAt(i).IsUsed = false;
                    break;
                }
            }

            for (int i = 0; i < usedRecords.Count; i++)
            {
                if (usedRecords.ElementAt(i).ConnCodeProduct.Equals(p.CodeProduct) && usedRecords.ElementAt(i).ConnStoreItemCode.Equals(sItem.CodeProduct))
                {
                    usedRecords.RemoveAt(i);
                    break;
                }
            }

            cvUsedRecords = CollectionViewSource.GetDefaultView(usedRecords);
            if (cvUsedRecords != null)
            {
                dgridUsed.ItemsSource = cvUsedRecords;
            }
        }

        private void reportToDatabaseItemInUse(Product p, StoreItemProduct sItem) 
        {
            try
            {
                //report in database store item is in use.
                con.Open();
                string query = "UPDATE storeItems SET isUsed = " + "'" + Constants.YES + "'" + " WHERE StoreItemCode =" + "'" + sItem.CodeProduct + "'" + ";";
                com = new OleDbCommand(query, con);
                com.ExecuteNonQuery();

                string queryConn = "UPDATE connectionTableProductsStore SET isUsed = " + "'" + Constants.YES + "'" + " WHERE ConnStoreItemCode =" + "'" + sItem.CodeProduct + "'" + " AND " + " ConnCodeProduct =" + "'" + p.CodeProduct + "'" +";";
                com = new OleDbCommand(queryConn, con);
                com.ExecuteNonQuery();

                string query2 = "UPDATE connectionTableProductsStore SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + " WHERE ConnStoreItemCode =" + "'" + sItem.CodeProduct + "'" + ";";
                com = new OleDbCommand(query2, con);
                com.ExecuteNonQuery();

                string queryConn2 = "SELECT NumberOfUpdates FROM connectionTableProductsStore WHERE ConnStoreItemCode = " + "'" + sItem.CodeProduct + "'" + ";";
                com = new OleDbCommand(queryConn2, con);
                dr2 = com.ExecuteReader();
                int oldUpNum = 0;
                while (dr2.Read())
                {
                    bool isNum = int.TryParse(dr2["NumberOfUpdates"].ToString(), out oldUpNum);
                }


                int upNum = oldUpNum + 1;
                queryConn = "UPDATE connectionTableProductsStore SET NumberOfUpdates = " + "'" + upNum.ToString() + "'" + "WHERE ConnStoreItemCode =" + "'" + sItem.CodeProduct + "'" + ";";
                com = new OleDbCommand(queryConn, con);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                MainWindow window = (MainWindow)MainWindow.GetWindow(this);
                window.savenumofitemsEVERCreated();
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
                if (dr2 != null)
                {
                    dr2.Close();
                }
            }

            return;
        }


        private void reportToDatabaseItemNotInUse(Product p, StoreItemProduct sItem)
        {
            try
            {
                //report in database store item is in use.
                con.Open();
                string query = "UPDATE storeItems SET isUsed = " + "'" + Constants.NO + "'" + " WHERE StoreItemCode =" + "'" + sItem.CodeProduct + "'" + ";";
                com = new OleDbCommand(query, con);
                com.ExecuteNonQuery();

                string queryStorehouse = "UPDATE storeItems SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE StoreItemCode =" + "'" + sItem.CodeProduct + "'" + ";";
                com = new OleDbCommand(queryStorehouse, con);
                com.ExecuteNonQuery();

                query = "SELECT NumberOfUpdates FROM storeItems WHERE StoreItemCode = " + "'" + sItem.CodeProduct + "'" + ";";
                com = new OleDbCommand(query, con);
                dr = com.ExecuteReader();
                int oldUpNum2 = 0;
                while (dr.Read())
                {
                    bool isNum = int.TryParse(dr["NumberOfUpdates"].ToString(), out oldUpNum2);
                }


                int upNum2 = oldUpNum2 + 1;
                queryStorehouse = "UPDATE storeItems SET NumberOfUpdates = " + "'" + upNum2.ToString() + "'" + "WHERE StoreItemCode =" + "'" + sItem.CodeProduct + "'" + ";";
                com = new OleDbCommand(queryStorehouse, con);
                com.ExecuteNonQuery();

                string queryConn = "UPDATE connectionTableProductsStore SET isUsed = " + "'" + Constants.NO + "'" + " WHERE ConnStoreItemCode =" + "'" + sItem.CodeProduct + "'" + " AND " + " ConnCodeProduct =" + "'" + p.CodeProduct + "'" + ";";
                com = new OleDbCommand(queryConn, con);
                com.ExecuteNonQuery();

                string query2 = "UPDATE connectionTableProductsStore SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + " WHERE ConnStoreItemCode =" + "'" + sItem.CodeProduct + "'" + ";";
                com = new OleDbCommand(query2, con);
                com.ExecuteNonQuery();

                string queryConn2 = "SELECT NumberOfUpdates FROM connectionTableProductsStore WHERE ConnStoreItemCode = " + "'" + sItem.CodeProduct + "'" + ";";
                com = new OleDbCommand(queryConn2, con);
                dr2 = com.ExecuteReader();
                int oldUpNum = 0;
                while (dr2.Read())
                {
                    bool isNum = int.TryParse(dr2["NumberOfUpdates"].ToString(), out oldUpNum);
                }


                int upNum = oldUpNum + 1;
                queryConn = "UPDATE connectionTableProductsStore SET NumberOfUpdates = " + "'" + upNum.ToString() + "'" + "WHERE ConnStoreItemCode =" + "'" + sItem.CodeProduct + "'" + ";";
                com = new OleDbCommand(queryConn, con);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                MainWindow window = (MainWindow)MainWindow.GetWindow(this);
                window.savenumofitemsEVERCreated();
            }
            finally
            {

                if (con != null)
                {
                    con.Close();
                }
                if (dr2 != null)
                {
                    dr2.Close();
                }
                if (dr != null)
                {
                    dr.Close();
                }
            }

            return;
        }

        private void cmbItemStore_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbItemStore.SelectedIndex == 0)
            {
                dgrid.Visibility = Visibility.Hidden;
                dgridinUse.Visibility = Visibility.Hidden;
            }
            else 
            {
                
                _currStoreItemProductsInUse.Clear();
                dgrid.Visibility = Visibility.Visible;
                dgridinUse.Visibility = Visibility.Visible;
                string kindOfProduct = String.Empty;
                if (cmbItemStore.SelectedItem != null)
                {
                    kindOfProduct = cmbItemStore.SelectedItem.ToString();
                }
                MainWindow window = (MainWindow)MainWindow.GetWindow(this);

                for (int i = 0; i < window.ProductsWholeInformation.Count; i++)
                {
                    if (window.ProductsWholeInformation.ElementAt(i).KindOfProduct.Equals(kindOfProduct) == true)
                    {
                        _currStoreItemProducts = window.ProductsWholeInformation.ElementAt(i).StoreItemProducts;
                        dgrid.ItemsSource = _currStoreItemProducts;
                        workingProduct = window.ProductsWholeInformation.ElementAt(i);
                        Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab2 Izabiranje proizvoda kafica. Sifra izabranog proizvoda je :" + workingProduct.CodeProduct + ". Vrsta izabranog proizvoda je :" + workingProduct.KindOfProduct);
                        break;
                    }
                }

                // add store items in use
                for (int i = 0; i < workingProduct.StoreItemProducts.Count; i++)
                {
                    if (workingProduct.StoreItemProducts.ElementAt(i).isUsed == true)
                    {
                        _currStoreItemProductsInUse.Add(workingProduct.StoreItemProducts.ElementAt(i));
                        dgridinUse.ItemsSource = _currStoreItemProductsInUse;
                    }
                }

              
            }
        }



        private double askDatabaseHowMuchSearchStoreHaveInStorehouse(StoreItemProduct searchStore)
        {
            try
            {
                double oldamount = 0.0;
                con.Open();
                string id = "18";//Queries.xml ID
                XDocument xdoc2 = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query2 = (from xml2 in xdoc2.Descendants("Query")
                                   where xml2.Element("ID").Value == id
                                   select xml2).FirstOrDefault();

                string query2 = Query2.Attribute(Constants.TEXT).Value;
                query2 = query2 + "'" + searchStore.CodeProduct + "'";


                com = new OleDbCommand(query2, con);
                dr = com.ExecuteReader();


                while (dr.Read())
                {
                    string realAmountWithPoint = dr["RealAmount"].ToString().Replace(',', '.');
                    bool isN = Double.TryParse(realAmountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out oldamount);
                }

                return oldamount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                MainWindow window = (MainWindow)MainWindow.GetWindow(this);
                window.savenumofitemsEVERCreated();
                return -10000.0;
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

        // upper datagrid
        private void dgrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StoreItemProduct searchStore;
            string itemStoreInUse = dgrid.SelectedItem.ToString();
            for (int i = 0; i < workingProduct.StoreItemProducts.Count; i++)
            {
                if (workingProduct.StoreItemProducts.ElementAt(i).KindOfProduct.Equals(itemStoreInUse) == true)
                {
                    if (workingProduct.StoreItemProducts.ElementAt(i).isUsed == false)
                    {

                        searchStore = workingProduct.StoreItemProducts.ElementAt(i);
                        Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab2 Stavljanje u upotrebu stavke šanka za izabrani proizvod. Sifra stavke šanka :" + searchStore.CodeProduct + ". Naziv stavke šanka :" + searchStore.KindOfProduct + ". Grupa stavke šanka :" + searchStore.Group);
                        Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab2 Stavljanje u upotrebu stavke šanka za izabrani proizvod. Sifra proizvoda :" + workingProduct.CodeProduct + ". Vrsta proizvoda kafica :" + workingProduct.KindOfProduct);
                        workingProduct.StoreItemProducts.ElementAt(i).RealAmount = askDatabaseHowMuchSearchStoreHaveInStorehouse(searchStore);

                        if (workingProduct.StoreItemProducts.ElementAt(i).RealAmount == 0.0)
                        {
                            MessageBox.Show("Ovu stavku šanka trenutno nemate na stanju");
                            Logger.writeNode(Constants.MESSAGEBOX, "Ovu stavku šanka trenutno nemate na stanju");
                            return;
                        }


                        for (int j = 0; j < _currStoreItemProductsInUse.Count; j++)
                        {
                            if (_currStoreItemProductsInUse.ElementAt(j).Group.Equals(workingProduct.StoreItemProducts.ElementAt(i).Group) == true)
                            {
                                MessageBox.Show("Ova grupa stavki magacina  [" + workingProduct.StoreItemProducts.ElementAt(i).Group + "]    je u upotrebi!");
                                Logger.writeNode(Constants.MESSAGEBOX, "Ova grupa stavki magacina  [" + workingProduct.StoreItemProducts.ElementAt(i).Group + "]    je u upotrebi!");
                                return;
                            }
                        }



                        workingProduct.StoreItemProducts.ElementAt(i).isUsed = true;
                        reportToDatabaseItemInUse(workingProduct, workingProduct.StoreItemProducts.ElementAt(i));
                        sayItemInUseForAllRecords(workingProduct, workingProduct.StoreItemProducts.ElementAt(i)); 

                       
                        _currStoreItemProductsInUse.Add(workingProduct.StoreItemProducts.ElementAt(i));
                        dgridinUse.ItemsSource = _currStoreItemProductsInUse;
                        break;
                    }
                    else 
                    {
                        MessageBox.Show("Ova stavka magacina je već u upotrebi za izabrani proizvod !", "STAVKA JE U UPOTREBI");
                        Logger.writeNode(Constants.MESSAGEBOX, "Ova stavka magacina je već u upotrebi za izabrani proizvod !");
                        break;
                    }
                }
            }// end for loop
        }

        private void dgridinUse_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgridinUse.SelectedItem != null)
            {
                string itemNotInUse = dgridinUse.SelectedItem.ToString();
                int indNotInUse = dgridinUse.SelectedIndex;

                for (int i = 0; i < workingProduct.StoreItemProducts.Count; i++)
                {
                    if (workingProduct.StoreItemProducts.ElementAt(i).KindOfProduct.Equals(itemNotInUse) == true)
                    {

                        workingProduct.StoreItemProducts.ElementAt(i).isUsed = false;
                        reportToDatabaseItemNotInUse(workingProduct, workingProduct.StoreItemProducts.ElementAt(i));
                        sayItemInNOTUseForAllRecords(workingProduct, workingProduct.StoreItemProducts.ElementAt(i));
                        Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab2 Uklanjanje iz upotrebe odgovarajuce stavke šanka za izabrani proizvod kafica. Sifra stavke šanka :" + workingProduct.StoreItemProducts.ElementAt(i).CodeProduct + ". Naziv stavke šanka :" + workingProduct.StoreItemProducts.ElementAt(i).KindOfProduct + ". Grupa stavke šanka :" + workingProduct.StoreItemProducts.ElementAt(i).Group);
                        Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab2 Uklanjanje iz upotrebe odgovarajuce stavke šanka za izabrani proizvod kafica. Sifra proizvoda :" + workingProduct.CodeProduct + ". Vrsta proizvoda kafica :" + workingProduct.KindOfProduct);
                        _currStoreItemProductsInUse.RemoveAt(indNotInUse);
                        dgridinUse.ItemsSource = _currStoreItemProductsInUse;
                        break;

                    }
                }// end for loop
            }

        }

        #endregion


        #region rightside_TabTwo

        private ObservableCollection<ConnectionRecord> getUsedData()
        {
            ObservableCollection<ConnectionRecord> res = new ObservableCollection<ConnectionRecord>();

            try
            {

                string id = "13";//Queries.xml ID
                XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;


                con.Open();
                com = new OleDbCommand(query, con);
                dr = com.ExecuteReader();
                string yesNo = String.Empty;

                while (dr.Read())
                {
                    ConnectionRecord cr = new ConnectionRecord(dr["ConnCodeProduct"].ToString(), dr["ConnStoreItemCode"].ToString(), dr["ConnKindOfProduct"].ToString(), dr["ConnStoreItemName"].ToString(), dr["GroupStoreItem"].ToString(), dr["AmountProduct"].ToString(), dr["AmountStoreItem"].ToString(), dr["Price"].ToString());
                    yesNo = dr["isUsed"].ToString();
                    if (yesNo.Equals(Constants.YES))
                    {
                        cr.IsUsed = true;
                        res.Add(cr);
                    }
                
                   
                }

                return res;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                MainWindow window = (MainWindow)MainWindow.GetWindow(this);
                window.savenumofitemsEVERCreated();
                return new ObservableCollection<ConnectionRecord>();

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


        private void btnAddFilter_MouseEnter(object sender, MouseEventArgs e)
        {
            btnAddFilter.Background = Brushes.Orange;
        }

        private void btnAddFilter_MouseLeave(object sender, MouseEventArgs e)
        {
            btnAddFilter.Background = Brushes.Black;
        }

        private void filterData() 
        {

            tblFilterStatus.Text = Constants.FILTERON;
            tblFilterStatus.Background = Brushes.Orange;

            this.cvUsedRecords.Filter = item =>
            {

                if (cmbFilterColumn.SelectedIndex == 1)
                {
                    var vitem = item as ConnectionRecord;
                    if (vitem == null) return false;
                    string kindOfProduct = vitem.ConnKindOfProduct.ToUpper();
                    string searchText = tfFilter.Text.ToUpper();
                    if (kindOfProduct.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumn.SelectedIndex == 2)
                {
                    var vitem = item as ConnectionRecord;
                    if (vitem == null) return false;
                    string storeName = vitem.ConnStoreItemName.ToUpper();
                    string searchText = tfFilter.Text.ToUpper();
                    if (storeName.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumn.SelectedIndex == 3)
                {
                    var vitem = item as ConnectionRecord;
                    if (vitem == null) return false;
                    string group = vitem.GroupStoreItem.ToUpper();
                    string searchText = tfFilter.Text.ToUpper();
                    if (group.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumn.SelectedIndex == 4)
                {
                    var vitem = item as ConnectionRecord;
                    if (vitem == null) return false;
                    string amountProduct = vitem.AmountProduct.ToUpper();
                    string searchText = tfFilter.Text.ToUpper();
                    if (amountProduct.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumn.SelectedIndex == 5)
                {
                    var vitem = item as ConnectionRecord;
                    if (vitem == null) return false;
                    string amountStore = vitem.AmountStoreItem.ToUpper();
                    string searchText = tfFilter.Text.ToUpper();
                    if (amountStore.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else
                {
                    var vitem = item as ConnectionRecord;
                    if (vitem == null) return false;
                    string price = vitem.Price.ToUpper();
                    string searchText = tfFilter.Text.ToUpper();
                    if (price.Contains(searchText) == true) { return true; }
                    else { return false; }
                }

            };
 
        }

        private void unFilteredData() 
        {
            tblFilterStatus.Text = String.Empty;
            MainWindow win = (MainWindow)Window.GetWindow(this);
            if (win.options.chkbMask.IsChecked == true)
            {
                Object obj1 = this.Resources["Gradient4"];
                tblFilterStatus.Background = (Brush)obj1;
            }
            else
            {
                tblFilterStatus.Background = Brushes.White;
            }

            this.cvUsedRecords.Filter = item =>
            {

                var vitem = item as ConnectionRecord;
                if (vitem == null) return false;
                else return true;

            };
        }

        private void tfFilter_MouseEnter(object sender, MouseEventArgs e)
        {
            if (cmbFilterColumn.SelectedIndex == 0)
            {
                tfFilter.IsReadOnly = true;

                tblFilterStatus.Text = Constants.FILTER_COLUMN;
                tblFilterStatus.Foreground = Brushes.White;
                tblFilterStatus.Background = Brushes.Red;
            }
            else 
            {
                tfFilter.IsReadOnly = false;
            }
        }

        private void tfFilter_MouseLeave(object sender, MouseEventArgs e)
        {
            if (filteredDgridUsed == false)
            {
                tblFilterStatus.Text = String.Empty;
                MainWindow win = (MainWindow)Window.GetWindow(this);
                if (win.options.chkbMask.IsChecked == true)
                {
                    Object obj1 = this.Resources["Gradient4"];
                    tblFilterStatus.Background = (Brush)obj1;
                }
                else
                {
                    tblFilterStatus.Background = Brushes.White;
                }
            }
        }

        private void tfFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (filteredDgridUsed)
            {
                if (e.Key == Key.Enter)
                {
                    unFilteredData();
                    filteredDgridUsed = false;
                }
            }
            else 
            {
                if (e.Key == Key.Enter)
                {
                    Logger.writeNode(Constants.INFORMATION,"Tab3 PodTab2 Filtriranje reci. Filtrirana rec je :" + tfFilter.Text);
                    filterData();
                    filteredDgridUsed = true;
                }
            }
        }

        private void btnAddFilter_Click(object sender, RoutedEventArgs e)
        {
            Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab2 Filtriranje reci. Filtrirana rec je :" + tfFilter.Text);
            filterData();
            filteredDgridUsed = true;
        }


         private void btnRemoveFilter_Click(object sender, RoutedEventArgs e)
         {
             unFilteredData();
             filteredDgridUsed = false;
         }

        private void btnRemoveFilter_MouseEnter(object sender, MouseEventArgs e)
        {
            btnRemoveFilter.Background = Brushes.Orange;
        }

        private void btnRemoveFilter_MouseLeave(object sender, MouseEventArgs e)
        {
            btnRemoveFilter.Background = Brushes.Black;
        }

       



        #endregion


        #region TabThree

        private void cmbThresholdGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            MainWindow window = (MainWindow)MainWindow.GetWindow(this);


            if (cmbThresholdGroups.SelectedIndex == 0)
            {
                cmbThresholdItems.IsEnabled = false;
                tfThresholdForItem.IsEnabled = false;
            }
            else
            {
                tfThresholdForItem.IsEnabled = true;
                string group;
                cmbThresholdItems.IsEnabled = true;


                if (cmbThresholdGroups.SelectedItem != null)
                {
                    group = cmbThresholdGroups.SelectedItem.ToString();
                    Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab3 Izabiranje grupe magacinske stavke. Izabrana grupa magacinske stavke je :" + group);
                }
                else
                {
                    cmbThresholdGroups.SelectedIndex = 0;
                    return;
                }

                for (int j = 1; j < window.enterStoreItemsTab2.GroupsItemsInStore.Count; j++)
                {
                    if (window.enterStoreItemsTab2.GroupsItemsInStore.ElementAt(j).Equals(group) == true)
                    {
                        cmbThresholdItems.ItemsSource = window.enterStoreItemsTab2.StoreItemsByGroup.ElementAt(j);
                        cmbThresholdItems.SelectedIndex = 0;
                    }
                }
            }
        }


        private void tfThresholdForItem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfThresholdForItem.Text.Equals(String.Empty) == false)
            {
                btnEnterThreshold.IsEnabled = true;
                double d;
                double amount;
                string tfThresholdForItemWithPoint = tfThresholdForItem.Text.Replace(',', '.');
                bool isNumeric = Double.TryParse(tfThresholdForItemWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out amount);
                if (isNumeric == false)
                {
                    MessageBox.Show("Količina nije uneta kao broj!!!");
                    Logger.writeNode(Constants.MESSAGEBOX, "Količina nije uneta kao broj!!!");
                    return;
                }
            }
            else
            {
                btnEnterThreshold.IsEnabled = false;
            }
        }


        private void setNewThresholdForStoreItemsByGroup(StoreItemProduct sItem) 
        {
            MainWindow window = (MainWindow)MainWindow.GetWindow(this);
            int groupNum = window.enterStoreItemsTab2.GroupsItemsInStore.IndexOf(sItem.Group);
            for (int i = 0; i < window.enterStoreItemsTab2.StoreItemsByGroup.ElementAt(groupNum).Count; i++)
            {
                if (window.enterStoreItemsTab2.StoreItemsByGroup.ElementAt(groupNum).ElementAt(i).KindOfProduct.Equals(sItem.KindOfProduct))
                {
                    window.enterStoreItemsTab2.StoreItemsByGroup.ElementAt(groupNum).ElementAt(i).Threshold = sItem.Threshold;
                    break;
                }
            }
        }




        #region partforFiltering_Tab3


        private void filterDataTab3()
        {

            tblFilterStatusTab3.Text = Constants.FILTERON;
            tblFilterStatusTab3.Background = Brushes.Orange;

            this.cvItemsThreshold.Filter = item =>
            {
                var vitem = item as StoreItemProduct;
                if (vitem == null) return false;
                string searchText = tfFilterTab3.Text.ToUpper();

                if (cmbFilterColumnTab3.SelectedIndex == 1)
                {
                    
                    string codeOfProduct = vitem.CodeProduct.ToUpper();
                    if (codeOfProduct.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab3.SelectedIndex == 2)
                {
                    string storeName = vitem.KindOfProduct.ToUpper();
                    if (storeName.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab3.SelectedIndex == 3)
                {
                    
                    string group = vitem.Group.ToUpper();
                    if (group.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab3.SelectedIndex == 4)
                {
                    
                    string priceForOneItem = vitem.Price.ToString().ToUpper();
                    if (priceForOneItem.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab3.SelectedIndex == 5)
                {
                   
                    string amountInOneItem = vitem.Amount.ToString().ToUpper();
                    if (amountInOneItem.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else
                {
                   
                    string threshold = vitem.Threshold.ToString().ToUpper();
                    if (threshold.Contains(searchText) == true) { return true; }
                    else { return false; }
                }

            };

        }

        private void unFilteredDataTab3()
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

            this.cvItemsThreshold.Filter = item =>
            {

                var vitem = item as StoreItemProduct;
                if (vitem == null) return false;
                else return true;

            };
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

        private void tfFilterTab3_KeyDown(object sender, KeyEventArgs e)
        {
            if (filteredDgridUsedTab3)
            {
                if (e.Key == Key.Enter)
                {
                    unFilteredDataTab3();
                    filteredDgridUsedTab3 = false;
                }
            }
            else
            {
                if (e.Key == Key.Enter)
                {
                    Logger.writeNode(Constants.INFORMATION, "Tab3 Podtab3 Filtriranje reci. Filtrirana rec je :" + tfFilterTab3.Text);
                    filterDataTab3();
                    filteredDgridUsedTab3 = true;
                }
            }
        }


        private void btnAddFilterTab3_MouseEnter(object sender, MouseEventArgs e)
        {
            btnAddFilterTab3.Background = Brushes.Orange;
        }

        private void btnAddFilterTab3_MouseLeave(object sender, MouseEventArgs e)
        {
            btnAddFilterTab3.Background = Brushes.Black;
        }

        private void btnAddFilterTab3_Click(object sender, RoutedEventArgs e)
        {
            Logger.writeNode(Constants.INFORMATION, "Tab3 Podtab3 Filtriranje reci. Filtrirana rec je :" + tfFilterTab3.Text);
            filterDataTab3();
            filteredDgridUsedTab3 = true;  
        }


        private void btnRemoveFilterTab3_MouseEnter(object sender, MouseEventArgs e)
        {
             btnRemoveFilterTab3.Background = Brushes.Orange;
        }

        private void btnRemoveFilterTab3_MouseLeave(object sender, MouseEventArgs e)
        {
             btnRemoveFilterTab3.Background = Brushes.Black;
        }

        private void btnRemoveFilterTab3_Click(object sender, RoutedEventArgs e)
        {

            unFilteredDataTab3();
            filteredDgridUsedTab3 = false;
        }




        #endregion




        private void btnEnterThreshold_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = String.Empty;
            StoreItemProduct sItem;
            MainWindow window = (MainWindow)MainWindow.GetWindow(this);

            if (cmbThresholdItems.SelectedItem != null)
            {
                selectedItem = cmbThresholdItems.SelectedItem.ToString();
            }
            else 
            {
                return;
            }

            for (int i = 0; i < window.enterStoreItemsTab2.StoreItemProducts.Count; i++)
            {
                if (window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).KindOfProduct.Equals(selectedItem) == true)
                {
                    sItem = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i);
                    tfThresholdForItemRead.Text = tfThresholdForItem.Text;
                    double d;
                    string tfThresholdForItemWithPoint = tfThresholdForItem.Text.Replace(',', '.');
                    bool isNum = Double.TryParse(tfThresholdForItemWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out d);
                    window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Threshold = d;
                    sItem.Threshold = d;

                    cvItemsThreshold = CollectionViewSource.GetDefaultView(ItemsThreshold);
                    if (cvItemsThreshold != null)
                    {
                        dgridThresholds.ItemsSource = cvItemsThreshold;
                    }

                    setNewThresholdForStoreItemsByGroup(sItem);
                    // update store item data in database
                    try
                    {
                        Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab3 Postavljanje praga za izabranu stavku magcina. Sifra stavke šanka je :" + sItem.CodeProduct + ". Naziv magacinske stavke je :" + sItem.KindOfProduct + ". Novopostavljeni prag je(kg/l) :" + sItem.Threshold.ToString() );
                        con.Open();
                        string queryStorehouse = "UPDATE storeItems SET Threshold = " + "'" + sItem.Threshold.ToString() + "'" + " WHERE StoreItemCode =" + "'" + sItem.CodeProduct + "'" + ";";                      

                        com = new OleDbCommand(queryStorehouse, con);
                        com.ExecuteNonQuery();

                        queryStorehouse = "UPDATE storeItems SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE StoreItemCode =" + "'" + sItem.CodeProduct + "'" + ";";
                        com = new OleDbCommand(queryStorehouse, con);
                        com.ExecuteNonQuery();

                        string query = "SELECT NumberOfUpdates FROM storeItems WHERE StoreItemCode = " + "'" + sItem.CodeProduct + "'" + ";";
                        com = new OleDbCommand(query, con);
                        dr = com.ExecuteReader();
                        int oldUpNum = 0;

                        while (dr.Read())
                        {
                            bool isNum2 = int.TryParse(dr["NumberOfUpdates"].ToString(), out oldUpNum);
                        }


                        int upNum = oldUpNum + 1;
                        queryStorehouse = "UPDATE storeItems SET NumberOfUpdates = " + "'" + upNum.ToString() + "'" + "WHERE StoreItemCode =" + "'" + sItem.CodeProduct + "'" + ";";
                        com = new OleDbCommand(queryStorehouse, con);
                        com.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        Logger.writeNode(Constants.EXCEPTION, ex.Message);
                        MainWindow window2 = (MainWindow)MainWindow.GetWindow(this);
                        window2.savenumofitemsEVERCreated();
                    }
                    finally
                    {
                        if(con != null)
                        {
                            con.Close();
                        }
                        if (dr != null)
                        {
                            dr.Close();
                        }
                    }

                    
                    break;
                }
            }

                        
               
        }





        #endregion


        #region stateOfStorehouse_TabFour

        private string LoadPathOfCreatingReport() 
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            return window.options.tblPathStateStore2.Text + "StanjeMagacina" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls";
        }


        private void datepickerCorrDelStore_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
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

                    _dateCreatedCorrOrDel = date.Value;
                    Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab4 Postavljanje datuma korekcije ili uklanjanja stavke šanka. Postavljeni datum je :" + _dateCreatedCorrOrDel.ToString());

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("You did not enter date in tab1!!!");
                Logger.writeNode(Constants.EXCEPTION, "You did not enter date in tab1!!!");
            }
        }

       


        private void btnCreateStateOfStorehouseExcel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            // Create a thread
            Thread newWindowThread = new Thread(new ThreadStart(() =>
            {
                // Create and show the Window
                Timer tempWindow = new Timer();
                tempWindow.Show();
                // Start the Dispatcher Processing
                System.Windows.Threading.Dispatcher.Run();
            }));
            // Set the apartment state
            newWindowThread.SetApartmentState(ApartmentState.STA);
            // Make the thread a background thread
            newWindowThread.IsBackground = true;
            // Start the thread
            newWindowThread.Start();

            if (window.options.cmbAppSound.IsChecked == true)
            {
                window.player.Play();
            }

            bool isCreated;
            System.Drawing.Color headerBackColor = System.Drawing.Color.LightGray;

            string pathOfCreatingReport = String.Empty;
            pathOfCreatingReport = LoadPathOfCreatingReport();


            ExcelFile report = new ExcelFile(pathOfCreatingReport);
            if (window.options.rbtnPortrait.IsChecked == true)
            {
                isCreated = report.createFile(3, 4, 'P');
                if (isCreated == false)
                {
                    MessageBox.Show("Izveštaj neće biti kreiran!! Molimo vas da promenite putanju fajla koji niste želeli obrisati ili promenite u Opcijama putanju izveštaja koji želite da kreirate!!!");
                    Logger.writeNode(Constants.MESSAGEBOX, "Izveštaj neće biti kreiran!! Molimo vas da promenite putanju fajla koji niste želeli obrisati ili promenite u Opcijama putanju izveštaja koji želite da kreirate!!!");
                    newWindowThread.Abort();
                    report.closeFile();
                    return;
                }
            }
            if (window.options.rbtnLandscape.IsChecked == true)
            {
                isCreated = report.createFile(3, 4, 'L');
                if (isCreated == false)
                {
                    MessageBox.Show("Izveštaj neće biti kreiran!! Molimo vas da promenite putanju fajla koji niste želeli obrisati ili promenite u Opcijama putanju izveštaja koji želite da kreirate!!!");
                    Logger.writeNode(Constants.MESSAGEBOX, "Izveštaj neće biti kreiran!! Molimo vas da promenite putanju fajla koji niste želeli obrisati ili promenite u Opcijama putanju izveštaja koji želite da kreirate!!!");
                    newWindowThread.Abort();
                    report.closeFile();
                    return;
                }
            }

            List<string> StoreItemCodes = new List<string>();
            List<string> StoreItemNames = new List<string>();
            List<string> StoreItemGroups  = new List<string>();
            List<string> StoreItemRealAmounts = new List<string>();
            List<string> StoreItemRealPrice = new List<string>();

            StoreItemCodes.Add(Constants.HEADER_STORECODE);
            StoreItemNames.Add(Constants.HEADER_STORENAME);
            StoreItemGroups.Add(Constants.HEADER_STOREGROUP);
            StoreItemRealAmounts.Add(Constants.HEADER_REALAMOUNT);
            StoreItemRealPrice.Add(Constants.HEADER_REALPRICE);




            for (int i = 0; i < StorehouseItems.Count; i++)
            {
                StoreItemCodes.Add(StorehouseItems.ElementAt(i).ItemCode);
                StoreItemNames.Add(StorehouseItems.ElementAt(i).ItemName);
                StoreItemGroups.Add(StorehouseItems.ElementAt(i).ItemGroup);
                StoreItemRealAmounts.Add(StorehouseItems.ElementAt(i).ItemRealAmount.ToString());
                StoreItemRealPrice.Add(StorehouseItems.ElementAt(i).ItemPrice.ToString());
            }

            report.setBackgroundArea("B4", "F4", headerBackColor);


            report.writeArrayVer("B", 4, "B", (4 + StorehouseItems.Count), StoreItemCodes.ToArray());

            report.writeArrayVer("C", 4, "C", (4 + StorehouseItems.Count), StoreItemNames.ToArray());

            report.writeArrayVer("D", 4, "D", (4 + StorehouseItems.Count), StoreItemGroups.ToArray());

            report.writeArrayVer("E", 4, "E", (4 + StorehouseItems.Count), StoreItemRealAmounts.ToArray());

            report.writeArrayVer("F", 4, "F", (4 + StorehouseItems.Count), StoreItemRealPrice.ToArray());

            report.setBorderArrayVertical(4, 2, (4 + StorehouseItems.Count), 2, 3);

            report.setBorderArrayVertical(4, 3, (4 + StorehouseItems.Count), 3, 3);

            report.setBorderArrayVertical(4, 4, (4 + StorehouseItems.Count), 4, 3);

            report.setBorderArrayVertical(4, 5, (4 + StorehouseItems.Count), 5, 3);

            report.setBorderArrayVertical(4, 6, (4 + StorehouseItems.Count), 6, 3);


            string company = String.Empty;
            string author = String.Empty;

            if (window.options.tblCompany2.Text.Equals(Constants.DEFAULTOPTION) == true)
            {
                company = window.options.tblInitialCompany.Text;
            }
            else
            {
                company = window.options.tblInitialAuthor.Text;
            }

            if (window.options.tblAuthor2.Text.Equals(Constants.DEFAULTOPTION) == true)
            {
                author = window.options.tblInitialAuthor.Text;
            }
            else
            {
                author = window.options.tblAuthor2.Text;
            }

          
                report.writeCell(3, window.excelNumbers("B"), company, false);
                report.writeCell((4 + StorehouseItems.Count + 3), window.excelNumbers("B"), "Author : " + author, true);


                report.writeCell(3, window.excelNumbers("F"), DateTime.Now.ToString("dd_MM_yyyy"), false);



            newWindowThread.Abort();
            if (window.options.cmbAppOpen.IsChecked == true)
            {
                report.openFile();
            }
            else
            {
                report.closeFile();
            }

            if (window.options.cmbAppSound.IsChecked == true)
            {
                window.player.Stop();
            }

            MessageBox.Show(Constants.NOTIFICATION_REPORTSTATESTOREBEGIN + DateTime.Now.ToShortDateString() + Constants.NOTIFICATION_REPORTSTATESTOREEND, "STANJE MAGACINA JE KREIRANO");
            Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab4 Kreiranje izvestaja stanja magacina.");
        }


      

        #endregion



        #region partforFiltering_Tab4


        private void filterDataTab4()
        {

            tblFilterStatusTab4.Text = Constants.FILTERON;
            tblFilterStatusTab4.Background = Brushes.Orange;

            if (cmbFilterColumnTab4.SelectedItem != null)
            {
                Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab4 Filtriranje magacina. Kolona koja se filtrira :" + cmbFilterColumnTab4.SelectedIndex.ToString() + ". Rec koja se filtrira :" + tfFilterTab4.Text);
            }
            else 
            {
                Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab4 Filtriranje magacina." + " Rec koja se filtrira :" + tfFilterTab4.Text);
            }

            this.cvStorehouseItems.Filter = item =>
            {
                var vitem = item as StorehouseItem;
                if (vitem == null) return false;
                string searchText = tfFilterTab4.Text.ToUpper();

                if (cmbFilterColumnTab4.SelectedIndex == 1)
                {

                    string codeOfProduct = vitem.ItemCode.ToUpper();
                    if (codeOfProduct.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab4.SelectedIndex == 2)
                {
                    string storeName = vitem.ItemName.ToUpper();
                    if (storeName.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab4.SelectedIndex == 3)
                {

                    string group = vitem.ItemGroup.ToUpper();
                    if (group.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab4.SelectedIndex == 4)
                {

                    string realAmount = vitem.ItemRealAmount.ToString().ToUpper();
                    if (realAmount.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else 
                {

                    string price = vitem.ItemPrice.ToString().ToUpper();
                    if (price.Contains(searchText) == true) { return true; }
                    else { return false; }
                }      

            };

        }

        private void unFilteredDataTab4()
        {
            tblFilterStatusTab4.Text = String.Empty;
            MainWindow win = (MainWindow)Window.GetWindow(this);
            if (win.options.chkbMask.IsChecked == true)
            {
                Object obj1 = this.Resources["Gradient4"];
                tblFilterStatusTab4.Background = (Brush)obj1;
            }
            else
            {
                tblFilterStatusTab4.Background = Brushes.White;
            }

            this.cvStorehouseItems.Filter = item =>
            {

                var vitem = item as StorehouseItem;
                if (vitem == null) return false;
                else return true;

            };
        }

        private void tfFilterTab4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfFilterTab4.Text.Equals(String.Empty) == false)
            {
                filterDataTab4();
                filteredDgridUsedTab4 = true;
            }
            else 
            {
                unFilteredDataTab4();
                filteredDgridUsedTab4 = false;
                tblFilterStatusTab4.Text = String.Empty;
                MainWindow win = (MainWindow)Window.GetWindow(this);
                if (win.options.chkbMask.IsChecked == true)
                {
                    Object obj1 = this.Resources["Gradient4"];
                    tblFilterStatusTab4.Background = (Brush)obj1;
                }
                else
                {
                    tblFilterStatusTab4.Background = Brushes.White;
                }
            }

        }


        private void tfFilterTab4_MouseEnter(object sender, MouseEventArgs e)
        {
            if (cmbFilterColumnTab4.SelectedIndex == 0)
            {
                tfFilterTab4.IsReadOnly = true;

                tblFilterStatusTab4.Text = Constants.FILTER_COLUMN;
                tblFilterStatusTab4.Foreground = Brushes.White;
                tblFilterStatusTab4.Background = Brushes.Red;
            }
            else
            {
                tfFilterTab4.IsReadOnly = false;
            }
        }

        private void tfFilterTab4_MouseLeave(object sender, MouseEventArgs e)
        {
            if (filteredDgridUsedTab4 == false)
            {
                tblFilterStatusTab4.Text = String.Empty;
                MainWindow win = (MainWindow)Window.GetWindow(this);
                if (win.options.chkbMask.IsChecked == true)
                {
                    Object obj1 = this.Resources["Gradient4"];
                    tblFilterStatusTab4.Background = (Brush)obj1;
                }
                else
                {
                    tblFilterStatusTab4.Background = Brushes.White;
                }
            }
        }

        private void tfFilterTab4_KeyDown(object sender, KeyEventArgs e)
        {
            if (filteredDgridUsedTab4)
            {
                if (e.Key == Key.Enter)
                {
                    unFilteredDataTab4();
                    filteredDgridUsedTab4 = false;
                }
            }
            else
            {
                if (e.Key == Key.Enter)
                {
                    filterDataTab4();
                    filteredDgridUsedTab4 = true;
                }
            }
        }



        #endregion





        private void dgridStateOfStorehouse_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgridStateOfStorehouse.SelectedIndex > -1)
            {
                string selItem = dgridStateOfStorehouse.SelectedItem.ToString();

                string[] items = selItem.Split('&');

                tf1.Text = items[0];
                tf2.Text = items[1];
                tf3.Text = items[2];
                tf4.Text = items[3];
                tf5.Text = items[4];
                Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab4 Selektovanje stavke u magacinu. Sifra magacinske stavke je :" + tf1.Text + ". Naziv magacinske stavke je :" + tf2.Text + ". Grupa magacinske stavke :" + tf3.Text + ". Ukupna kolicina stavke u magacinu(kg/l) :" + tf4.Text + "Ukupna vrednost stavke u magacinu(din) :" + tf5.Text);
                string oldRealAmountWithPoint = tf4.Text.Replace(',','.');
                bool isN = Double.TryParse(oldRealAmountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out oldRealAmount);
                string oldRealPriceWithPoint = tf5.Text.Replace(',', '.');
                bool isNN = Double.TryParse(oldRealPriceWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out oldRealPrice);
            }
        }

        private void tfCorrectionDelReasonStorehouse_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfCorrectionDelReasonStorehouse.Text.Equals(String.Empty) == false)
            {
                deletionCorrectionReasonStorehouse = tfCorrectionDelReasonStorehouse.Text;
            }
        }



        private void tfCorrectionDelReasonStorehouse_MouseEnter(object sender, MouseEventArgs e)
        {
            if (tfCorrectionDelReasonStorehouse.Text.Equals(Constants.tfCorrectionDelReasonStorehouse_INITIALTEXT) == true)
            {
                tfCorrectionDelReasonStorehouse.Text = String.Empty;
            }

        }

        private void tfCorrectionDelReasonStorehouse_MouseLeave(object sender, MouseEventArgs e)
        {
            if (tfCorrectionDelReasonStorehouse.Text.Equals(String.Empty) == true) 
            {
                tfCorrectionDelReasonStorehouse.Text = Constants.tfCorrectionDelReasonStorehouse_INITIALTEXT;
            }
        }


        private void insertintoCorrectionTableForStorehouse(string storeItemCode, string storeItemName, string newAmount, string newRealPrice, out double diffAmount)
        {

            try
            {
                MainWindow win = (MainWindow)MainWindow.GetWindow(this);
               
                double diffRealPrice;
                double newAm=0.0, newPr=0.0;
                string newAmountWithPoint = newAmount.Replace(',','.');
                bool isN = Double.TryParse(newAmountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out newAm);
                string newRealPriceWithPoint = newRealPrice.Replace(',','.');
                bool isNN = Double.TryParse(newRealPriceWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out newPr);
                diffAmount = newAm - oldRealAmount;
                diffRealPrice = newPr - oldRealPrice;

                Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab4 Korekcija selektovane stavke u magacinu. Sifra stavke :" + storeItemCode + ". Naziv stavke šanka :" + storeItemName + ". Nova ukupna kolicina stavke šanka(kg/l) :" + newAm.ToString() + ". Stara ukupna kolicina stavke šanka(kg/l) :" + oldRealAmount + "Razlika nove i stare uk kol(kg/l) :" + diffAmount + "Nova ukupna cena(din) :" + newPr + "Stara ukupna cena(din) :" + oldRealPrice + ". Razlika nove i stare ukupne cene(din): " + diffRealPrice);

                string id = "36";//Queries.xml ID

                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;


                conHelp.Open();
                com = new OleDbCommand(query, conHelp);
                com.Parameters.AddWithValue("@StoreItemCode", storeItemCode);
                com.Parameters.AddWithValue("@StoreItemName", storeItemName);
                com.Parameters.AddWithValue("@OLDRealAmount", oldRealAmount.ToString());
                com.Parameters.AddWithValue("@NEWRealAmount", newAmount);
                com.Parameters.AddWithValue("@DifferenceRealAmount", diffAmount.ToString());
                com.Parameters.AddWithValue("@OLDRealPrice", oldRealPrice.ToString());
                com.Parameters.AddWithValue("@NEWRealPrice", newRealPrice);
                com.Parameters.AddWithValue("@DifferenceRealPrice", diffRealPrice.ToString());
                com.Parameters.AddWithValue("@Valuta", win.Currency);
                com.Parameters.AddWithValue("@CorrectionDateTimeInApp", DateTime.Now.ToString());
                com.Parameters.AddWithValue("@CorrectionUserDateTime", _dateCreatedCorrOrDel.ToShortDateString());
                com.Parameters.AddWithValue("@CorrectionReason", tfCorrectionDelReasonStorehouse.Text);
               

                com.ExecuteNonQuery();



                //refresh sRecordCorr
                for (DateTime x = win.overviewStorehouse.DateCreatedReportStartTab3; x <= win.overviewStorehouse.DateCreatedReportEndTab3; x = x.AddDays(1))
                    {
                        string dateCurrStr = x.ToString().Replace("0:00:00", "");
                        dateCurrStr = dateCurrStr.Substring(0,dateCurrStr.Length-1);
                        StorehouseItemRecordCorr sRCorr = new StorehouseItemRecordCorr(storeItemCode, storeItemName, oldRealAmount.ToString(), newAmount, diffAmount.ToString(), oldRealPrice.ToString(), newRealPrice, diffRealPrice.ToString(), win.Currency, _dateCreatedCorrOrDel.ToShortDateString(), tfCorrectionDelReasonStorehouse.Text);
                        DateTime sRCorrDate = DateTime.Parse(sRCorr.CorrectionUserDateTime);
                        
                        
                        if (dateCurrStr.Equals(sRCorrDate.ToShortDateString()) == true)
                        {
						
                            win.overviewStorehouse.sRecordCor.Add(sRCorr);

                            win.overviewStorehouse.cvsRecordCor = CollectionViewSource.GetDefaultView(win.overviewStorehouse.sRecordCor);
                            if (win.overviewStorehouse.cvsRecordCor != null)
                            {
                                win.overviewStorehouse.dataGridReadStoreTab3.ItemsSource = win.overviewStorehouse.cvsRecordCor;
                            }


                        break;
                        }
                    }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                diffAmount = 0.0;
            }
            finally
            {
                if (conHelp != null)
                {
                    conHelp.Close();
                }
            }
        }


        private void btnStateOfStorehouseCorrection_Click(object sender, RoutedEventArgs e)
        {

            if (tfCorrectionDelReasonStorehouse.Text.Equals(Constants.tfCorrectionDelReasonStorehouse_INITIALTEXT) == true)
            {
                MessageBox.Show("Morate uneti razlog korekcije/uklanjanja !!!");
                Logger.writeNode(Constants.MESSAGEBOX, "Morate uneti razlog korekcije/uklanjanja !!!");
                return;
            }

            if (tf1.Text.Equals(String.Empty))
            {
                MessageBox.Show("Morate selektovati stavku koju želite korigovati.");
                Logger.writeNode(Constants.MESSAGEBOX, "Morate selektovati stavku koju želite korigovati.");
                return;
            }

            if (datepickerCorrDelStore.Text.Equals(String.Empty))
            {
                MessageBox.Show("Morate uneti datum korigovanja stavke.");
                Logger.writeNode(Constants.MESSAGEBOX, "Morate uneti datum korigovanja stavke.");
                return;
            }

            StorehouseItem si = new StorehouseItem();
            for (int i = 0; i < StorehouseItems.Count; i++)
            {
                if (StorehouseItems.ElementAt(i).ItemCode.Equals(tf1.Text) == true)
                {
                    double d;
                    string tf4WithPoint = tf4.Text.Replace(',', '.');
                    bool isN = Double.TryParse(tf4WithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out d);
                    if (isN)
                    {
                        StorehouseItems.ElementAt(i).ItemRealAmount = d;
                        StorehouseItems.ElementAt(i).ItemPrice = StorehouseItems.ElementAt(i).ItemRealAmount / StorehouseItems.ElementAt(i).ItemforOneAmount * StorehouseItems.ElementAt(i).ItemforOnePrice;
                        cvStorehouseItems = CollectionViewSource.GetDefaultView(StorehouseItems);
                        if (cvStorehouseItems != null)
                        {
                            dgridStateOfStorehouse.ItemsSource = cvStorehouseItems;
                        }
                        si = StorehouseItems.ElementAt(i);
                        tf5.Text = si.ItemPrice.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Količinu niste uneli kao ceo broj !");
                        Logger.writeNode(Constants.MESSAGEBOX, "Količinu niste uneli kao ceo broj !");
                    }
                }


            }

            // update in database
            try
            {
                con.Open();
                string queryStorehouse = "UPDATE storehouse SET RealAmount = " + "'" + si.ItemRealAmount + "'" + " WHERE StoreItemCode =" + "'" + si.ItemCode + "'" + ";";
                com = new OleDbCommand(queryStorehouse, con);
                com.ExecuteNonQuery();
                queryStorehouse = "UPDATE storehouse SET RealPrice = " + "'" + si.ItemPrice + "'" + " WHERE StoreItemCode =" + "'" + si.ItemCode + "'" + ";";
                com = new OleDbCommand(queryStorehouse, con);
                com.ExecuteNonQuery();

                queryStorehouse = "UPDATE storehouse SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE StoreItemCode =" + "'" + si.ItemCode + "'" + ";";
                com = new OleDbCommand(queryStorehouse, con);
                com.ExecuteNonQuery();

                string query = "SELECT NumberOfUpdates FROM storehouse WHERE StoreItemCode = " + "'" + si.ItemCode + "'" + ";";
                com = new OleDbCommand(query, con);
                dr = com.ExecuteReader();
                int oldUpNum = 0;
                while (dr.Read())
                {
                    bool isNum = int.TryParse(dr["NumberOfUpdates"].ToString(), out oldUpNum);
                }


                int upNum = oldUpNum + 1;
                queryStorehouse = "UPDATE storehouse SET NumberOfUpdates = " + "'" + upNum.ToString() + "'" + "WHERE StoreItemCode =" + "'" + si.ItemCode + "'" + ";";
                com = new OleDbCommand(queryStorehouse, con);
                com.ExecuteNonQuery();


                //insert record into table EverCorrectedInStorehouse
                double diffAmount;
                insertintoCorrectionTableForStorehouse(tf1.Text, tf2.Text, tf4.Text, tf5.Text, out diffAmount);


                //update states in storehouse
                //first check storehouse states overview for clearing view if view was changed
                MainWindow win = (MainWindow)Window.GetWindow(this);
                for (DateTime x = _dateCreatedCorrOrDel; x <= win.DateOfLastCreatedBarBook; x = x.AddDays(1))
                {
                    DateTime currDate = x;
                    string currDateString = currDate.ToShortDateString();
                    string endDateOverviewTab5 = _dateCreatedReportEndTab5.ToShortDateString();

                    if (currDateString.Equals(endDateOverviewTab5) == true)
                    {
                        sRecordState.Clear();
                        datepickerStartTab5.Text = String.Empty;
                        datepickerEndTab5.Text = String.Empty;
                        break;
                    }

                }
                //then update all states of storehouse in database
                DateTime currDateNew;
                for (DateTime x = _dateCreatedCorrOrDel; x <= win.DateOfLastCreatedBarBook; x = x.AddDays(1))
                {
                    currDateNew = x;

                    double oldvalue = getOldValueFromStateOfStorehouse(si.ItemCode, currDateNew);


                    double newvalue = oldvalue + diffAmount;

                    if (newvalue > 0)
                    {
                        queryStorehouse = "UPDATE statesStoreOnEndDay SET RealAmount = " + "'" + newvalue + "'" + " WHERE StoreItemCode = " + "'" + si.ItemCode + "'" + " AND StateOfEndDateTime = @Date;";
                        com = new OleDbCommand(queryStorehouse, con);
                        com.Parameters.AddWithValue("@Date", currDateNew);
                        com.ExecuteNonQuery();
                    }
                    else 
                    {
                        string id2 = "57";//Queries.xml ID
                        XDocument xdoc2 = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                        XElement Query2 = (from xml2 in xdoc2.Descendants("Query")
                                           where xml2.Element("ID").Value == id2
                                           select xml2).FirstOrDefault();
                        Console.WriteLine(Query2.ToString());
                        string query2 = Query2.Attribute(Constants.TEXT).Value;
                        com = new OleDbCommand(query2, con);
                        com.Parameters.Add("@StoreItemCode", tf1.Text);
                        com.Parameters.Add("@StateOfEndDateTime", currDateNew);
                        com.ExecuteNonQuery();
 
                    }
                }


                tf1.Text = String.Empty;
                tf2.Text = String.Empty;
                tf3.Text = String.Empty;
                tf4.Text = String.Empty;
                tf5.Text = String.Empty;
                tfCorrectionDelReasonStorehouse.Text = String.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                MainWindow window = (MainWindow)MainWindow.GetWindow(this);
                window.savenumofitemsEVERCreated();
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


        private double getOldValueFromStateOfStorehouse(string code, DateTime date) 
        {
            try
            {
                double oldamount = 0.0;
                string idCount = "58";//Queries.xml ID

                XDocument xdocCount = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement QueryCount = (from xml2 in xdocCount.Descendants("Query")
                                       where xml2.Element("ID").Value == idCount
                                       select xml2).FirstOrDefault();

                string query = QueryCount.Attribute(Constants.TEXT).Value;
                string storeMeasure = String.Empty;


                conHelp.Open();
                com = new OleDbCommand(query, conHelp);
                com.Parameters.Add("@StoreItemCode", code);
                com.Parameters.Add("@StateOfEndDateTime", date);
                dr = com.ExecuteReader();
               
                string realAmount;
                


                while (dr.Read())
                {
                    
                    realAmount = dr["RealAmount"].ToString();
                    double realAmountDouble;
                    string realAmountWithPoint = realAmount.Replace(',', '.');
                    bool isDD = Double.TryParse(realAmountWithPoint, out realAmountDouble);

                    oldamount = realAmountDouble;

                }


                return oldamount;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                
                return 0.0;
            }
            finally
            {
                if (conHelp != null)
                {
                    conHelp.Close();
                }
                if (dr != null)
                {
                    dr.Close();

                }
            }
        }

        private void tf4_TextChanged(object sender, TextChangedEventArgs e)
        {
            StorehouseItem si = new StorehouseItem();
            for (int i = 0; i < StorehouseItems.Count; i++)
            {
                if (StorehouseItems.ElementAt(i).ItemCode.Equals(tf1.Text) == true)
                {
                    double d;
                    string tf4WithPoint = tf4.Text.Replace(',', '.');
                    bool isN = Double.TryParse(tf4WithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out d);
                    if (isN)
                    {
                        StorehouseItems.ElementAt(i).ItemRealAmount = d;
                        StorehouseItems.ElementAt(i).ItemPrice = StorehouseItems.ElementAt(i).ItemRealAmount / StorehouseItems.ElementAt(i).ItemforOneAmount * StorehouseItems.ElementAt(i).ItemforOnePrice;
                        cvStorehouseItems = CollectionViewSource.GetDefaultView(StorehouseItems);
                        if (cvStorehouseItems != null)
                        {
                            dgridStateOfStorehouse.ItemsSource = cvStorehouseItems;
                        }
                        si = StorehouseItems.ElementAt(i);
                        tf5.Text = si.ItemPrice.ToString();
                    }
                    else 
                    {
                        MessageBox.Show("Količinu niste uneli kao ceo broj !");
                        Logger.writeNode(Constants.MESSAGEBOX, "Količinu niste uneli kao ceo broj !");
                    }
                }

             
            }
        }


        private ObservableCollection<StorehouseItemRecord> getStoreRecordFromStore() 
        {
            ObservableCollection<StorehouseItemRecord> result = new ObservableCollection<StorehouseItemRecord>();
            try
            {
                

                string id = "34";//Queries.xml ID
                XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;

                conHelp.Open();
                com = new OleDbCommand(query, conHelp);
                com.Parameters.AddWithValue("@StoreItemCode", tf1.Text);
                dr = com.ExecuteReader();
                string codeProduct = String.Empty;
                string nameProduct = String.Empty;
                string realAmount = String.Empty;
                string realPrice = String.Empty;
                string valuta = String.Empty;
                DateTime createdDateTimeInApp;
                DateTime lastDateTimeUpdatedInApp;
                DateTime userCanControlDateTime;
                DateTime userLastUpdateDateTime;
                string numberOfUpdates = String.Empty;
                string threshold = String.Empty;
                

                // get data from storehouse
                while (dr.Read())
                {
                    codeProduct = dr["StoreItemCode"].ToString();
                    nameProduct = dr["StoreItemName"].ToString();
                    realAmount = dr["RealAmount"].ToString();
                    realPrice = dr["RealPrice"].ToString();
                    valuta = dr["Valuta"].ToString();
                    createdDateTimeInApp = Convert.ToDateTime(dr["CreatedDateTimeInApp"].ToString());
                    lastDateTimeUpdatedInApp = Convert.ToDateTime(dr["LastDateTimeUpdatedInApp"].ToString());
                    userCanControlDateTime = Convert.ToDateTime(dr["UserCanControlDateTime"].ToString());
                    userLastUpdateDateTime = Convert.ToDateTime(dr["userLastUpdateDateTime"].ToString());
                    numberOfUpdates = dr["NumberOfUpdates"].ToString();
                    threshold = dr["Threshold"].ToString();


                    StorehouseItemRecord storeRecord = new StorehouseItemRecord(codeProduct, nameProduct, realAmount, realPrice, valuta, createdDateTimeInApp, lastDateTimeUpdatedInApp, userCanControlDateTime, userLastUpdateDateTime, numberOfUpdates, threshold);

                    result.Add(storeRecord);
                   

                } // end of main while loop
               
                

                    return result;

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
               
                return new ObservableCollection<StorehouseItemRecord>();
            }
            finally
            {
                if (conHelp != null)
                {
                    conHelp.Close();
                }
                if (dr != null)
                {
                    dr.Close();

                }
                if (drInner != null)
                {
                    drInner.Close();
                }
            }
        }


        private void insertRecordInEverDeletedFromStorehouse(StorehouseItemRecord sRecord) 
        {
            try
            {
                MainWindow win = (MainWindow)MainWindow.GetWindow(this);

                string id = "35";//Queries.xml ID

                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;


                conHelp.Open();
                com = new OleDbCommand(query, conHelp);
                com.Parameters.AddWithValue("@StoreItemCode", sRecord.StoreItemCode);
                com.Parameters.AddWithValue("@StoreItemName", sRecord.StoreItemName);
                com.Parameters.AddWithValue("@RealAmount", sRecord.RealAmount);
                com.Parameters.AddWithValue("@RealPrice", sRecord.RealPrice);
                com.Parameters.AddWithValue("@Valuta", win.Currency);
                com.Parameters.AddWithValue("@CreatedDateTimeInApp", DateTime.Now.ToString());
                com.Parameters.AddWithValue("@LastDateTimeUpdatedInApp", DateTime.Now.ToString());
                com.Parameters.AddWithValue("@UserCanControlDateTime", sRecord.UserCanControlDateTime.ToString());
                com.Parameters.AddWithValue("@UserLastUpdateDateTime", sRecord.LastDateTimeUpdatedInApp.ToString());
                com.Parameters.AddWithValue("@NumberOfUpdates", "0");
                com.Parameters.AddWithValue("@Threshold", sRecord.Threshold);
                com.Parameters.AddWithValue("@DeletionUserDateTime",_dateCreatedCorrOrDel.ToShortDateString());
                com.Parameters.AddWithValue("@DeletionReason", deletionCorrectionReasonStorehouse);

                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
            }
            finally
            {
                if (conHelp != null)
                {
                    conHelp.Close();
                }
            }
        }

        private void btnStateOfStorehouseDelete_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);

            if (tfCorrectionDelReasonStorehouse.Text.Equals(Constants.tfCorrectionDelReasonStorehouse_INITIALTEXT) == true)
            {
                MessageBox.Show("Morate uneti razlog korekcije/uklanjanja !!!");
                Logger.writeNode(Constants.MESSAGEBOX, "Morate uneti razlog korekcije/uklanjanja !!!");
                return;
            }

            if (tf1.Text.Equals(String.Empty)) 
            {
                MessageBox.Show("Morate selektovati stavku koju želite ukloniti.");
                Logger.writeNode(Constants.MESSAGEBOX, "Morate selektovati stavku koju želite ukloniti.");
                return;
            }

            if (datepickerCorrDelStore.Text.Equals(String.Empty))
            {
                MessageBox.Show("Morate uneti datum uklanjanja stavke.");
                Logger.writeNode(Constants.MESSAGEBOX, "Morate uneti datum uklanjanja stavke.");
                return;
            }

            StorehouseItem si = new StorehouseItem();
            for (int i = 0; i < StorehouseItems.Count; i++)
            {
                if (StorehouseItems.ElementAt(i).ItemCode.Equals(tf1.Text) == true)
                {
                    si = StorehouseItems.ElementAt(i);
                    Logger.writeNode(Constants.INFORMATION,"Tab3 PodTab4 Rucno uklanjanje stavke iz magacina. Sifra stavke šanka :" + si.ItemCode + ". Naziv stavke šanka :" + si.ItemName + ". Grupa magacinske stavke :" + si.ItemGroup + ". Ukupna kolicina(kg/l) :" + si.ItemRealAmount + ". Ukupna vrednost stavke(din) :" + si.ItemPrice);
                    StorehouseItems.RemoveAt(i);
                    cvStorehouseItems = CollectionViewSource.GetDefaultView(StorehouseItems);
                    if (cvStorehouseItems != null)
                    {
                        dgridStateOfStorehouse.ItemsSource = cvStorehouseItems;
                    }
                }
            }

                // delete from database 
                try
                {
                    con.Open();
                    //update/delete states in storehouse
                    //first check storehouse states overview for clearing view if view was changed
                    MainWindow win = (MainWindow)Window.GetWindow(this);
                    for (DateTime x = _dateCreatedCorrOrDel; x <= win.DateOfLastCreatedBarBook; x = x.AddDays(1))
                    {
                        DateTime currDate = x;
                        string currDateString = currDate.ToShortDateString();
                        string endDateOverviewTab5 = _dateCreatedReportEndTab5.ToShortDateString();

                        if (currDateString.Equals(endDateOverviewTab5) == true)
                        {
                            sRecordState.Clear();
                            datepickerStartTab5.Text = String.Empty;
                            datepickerEndTab5.Text = String.Empty;
                            break;
                        }

                    }
                    //then update/delete all states of storehouse in database
                    DateTime currDateDel;
                    for (DateTime x = _dateCreatedCorrOrDel; x <= win.DateOfLastCreatedBarBook; x = x.AddDays(1))
                    {

                        currDateDel = x;

                        double oldvalue = getOldValueFromStateOfStorehouse(si.ItemCode, currDateDel);

                        double delAmount;
                        string delAmountWithPoint = tf4.Text.Replace(',','.');
                        bool isD = Double.TryParse(delAmountWithPoint, out delAmount);

                        double newvalue = oldvalue - delAmount;

                        if (newvalue > 0)
                        {
                            string queryStorehouse = "UPDATE statesStoreOnEndDay SET RealAmount = " + "'" + newvalue + "'" + " WHERE StoreItemCode = " + "'" + si.ItemCode + "'" + " AND StateOfEndDateTime = @Date;";
                            com = new OleDbCommand(queryStorehouse, con);
                            com.Parameters.AddWithValue("@Date", currDateDel);
                            com.ExecuteNonQuery();
                        }
                        else
                        {
                            string id2 = "57";//Queries.xml ID
                            XDocument xdoc2 = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                            XElement Query2 = (from xml2 in xdoc2.Descendants("Query")
                                               where xml2.Element("ID").Value == id2
                                               select xml2).FirstOrDefault();
                            Console.WriteLine(Query2.ToString());
                            string query2 = Query2.Attribute(Constants.TEXT).Value;
                            com = new OleDbCommand(query2, con);
                            com.Parameters.Add("@StoreItemCode", tf1.Text);
                            com.Parameters.Add("@StateOfEndDateTime", currDateDel);
                            com.ExecuteNonQuery();

                        }
                    }




                    string id = "19";//Queries.xml ID

                    XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement Query = (from xml2 in xdoc.Descendants("Query")
                                      where xml2.Element("ID").Value == id
                                      select xml2).FirstOrDefault();
                    Console.WriteLine(Query.ToString());
                    string query = Query.Attribute(Constants.TEXT).Value;
                    query = query + "'" + si.ItemCode + "'" + ";";

                   
                    com = new OleDbCommand(query, con);
                    com.ExecuteNonQuery();

                    //select record code for deletion from table EverEnterInStorehouse
                    ObservableCollection<StorehouseItemRecord> storeRecordforDelete = getStoreRecordFromStore();


                    //update NumofUpdates on 1 in table EverEnterInStorehouse
                    query = "UPDATE EverEnterInStorehouse SET LastDateTimeUpdatedInApp = " + "'" + DateTime.Now.ToString() + "'" + "  WHERE StoreItemCode =" + "'" + storeRecordforDelete.ElementAt(0).StoreItemCode + "'" + ";";
                    com = new OleDbCommand(query, con);
                    com.ExecuteNonQuery();

                    query = "SELECT NumberOfUpdates FROM EverEnterInStorehouse WHERE StoreItemCode = " + "'" + tf1.Text + "'" + ";";
                    com = new OleDbCommand(query, con);
                    dr = com.ExecuteReader();
                    int oldUpNum = 0;
                    while (dr.Read())
                    {
                        bool isNum = int.TryParse(dr["NumberOfUpdates"].ToString(), out oldUpNum);
                    }


                    int upNum = oldUpNum + 1;
                    query = "UPDATE EverEnterInStorehouse SET NumberOfUpdates = " + "'" + upNum.ToString() + "'" + "WHERE StoreItemCode =" + "'" + tf1.Text + "'" + ";";
                    com = new OleDbCommand(query, con);
                    com.ExecuteNonQuery();


                    //write selected records in table 
                    for (int i = 0; i < storeRecordforDelete.Count; i++)
                    {
                        storeRecordforDelete.ElementAt(i).LastDateTimeUpdatedInApp = DateTime.Now.ToString();
                        insertRecordInEverDeletedFromStorehouse(storeRecordforDelete.ElementAt(i)); 
                    }

                    // set of num updates 1 sRecord collection
                    
                    StorehouseItemRecord sR = new StorehouseItemRecord();
                    for (int i = 0; i < window.overviewStorehouse.sRecord.Count; i++)
                    {
                        if (window.overviewStorehouse.sRecord.ElementAt(i).StoreItemCode.Equals(tf1.Text))
                        {
                            sR = window.overviewStorehouse.sRecord.ElementAt(i);
                            window.overviewStorehouse.sRecord.ElementAt(i).NumberOfUpdates = "1";
                            break;
                        }
                    }

                    if (window.overviewStorehouse.sRecord.Count != 0)
                    {
                        for (DateTime x = window.overviewStorehouse.DateCreatedReportStartTab2; x <= window.overviewStorehouse.DateCreatedReportEndTab2; x = x.AddDays(1))
                        {
                            string dateCurrStr = x.ToString().Replace("0:00:00", "");
                            dateCurrStr = dateCurrStr.Substring(0, dateCurrStr.Length - 1);
                            StorehouseItemRecordDel srDel = new StorehouseItemRecordDel(sR, tfCorrectionDelReasonStorehouse.Text);
                            if (dateCurrStr.Equals(srDel.UserCanControlDateTime.ToString()) == true)
                            {


                                srDel.NumberOfUpdates = "1";
                                window.overviewStorehouse.sRecordDel.Add(srDel);
                                window.overviewStorehouse.cvsRecordDel = CollectionViewSource.GetDefaultView(window.overviewStorehouse.sRecordDel);
                                if (window.overviewStorehouse.cvsRecordDel != null)
                                {
                                    window.overviewStorehouse.dataGridReadStoreTab2.ItemsSource = window.overviewStorehouse.cvsRecordDel;
                                }

                                break;
                            }
                        }
                    }




                    


                    tf1.Text = String.Empty;
                    tf2.Text = String.Empty;
                    tf3.Text = String.Empty;
                    tf4.Text = String.Empty;
                    tf5.Text = String.Empty;
                    tfCorrectionDelReasonStorehouse.Text = String.Empty;


                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Logger.writeNode(Constants.EXCEPTION, ex.Message);
                    MainWindow window2 = (MainWindow)MainWindow.GetWindow(this);
                    window2.savenumofitemsEVERCreated();

                }
                finally
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                }
            
        }



        #region Tab5

        private DateTime _dateCreatedReportStartTab5, _dateCreatedReportEndTab5;
        private ObservableCollection<StateOfStorehouseItem> sRecordState = new ObservableCollection<StateOfStorehouseItem>();
        public ICollectionView cvsRecordState;

        private void datepickerStartTab5_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
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

                    _dateCreatedReportStartTab5 = date.Value;
                    Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab5 Postavljanje pocetnog datuma. Postavljeni datum je :" + _dateCreatedReportStartTab5.ToString());

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("You did not enter date in tab1!!!");
                Logger.writeNode(Constants.EXCEPTION, "You did not enter date in tab1!!!");
            }
        }



        private void datepickerEndTab5_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
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

                    _dateCreatedReportEndTab5 = date.Value;
                    Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab5 Postavljanje krajnjeg datuma. Postavljeni datum je :" + _dateCreatedReportEndTab5.ToString());

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("You did not enter date in tab1!!!");
                Logger.writeNode(Constants.EXCEPTION, "You did not enter date in tab1!!!");
            }
        }



        private void btnloadReportStateStore_Click(object sender, RoutedEventArgs e)
        {
            if (datepickerStartTab5.Text.Equals(String.Empty) || datepickerEndTab5.Text.Equals(String.Empty))
            {
                System.Windows.Forms.MessageBox.Show("Morate uneti vremenski interval koji želite ucitati!");
                Logger.writeNode(Constants.MESSAGEBOX, "Morate uneti vremenski interval koji želite ucitati!");

                return;
            }
            else
            {
                try
                {
                    if (sRecordState.Count > 0) sRecordState.Clear();

                    Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab5 Ucitavanje izvestaja stanja magacina");

                    string id = "56";//Queries.xml ID
                    XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                      where xml2.Element("ID").Value == id
                                      select xml2).FirstOrDefault();
                    Console.WriteLine(Query.ToString());
                    string query = Query.Attribute(Constants.TEXT).Value;

                    con.Open();
                    com = new OleDbCommand(query, con);
                    com.Parameters.AddWithValue("@StartDate", _dateCreatedReportStartTab5);
                    com.Parameters.AddWithValue("@EndDate", _dateCreatedReportEndTab5);
                    dr = com.ExecuteReader();


                    string StoreItemCode = String.Empty;
                    string StoreItemName = String.Empty;
                    string RealAmount = String.Empty;
                    string StoreItemGroup = String.Empty;
                    string StateOfEndDateTime = String.Empty;
                    

                    string DateCreatedReport = String.Empty;


                    while (dr.Read())
                    {

                        StoreItemCode = dr["StoreItemCode"].ToString();
                        StoreItemName = dr["StoreItemName"].ToString();
                        StoreItemGroup = dr["StoreItemGroup"].ToString();
                        RealAmount = dr["RealAmount"].ToString();
                        
                        StateOfEndDateTime = dr["StateOfEndDateTime"].ToString();
                        DateTime stateOfEndDateTime = DateTime.Parse(StateOfEndDateTime);
                        


                        StateOfStorehouseItem sRState = new StateOfStorehouseItem(StoreItemCode, StoreItemName, StoreItemGroup, RealAmount, stateOfEndDateTime);
                        sRecordState.Add(sRState);
                    }

                    cvsRecordState = CollectionViewSource.GetDefaultView(sRecordState);
                    if (cvsRecordState != null)
                    {
                        dataGridReadStateStorehouse.ItemsSource = cvsRecordState;
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



        #region filteringTab5

        private bool filteredDgridUsedTab5;

        private void filterDataTab5()
        {

            tblFilterStatusTab5.Text = Constants.FILTERON;
            tblFilterStatusTab5.Background = Brushes.Orange;

            this.cvsRecordState.Filter = item =>
            {
                var vitem = item as StateOfStorehouseItem;
                if (vitem == null) return false;
                string searchText = tfFilterTab5.Text.ToUpper();

                if (cmbFilterColumnTab5.SelectedIndex == 1)
                {

                    string codeOfProduct = vitem.StoreItemCode.ToUpper();
                    if (codeOfProduct.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnTab5.SelectedIndex == 2)
                {
                    string storeName = vitem.StoreItemName.ToUpper();
                    if (storeName.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else 
                {

                    string group = vitem.StoreItemGroup.ToUpper();
                    if (group.Equals(searchText) == true) { return true; }
                    else { return false; }
                }
               


            };

        }


        private void unFilteredDataTab5()
        {
            tblFilterStatusTab5.Text = String.Empty;
            tblFilterStatusTab5.Background = Brushes.White;

            this.cvsRecordState.Filter = item =>
            {

                var vitem = item as StateOfStorehouseItem;
                if (vitem == null) return false;
                else return true;

            };
        }



        private void tfFilterTab5_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfFilterTab5.Text.Equals(String.Empty) == false)
            {
                Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab5 Filtriranje reci. Broj kolone koja se filtrira " + cmbFilterColumnTab5.SelectedIndex.ToString() + ". Filtrirana rec :" + tfFilterTab5.Text);
                filterDataTab5();
                filteredDgridUsedTab5 = true;
            }
            else
            {
                unFilteredDataTab5();
                filteredDgridUsedTab5 = false;
                tblFilterStatusTab5.Text = String.Empty;
                MainWindow win = (MainWindow)Window.GetWindow(this);
                if (win.options.chkbMask.IsChecked == true)
                {
                    Object obj1 = this.Resources["Gradient4"];
                    tblFilterStatusTab5.Background = (Brush)obj1;
                }
                else
                {
                    tblFilterStatusTab5.Background = Brushes.White;
                }
            }
        }

        private void tfFilterTab5_MouseEnter(object sender, MouseEventArgs e)
        {
            if (cmbFilterColumnTab5.SelectedIndex == 0)
            {
                tfFilterTab5.IsReadOnly = true;

                tblFilterStatusTab5.Text = Constants.FILTER_COLUMN;
                tblFilterStatusTab5.Foreground = Brushes.White;
                tblFilterStatusTab5.Background = Brushes.Red;
            }
            else
            {
                tfFilterTab5.IsReadOnly = false;
            }
        }

        private void tfFilterTab5_MouseLeave(object sender, MouseEventArgs e)
        {
            if (filteredDgridUsedTab5 == false)
            {
                tblFilterStatusTab5.Text = String.Empty;
                MainWindow win = (MainWindow)Window.GetWindow(this);
                if (win.options.chkbMask.IsChecked == true)
                {
                    Object obj1 = this.Resources["Gradient4"];
                    tblFilterStatusTab5.Background = (Brush)obj1;
                }
                else
                {
                    tblFilterStatusTab5.Background = Brushes.White;
                }
            }
        }



        #endregion

        private void btnReturnOneDay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();

                //enable dugme za unos u sank
                if (cmbSGroup.SelectedIndex > 0)
                {
                    btnEnter.IsEnabled = true;
                }


                //newlastenteredDay = _dateCreatedLastBarBook + 1 or newlastenteredDay = _dateCreatedLastBarBook(unchanged)
                MainWindow window = (MainWindow)Window.GetWindow(this);

                //first remove storehouse datepicker1 one day back
                DateTime returnedDay = datepicker1.SelectedDate.Value;
                DateTime newlastenteredDay = returnedDay.AddDays(-1);
                datepicker1.SelectedDate = newlastenteredDay;


                //then set current storehouse at yesterday (_dateCreatedLastBarBook -1 )(table storehouse)
                DateTime dateForStorehouse = window.DateOfLastCreatedBarBook.AddDays(-1);
                //first get real amounts from statesStoreOnEndDay
                if (sRecordState.Count > 0) sRecordState.Clear();

              
                string id = "56";//Queries.xml ID
                XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;

              
                com = new OleDbCommand(query, con);
                com.Parameters.AddWithValue("@StartDate", dateForStorehouse);
                com.Parameters.AddWithValue("@EndDate", dateForStorehouse);
                dr = com.ExecuteReader();


                string StoreItemCode = String.Empty;
                string StoreItemName = String.Empty;
                string RealAmount = String.Empty;
                string StoreItemGroup = String.Empty;
                string StateOfEndDateTime = String.Empty;


                string DateCreatedReport = String.Empty;


                while (dr.Read())
                {

                    StoreItemCode = dr["StoreItemCode"].ToString();
                    StoreItemName = dr["StoreItemName"].ToString();
                    StoreItemGroup = dr["StoreItemGroup"].ToString();
                    RealAmount = dr["RealAmount"].ToString();

                    StateOfEndDateTime = dr["StateOfEndDateTime"].ToString();
                    DateTime stateOfEndDateTime = DateTime.Parse(StateOfEndDateTime);



                    StateOfStorehouseItem sRState = new StateOfStorehouseItem(StoreItemCode, StoreItemName, StoreItemGroup, RealAmount, stateOfEndDateTime);
                    sRecordState.Add(sRState);
                }
                //then set current storehouse at yesterday (_dateCreatedLastBarBook -1 )(table storehouse)
                for (int i = 0; i < sRecordState.Count; i++)
                {
                    string queryUp = "UPDATE storehouse SET RealAmount = '" + sRecordState.ElementAt(i).RealAmount + "' WHERE StoreItemCode = '" + sRecordState.ElementAt(i).StoreItemCode + "';";
                    com = new OleDbCommand(queryUp, con);
                    com.ExecuteNonQuery();
                }
                //refresh storehouse collection
                for (int i = 0; i < StorehouseItems.Count; i++)
                {
                    for (int j = 0; j < sRecordState.Count; j++)
                    {
                        if (sRecordState.ElementAt(j).StoreItemCode.Equals(StorehouseItems.ElementAt(i).ItemCode) == true)
                        {
                            double realAm;
                            bool isN = Double.TryParse(sRecordState.ElementAt(j).RealAmount, out realAm);
                            StorehouseItems.ElementAt(i).ItemRealAmount = realAm;
                            StorehouseItems.ElementAt(i).ItemPrice = StorehouseItems.ElementAt(i).ItemRealAmount / StorehouseItems.ElementAt(i).ItemforOneAmount * StorehouseItems.ElementAt(i).ItemforOnePrice;
                        }
                    }
                }


                cvStorehouseItems = CollectionViewSource.GetDefaultView(StorehouseItems);
                if (cvStorehouseItems != null)
                {
                    dgridStateOfStorehouse.ItemsSource = cvStorehouseItems;
                }


                string queryDel;
                //delete from table statesStoreOnEndDay for date _dateCreatedLastBarBook
                
                 id = "63";//Queries.xml ID
                 XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                 Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                

                 query = Query.Attribute(Constants.TEXT).Value;

                com = new OleDbCommand(query, con);
                com.Parameters.Add("@StateOfEndDateTime", window.DateOfLastCreatedBarBook);
                com.ExecuteNonQuery();



                //delete from table EverEnterInStorehouse for date _dateCreatedLastBarBook
                id = "64";//Queries.xml ID
                xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                Query = (from xml2 in xdoc.Descendants("Query")
                         where xml2.Element("ID").Value == id
                         select xml2).FirstOrDefault();


                query = Query.Attribute(Constants.TEXT).Value;

                com = new OleDbCommand(query, con);
                com.Parameters.Add("@UserCanControlDateTime", window.DateOfLastCreatedBarBook);
                com.ExecuteNonQuery();



                //delete from table EverDeletedFromStorehouse for date _dateCreatedLastBarBook
                id = "65";//Queries.xml ID
                xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                Query = (from xml2 in xdoc.Descendants("Query")
                         where xml2.Element("ID").Value == id
                         select xml2).FirstOrDefault();


                query = Query.Attribute(Constants.TEXT).Value;

                com = new OleDbCommand(query, con);
                com.Parameters.Add("@UserCanControlDateTime", window.DateOfLastCreatedBarBook);
                com.ExecuteNonQuery();

                //delete from table EverCorrectedInStorehouse for date _dateCreatedLastBarBook
                id = "66";//Queries.xml ID
                xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                Query = (from xml2 in xdoc.Descendants("Query")
                         where xml2.Element("ID").Value == id
                         select xml2).FirstOrDefault();


                query = Query.Attribute(Constants.TEXT).Value;

                com = new OleDbCommand(query, con);
                com.Parameters.Add("@CorrectionUserDateTime", window.DateOfLastCreatedBarBook);
                com.ExecuteNonQuery();




                //delete from table allItemsSoldEver for date _dateCreatedLastBarBook
                id = "67";//Queries.xml ID
                xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                Query = (from xml2 in xdoc.Descendants("Query")
                         where xml2.Element("ID").Value == id
                         select xml2).FirstOrDefault();


                query = Query.Attribute(Constants.TEXT).Value;

                com = new OleDbCommand(query, con);
                com.Parameters.Add("@DateCreatedReport", window.DateOfLastCreatedBarBook);
                com.ExecuteNonQuery();



                //delete from table allItemsDeletedEver for date _dateCreatedLastBarBook
                id = "68";//Queries.xml ID
                xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                Query = (from xml2 in xdoc.Descendants("Query")
                         where xml2.Element("ID").Value == id
                         select xml2).FirstOrDefault();


                query = Query.Attribute(Constants.TEXT).Value;

                com = new OleDbCommand(query, con);
                com.Parameters.Add("@DateCreatedReport", window.DateOfLastCreatedBarBook);
                com.ExecuteNonQuery();

                //delete from table allItemsCorrectedEver for date _dateCreatedLastBarBook
                id = "69";//Queries.xml ID
                xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                Query = (from xml2 in xdoc.Descendants("Query")
                         where xml2.Element("ID").Value == id
                         select xml2).FirstOrDefault();


                query = Query.Attribute(Constants.TEXT).Value;

                com = new OleDbCommand(query, con);
                com.Parameters.Add("@DateCreatedReport", window.DateOfLastCreatedBarBook);
                com.ExecuteNonQuery();





                //delete from table HistoryItemsOutput for date _dateCreatedLastBarBook [DATABASE_HISTORY]
                conHistory.Open();
                id = "70";//Queries.xml ID
                xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                Query = (from xml2 in xdoc.Descendants("Query")
                         where xml2.Element("ID").Value == id
                         select xml2).FirstOrDefault();


                query = Query.Attribute(Constants.TEXT).Value;

                com = new OleDbCommand(query, conHistory);
                com.Parameters.Add("@DateReportCreated", window.DateOfLastCreatedBarBook);
                com.ExecuteNonQuery();


                //first tab datepicker decrement
                DateTime returnedDayTab1 = window.datepicker1.SelectedDate.Value;
                DateTime newlastenteredDayTab1 = returnedDayTab1.AddDays(-1);
                window.datepicker1.SelectedDate = newlastenteredDayTab1;


                // decrement _dateCreatedLastBarBook and update in table savedOptions
                window.DateOfLastCreatedBarBook = window.DateOfLastCreatedBarBook.AddDays(-1);
                string queryUp2 = "UPDATE savedOptions SET DateOfLastCreatedBarBook = '" + window.DateOfLastCreatedBarBook + "' WHERE Options = 'options';";
                com = new OleDbCommand(queryUp2, con);
                com.ExecuteNonQuery();


                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                if (con != null)
                {
                    con.Close();
                }

                if (conHistory != null)
                {
                    conHistory.Close();
                }

                if (dr != null)
                {
                    dr.Close();
                }
            }
        }

        

        #endregion


       
       













    }
}
