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


namespace caffeKambodzaApplication
{
    /// <summary>
    /// Interaction logic for StorehouseItemsTab2.xaml
    /// </summary>
    public partial class StorehouseItemsTab2 : UserControl
    {
        private OleDbConnection conStore = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
        private OleDbCommand com;
        private OleDbDataReader dr;
        private string _currStore = "din";
        private bool _storeCodeExist = false;
        private string _lastEnteredCode = String.Empty;
        private Product p;
        private StoreItemProduct sitem;
       

        public ObservableCollection<StoreItemProduct> StoreItemProducts = new ObservableCollection<StoreItemProduct>();
        private ObservableCollection<StoreItemProduct> _currStoreItemProducts = new ObservableCollection<StoreItemProduct>();
        public ObservableCollection<string> StoreItemCodes = new ObservableCollection<string>();
        public ObservableCollection<string> StoreItems = new ObservableCollection<string>();
        public ObservableCollection<string> StoreItemsMeasures = new ObservableCollection<string>();
        public ObservableCollection<ObservableCollection<StoreItemProduct>> StoreItemsByGroup = new ObservableCollection<ObservableCollection<StoreItemProduct>>();
        private int numGroup = 0;
        public ObservableCollection<string> GroupsItemsInStore = new ObservableCollection<string>();

        private void initialComboboxs()
        {
            try
            {
                string id = "7";//Queries.xml ID
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
                string storeItemMeasure = String.Empty;
                int price = -1;
                string storeGroup = String.Empty;
                string isUsed = String.Empty;
                bool isUsedBool;
                string amount = String.Empty;
                double amountDouble;
                string threshold = String.Empty;
                double thresholdDouble = 0.0;

                StoreItems.Add(Constants.CHOOSEPRODUCT_STORE);
                StoreItemCodes.Add(Constants.CHOOSECODE_STORE);
                while (dr.Read())
                {
                    codeProduct = dr["StoreItemCode"].ToString();
                    kindOfProduct = dr["StoreItemName"].ToString();
                    storeItemMeasure = dr["StoreItemMeasure"].ToString();
                    int n;
                    bool isNumeric = int.TryParse(dr["StoreItemPrice"].ToString(), out n);
                    if (isNumeric) { price = Convert.ToInt32(dr["StoreItemPrice"].ToString()); }
                    storeGroup = dr["StoreItemGroup"].ToString();
                    isUsed = dr["isUsed"].ToString();
                    if (isUsed.Equals(Constants.YES)) isUsedBool = true;
                    else isUsedBool = false;
                    amount = dr["Amount"].ToString();
                    string amountWithPoint = dr["Amount"].ToString().Replace(',', '.');
                    bool isNum = Double.TryParse(amountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out amountDouble);
                    threshold = dr["Threshold"].ToString();
                    string thresholdWithPoint = dr["Threshold"].ToString().Replace(',', '.');
                    bool isNumm = Double.TryParse(thresholdWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out thresholdDouble);

                    StoreItemProduct storeProduct = new StoreItemProduct(codeProduct, kindOfProduct,storeItemMeasure, price, storeGroup, isUsedBool, amountDouble, thresholdDouble);
                    StoreItemProducts.Add(storeProduct);
                    StoreItems.Add(storeProduct.ComboBoxForm());
                    StoreItemCodes.Add(storeProduct.Code());




                     if (numGroup == 0)
                    {
                        GroupsItemsInStore.Add("Izaberite grupu stavki šanka ");
                        ObservableCollection<StoreItemProduct> spListInitial = new ObservableCollection<StoreItemProduct>();
                        StoreItemsByGroup.Add(spListInitial);
                        GroupsItemsInStore.Add(storeProduct.Group);
                        numGroup++;
                        ObservableCollection<StoreItemProduct> spList = new ObservableCollection<StoreItemProduct>();
                        StoreItemsByGroup.Add(spList);
                    }
                    else
                    {
                        int g;
                        for ( g = 0; g < GroupsItemsInStore.Count; g++) 
                        {
                            if (GroupsItemsInStore.ElementAt(g).Equals(storeProduct.Group) == true) 
                            {
                                break;
                            }
                        }
                        if (g == GroupsItemsInStore.Count) 
                        {
                            GroupsItemsInStore.Add(storeProduct.Group);
                            numGroup++;
                            ObservableCollection<StoreItemProduct> spList = new ObservableCollection<StoreItemProduct>();
                            StoreItemsByGroup.Add(spList);
                        }


                    }
                }
                //cmbChooseStoreItem2.ItemsSource = StoreItems;
                //cmbRemoveStoreItem.ItemsSource = StoreItems;


                //cmbChooseStoreItem2.SelectedIndex = 0;
                //cmbRemoveStoreItem.SelectedIndex = 0;

                cmbStoreItemCode.ItemsSource = StoreItemCodes;
                cmbStoreItemCode.SelectedIndex = 0;

                cmbRemoveStoreItemGroup.ItemsSource = GroupsItemsInStore;
                cmbRemoveStoreItemGroup.SelectedIndex = 0;


                if (StoreItemsByGroup.Count == 0)
                {
                    GroupsItemsInStore.Add("Izaberite grupu stavki šanka ");
                    ObservableCollection<StoreItemProduct> spListInitial = new ObservableCollection<StoreItemProduct>();
                    StoreItemsByGroup.Add(spListInitial);
                   
                    numGroup++;
                    ObservableCollection<StoreItemProduct> spList = new ObservableCollection<StoreItemProduct>();
                    StoreItemsByGroup.Add(spList);
                }


                StoreItemProduct sInitial = new StoreItemProduct("Izaberite stavku šanka");
                StoreItemsByGroup.ElementAt(0).Add(sInitial);

                if (StoreItemProducts != null)
                {

                    for (int i = 0; i < StoreItemProducts.Count; i++)
                    {
                        StoreItemProduct sitem = StoreItemProducts.ElementAt(i);
                        for (int j = 1; j < GroupsItemsInStore.Count; j++)
                        {

                            if (GroupsItemsInStore.ElementAt(j).Equals(sitem.Group) == true)
                            {
                                StoreItemsByGroup.ElementAt(j).Add(sitem);
                            }
                        }
                    }

                }
                cmbChooseStoreItemGroup.ItemsSource = GroupsItemsInStore;
                cmbChooseStoreItemGroup.SelectedIndex = 0;
                cmbChooseStoreItem2.ItemsSource = StoreItemsByGroup.ElementAt(0);
                cmbChooseStoreItem2.SelectedIndex = 0;
                cmbRemoveStoreItem.ItemsSource = StoreItemsByGroup.ElementAt(0);
                cmbRemoveStoreItem.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
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
            }


        }


        private void initialStoreItemMeasures() 
        {
            try
            {
                conStore.Close();
                string idCount = "52";//Queries.xml ID

                XDocument xdocCount = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement QueryCount = (from xml2 in xdocCount.Descendants("Query")
                                       where xml2.Element("ID").Value == idCount
                                       select xml2).FirstOrDefault();

                string query = QueryCount.Attribute(Constants.TEXT).Value;
                string storeMeasure = String.Empty;


                conStore.Open();
                com = new OleDbCommand(query, conStore);
                dr = com.ExecuteReader();

                StoreItemsMeasures.Add(Constants.CHOOSEMEASURE_STORE);

                while (dr.Read())
                {
                    storeMeasure = dr["MeasureName"].ToString();
                    StoreItemsMeasures.Add(storeMeasure);
                }

                cmbStoreItemMeasureRemove.ItemsSource = StoreItemsMeasures;
                cmbStoreItemMeasureRemove.SelectedIndex = 0;
                cmbStoreItemMeasure.ItemsSource = StoreItemsMeasures;
                cmbStoreItemMeasure.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
            }
            finally
            {
                if (conStore != null)
                {
                    conStore.Close();
                }
            }
        }
         
        public StorehouseItemsTab2()
        {
            InitializeComponent();
            initialComboboxs();
            initialStoreItemMeasures();
            cmbChooseStoreItem2.SelectedIndex = 0;
            cmbRemoveStoreItem.SelectedIndex = 0;
            cmbStoreItemCode.SelectedIndex = 0;
            cmbChooseStoreItem2.IsEnabled = false;
            cmbMeasureSI.SelectedIndex = 0;
            cmbMeasureSI2.SelectedIndex = 0;

            btnRemoveNewStoreItemMeasure.IsEnabled = false;

            btnRemoveStoreItem.IsEnabled = false;
            btnAddStoreItemDown.IsEnabled = false;
            dgridCurrProductStoreItemConn.Visibility = Visibility.Hidden;
        }

    
        #region Part2

        private void tfNewStoreItemCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfNewStoreItemCode.Text.Equals(String.Empty) == false)
            {
                tblRemark.Text = String.Empty;
                StoreItemProduct storeProduct;
                for (int i = 0; i < StoreItemProducts.Count; i++)
                {
                    if (StoreItemProducts.ElementAt(i).CodeProduct.Equals(tfNewStoreItemCode.Text) == true)
                    {
                        storeProduct = StoreItemProducts.ElementAt(i);
                        tblJustEnteredProductCodeInformationPart2.Text = storeProduct.KindOfProduct;
                        tblJustEnteredProductPriceInformationPart2.Text = storeProduct.Price.ToString() + " " + _currStore;
                        tfNewStoreItemCode.Foreground = Brushes.Red;
                        _storeCodeExist = true;
                        return;
                    }
                }
                tfNewStoreItemCode.Foreground = Brushes.Black;
                _storeCodeExist = false;
                tblJustEnteredProductCodeInformationPart2.Text = Constants.NOTCHOOSEDPRODUCT_STORE;
                tblJustEnteredProductPriceInformationPart2.Text = Constants.NOTCHOOSEDPRODUCT_STORE;
                tfNewStoreItem.IsReadOnly = false;
            }
        }


       

        private void tfNewStoreItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (tfNewStoreItemCode.Text.Equals(String.Empty))
            {
                tblRemark.Text = Constants.MUSTENTERSTOREITEMCODE;
                tblRemark.Foreground = Brushes.Red;
                tfNewStoreItem.IsReadOnly = true;
            }
            else if (_storeCodeExist)
            {
                tblRemark.Text = Constants.STORECODEEXIST;
                tblRemark.Foreground = Brushes.Red;
                tfNewStoreItem.IsReadOnly = true;
            }
        }

        private void tfNewStoreItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if (tfNewStoreItemCode.Text.Equals(String.Empty))
            {
                tblRemark.Foreground = Brushes.Black;
            }
        }

        private void tfNewStoreItem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfNewStoreItem.Text.Equals(String.Empty) == false)
            {
                tblRemark.Text = String.Empty;
                tfNewStoreItemGroup.IsReadOnly = false;
            }
        }


        private void cmbStoreItemMeasure_MouseEnter(object sender, MouseEventArgs e)
        {
            if (tfNewStoreItem.Text.Equals(String.Empty))
            {
                tblRemark.Text = Constants.MUSTENTERSTOREITEMNAME;
                tblRemark.Foreground = Brushes.Red;
                tfNewStoreItemGroup.IsReadOnly = true;
            }
        }

        private void tfNewStoreItemGroup_MouseEnter(object sender, MouseEventArgs e)
        {
            if (tfNewStoreItem.Text.Equals(String.Empty))
            {
                tblRemark.Text = Constants.MUSTENTERSTOREITEMMEASURE;
                tblRemark.Foreground = Brushes.Red;
                tfNewStoreItemGroup.IsReadOnly = true;
            }
        }

        private void tfNewStoreItemGroup_MouseLeave(object sender, MouseEventArgs e)
        {
            if (tfNewStoreItem.Text.Equals(String.Empty))
            {
                tblRemark.Foreground = Brushes.Black;
            }
        }

        private void tfNewStoreItemGroup_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfNewStoreItemGroup.Text.Equals(String.Empty) == false)
            {
                tfNewStoreItemPrice.IsReadOnly = false;
            }
        }



        private void tfNewStoreItemPrice_MouseEnter(object sender, MouseEventArgs e)
        {
            if (tfNewStoreItemGroup.Text.Equals(String.Empty))
            {
                tblRemark.Text = Constants.MUSTENTERSTOREITEMGROUP;
                tblRemark.Foreground = Brushes.Red;
                tfNewStoreItemPrice.IsReadOnly = true;
            }
        }

        private void tfNewStoreItemPrice_MouseLeave(object sender, MouseEventArgs e)
        {
            if (tfNewStoreItemGroup.Text.Equals(String.Empty))
            {
                tblRemark.Foreground = Brushes.Black;
            }
        }

        private void tfNewStoreItemPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfNewStoreItemPrice.Text.Equals(String.Empty) == false) 
            {
                tfStoreAmount.IsReadOnly = false;
            }
        }

        private void tfStoreAmount_MouseEnter(object sender, MouseEventArgs e)
        {
            if (tfNewStoreItemPrice.Text.Equals(String.Empty))
            {
                tfStoreAmount.IsReadOnly = true;
                tblRemark.Text = Constants.MUSTENTERSTOREITEMPRICE;
                tblRemark.Foreground = Brushes.Red;
            }
        }


        private void tfStoreAmount_MouseLeave(object sender, MouseEventArgs e)
        {
            tblRemark.Foreground = Brushes.Black;
        }

        private void tfStoreAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfStoreAmount.Text.Equals(String.Empty) == false)
            {
                double d;
                double amount;
                string tfStoreAmountWithPoint = tfStoreAmount.Text.Replace(',', '.');
                bool isNumeric = Double.TryParse(tfStoreAmountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out amount);
                if (isNumeric == false)
                {
                    MessageBox.Show("Količina nije uneta kao broj!!!");
                    Logger.writeNode(Constants.MESSAGEBOX, "Količina nije uneta kao broj!!!");
                    return;
                }
            }
        }

        private void btnNewStoreItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)MainWindow.GetWindow(this);
            window.storehouse.cmbSGroup.SelectedIndex = 0;
            if (tfNewStoreItemPrice.Text.Equals(String.Empty))
            {
                tblRemark.Text = Constants.MUSTENTERSOREITEM_AMOUNT;
                tfNewStoreItemCode.Foreground = Brushes.Red;
                tfNewStoreItem.Foreground = Brushes.Red;
                tfNewStoreItemPrice.Foreground = Brushes.Red;
                tfNewStoreItemGroup.Foreground = Brushes.Red;
                tfStoreAmount.Foreground = Brushes.Red;
                return;
            }
            else
            {
                 tblRemark.Text = String.Empty;
                 tfNewStoreItemCode.Foreground = Brushes.Black;
                 tfNewStoreItem.Foreground = Brushes.Black;
                 tfNewStoreItemPrice.Foreground = Brushes.Black;
                 tfNewStoreItemGroup.Foreground = Brushes.Black;
                 tfStoreAmount.Foreground = Brushes.Black;
            }


            if (_lastEnteredCode.Equals(tfNewStoreItemCode.Text))
            {
                tblRemark.Text = Constants.STORECODEEXIST2;
                tfNewStoreItemCode.Foreground = Brushes.Red;
                tfNewStoreItem.Foreground = Brushes.Red;
                tfNewStoreItemPrice.Foreground = Brushes.Red;
                tfNewStoreItemGroup.Foreground = Brushes.Red;
                tfStoreAmount.Foreground = Brushes.Red;
                return;
            }
            else
            {
                _lastEnteredCode = tfNewStoreItemCode.Text;
                tfNewStoreItemCode.Foreground = Brushes.Black;
                tfNewStoreItem.Foreground = Brushes.Black;
                tfNewStoreItemPrice.Foreground = Brushes.Black;
                tfNewStoreItemGroup.Foreground = Brushes.Black;
                tfStoreAmount.Foreground = Brushes.Black;
            }
            try
            {
                string newStoreItemCode = String.Empty;
                newStoreItemCode = tfNewStoreItemCode.Text;
                string newStoreItem = String.Empty;
                newStoreItem = tfNewStoreItem.Text;
                string newStoreItemGroup = String.Empty;
                newStoreItemGroup = tfNewStoreItemGroup.Text;
                int newStoreItemPrice = -1;
                newStoreItemPrice = Convert.ToInt32(tfNewStoreItemPrice.Text);
                double amount;
                string tfStoreAmountWithPoint = tfStoreAmount.Text.Replace(',', '.');
                bool isNum = Double.TryParse(tfStoreAmountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out amount);


                if (cmbMeasureSI2.SelectedIndex == 0 || cmbMeasureSI2.SelectedIndex == 2)
                {
                    amount = amount / 1000.0;
                }

                StoreItemProduct storeItem = new StoreItemProduct(newStoreItemCode, newStoreItem,cmbStoreItemMeasure.SelectedItem.ToString(), newStoreItemPrice, newStoreItemGroup, false, amount);
                Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab2 Unosenje nove stavke šanka u sistem. Sifra nove stavke šanka :" + newStoreItemCode + ". Naziv nove stavke šanka: " + newStoreItem + ". Grupa stavke šanka je: " + newStoreItemGroup + ". Jedinicna cena je(din) :" + newStoreItemPrice + ". Kolicina stavke šanka je(kg/l) :" + amount);

                string id = "6";//Queries.xml ID

                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;
                query = query + "(" + "'" + storeItem.CodeProduct + "'" + "," + "'" + storeItem.KindOfProduct + "'" + "," + "'" + storeItem.Measure + "'" + "," + "'" + storeItem.Price.ToString() + "'" + "," + "'" + _currStore + "'" + "," + "'" + storeItem.Group + "'" + "," + "'" + storeItem.IsUsedInformation() + "'" + "," + "'" + amount.ToString() + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + "0" + "'" + ");";


                conStore.Open();
                com = new OleDbCommand(query, conStore);
                com.ExecuteNonQuery();
                tfNewStoreItem.Foreground = System.Windows.Media.Brushes.Green;
                tfNewStoreItemCode.Foreground = Brushes.Green;
                tfNewStoreItemGroup.Foreground = Brushes.Green;
                tfNewStoreItemPrice.Foreground = Brushes.Green;
                tfStoreAmount.Foreground = Brushes.Green;
                cmbStoreItemMeasure.Foreground = Brushes.Green;
                StoreItemProducts.Add(storeItem);
                StoreItemCodes.Add(storeItem.CodeProduct);
                StoreItems.Add(storeItem.KindOfProduct);


            
                //add in exist group
                bool exist = false;
                
                for (int j = 1; j < GroupsItemsInStore.Count; j++)
                {
                    if (GroupsItemsInStore.ElementAt(j).Equals(storeItem.Group) == true)
                    {
                        StoreItemsByGroup.ElementAt(j).Add(storeItem);
                        cmbRemoveStoreItem.ItemsSource = StoreItemsByGroup.ElementAt(j);
                        window.storehouse.cmbSItem.ItemsSource = StoreItemsByGroup.ElementAt(j);
                        exist = true;
                    }
                }

                if (exist == false)
                {
                    GroupsItemsInStore.Add(storeItem.Group);
                    cmbRemoveStoreItemGroup.ItemsSource = GroupsItemsInStore;
                    cmbChooseStoreItemGroup.ItemsSource = GroupsItemsInStore;
                    window.storehouse.cmbSGroup.ItemsSource = GroupsItemsInStore;
                    ObservableCollection<StoreItemProduct> spList = new ObservableCollection<StoreItemProduct>();
                    StoreItemsByGroup.Add(spList);
                    StoreItemsByGroup.Last().Add(storeItem);
                  
                }

               // cmbChooseStoreItem2.ItemsSource = StoreItems;
               // cmbRemoveStoreItem.ItemsSource = StoreItems;

                
               
                

                cmbStoreItemCode.ItemsSource = StoreItemCodes;
                cmbStoreItemCode.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);

            }
            finally
            {
                conStore.Close();
            }
        }

        private void btnDeletetfMeasure_Click(object sender, RoutedEventArgs e)
        {
            Logger.writeNode(Constants.INFORMATION, "Tab 2 PodTab2 Brisanje tekstualnih polja za unos nove stavke šanka. Sifra stavke :" + tfNewStoreItemCode.Text + ". Naziv stavke šanka :" + tfNewStoreItem.Text + ". Grupa :" + tfNewStoreItemGroup.Text + "Jedinicna cena je(din) :" + tfNewStoreItemPrice.Text + "Jedinicna kolicina(kg/l) :" + tfStoreAmount.Text);

            tfNewStoreItemCode.Text = String.Empty;
            tfNewStoreItemCode.Foreground = Brushes.Black;
            tfNewStoreItem.Text = String.Empty;
            tfNewStoreItem.Foreground = Brushes.Black;
            tfNewStoreItemGroup.Text = String.Empty;
            tfNewStoreItemGroup.Foreground = Brushes.Black;
            tfNewStoreItemPrice.Text = String.Empty;
            tfNewStoreItemPrice.Foreground = Brushes.Black;
            tfStoreAmount.Text = String.Empty;
            tfStoreAmount.Foreground = Brushes.Black;
            cmbStoreItemMeasure.Foreground = Brushes.Black;
            cmbStoreItemMeasure.SelectedIndex = 0;
        }

        private void cmbStoreItemCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStoreItemCode.SelectedIndex == 0)
            {
                tblJustEnteredProductCodeInformationPart2.Text = Constants.NOTCHOOSEDPRODUCT_STORE;
                tblJustEnteredProductPriceInformationPart2.Text = Constants.NOTCHOOSEDPRODUCT_STORE;
            }
            else 
            {
                if (cmbStoreItemCode.SelectedItem != null)
                {
                    string code = cmbStoreItemCode.SelectedItem.ToString();
                    StoreItemProduct storeProduct;
                    for (int i = 0; i < StoreItemProducts.Count; i++)
                    {
                        if (StoreItemProducts.ElementAt(i).CodeProduct.Equals(code) == true)
                        {
                            storeProduct = StoreItemProducts.ElementAt(i);
                            tblJustEnteredProductCodeInformationPart2.Text = storeProduct.KindOfProduct;
                            tblJustEnteredProductPriceInformationPart2.Text = storeProduct.Price.ToString() + " " + _currStore;
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error Enter Store Tab 2!");
                    Logger.writeNode(Constants.MESSAGEBOX, "Error Enter Store Tab 2!");
                }
            }
        }

        private void removeFromDatabaseStoreItem(StoreItemProduct storeProduct)
        {
            try
            {
                conStore.Open();
                string id = "8";//Queries.xml ID
                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value + "'" + storeProduct.KindOfProduct + "'" + ";";
                com = new OleDbCommand(query, conStore);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
            }
            finally
            {
                conStore.Close();
            }
        }

        private void cmbRemoveStoreItemGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbRemoveStoreItemGroup.SelectedIndex == 0)
            {
                cmbRemoveStoreItem.IsEnabled = false;
                btnRemoveStoreItem.IsEnabled = false;
            }
            else
            {
                string group = String.Empty;
                cmbRemoveStoreItem.IsEnabled = true;
                btnRemoveStoreItem.IsEnabled = true;
                if (cmbRemoveStoreItemGroup.SelectedItem != null)
                {
                    group = cmbRemoveStoreItemGroup.SelectedItem.ToString();
                    Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab2 Part2 Izabrana grupa stavke šanka, koju zelimo ukloniti iz sistema. Naziv izabrane grupa je :" + group);
                }
                else 
                {
                    cmbRemoveStoreItemGroup.SelectedIndex = 0;
                    return;
                }
                for (int j = 1; j < GroupsItemsInStore.Count; j++)
                {
                    if (GroupsItemsInStore.ElementAt(j).Equals(group) == true)
                    {
                        cmbRemoveStoreItem.ItemsSource = StoreItemsByGroup.ElementAt(j);
                        cmbRemoveStoreItem.SelectedIndex = 0;
                    }
                }
            }
        }

        private double checkInStorehouseRealAmount(StoreItemProduct sp) 
        {
            try
            {
            //first find real amount of existing store item
            double realamount = 0.0;
            string id = "18";//Queries.xml ID
            XDocument xdoc2 = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
            XElement Query2 = (from xml2 in xdoc2.Descendants("Query")
                               where xml2.Element("ID").Value == id
                               select xml2).FirstOrDefault();

            string query2 = Query2.Attribute(Constants.TEXT).Value;
            query2 = query2 + "'" + sp.CodeProduct + "'";

            conStore.Open();
            com = new OleDbCommand(query2, conStore);
            dr = com.ExecuteReader();


            while (dr.Read())
            {
                string realAmountWithPoint = dr["RealAmount"].ToString().Replace(',', '.');
                bool isN = Double.TryParse(realAmountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out realamount);
            }

            return realamount;
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                return -1.0;
            }
            finally
            {
                if(conStore != null)
                {
                    conStore.Close();
                }
            }
        }


        private void deleteFromStoreHouse(StoreItemProduct sp)
        {
            try
            {

                string id = "19";//Queries.xml ID

                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;
                query = query + "'" + sp.CodeProduct + "'" + ";";

                conStore.Open();
                com = new OleDbCommand(query, conStore);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);

            }
            finally
            {
                if (conStore != null)
                {
                    conStore.Close();
                }
            }
        }

        private void btnRemoveStoreItem_Click(object sender, RoutedEventArgs e)
        {

            MainWindow window = (MainWindow)MainWindow.GetWindow(this);


            string groupName = cmbRemoveStoreItemGroup.SelectedItem.ToString();
            string storeItemName = cmbRemoveStoreItem.SelectedItem.ToString();
            string storeItemCode = String.Empty;
            StoreItemProduct sp = new StoreItemProduct();
            cmbStoreItemCode.SelectedIndex = 0;

            for (int i = 0; i < StoreItemProducts.Count; i++ )
            {
                if (StoreItemProducts.ElementAt(i).KindOfProduct.Equals(storeItemName) == true)
                {
                    sp = StoreItemProducts.ElementAt(i);
                    if (sp.CodeProduct.Equals(_lastEnteredCode)) { _lastEnteredCode = StoreItemProducts.ElementAt(i - 1).CodeProduct; }
                    for (int j = 0; j < window.ProductsWholeInformation.Count; j++)
                    {
                        for (int k = 0; k < window.ProductsWholeInformation.ElementAt(j).StoreItemProducts.Count; k++)
                        {
                            if (window.ProductsWholeInformation.ElementAt(j).StoreItemProducts.ElementAt(k).CodeProduct.Equals(sp.CodeProduct) == true)
                            {
                                MessageBox.Show("Za proizvod " + window.ProductsWholeInformation.ElementAt(j).KindOfProduct + " morate ukloniti stavku šanka " + sp.KindOfProduct + "!");
                                Logger.writeNode(Constants.MESSAGEBOX, "Za proizvod " + window.ProductsWholeInformation.ElementAt(j).KindOfProduct + " morate ukloniti stavku šanka " + sp.KindOfProduct + "!");
                                for (int counter = 0; counter < cmbChooseEarlierProduct.Items.Count; counter++)
                                {
                                    if (cmbChooseEarlierProduct.Items[counter].ToString().Equals(window.ProductsWholeInformation.ElementAt(j).KindOfProduct))
                                    {
                                        cmbChooseEarlierProduct.SelectedIndex = counter;
                                        break;
                                    }
                                }

                                return;
                            }
                        }
                    }
                    // check for real amount in storehouse
                    double realAmount;
                    realAmount = checkInStorehouseRealAmount(sp);
                    System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Uklanjanjem izabrane stavke iz sistema uklanjate i " + realAmount.ToString() + " u (kg/l) iste stavke iz magacina. Da li to zaista želite ?", "STAVKA SE NALAZI U MAGACINU", System.Windows.Forms.MessageBoxButtons.YesNo);
                    Logger.writeNode(Constants.MESSAGEBOX, "Uklanjanjem izabrane stavke iz sistema uklanjate i " + realAmount.ToString() + " u (kg/l) iste stavke iz šanka. Da li to zaista želite ?");
                    if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        //delete store item from storehouse
                        deleteFromStoreHouse(sp);
                        // refresh StorehouseItems collection
                        window.storehouse.StorehouseItems = window.storehouse.getStateOfStorehouse();
                        window.storehouse.cvStorehouseItems = CollectionViewSource.GetDefaultView(window.storehouse.StorehouseItems);
                        if (window.storehouse.cvStorehouseItems != null)
                        {
                            window.storehouse.dgridStateOfStorehouse.ItemsSource = window.storehouse.cvStorehouseItems;
                        }
                    }
                    else if (dialogResult == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }


                    removeFromDatabaseStoreItem(sp);
                    storeItemCode = StoreItemProducts.ElementAt(i).CodeProduct;
                    Logger.writeNode(Constants.INFORMATION, " Tab2 PodTab2 Uklanjanje magacinske stavke iz sistema. Sifra stavke :" + StoreItemProducts.ElementAt(i).CodeProduct + ". Naziv stavke :" + StoreItemProducts.ElementAt(i).KindOfProduct + ".Grupa stavke :" + StoreItemProducts.ElementAt(i).Group + ".Jedinicna cena(din) :" + StoreItemProducts.ElementAt(i).Price + ". Jedinicna kolicina :" + StoreItemProducts.ElementAt(i).Amount);
                    StoreItemProducts.RemoveAt(i);
                    break;
                }
            }

           
            for (int i = 0; i < StoreItems.Count; i++)
            {
                if (StoreItems.ElementAt(i).Equals(storeItemName))
                {
             
                    StoreItems.RemoveAt(i);
                   // cmbRemoveStoreItem.ItemsSource = StoreItems;
                   // cmbRemoveStoreItem.SelectedIndex = 0;

                   // window.storehouse.cmbSItem.ItemsSource = StoreItems;
                   // window.storehouse.cmbSItem.SelectedIndex = 0;
                    
                    break;
                }
            }

            //remove from two and more elements list
            int groupNum = GroupsItemsInStore.IndexOf(groupName);
            int iteminGroupNum = StoreItemsByGroup.ElementAt(groupNum).IndexOf(sp);
            StoreItemsByGroup.ElementAt(groupNum).RemoveAt(iteminGroupNum);
            cmbRemoveStoreItem.ItemsSource = StoreItemsByGroup.ElementAt(groupNum);
            cmbRemoveStoreItem.SelectedIndex = 0;
            cmbChooseStoreItem2.SelectedIndex = 0;
            cmbChooseStoreItem2.IsEnabled = true;

            //remove item and group
            if (StoreItemsByGroup.ElementAt(groupNum).Count == 0) 
            {
                GroupsItemsInStore.RemoveAt(groupNum);
                //important
                StoreItemsByGroup.RemoveAt(groupNum);
                cmbRemoveStoreItemGroup.ItemsSource = GroupsItemsInStore;
                cmbChooseStoreItemGroup.ItemsSource = GroupsItemsInStore;

                
              
            }

             for (int i = 0; i < StoreItemCodes.Count; i++ )
            {
                if (StoreItemCodes.ElementAt(i).Equals(storeItemCode))
                {
                    StoreItemCodes.RemoveAt(i);
                    cmbStoreItemCode.ItemsSource = StoreItemCodes;
                    cmbStoreItemCode.SelectedIndex = 0;
                    break;
                }
            }
        }

        #endregion

       

        #region Part1

        private void cmbChooseEarlierProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbChooseEarlierProduct.SelectedIndex == 0)
            {
                tblJustEnteredProductCodeInformationDown.Text = Constants.NOTCHOOSEDPRODUCT_DOWN;
                tblJustEnteredKindOfProductInformationDown.Text = Constants.NOTCHOOSEDPRODUCT_DOWN;
                tblJustEnteredProductPriceInformationDown.Text = Constants.NOTCHOOSEDPRODUCT_DOWN;
                cmbChooseStoreItemGroup.IsEnabled = false;
                cmbChooseStoreItemGroup.SelectedIndex = 0;
                cmbChooseStoreItem2.IsEnabled = false;
                cmbChooseStoreItem2.SelectedIndex = 0;
                btnAddStoreItemDown.IsEnabled = false;
               
                //_currStoreItemProducts.Clear();
                //dgridCurrProductStoreItemConn.ItemsSource = _currStoreItemProducts;
                dgridCurrProductStoreItemConn.Visibility = Visibility.Hidden;
                return;
            }
            else
            {
                cmbChooseStoreItemGroup.SelectedIndex = 0;

                dgridCurrProductStoreItemConn.Visibility = Visibility.Visible;

                string kindfOfProduct = String.Empty;
                if (cmbChooseEarlierProduct.SelectedItem != null)
                {
                    kindfOfProduct = cmbChooseEarlierProduct.SelectedItem.ToString();
                }
              
              
              
                MainWindow window = (MainWindow)MainWindow.GetWindow(this);
                Product p;
                for (int i = 0; i < window.ProductsWholeInformation.Count; i++)
                {
                    if (window.ProductsWholeInformation.ElementAt(i).KindOfProduct.Equals(kindfOfProduct))
                    {
                        p = window.ProductsWholeInformation.ElementAt(i);
                        Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab2 Izabran je proizvod kafica. Sifra proizvoda :" + p.CodeProduct + ". Vrsta proizvoda :" + p.KindOfProduct + ". Jedinicna cena(din) :" + p.Price.ToString());
                        tblJustEnteredProductCodeInformationDown.Text = p.CodeProduct;
                        tblJustEnteredKindOfProductInformationDown.Text = p.KindOfProduct;
                        tblJustEnteredProductPriceInformationDown.Text = p.Price.ToString();
                        _currStoreItemProducts = p.StoreItemProducts;
                        break;
                    }
                }

                dgridCurrProductStoreItemConn.ItemsSource = _currStoreItemProducts;

                    if (cmbChooseEarlierProduct.SelectedIndex != 0)
                    {
                        cmbChooseStoreItemGroup.IsEnabled = true;
                        cmbChooseStoreItem2.IsEnabled = false;
                        btnAddStoreItemDown.IsEnabled = true;


                    }
                
            }
           

        }


        private void cmbChooseStoreItemGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbChooseStoreItemGroup.SelectedIndex == 0)
            {
                cmbChooseStoreItem2.IsEnabled = false;
                btnAddStoreItemDown.IsEnabled = false;
                tfProductAmount.IsReadOnly = true;
            }
            else
            {
                tfProductAmount.IsReadOnly = false;
                btnAddStoreItemDown.IsEnabled = true;
                tblRemarkAddStoreItem2.Text = String.Empty;
                string group;
                cmbChooseStoreItem2.IsEnabled = true;


                if (cmbChooseStoreItemGroup.SelectedItem != null)
                {
                    group = cmbChooseStoreItemGroup.SelectedItem.ToString();
                    Logger.writeNode(Constants.INFORMATION,"Tab2 PodTab2 Part1 Izabrana grupa stavke šanka, koju zelimo vezati za izabrani proizvod kafica. Naziv izabrane grupa je :" + group);
                }
                else
                {
                    cmbChooseStoreItemGroup.SelectedIndex = 0;
                    return;
                }

                for (int j = 1; j < GroupsItemsInStore.Count; j++)
                {
                    if (GroupsItemsInStore.ElementAt(j).Equals(group) == true)
                    {
                        cmbChooseStoreItem2.ItemsSource = StoreItemsByGroup.ElementAt(j);
                        cmbChooseStoreItem2.SelectedIndex = 0;
                    }
                }
            }

        }

      

        private void tfProductAmount_MouseEnter(object sender, MouseEventArgs e)
        {
            if (cmbChooseStoreItemGroup.SelectedIndex == 0)
            {
                tfProductAmount.IsReadOnly = true;
            }
            else 
            {
                tfProductAmount.IsReadOnly = false;
            }
        }

        private void tfProductAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfProductAmount.Text.Equals(String.Empty) == false)
            {
                double d;
                double amount;
                string tfProductAmountWithPoint = tfProductAmount.Text.Replace(',', '.');
                bool isNumeric = Double.TryParse(tfProductAmountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out amount);
                if (isNumeric == false)
                {
                    MessageBox.Show("Količina nije uneta kao broj!!!");
                    Logger.writeNode(Constants.MESSAGEBOX, "Količina nije uneta kao broj!!!");
                    return;
                }
            }
        }

       

      private void btnAddStoreItemDown_Click(object sender, RoutedEventArgs e)
        {
            string storeItemName = cmbChooseStoreItem2.SelectedItem.ToString();
            if (cmbChooseStoreItemGroup.SelectedIndex == 0)
            {
                tblRemarkAddStoreItem2.Text = Constants.MUSTCHOOSESTOREGROUP;
                return;
            }
            else
            {
                tblRemarkAddStoreItem2.Text = String.Empty;
                cmbChooseStoreItem2.SelectedIndex = 0;
            }

          
           

            // here begin serious work because everything is OK
            MainWindow window = (MainWindow)MainWindow.GetWindow(this);
            ObservableCollection<Product> arrayProduct = window.ProductsWholeInformation;
            string thatKindOfProduct = cmbChooseEarlierProduct.SelectedItem.ToString();
            int ind = -1;

            for (int i = 0; i < arrayProduct.Count; i++)
            {
                if (arrayProduct.ElementAt(i).KindOfProduct.Equals(thatKindOfProduct))
                {
                    p = arrayProduct.ElementAt(i);
                    ind = i;
                    break;
                }
            }

            
 
            for (int i = 0; i < StoreItemProducts.Count; i++)
            {
                if (StoreItemProducts.ElementAt(i).KindOfProduct.Equals(storeItemName))
                {
                    sitem = StoreItemProducts.ElementAt(i);
                    _currStoreItemProducts.Add(sitem);
                    dgridCurrProductStoreItemConn.ItemsSource = _currStoreItemProducts;
                    //p.StoreItemProducts.Add(sitem); povezao si selektovanje proizvvoda p.StoreItemsProduct sa _currStoreItemProducts
                    arrayProduct[ind] = p;
                    break;
                }
            }

          //get amount of product and storeItem for database

            double d;
            double amountP;
            string tfProductAmountWithPoint =  tfProductAmount.Text.Replace(',', '.');
            bool isNumeric = Double.TryParse(tfProductAmountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out amountP);
            if (isNumeric == false)
            {
                MessageBox.Show("Količina merne jedinice nije uneta kao broj!!!");
                Logger.writeNode(Constants.MESSAGEBOX, "Količina merne jedinice nije uneta kao broj!!!");
                return;
            }
            if (cmbMeasureSI.SelectedIndex == 0 || cmbMeasureSI.SelectedIndex == 2)
            {
                amountP = amountP / 1000;
            }
            if (p.StoreItemProducts.Count == 1)
            {
                p.Amount = amountP;
                //update in database table Products
                try
                {
                    conStore.Open();
                    string query = "INSERT INTO productsAmounts(PrCode,PrName,PrAmount) VALUES (@PrCode,@PrName,@PrAmount);";
                    com = new OleDbCommand(query, conStore);
                    com.Parameters.Add("@PrCode",p.CodeProduct);
                    com.Parameters.Add("@PrName", p.KindOfProduct);
                    com.Parameters.Add("@PrAmount", p.Amount.ToString());
                    com.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    Logger.writeNode(Constants.EXCEPTION, ex.Message);
                    MainWindow window2 = (MainWindow)MainWindow.GetWindow(this);
                    window.savenumofitemsEVERCreated();
                }
                finally
                {
                    if (conStore != null)
                    {
                        conStore.Close();
                    }
                  
                }

            }

                       
            writeRecordinConnectionTable(p,sitem);
            Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab2 Vezivanje stavke šanka za odgovarajuci proizvod. Vrsta proizvoda :" + p.KindOfProduct + ". Kolicinski udeo proizvoda(kg/l) :" + amountP);
            Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab2 Vezivanje stavke šanka za odgovarajuci proizvod. Sifra magacinske stavke :" + sitem.CodeProduct + ". Naziv stavke šanka :" + sitem.KindOfProduct + ". Grupa stavke šanka :" + sitem.Group + ". Jedinicna cena(din) :" + sitem.Price + ". Kolicina stavke šanka :" + sitem.Amount);
            window.ProductsWholeInformation = arrayProduct;
            tfProductAmount.Text = String.Empty;
            tfStoreAmount.Text = String.Empty;

            window.selectUpdateConnProdStore.updateConnection();


           
         
        }


        private void writeRecordinConnectionTable(Product p, StoreItemProduct sitem) 
        {
            try
            {
                string id = "9";//Queries.xml ID

                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;
                query = query + "(" + "'" + p.CodeProduct + "'" + "," + "'" + sitem.CodeProduct + "'" + "," + "'" + p.KindOfProduct + "'" + "," + "'" + sitem.KindOfProduct + "'" + "," + "'" + sitem.Ratio.ToString() + "'" + "," + "'" + sitem.Group + "'" + "," + "'" + sitem.IsUsedInformation() + "'" + "," + "'" + sitem.Price.ToString() + "'" + "," + "'" + p.Amount.ToString() + "'" + "," + "'" + sitem.Amount.ToString() + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + "0" + "'" + ");";

                conStore.Open();
                com = new OleDbCommand(query, conStore);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);

            }
            finally
            {
                conStore.Close();
            }
        }

        private void btnRemoveStoreItemForCurrProduct_Click(object sender, RoutedEventArgs e)
        {
            int index = dgridCurrProductStoreItemConn.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Nijedna stavka nije selektovana. Morate selektovati barem jednu stavku da biste je uklonili!", "NEMOGUĆNOST UKLANJANJA STAVKE");
                Logger.writeNode(Constants.MESSAGEBOX, "Nijedna stavka nije selektovana. Morate selektovati barem jednu stavku da biste je uklonili!");
            }
            else 
            {
                try
                {

                    // here begin serious work because everything is OK
                    MainWindow window = (MainWindow)MainWindow.GetWindow(this);
                    ObservableCollection<Product> arrayProduct = window.ProductsWholeInformation;
                    string thatKindOfProduct = cmbChooseEarlierProduct.SelectedItem.ToString();


                    for (int i = 0; i < arrayProduct.Count; i++)
                    {
                        if (arrayProduct.ElementAt(i).KindOfProduct.Equals(thatKindOfProduct))
                        {
                            p = arrayProduct.ElementAt(i);
                            Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab2 Uklanjanje veze izabrane stavke šanka i proizvoda kafica. Sifra proizvoda kafica :" + p.CodeProduct + ". Vrsta proizvoda kafica :" + p.KindOfProduct + ". Jedinicna cena(din) :" + p.Price);
                            break;
                        }
                    }

                    StoreItemProduct sitemDel = p.StoreItemProducts.ElementAt(index);
                    deleteRecordinConnectionTable(p, sitemDel);
                    Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab2 Uklanjanje veze izabrane stavke šanka i proizvoda kafica. Sifra magacinske stavke :" + sitemDel.CodeProduct + ". Naziv magacinske stavke :" + sitemDel.KindOfProduct + ". Grupa stavke: " + sitemDel.Group + ". Jedinicna cena(din) :" + sitemDel.Price + ". Jedinicna kolicina(kd/l) :" + sitemDel.Amount);
                    p.StoreItemProducts.RemoveAt(index);
                    // _currStoreItemProducts = p.StoreItemProducts; duplo brisanje jel ove dve reference pokazuju na isti objekat

                    dgridCurrProductStoreItemConn.ItemsSource = _currStoreItemProducts;
                    window.ProductsWholeInformation = arrayProduct;
                    window.selectUpdateConnProdStore.updateConnection();


                    // inform storehouse if item in use
                    for (int i = 0; i < window.storehouse.UsedRecords.Count; i++)
                    {
                        if (window.storehouse.UsedRecords.ElementAt(i).ConnStoreItemCode.Equals(sitemDel.CodeProduct))
                        {
                            window.storehouse.UsedRecords.RemoveAt(i);
                            window.storehouse.cvUsedRecords = CollectionViewSource.GetDefaultView(window.storehouse.UsedRecords);
                            if (window.storehouse.cvUsedRecords != null)
                            {
                                window.storehouse.dgridUsed.ItemsSource = window.storehouse.cvUsedRecords;
                            }
                        }
                    }

                    conStore.Open();
                    string query = "DELETE FROM productsAmounts WHERE PrCode = @PrCode";
                    com = new OleDbCommand(query, conStore);
                    com.Parameters.Add("@PrCode", p.CodeProduct);
                    
                    com.ExecuteNonQuery();



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Logger.writeNode(Constants.EXCEPTION, ex.Message);
                }
                finally 
                {
                    if (conStore != null)
                    {
                        conStore.Close();
                    }
                }
            }
        }

        private void deleteRecordinConnectionTable(Product p, StoreItemProduct sitem)
        {
            try
            {
                string id = "10";//Queries.xml ID

                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;
                query = query + "ConnCodeProduct=" + "'" + p.CodeProduct + "'" + " AND " + "ConnStoreItemCode=" + "'" + sitem.CodeProduct + "'" + ";";

                conStore.Open();
                com = new OleDbCommand(query, conStore);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);

            }
            finally
            {
                if (conStore != null)
                {
                    conStore.Close();
                }
            }
        }

       

    

        #endregion


        #region Part3

        private void btnEnterNewStoreItemMeasure_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    string newMeasure = String.Empty;
                    newMeasure = tfNewStoreItemMeasure.Text.Replace(" ", String.Empty);
                    newMeasure = newMeasure.Trim();

                    string id = "51";//Queries.xml ID

                    XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement Query = (from xml2 in xdoc.Descendants("Query")
                                      where xml2.Element("ID").Value == id
                                      select xml2).FirstOrDefault();
                    Console.WriteLine(Query.ToString());
                    string query = Query.Attribute(Constants.TEXT).Value;
                    string MeasureName = tfNewStoreItemMeasure.Text;

                    conStore.Open();
                    com = new OleDbCommand(query, conStore);
                    com.Parameters.Add("@MeasureName", MeasureName);
                    com.ExecuteNonQuery();
                    tfNewStoreItemMeasure.Foreground = System.Windows.Media.Brushes.Green;

                    StoreItemsMeasures.Add(MeasureName);
                    cmbStoreItemMeasureRemove.ItemsSource = StoreItemsMeasures;
                    cmbStoreItemMeasure.ItemsSource = StoreItemsMeasures;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                MainWindow win = (MainWindow)Window.GetWindow(this);
                win.savenumofitemsEVERCreated();

            }
            finally
            {
                if (conStore != null)
                {
                    conStore.Close();
                }
            }
        }



        private void tfNewStoreItemMeasure_TextChanged(object sender, TextChangedEventArgs e)
        {
            tfNewStoreItemMeasure.Foreground = Brushes.Black;
        }


        private void btnRemoveNewStoreItemMeasure_Click(object sender, RoutedEventArgs e)
        {
            if (cmbStoreItemMeasureRemove.SelectedItem != null)
            {
                try
                {


                    conStore.Open();
                    string id = "53";//Queries.xml ID
                    XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement Query = (from xml2 in xdoc.Descendants("Query")
                                      where xml2.Element("ID").Value == id
                                      select xml2).FirstOrDefault();
                    Console.WriteLine(Query.ToString());
                    string query = Query.Attribute(Constants.TEXT).Value;
                    com = new OleDbCommand(query, conStore);
                    com.Parameters.Add("@MeasureName", cmbStoreItemMeasureRemove.SelectedItem.ToString());
                    com.ExecuteNonQuery();


                    StoreItemsMeasures.RemoveAt(cmbStoreItemMeasureRemove.SelectedIndex);
                    cmbStoreItemMeasureRemove.ItemsSource = StoreItemsMeasures;
                    cmbStoreItemMeasureRemove.SelectedIndex = 0;
                    cmbStoreItemMeasure.ItemsSource = StoreItemsMeasures;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Logger.writeNode(Constants.EXCEPTION, ex.Message);
                    MainWindow win = (MainWindow)Window.GetWindow(this);
                    win.savenumofitemsEVERCreated();
                }
                finally
                {
                    if (conStore != null)
                    {
                        conStore.Close();
                    }
                }
            }

        }


        private void cmbStoreItemMeasureRemove_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStoreItemMeasureRemove.SelectedIndex == 0)
            {
                btnRemoveNewStoreItemMeasure.IsEnabled = false;
            }
            else 
            {
                btnRemoveNewStoreItemMeasure.IsEnabled = true;
            }
        }

        #endregion

    

      


      




































    }
}
